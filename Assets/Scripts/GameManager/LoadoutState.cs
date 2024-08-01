using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

/// <summary>
/// State pushed on the GameManager during the Loadout, when player select player, theme and accessories
/// Take care of init the UI, load all the data used for it etc.
/// </summary>
public class LoadoutState : AState
{
    public static LoadoutState instance;
    /// <summary>
    /// // Create an empty gameobjet and assign to m_EndPoint in  inspector,
    /// m_camera tranlate to empty object position
    /// </summary>
    [Header("CameraSetup")]
    public GameObject m_camera;
    public Animator m_camera_animator;
    private Vector3 m_camCurrentPos;
    private Vector3 CameraPositionInGame = new Vector3(0, 4, -2);
    //private Vector3 CameraPositionInSplash = new Vector3(-3, 4, 2.7f);
    private Vector3 CameraPositionInSplash = new Vector3(-3, 3.5f, 2);
    private Vector3 CameraAngleInGame = new Vector3(19, 0, 0);

    public Transform Target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    
    public Transform m_EndPoint;  
    public float m_Speed = 1.0f;
    private bool IsCameraMoved = false;
    private bool IsFollowCamera = false;
    

    [Header("Canvas")]
    //.......................................................................
    public Canvas inventoryCanvas;
    public Text CoinTextHighScore_loadout;
    [Header("MainCh")]
    //public Animator splashAnimator;
    private Vector3 m_mainCurrentPos;
    public GameObject m_mainSplashChar;
    public Animator m_msAnimator;  // main splash animator > ms.
    public Transform m_msCharEndPoint;
    public float m_msSpeed = 1.0f;
    private bool IsmainCharMoved = false;
    [Header("Enemy")]
    private Vector3 m_enemyCurrentPos;
    public GameObject m_enemySplashChar;
    public Animator m_esAnimator;  // enemy splash animatior > es.
    public Transform m_esCharEndPoint;
    public float m_esSpeed = 1.0f;
    private bool IsEnemyCharMoved = false;
    //............UI..................
    public GameObject splashUI;
    public GameObject splashBlur;

    [Header("Char UI")]
   
	public Transform charPosition;
    public GameObject m_enemyCharacter;
	

	[Header("PowerUp UI")]	
    public Sprite noItemIcon;

	[Header("Other Data")]	
	public Button runButton;
    public GameObject Exit_panel;
    public GameObject tutorialBlocker;
    public GameObject tutorialPrompt;

	public MeshFilter skyMeshFilter;
    public MeshFilter UIGroundFilter;

	public AudioClip menuTheme;
    public AudioSource _evilLaugh;


    [Header("Prefabs")]
    public ConsumableIcon consumableIcon;

    Consumable.ConsumableType m_PowerupToUse = Consumable.ConsumableType.NONE;

    protected GameObject m_Character;
    protected List<int> m_OwnedAccesories = new List<int>();
    protected int m_UsedAccessory = -1;
	protected int m_UsedPowerupIndex;
    protected bool m_IsLoadingCharacter;

	protected Modifier m_CurrentModifier = new Modifier();

    protected const float k_CharacterRotationSpeed =0f;
    protected const string k_ShopSceneName = "shop";
    protected const float k_OwnedAccessoriesCharacterOffset = -0.1f;
    protected int k_UILayer;
    protected readonly Quaternion k_FlippedYAxisRotation = Quaternion.Euler (0f, 180f, 0f);

