using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider masterSFXSlider;
    public AudioSource enemylaugh;
    public AudioSource _enemyLaugh;

    public static SettingPopup instance;

    public LoadoutState loadoutState;
    public DataDeleteConfirmation confirmationPopup;

    protected float m_MasterVolume;
    protected float m_MusicVolume;
    protected float m_MasterSFXVolume;

    protected const float k_MinVolume = -80f;
    protected const string k_MasterVolumeFloatName = "MasterVolume";
    protected const string k_MusicVolumeFloatName = "MusicVolume";
    protected const string k_MasterSFXVolumeFloatName = "MasterSFXVolume";

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        int audioV = PlayerPrefs.GetInt("Audio");
        if (audioV==0)
        {
            masterSlider.value = 0;
        }
        if (audioV==1)
        {
            masterSlider.value = 1;
        }
    }
    public void Open()
    {
        gameObject.SetActive(true);
        UpdateUI();
    }

    public void Close()
    {
		PlayerData.instance.Save ();
        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        mixer.GetFloat(k_MasterVolumeFloatName, out m_MasterVolume);
        mixer.GetFloat(k_MusicVolumeFloatName, out m_MusicVolume);
        mixer.GetFloat(k_MasterSFXVolumeFloatName, out m_MasterSFXVolume);

       // masterSlider.value = 0.0f;
       
        musicSlider.value = 1.0f;
        masterSFXSlider.value = 1.0f;
    }

    // master sound , all on and all off..
    public void masterSliderSoundOn()
    {
        masterSlider.value = 1;
        PlayerPrefs.SetInt("Audio", 1);
    }


    public void masterSliderSoundOff()
    {
        masterSlider.value = 0;
        PlayerPrefs.SetInt("Audio", 0);
    }


    public void DeleteData()
    {
        confirmationPopup.Open(loadoutState);
    }


    public void MasterVolumeChangeValue(float value)
    {
        m_MasterVolume = k_MinVolume * (1.0f - value);
        mixer.SetFloat(k_MasterVolumeFloatName, m_MasterVolume);
		PlayerData.instance.masterVolume = m_MasterVolume;
    }

    public void MusicVolumeChangeValue(float value)
    {
        m_MusicVolume = k_MinVolume * (1.0f - value);
        mixer.SetFloat(k_MusicVolumeFloatName, m_MusicVolume);
		PlayerData.instance.musicVolume = m_MusicVolume;
    }

    public void MasterSFXVolumeChangeValue(float value)
    {
        m_MasterSFXVolume = k_MinVolume * (1.0f - value);
        mixer.SetFloat(k_MasterSFXVolumeFloatName, m_MasterSFXVolume);
		PlayerData.instance.masterSFXVolume = m_MasterSFXVolume;
    }
}
