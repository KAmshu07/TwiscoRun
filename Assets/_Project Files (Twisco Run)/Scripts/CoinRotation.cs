using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    private float rotationSpeed;

    private void Start()
    {
        // Generate a random rotation speed for each coin
        rotationSpeed = Random.Range(200f, 250f);
    }

    private void Update()
    {
        // Rotate the coin based on the rotationSpeed and rotationAxis
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
