using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelperUtil : MonoBehaviour
{
    public static HelperUtil instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Helper method to start an async call.(action will be called after delay).
    /// </summary>
    public static void CallAfterDelay(Action action, float delay, Func<bool> cancelCondition = null)
    {
        float initialTime = Time.time;

        instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            while (true)
            {
                //If cancel condition gets true, return control from this line.
                if (cancelCondition != null && cancelCondition()) yield break;
                //If delay is reached, break this loop.
                else if (Time.time > initialTime + delay) break;

                //Hold control for set amount of time to decrease CPU pressure.
                yield return new WaitForSeconds(0.02f);
            }

            //Execute the action if delay reached and cancel condition is still false.
            action();
        }

    }

    /// <summary>
    /// Helper method to start an async call.(action will be called after the condition gets true).
    /// </summary>
    public static void CallAfterCondition(Action action, Func<bool> condition, Func<bool> cancelCondition = null)
    {
        instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            while (!condition())
            {
                //If cancel condition gets true, return control from this line.
                if (cancelCondition != null && cancelCondition()) yield break;

                yield return new WaitForSeconds(0.5f);
            }
            action();
        }
    }

}