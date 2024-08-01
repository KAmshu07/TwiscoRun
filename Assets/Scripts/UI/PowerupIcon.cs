using UnityEngine;
using UnityEngine.UI;

public class PowerupIcon : MonoBehaviour
{
    [HideInInspector]
    public Consumable linkedConsumable;

    public Image icon;
    public Sprite[] PowerUpBoosterImage;
    public Sprite[] PowerUpSpeedImage;
    

    //public Slider slider;
    public Image progress_Bar;
    public static PowerupIcon Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start ()
    { 
        icon.sprite = linkedConsumable.icon;

        if (CharacterCollider.instance.IsPlayerBoosting())
        {
            if ((PlayerPrefs.GetInt("LS") == 4))
            {
                icon.sprite = PowerUpBoosterImage[2];
            }
            else if ((PlayerPrefs.GetInt("LS") == 2))
            {
                icon.sprite = PowerUpBoosterImage[1];
            }

            else if ((PlayerPrefs.GetInt("LS") == 1))
            {
                icon.sprite = PowerUpBoosterImage[0];
            }
            else if (PlayerPrefs.GetInt("LS") == 3)
            {
                icon.sprite = PowerUpBoosterImage[3];
            }
        }

        else
        {
            if ((PlayerPrefs.GetInt("LS") == 4))
            {
                icon.sprite = PowerUpSpeedImage[2];
            }
            else if ((PlayerPrefs.GetInt("LS") == 2))
            {
                icon.sprite = PowerUpSpeedImage[1];
            }

            else if ((PlayerPrefs.GetInt("LS") == 1))
            {
                icon.sprite = PowerUpSpeedImage[0];
            }
            else if (PlayerPrefs.GetInt("LS") == 3)
            {
                icon.sprite = PowerUpSpeedImage[3];
            }
        }

        // Setting Booster Images
        




        //if(IsBooster )
        //{
        //    // Setting Booster Images
        //    if ((PlayerPrefs.GetInt("LS") == 4))
        //    {
        //        icon.sprite = PowerUpBoosterImage[2];
        //    }
        //    else if ((PlayerPrefs.GetInt("LS") == 2))
        //    {
        //        icon.sprite = PowerUpBoosterImage[1];
        //    }

        //    else if ((PlayerPrefs.GetInt("LS") == 1))
        //    {
        //        icon.sprite = PowerUpBoosterImage[0];
        //    }
        //}
        //else
        //{
        //    // Setting Speed Images
        //    if ((PlayerPrefs.GetInt("LS") == 4))
        //    {
        //        icon.sprite = PowerUpSpeedImage[2];
        //    }
        //    else if ((PlayerPrefs.GetInt("LS") == 2))
        //    {
        //        icon.sprite = PowerUpSpeedImage[1];
        //    }

        //    else if ((PlayerPrefs.GetInt("LS") == 1))
        //    {
        //        icon.sprite = PowerUpSpeedImage[0];
        //    }
        //}
    }

    void Update()
    {
        //slider.value = 1.0f - linkedConsumable.timeActive / linkedConsumable.duration;
        progress_Bar.fillAmount = 1.0f - linkedConsumable.timeActive / linkedConsumable.duration;
    }
}
