using UnityEngine;
using System.Collections;

public class Camera_splash : MonoBehaviour
{
    public static Camera_splash instance;
    private Vector3 m_camCurrentPos;
    public GameObject m_camera;
    // public Transform m_StarPoint;
    public Transform m_EndPoint;
    public float m_Speed = 1.0f;
    public Vector3 m_offsetValue = new Vector3(0, 0, 0);
    private bool IsCameraMoved = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_camCurrentPos = m_camera.transform.position;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            IsCameraMoved = true;

        }

        if (Input.GetKey(KeyCode.S))
        {
            SetCamCurrentPos();
        }



        if (IsCameraMoved)
        {
            m_camera.transform.position = Vector3.MoveTowards(transform.position, m_EndPoint.position + m_offsetValue, m_Speed * Time.deltaTime);
            if (transform.position == m_EndPoint.position + m_offsetValue)
            {
                IsCameraMoved = false;
            }
        }

    }



    public void StarCameraMovement()
    {
        while (transform.position != m_EndPoint.position + m_offsetValue)
        {
            m_camera.transform.position = Vector3.MoveTowards(transform.position, m_EndPoint.position + m_offsetValue, m_Speed * Time.deltaTime);         
        }
    }



    public void SetCamCurrentPos()
    {
        m_camera.transform.position = m_camCurrentPos;
    }


}