using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class languageSelection : MonoBehaviour
{
    public static languageSelection instance;


    public GameObject LoginPanel;
    public GameObject UICanvas;
    public GameObject languagePanel;
    public Button englishSelectionButton;
    public Button frenchSelectionButton;
    public Button PortugueseSelectionButton;
    public Button ArabicSelectionButton;
    public Button skipButton;
    public Text textOfBtn;
    public Button StartBtn;
    public GameObject GameOver;
    public GameObject PausedBoard;
    public int langID;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       // PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("FirstTimeLangSelction011", 1) == 1)
        {
            UICanvas.SetActive(false);
            LoginPanel.SetActive(false);
            Debug.Log("First Time Here  :FirstTimeLangSelction01 : ");
            skipButton.interactable = false;
           // PlayerPrefs.SetInt("FirstTimeLangSelction011", 0);
            englishSelectionButton.onClick.AddListener(EnglishLang);
            frenchSelectionButton.onClick.AddListener(frenchLang);
            PortugueseSelectionButton.onClick.AddListener(PortugueseLang);
            ArabicSelectionButton.onClick.AddListener(ArabicLang);
        }
        else
        {
           // LoginPanel.SetActive(true);
            skipButton.interactable = true;
            Debug.Log("You are not First time here");
            if (PlayerPrefs.GetInt("LS") == 1)
            {
                EnglishLang();
            }
            else if (PlayerPrefs.GetInt("LS") == 2)
            {
                frenchLang();
            }
            else if (PlayerPrefs.GetInt("LS") == 3)
            {
                PortugueseLang();
            }
            else if (PlayerPrefs.GetInt("LS") == 4)
            {
                ArabicLang();
                
                languagePanel.SetActive(false);
            }
        }
    }


    public void EnglishLang()
    {
        textOfBtn.text = "EN";
        skipButton.interactable = true;
        PlayerPrefs.SetInt("LS", 1);
        LocalizationManager.instace.SetLanguage("english");
        languagePanel.SetActive(false);
        langID = 0;
        if (LoginFirstTime.instance.isCanvasEnable == true)
        {
            UICanvas.SetActive(true);
        }

        if (PlayerPrefs.GetInt("FirstTimeLangSelction011", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeLangSelction011", 0);
            LoginPanel.SetActive(true);
        }
    }


    public void frenchLang()
    {
        textOfBtn.text = "FR";
        skipButton.interactable = true;
        PlayerPrefs.SetInt("LS", 2);
        LocalizationManager.instace.SetLanguage("french");    
        languagePanel.SetActive(false);
        langID = 1;
        if(LoginFirstTime.instance.isCanvasEnable == true)
        {
            UICanvas.SetActive(true);
        }

        if (PlayerPrefs.GetInt("FirstTimeLangSelction011", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeLangSelction011", 0);
            LoginPanel.SetActive(true);
        }
    }


    public void PortugueseLang()
    {
        textOfBtn.text = "PO";
        skipButton.interactable = true;
        PlayerPrefs.SetInt("LS", 3);
        LocalizationManager.instace.SetLanguage("Portugais");
        languagePanel.SetActive(false);
        langID = 3;
        if (LoginFirstTime.instance.isCanvasEnable == true)
        {
            UICanvas.SetActive(true);
        }
        if (PlayerPrefs.GetInt("FirstTimeLangSelction011", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeLangSelction011", 0);
            LoginPanel.SetActive(true);
        }
    }
    public void ArabicLang()
    {
        textOfBtn.text = "أر";
        skipButton.interactable = true;
        PlayerPrefs.SetInt("LS", 4);
        LocalizationManager.instace.SetLanguage("Arabic");
        languagePanel.SetActive(false);
        langID = 2;
        if (LoginFirstTime.instance.isCanvasEnable == true)
        {
            UICanvas.SetActive(true);
        }
        if (PlayerPrefs.GetInt("FirstTimeLangSelction011", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeLangSelction011", 0);
            LoginPanel.SetActive(true);
        }
    }

}
