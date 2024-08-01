using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDrum : MonoBehaviour
{
    public delegate void CheckBinCollision();
    public  CheckBinCollision checkBinCollision;

    [SerializeField]
    public CharacterCollider characterCollider;

    public ParticleSystem crashParticle;

    public ParticleSystem blastEffect;
   
    public Animator drumAnimator;
 
    public static ObstacleDrum instance;

    private void Awake()
    {
        instance = this;
        checkBinCollision += StopDrumAnimations;
    }

    private void Start()
    {
        characterCollider = GameObject.Find("CharacterSlot").GetComponent<CharacterCollider>();
    }

    private void StopDrumAnimations()
    {
		// drumAnimator.enabled = false;
		//foreach (Animator animator in FindObjectsOfType<Animator>())
		//{
		//	animator.enabled = false;
		//}

	}

    private void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.CompareTag("Player"))
        {
            drumAnimator.Play("OBS_Fall_Anim_Variant");
            drumAnimator.Play("OBS_Fall_Anim");
            drumAnimator.Play("OBS_Fall_Anim_Pot");

            Destroy(gameObject, 3f);
            if(PowerupIcon.Instance!=null) PowerupIcon.Instance.gameObject.SetActive(false);
        }

    }

}
