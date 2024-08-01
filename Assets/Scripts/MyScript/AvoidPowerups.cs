using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPowerups : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Invincibility"))
        {
            Invincibility.instance.gameObject.SetActive(false);
            Debug.Log("INVINCIBILITY---Power--UPs--Exits----------- INSIDE THE DRUM");
        }
        if (other.gameObject.CompareTag("Boost"))
        {
            InvincibilityPlueBoost.instance.gameObject.SetActive(false);
            Debug.Log("BOOST--Power--UPs--Exits----------- INSIDE THE DRUM");
        }
        
    }
   
}
