using UnityEngine;

public class Coin : MonoBehaviour
{
	static public Pooler coinPool;
    public bool isPremium = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "binBase")
        {
            Destroy(gameObject);
           
        }
             
    }


}