    public override void Enter(AState from)
    {
        Debug.Log("Enter in Lodout State");
        m_camera.transform.position = CameraPositionInSplash;  // Camera Position in Splash
        

        inventoryCanvas.gameObject.SetActive(true);   
        k_UILayer = LayerMask.NameToLayer("UI");
        skyMeshFilter.gameObject.SetActive(true);
        UIGroundFilter.gameObject.SetActive(true);
        // Reseting the global blinking value. Can happen if the game unexpectedly exited while still blinking
        Shader.SetGlobalFloat("_BlinkingValue", 0.0f);
        if (MusicPlayer.instance.GetStem(0) != menuTheme)
		{
            MusicPlayer.instance.SetStem(0, menuTheme);
            StartCoroutine(MusicPlayer.instance.RestartAllStems());
        }
        runButton.interactable = false;
       // runButton.GetComponentInChildren<Text>().text = "Loading...";
        if(m_PowerupToUse != Consumable.ConsumableType.NONE)
        {
            //if we come back from a run and we don't have any more of the powerup we wanted to use, we reset the powerup to use to NONE
            if (!PlayerData.instance.consumables.ContainsKey(m_PowerupToUse) || PlayerData.instance.consumables[m_PowerupToUse] == 0)
                m_PowerupToUse = Consumable.ConsumableType.NONE;
        }

        Refresh();
    }

    private void Awake()
    {
        instance = this;
    }

