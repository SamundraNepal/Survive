using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{

    [Header("Player Walking")]
    public AudioClip[] WalkingSounds;

    public AudioSource Source;
    public AudioSource SecondSource;
    public AudioSource ThridSource;


    [Header("High Kick")]
    public GameObject Kick;

    [Header("Jump Attack")]
    public GameObject JumpAttack;
    public SlowDownTimeManager SDT;
    public GameObject[] Rocks;
    public Transform[] RocksPoints;
    public GameObject Dust;
    public GameObject GunPosition;
    public AudioClip HItGroundAudio;
    public AudioClip RocksFalling;



    public bool IsAttacking;
    public Animator Anime;

    [Header("Jump Force")]
    PlayerMovement Pm;
    PlayerAttack Pa;
    public AudioClip Jump;


    [Header("Graanade Throw")]
    public bool IsGthrowTrue;
    ProjectileMotion Projectile;
    public bool ThrowGranadeNow;



    [Header("Hand Attack")]
    public GameObject Attacks1;
    public GameObject Attack2;
    public AudioClip[] Punches;


    public AudioClip[] Hurt;

   public CameraShake CS;

    private void Start()
    {
        Pa = GetComponent<PlayerAttack>();
        Projectile = GetComponent<ProjectileMotion>();
        Pm = GetComponent<PlayerMovement>();
        DisabledAllAttacks();

        Anime = GetComponent<Animator>();
        DisableAllAttcks();
        Attacks1.GetComponent<CapsuleCollider>().enabled = true;
        Attack2.GetComponent<CapsuleCollider>().enabled = true;

    }



    public void StopRootMotion()
    {

        Anime.applyRootMotion = false;
        IsAttacking = false;



    }

    public void HighKick()
    {

        Kick.SetActive(true);
        SecondSource.clip = Punches[Random.Range(0,Punches.Length)];
        SecondSource.Play();

    }


    public void JumpAttacks()
    {
        SDT.DoSlowDownTime();
        Pa.Anime.SetBool(Pa.JumpAttack, false);

    }

    public void JumpAttacksTimeUnDone()
    {
        SDT.FinishSlowDownTime();
    }


    public void DisabledAllAttacks()
    {
        JumpAttack.SetActive(false);
        Kick.SetActive(false);


    }

    public void JumpPlayer()
    {
        ThridSource.clip = Jump;
        ThridSource.Play();
        Pm.IsJumping = true;
        Pm.Rb.AddForce(Vector3.up * Pm.JumpForce * Time.fixedDeltaTime);
        Pm.Rb.AddForce(Vector3.forward * Pm.Momentum* Time.fixedDeltaTime);
    }


    public void PlayerHurt()
    {


        DisableAllAttcks();

        ThridSource.clip = Hurt[Random.Range(0, Hurt.Length)];
        ThridSource.Play();
        Anime.SetBool("Hurt", false);
        
    
    }

    public void Walking()
    {
       Source.clip = WalkingSounds[Random.Range(0, WalkingSounds.Length)];
        Source.Play();

    }


    public void JumpAttackHitGround()
    {
        ThridSource.clip = HItGroundAudio;
        ThridSource.Play();

        JumpAttack.SetActive(true);
        SecondSource.clip = RocksFalling;
        SecondSource.Play();
        CS.CanShake = true;

        foreach (var R in Rocks)
        {
            foreach (var P in RocksPoints)
            {
                GameObject G = Instantiate(R, P.transform.position, Quaternion.identity);

                Physics.IgnoreCollision(G.GetComponent<BoxCollider>(), transform.GetComponent<CapsuleCollider>());
                Destroy(G, 5f);
                GameObject D = Instantiate(Dust, P.transform.position, Quaternion.identity);
                Destroy(D, 3f);


            }


        }


      
        
    }



    public void GthroPose()
    {

        Anime.SetBool(Projectile.GThrow, false);
        Anime.SetLayerWeight(Anime.GetLayerIndex("Granade OverRide"), 0f);

        Projectile.Gun.SetActive(true);
        ThrowGranadeNow = false;

    }


    public void ThrowGranade()
    {

        ThrowGranadeNow = true;

    }


    public void One()
    {

        Attacks1.SetActive(true);
        SecondSource.clip = Punches[Random.Range(0, Punches.Length)];
        SecondSource.Play();
        


    }

    public void Two()
    {
        Attack2.SetActive(true);
        SecondSource.clip = Punches[Random.Range(0, Punches.Length)];
        SecondSource.Play();


    }



    public void DisableAllAttcks()
    {
        Attacks1.SetActive(false);

        Attack2.SetActive(false);



    }

    public void DisbalePlayerHurt()
    {
        StopRootMotion();


        Pm.IsPlayerHurt = false;
    }


    void Test()
    {
        Projectile.GranadeThrowPose = false;
        GunPosition.SetActive(true);
        Anime.SetLayerWeight(Anime.GetLayerIndex("Granade OverRide"), 0f);


    }
}
