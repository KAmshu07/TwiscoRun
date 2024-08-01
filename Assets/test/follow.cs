using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{   

    public Transform objectToFollow;  // Player in your Case
    public float offsetZ;


    void Update()
    {
        // Create a new Vector 3 with the positions of object to follow. Substract offset from pos.z
        Vector3 myNewPos = new Vector3(objectToFollow.position.x, objectToFollow.position.y, objectToFollow.position.z - offsetZ);



        // Set position of the scripts GameObject to the previous created postition
        transform.position = myNewPos;
    }
}


