using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public AudioSource buttonPress;
    public Button btnPress;

    private void Start()
    {
        buttonPress = GetComponent<AudioSource>();
        btnPress = GetComponent<Button>();
        btnPress.onClick.AddListener(buttonPressClick);

    }
    public void buttonPressClick()
    {
        buttonPress.Play();
    }
}
