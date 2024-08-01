using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;
using System;

public class Playgameservices : MonoBehaviour
{
#if UNITY_ANDROID
    public static Playgameservices instance;
    // [SerializeField] Text dheerajScore;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
#if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
            Debug.Log("Signedin");
           // debugtext.text = "playgames initialized";
        signinuserwithplaygames();
#endif
    }

    void signinuserwithplaygames()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (success) =>
        {
            switch (success)
            {
                case SignInStatus.Success:
                   // debugtext.text = "signined in player using play games successfully";
                    break;
                default:
                    //debugtext.text = "Signin not successfull";
                    break;
            }
        });
#endif
    }

    public void postscoretoleaderboard(int gameScore)
    {
#if UNITY_ANDROID
        Social.ReportScore(gameScore, "CgkIv7CLuMEXEAIQBA", (bool success) =>
         {
             if (success)
             {
                // debugtext.text = "successfully add score to leaderboard";
             }
             else
             {
                // debugtext.text = "not successfull";
             }
         });
#endif
    }

    public void showleaderboard()
    {
#if UNITY_ANDROID
        Social.ShowLeaderboardUI();
        int scorE = PlayerPrefs.GetInt("HighScore", 0);
        postscoretoleaderboard(scorE);
#endif
    }

    public void achievementcompleted()
    {
        Social.ReportProgress("CgkIzN21ycISEAIQAw", 100.0f, (bool success) =>
          {
              if (success)
              {
                  // debugtext.text = "successfully unlocked achievements";
              }
              else
              {
                  // debugtext.text = "not successfull";
              }
          });
    }
    public void showacievementui()
    {
        Social.ShowAchievementsUI();

    }


    private bool issaving = false;
    private string SAVE_NAME = "savegames";
    public void opensavetocloud(bool saving)
    {
#if UNITY_ANDROID
        // debugtext.text = "hello";
        if (Social.localUser.authenticated)
        {
            //  debugtext.text = "hello2";
            issaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution
                (SAVE_NAME, GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, savedgameopen);


        }
#endif
    }

    private void savedgameopen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
#if UNITY_ANDROID
        if(status == SavedGameRequestStatus.Success)
        {
           // debugtext.text = "hello in save1";
            if (issaving)//if is saving is true we are saving our data to cloud
            {
               // debugtext.text = "hello in save2";
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(GetDataToStoreinCloud());
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, saveupdate);
            }
            else//if is saving is false we are opening our saved data from cloud
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, ReadDataFromCloud);
            }
        }
#endif
    }

    private void ReadDataFromCloud(SavedGameRequestStatus status, byte[] data)
    {
#if UNITY_ANDROID
        if (status == SavedGameRequestStatus.Success)
        {
            string savedata = System.Text.ASCIIEncoding.ASCII.GetString(data);
            LoadDataFromCloudToOurGame(savedata);
        }
#endif
    }

    private void LoadDataFromCloudToOurGame(string savedata)
    {
#if UNITY_ANDROID
        string[] data = savedata.Split('|');
        //debugtext.text = data[0].ToString();
#endif
    }

	private void saveupdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
	{
		//use this to debug whether the game is uploaded to cloud
		//debugtext.text = "successfully add data to cloud";
	}

	private string GetDataToStoreinCloud()//  we seting the value that we are going to store the data in cloud
    {
        string Data = "";
#if UNITY_ANDROID
        //data [0]
        // Data += datatocloud.text.ToString();
        Data += "|";
#endif
        return Data;
    }
#endif
}