    // first time call method Set postioion of splash character
    private void Start()
    {
        m_camCurrentPos = m_camera.transform.position;
        m_mainCurrentPos = m_mainSplashChar.transform.position;
        m_enemyCurrentPos = m_enemySplashChar.transform.position;     
        CoinTextHighScore_loadout.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update method call with frame
    private void Update()
    {
        if (IsCameraMoved)
        {
            // Camera move at start button presse and enmy appeard
                //m_camera.transform.position = Vector3.MoveTowards(m_camera.transform.position, m_EndPoint.position, m_Speed * Time.deltaTime);      
            if (m_camera.transform.position == m_EndPoint.position)
            {
                IsCameraMoved = false;
            }
        }

        if(IsFollowCamera)
        {
            Vector3 targetPosition = Target.position + Offset;          
            m_camera.transform.position = Vector3.SmoothDamp(m_camera.transform.position, targetPosition, ref velocity, SmoothTime);
        }


        //_________________EndForCamera___________________________________
        //____________________________________________________________________________________________________

        // For CHARACTER movement in splsh screen..........................
        //___________________SplashForMainCharacter____________________________

        if (IsmainCharMoved)
        {
            m_mainSplashChar.transform.position = Vector3.MoveTowards(m_mainSplashChar.transform.position, m_msCharEndPoint.position, m_msSpeed * Time.deltaTime);
            if (m_mainSplashChar.transform.position == m_msCharEndPoint.position)
            {
                IsmainCharMoved = false;
            }
        }

       if(IsEnemyCharMoved)
        {
            m_enemySplashChar.transform.position = Vector3.MoveTowards(m_enemySplashChar.transform.position, m_esCharEndPoint.position, m_esSpeed * Time.deltaTime);
            if (m_enemySplashChar.transform.position == m_esCharEndPoint.position)
            {
                IsEnemyCharMoved = false;
            }
        }


		if (Input.GetKey(KeyCode.Escape))
		{
            if (!LoadoutState.instance.splashBlur.activeSelf)
            {
                // Quit Popup enable....
                Exit_panel.SetActive(true);
                Time.timeScale = 0;
            }
		}

	}

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void NotQuitApplication()
    {
        Exit_panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetCamCurrentPos()
    {        
       
        m_mainSplashChar.transform.position = m_mainCurrentPos;
        m_enemySplashChar.transform.position = m_enemyCurrentPos;
    }

    public override void Exit(AState to)
    {
        Debug.Log("Exit to Lodout");
        inventoryCanvas.gameObject.SetActive(false);

        if (m_Character != null) Addressables.ReleaseInstance(m_Character);

        GameState gs = to as GameState;

        skyMeshFilter.gameObject.SetActive(false);
        UIGroundFilter.gameObject.SetActive(false);

        if (gs != null)
        {
            gs.currentModifier = m_CurrentModifier;

            // We reset the modifier to a default one, for next run (if a new modifier is applied, it will replace this default one before the run starts)
            m_CurrentModifier = new Modifier();

            if (m_PowerupToUse != Consumable.ConsumableType.NONE)
            {
                PlayerData.instance.Consume(m_PowerupToUse);
                Consumable inv = Instantiate(ConsumableDatabase.GetConsumbale(m_PowerupToUse));
                inv.gameObject.SetActive(false);
                gs.trackManager.characterController.inventory = inv;
            }
        }
    }

    public void Refresh()
    {
		PopulatePowerup();

        StartCoroutine(PopulateCharacters());
        StartCoroutine(PopulateTheme());
    }

    public override string GetName()
    {
        return "Loadout";
    }

    public override void Tick()
    {
        if (!runButton.interactable)
        {
            bool interactable = ThemeDatabase.loaded && CharacterDatabase.loaded;
            if(interactable)
            {
                runButton.interactable = true;
                //runButton.GetComponentInChildren<Text>().text = "START!";

                //we can always enabled, as the parent will be disabled if tutorial is already done
                tutorialPrompt.SetActive(true);
            }
        }

        if(m_Character != null)
        {
            m_Character.transform.Rotate(0, k_CharacterRotationSpeed * Time.deltaTime, 0, Space.Self);
        }

		//charSelect.gameObject.SetActive(PlayerData.instance.characters.Count > 1);
		//themeSelect.gameObject.SetActive(PlayerData.instance.themes.Count > 1);
    }

	public void GoToStore()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(k_ShopSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}

    public void ChangeCharacter(int dir)
    {
        PlayerData.instance.usedCharacter += dir;
        if (PlayerData.instance.usedCharacter >= PlayerData.instance.characters.Count)
            PlayerData.instance.usedCharacter = 0;
        else if(PlayerData.instance.usedCharacter < 0)
            PlayerData.instance.usedCharacter = PlayerData.instance.characters.Count-1;

        StartCoroutine(PopulateCharacters());
    }

    public void ChangeAccessory(int dir)
    {
        m_UsedAccessory += dir;
        if (m_UsedAccessory >= m_OwnedAccesories.Count)
            m_UsedAccessory = -1;
        else if (m_UsedAccessory < -1)
            m_UsedAccessory = m_OwnedAccesories.Count-1;

        if (m_UsedAccessory != -1)
            PlayerData.instance.usedAccessory = m_OwnedAccesories[m_UsedAccessory];
        else
            PlayerData.instance.usedAccessory = -1;

        SetupAccessory();
    }

    public void ChangeTheme(int dir)
    {
        PlayerData.instance.usedTheme += dir;
        if (PlayerData.instance.usedTheme >= PlayerData.instance.themes.Count)
            PlayerData.instance.usedTheme = 0;
        else if (PlayerData.instance.usedTheme < 0)
            PlayerData.instance.usedTheme = PlayerData.instance.themes.Count - 1;

        StartCoroutine(PopulateTheme());
    }

    public IEnumerator PopulateTheme()
    {
        ThemeData t = null;

        while (t == null)
        {
            t = ThemeDatabase.GetThemeData(PlayerData.instance.themes[PlayerData.instance.usedTheme]);
            yield return null;
        }

       // themeNameDisplay.text = t.themeName;
		//themeIcon.sprite = t.themeIcon;

		skyMeshFilter.sharedMesh = t.skyMesh;
        UIGroundFilter.sharedMesh = t.UIGroundMesh;
	}

    public IEnumerator PopulateCharacters()
    {
		//accessoriesSelector.gameObject.SetActive(false);
        PlayerData.instance.usedAccessory = -1;
        m_UsedAccessory = -1;

        if (m_IsLoadingCharacter)  // P> ToDo
        {
            m_IsLoadingCharacter = true;
            GameObject newChar = null;
            while (newChar == null)
            {
                Character c = CharacterDatabase.GetCharacter(PlayerData.instance.characters[PlayerData.instance.usedCharacter]);

                if (c != null)
                {
                    m_OwnedAccesories.Clear();
                    for (int i = 0; i < c.accessories.Length; ++i)
                    {
						// Check which accessories we own.
                        string compoundName = c.characterName + ":" + c.accessories[i].accessoryName;
                        if (PlayerData.instance.characterAccessories.Contains(compoundName))
                        {
                            m_OwnedAccesories.Add(i);
                        }
                    }

                    Vector3 pos = charPosition.transform.position;
                    if (m_OwnedAccesories.Count > 0)
                    {
                        pos.x = k_OwnedAccessoriesCharacterOffset;
                    }
                    else
                    {
                        pos.x = 1.98f;
                        pos.z = 4.1f;
                    }
                    charPosition.transform.position = pos;

                   // accessoriesSelector.gameObject.SetActive(m_OwnedAccesories.Count > 0);

                    AsyncOperationHandle op = Addressables.InstantiateAsync(c.characterName);
                    yield return op;
                    if (op.Result == null || !(op.Result is GameObject))
                    {
                        Debug.LogWarning(string.Format("Unable to load character {0}.", c.characterName));
                        yield break;
                    }
                    newChar = op.Result as GameObject;
                    Helpers.SetRendererLayerRecursive(newChar, k_UILayer);
					newChar.transform.SetParent(charPosition, false);
                    newChar.transform.rotation = k_FlippedYAxisRotation;

                    if (m_Character != null)
                        Addressables.ReleaseInstance(m_Character);

                    m_Character = newChar;
                   // charNameDisplay.text = c.characterName;

                    m_Character.transform.localPosition = Vector3.right * 1000;
                    //animator will take a frame to initialize, during which the character will be in a T-pose.
                    //So we move the character off screen, wait that initialised frame, then move the character back in place.
                    //That avoid an ugly "T-pose" flash time
                    yield return new WaitForEndOfFrame();
                    m_Character.transform.localPosition = Vector3.zero;

                    SetupAccessory();
                }
                else
                    yield return new WaitForSeconds(1.0f);
            }
            m_IsLoadingCharacter = false;
        }
	}

    void SetupAccessory()
    {
        Character c = m_Character.GetComponent<Character>();
        c.SetupAccesory(PlayerData.instance.usedAccessory);

        if (PlayerData.instance.usedAccessory == -1)
        {
           // accesoryNameDisplay.text = "None";
			//accessoryIconDisplay.enabled = false;
		}
        else
        {
			//accessoryIconDisplay.enabled = true;
			//accesoryNameDisplay.text = c.accessories[PlayerData.instance.usedAccessory].accessoryName;
			//accessoryIconDisplay.sprite = c.accessories[PlayerData.instance.usedAccessory].accessoryIcon;
        }
    }

	void PopulatePowerup()
	{
		//powerupIcon.gameObject.SetActive(true);

        if (PlayerData.instance.consumables.Count > 0)
        {
            Consumable c = ConsumableDatabase.GetConsumbale(m_PowerupToUse);

           // powerupSelect.gameObject.SetActive(true);
            if (c != null)
            {
                //powerupIcon.sprite = c.icon;
                //powerupCount.text = PlayerData.instance.consumables[m_PowerupToUse].ToString();
            }
            else
            {
              //  powerupIcon.sprite = noItemIcon;
               // powerupCount.text = "";
            }
        }
        else
        {
           // powerupSelect.gameObject.SetActive(false);
        }
	}

	public void ChangeConsumable(int dir)
	{
		bool found = false;
		do
		{
			m_UsedPowerupIndex += dir;
			if(m_UsedPowerupIndex >= (int)Consumable.ConsumableType.MAX_COUNT)
			{
				m_UsedPowerupIndex = 0; 
			}
			else if(m_UsedPowerupIndex < 0)
			{
				m_UsedPowerupIndex = (int)Consumable.ConsumableType.MAX_COUNT - 1;
			}

			int count = 0;
			if(PlayerData.instance.consumables.TryGetValue((Consumable.ConsumableType)m_UsedPowerupIndex, out count) && count > 0)
			{
				found = true;
			}

		} while (m_UsedPowerupIndex != 0 && !found);

		m_PowerupToUse = (Consumable.ConsumableType)m_UsedPowerupIndex;
		PopulatePowerup();
	}

	public void UnequipPowerup()
	{
		m_PowerupToUse = Consumable.ConsumableType.NONE;
	}
	

	public void SetModifier(Modifier modifier)
	{
		m_CurrentModifier = modifier;
	}

    /// <summary>
    /// Splash Setup................................................................>P
    /// 
    /// </summary>

    public void StartGameWithSplash()
    {
        ApplicationState.isLoadingAfterTutorial = true;
        StartCoroutine(SplashAnimation());
        StartCoroutine(UIcharMove());
        StartCoroutine(UIEnmyCharMove());
        splashUI.SetActive(false);
        Debug.Log("loading hide");
        splashBlur.SetActive(false);
        m_camera_animator.Play("LateCameraShakeAnimation");
        //m_msAnimator.SetBool("Movesplash", true);  // Animator of main character
        m_esAnimator.SetBool("enmyMove", true);       // Animator of enmey character
        //IsCameraMoved = true;

    }

    IEnumerator UIcharMove()
    {
        int _audio = PlayerPrefs.GetInt("Audio");
        if(_audio == 0)
        {
            //Write a code to reduce volume
            _evilLaugh.volume = 0;
        }
        if(_audio == 1)
        {
            //Write a code to increase volume
            _evilLaugh.volume = 1;
        }
        _evilLaugh.Play();
        yield return new WaitForSeconds(0.33f);//Old waitforsecond
        //_evilLaugh.Play();
        m_msAnimator.SetBool("Movesplash", true);  // Animator of main character
        yield return new WaitForSeconds(1.4f);
        //m_camera.GetComponent<Animator>().Play("CameraShakeAnimation");
        m_camera_animator.Play("CameraShakeAnimation");
        m_camera_animator.Play("ZoomCamAnimation");
        yield return new WaitForSeconds(0.6f);
        IsmainCharMoved = true;
        IsEnemyCharMoved = true;
        IsFollowCamera = true;
        //m_esAnimator.SetBool("enmyMove", true); //Animator of enemy character
    }

    IEnumerator UIEnmyCharMove()
    {
        //yield return new WaitForSeconds(1.667f);//Old waitforseconds
        yield return new WaitForSeconds(3f);
        //IsEnemyCharMoved = true;
        IsCameraMoved = true;
    }

    public GameObject fakeEnvironment;
    public IEnumerator SplashAnimation()
    {
        yield return new WaitForSeconds(5);
        splashBlur.SetActive(true);
        yield return new WaitForSeconds(1);
        m_msAnimator.SetBool("Movesplash", false);  // Animator of main character
        m_esAnimator.SetBool("enmyMove", false);   // Animator of enmey character
        SetCamCurrentPos();
        m_camera.transform.position = CameraPositionInGame;
        IsmainCharMoved = false;
        IsEnemyCharMoved = false;
        IsFollowCamera = false;
        splashUI.SetActive(true);            
        m_enemyCharacter.SetActive(true);        
        PickUpObject.instance.SetCanOrigPos();
        StartGame();
    }

    public void StartGame()
    {
        
        fakeEnvironment.SetActive(false);   // P> Sign..
        if (PlayerData.instance.tutorialDone)
        {
            if (PlayerData.instance.ftueLevel == 1)
            {
                PlayerData.instance.ftueLevel = 2;
                PlayerData.instance.Save();
        
            }
        }
        manager.SwitchState("Game");   //Todo
        GameState.instance.quitePanel.SetActive(false);
        //splashBlur.SetActive(true);
    }

	public void Openleaderboard()
	{
		
    }
}
