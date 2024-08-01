using UnityEngine;

public class BinCollisionCheck : MonoBehaviour
{    
    public GameObject _MainDrum;
    public GameObject _BrokenDrum;
    public Animator _BrokenDrumAnim;
    public ParticleSystem _BlastEffect;
    public CharacterCollider _characterCollider;

    private void Start()
    {
        _characterCollider = GameObject.Find("CharacterSlot").GetComponent<CharacterCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {      
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log((_characterCollider.m_Invincible).ToString() + " " + "Here it goes");
            transform.parent.GetComponent<ObstacleDrum>().crashParticle.Play();
            //X();
            Destroy(transform.parent.GetComponent<ObstacleDrum>(), 0.5f);

            foreach (var item in GameObject.FindObjectsOfType<ObstacleDrum>())
            {
                item.GetComponent<ObstacleDrum>().checkBinCollision?.Invoke();
            }

            EnemyCharacter.checkCollision?.Invoke();
        }

    }
    public void X()
    {
        _MainDrum.SetActive(false);
        _BrokenDrum.SetActive(true);
        _BrokenDrumAnim.Play("BrokenDrum");
        _BlastEffect.Play();
        Destroy(transform.parent.GetComponent<ObstacleDrum>(), 0.4f);
    }
}
