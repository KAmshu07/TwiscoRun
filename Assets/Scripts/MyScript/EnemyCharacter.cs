using System.Collections;
using UnityEngine;


public class EnemyCharacter : MonoBehaviour
{
    public static EnemyCharacter instance;

    public delegate void CheckCollision();
    public static CheckCollision checkCollision;
    [Header("Character & Movements")]
    public Transform targetPlayer;
    public Transform playerPivot;
    public Vector3 enemyOffset = new Vector3(0, 0.55f, 7);
    public Vector3 targetPlayerOffset = new Vector3(0, 1.0f, 0);
    [Header("enmyPos")]
    public Vector3 enemy_Position = new Vector3(0, 0, 0.5f);
    private Vector3 tempPosition;
    public float minSpeed = 1.0f;
    public float m_enmySpeed = 50.0f;
    public float speedValue = 7.0f;
    private float m_distance = 25;
    public int speedStep = 4;
    public bool IsRun = false;
    public bool IsCatch = false;
    public Animator m_enemyAnimator;
    public AudioSource m_audio;
    public AudioClip LaughAudio;
    public Animator main_CharAnimator;
    private Coroutine EnemyRunningBehaviour;

    public GameObject pauseButton;

    public Coroutine timeHandler;
    public bool startPersonalTimer;
    public float timer = 1f;

    public bool isEnemyReached = false;
    public static bool isEnemyLaugh = false;

    private void Awake()
    {
        instance = this;
        checkCollision += reSpawnEnemy;
    }


    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
        EnemyRunningBehaviour = StartCoroutine(ChasePlayer());
        tempPosition = enemy_Position;
        //StartPersonalTimer();
    }


    private void Update()
    {
        int islaugh = PlayerPrefs.GetInt("Audio");
        if (islaugh == 0)
        {
            m_audio.enabled = false;
            // Debug.Log("Dheeraj AUDIO muted-------");
        }
        if (islaugh == 1)
        {
            m_audio.enabled = true;
            //  Debug.Log("Dheeraj AUDIO NOtMuted-------");
        }

        if (IsRun)
        {
            float transitionDelta = minSpeed * (Time.deltaTime == 0 ? 0.02f : Time.deltaTime);
            this.transform.position = Vector3.Lerp(new Vector3(this.gameObject.transform.position.x, 0, transform.position.z), targetPlayer.transform.position, transitionDelta);

            enemy_Position = this.gameObject.transform.position;
            if (enemy_Position.z >= m_distance)
            {
                minSpeed -= 0.1f;
                if (minSpeed < 1)
                {
                    IsRun = false;
                    minSpeed = speedValue;
                    enemy_Position = new Vector3(0, 0, 0.5f);
                    transform.position = enemy_Position;
                    gameObject.SetActive(false);
                    StopCoroutine(EnemyRunningBehaviour);
                }
            }
        }

        if (IsCatch)
        {
            if (this.gameObject.transform.position.z >= playerPivot.transform.position.z - 0.8f)
            {
                minSpeed = 20;
            }
            else
            {
                minSpeed = 2;
            }
            transform.position = Vector3.Lerp(this.gameObject.transform.position + targetPlayerOffset, targetPlayer.transform.position + enemyOffset, minSpeed * Time.deltaTime);
            if (this.gameObject.transform.position.z >= playerPivot.transform.position.z - 0.2f)
            {
                if (!m_audio.isPlaying)
                {
                    m_audio.Play();
                    //m_audio.PlayOneShot(LaughAudio);
                }
                m_enemyAnimator.SetBool("Moving", false);
                isEnemyReached = true;
                isEnemyLaugh = true;
                m_enemyAnimator.SetBool("Moving", false);
                m_enemyAnimator.SetBool("grab", true);

                //CharacterInputController.instance.character.animator.SetBool("Grab", true);                

            }
        }
    }


    public void MaieMenuCall()
    {
        m_audio.Stop();
        enemy_Position = new Vector3(0, 0, 0.5f);
        transform.position = enemy_Position;
        m_enemyAnimator.SetBool("Moving", false);
        m_enemyAnimator.SetBool("grab", false);
        this.gameObject.SetActive(false);
        IsCatch = false;
        minSpeed = speedValue;
        timer = 1f;
    }

    int i = 1;
    public void CallAgain()
    {
        StartPersonalTimer();
        if (i == 1)
        {
            timer = 7.0f;
            i = 0;
        }
        else
        {
            timer = 1f;
        }
    }


    public void EnemyReRun()
    {
        m_audio.Stop();
        IsCatch = false;
        transform.position = enemy_Position;
        enemy_Position = new Vector3(0, 0, 1f);
        minSpeed = speedValue;
        timer = 1f;
        StartPersonalTimer();
        CameraFollow.instance.gameObject.SetActive(true);
        GameState.instance.enemyCamera.gameObject.SetActive(false);
    }


    public void reSpawnEnemy()
    {
        Debug.Log("reSpawnEnemy");
        enemy_Position = new Vector3(0, 0, -5f);
        transform.position = enemy_Position;
        this.gameObject.SetActive(true);
        minSpeed = speedValue;
        m_enemyAnimator.SetBool("Moving", true);
        m_enemyAnimator.SetBool("grab", false);
        IsCatch = true;
        IsRun = false;
    }


    IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(0f);

    }


    public void StartPersonalTimer()
    {

        timeHandler = StartCoroutine(Countdown());
        IEnumerator Countdown()
        {

            startPersonalTimer = true;
            while (startPersonalTimer)
            {

                timer -= Time.deltaTime * 3;
                if (timer <= 0)
                {
                    m_enemyAnimator.SetBool("grab", false);
                    m_enemyAnimator.SetBool("Moving", true);
					if (!IsCatch )
					{
                        IsRun = true;
					}
                    StopPersonalTimer();
                }
                yield return null;
            }
        }
    }


    public void StopPersonalTimer()
    {
        if (timeHandler != null)
        {
            StopCoroutine(timeHandler);
            timeHandler = null;
        }
        startPersonalTimer = false;
    }


    // Game State Pause mode and Resume mode of game.

    public void PauseEnemyState()
    {
        Time.timeScale = 0;
        if (timer >= 0)
        {
            //StopCoroutine(timeHandler);
        }
        else
        {
            IsRun = false;
        }
    }


    public void ResumeEnemyState()
    {
        Time.timeScale = 1;
        if (timer >= 0)
        {
            StartPersonalTimer();
        }
        else
        {
            IsRun = true;
        }

    }

}
