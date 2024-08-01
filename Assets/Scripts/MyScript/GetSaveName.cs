using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSaveName : MonoBehaviour
{

    public Text NameBox;

    // Start is called before the first frame update
    void Start()
    {
        NameBox.text = PlayerPrefs.GetString("name");
    }


}
