using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginFirstTime : MonoBehaviour
{
    public static LoginFirstTime instance;
    public GameObject LoginSection;
    public GameObject loginManager;
    public GameObject UICanvas;
    public InputField NameInputField;
    public Button confirm;
    public Text NameBox;
    public bool isCanvasEnable = false;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {     
        confirm.interactable = false;
        if (PlayerPrefs.GetInt("YouAreFirstTimeHere001", 1) == 1)
        {
            UICanvas.SetActive(false);
            SettingPopup.instance.masterSliderSoundOff();                        
        }
        else
        {
            SettingPopup.instance.masterSliderSoundOn();           
            NameBox.text = PlayerPrefs.GetString("name");
            LoginSection.SetActive(false);
            loginManager.SetActive(false);
            UICanvas.SetActive(true);
            isCanvasEnable = true;           
        }
        
    }


    public void SceneLoadFirstTime()
    {
        UICanvas.SetActive(true);
        isCanvasEnable = true;
        PlayerPrefs.SetString("name", NameInputField.text);
        Debug.Log(PlayerPrefs.GetString("name"));
        NameBox.text = PlayerPrefs.GetString("name");
        LoginSection.SetActive(false);
        loginManager.SetActive(false);
        SettingPopup.instance.masterSliderSoundOn();
        PlayerPrefs.SetInt("YouAreFirstTimeHere001", 0);        
    }


    private void Update()
    {
        if (NameInputField.text.Length >= 3)
        {         
            confirm.interactable = true;
        }
        else
        {
            confirm.interactable = false;
        }
    }

}
