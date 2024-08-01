using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessLeaderBoard : MonoBehaviour
{
    public void ShowLeaderBoard()
    {
#if UNITY_ANDROID
        Playgameservices.instance.showleaderboard();
#elif UNITY_IPHONE
        AppleLogin.ShowLeaderboard();
#endif
    }
}
