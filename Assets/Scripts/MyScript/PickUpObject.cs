using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public static PickUpObject instance;
    public GameObject milkCanParent;
    public GameObject MilkCan;
    public GameObject soket;
    private Vector3 m_orignalPositiionOfCan;
    private Vector3 m_orignalRotationOfCan;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       m_orignalPositiionOfCan = MilkCan.transform.position;
       m_orignalRotationOfCan = MilkCan.transform.eulerAngles;
        Debug.Log("Can:: "+ m_orignalPositiionOfCan);
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            
            SetCanOrigPos();
        }
    }
    

    public void SetCanOrigPos()
    {
        //MilkCan.transform.SetParent(null);
        MilkCan.transform.parent = milkCanParent.transform;
        MilkCan.transform.position = m_orignalPositiionOfCan;
        MilkCan.transform.eulerAngles = m_orignalRotationOfCan;
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "milkcan")
        {
            MilkCan.transform.parent = this.transform;
            MilkCan.transform.position = soket.transform.position;
            
        }
    }
   
}
