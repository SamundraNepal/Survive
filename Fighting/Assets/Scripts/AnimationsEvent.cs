using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvent : MonoBehaviour
{


    EnemyMotor EM;


    [Header("Audio")]
    public AudioClip Slash;
    public AudioSource Source;
    public AudioClip[] clips;




    [Header("High Level Attack")]
    public GameObject HighLevelAttack;
    public CapsuleCollider Col1;
    public AudioClip HLAA;

    [Header("Low Level Attack")]
    public GameObject LowLevelAttack;
    public CapsuleCollider Col2;


    public Animator anime;

    private void Start()
    {
        Source = GetComponent<AudioSource>();
        DisableAttacks();
        EM = GetComponent<EnemyMotor>();
        Col1.enabled = true;
        Col2.enabled = true;
    }







   public void DisabledAllTheAnimations()
    {

        EM.Callme();
    }






   public void LowAttack()
    {
        LowLevelAttack.SetActive(true);
        Source.clip = Slash;
        Source.Play();

    }


  public  void HighLevelAttacks()
    {

        HighLevelAttack.SetActive( true);
        Source.clip = HLAA;
        Source.Play();
    }




  public  void DisableAttacks()
    {

        LowLevelAttack.SetActive ( false);
        HighLevelAttack.SetActive( false);




    }

    public void EnemyHurt()
    {

        DisableAttacks();
        Source.clip = clips[Random.Range(0, clips.Length)];
        Source.Play();
        anime.SetBool("Hurt", false);
        
    }

}
