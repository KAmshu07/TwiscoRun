using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashState : AState
{
    public Text splashText;
    public Canvas canvas;
    public static SplashState instance;

    public void Awake()
    {
        instance = this;
    }

    public override void Enter(AState from)
    {
        canvas.gameObject.SetActive(true);
        splashText.gameObject.SetActive(true);
    }

    public override void Exit(AState to)
    {
        canvas.gameObject.SetActive(false);
    }

    public override string GetName()
    {
        return "Splash";
    }

    public override void Tick()
    {
        
    }
   
}
