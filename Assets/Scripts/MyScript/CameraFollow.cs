using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Transform Target;
    public Vector3 Offset;
    public Vector3 cameraOffsetOnLaugh;
    public Vector3 cameraRotaion = new Vector3(0, 0, 0);
    public float SmoothTimeForX;
    public float SmoothTimeForY;
    public Vector3 targetPosition;
    public Transform deathPosition;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(instance);
        }

    }
    private void Start()
    {
       // transform.eulerAngles = new Vector3(0, 0, 0);
    }
    private void LateUpdate()
    {
        if (Target != null)
        {
             targetPosition = Target.position + Offset;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x, SmoothTimeForX), 
                                            Mathf.Lerp(transform.position.y, targetPosition.y, SmoothTimeForY), targetPosition.z);
        }
    }

    /*private void Update()
    {
        Debug.Log("Value form----" + EnemyCharacter.isEnemyLaugh);
        if (EnemyCharacter.isEnemyLaugh)
        {
            // Vector3 targetPosition1 = Target.position + cameraOffsetOnLaugh;
            // transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition1.x, 1), Mathf.Lerp(transform.position.y, targetPosition1.y, 1), Mathf.Lerp(transform.position.z, targetPosition1.z, 1));
            transform.position = deathPosition.position;
            // transform.eulerAngles = new Vector3(Mathf.Lerp(cameraRotaion.x, targetPosition1.x, 1), Mathf.Lerp(cameraRotaion.y, targetPosition1.y, 1), cameraRotaion.z);
            //transform.LookAt(Target, Vector3.up);
            EnemyCharacter.isEnemyLaugh = false;
        }
    }*/
}
