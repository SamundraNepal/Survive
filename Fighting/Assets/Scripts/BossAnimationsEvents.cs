using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationsEvents : MonoBehaviour
{

    [Header("Hit Ground")]
    public GameObject[] Shards;
    public GameObject[] Points;
    public GameObject HitGroundEffects;
    public AudioClip Hit_Ground;
    public AudioClip RocksFalling;
    BossMotor BM;
    public CameraShake CS;
    public float Range;
    GameObject Player;
    public AudioSource SourceOne;
    public AudioSource SourceTwo;
    public float DamageRate;
    public float Distnace;
    [Header("Throw Projectile")]
    public Transform ThrowPoint;
    public float ThrowSpeed;
    public AudioClip SwingSound;

    [Header("Beam Attack")]
    public GameObject Beam;
    public GameObject Minions;
    public AudioClip Snarling;

    [Header("Walking")]
    public GameObject WalkingEffects;
    public GameObject BossHitEffects;
    public Transform[] WalkingPoints;
    public AudioClip[] WalkingSounds;



    [Header("Chase Jump Effect")]
    public AudioClip BuildUpSound;
    public AudioSource LoopSound;
    public AudioClip Buildingup;
    Animator Anime;


    public GameObject[] Weakpoints;
  
    private void Start()
    {
        Anime = GetComponent<Animator>();
        BM = GetComponent<BossMotor>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Weakpoints = GameObject.FindGameObjectsWithTag("WeakPoints");


        DisableWeakPoints();
    }
    public void HitGround()
    {
        GameObject G = Instantiate(BossHitEffects, transform.position, Quaternion.identity);
        Destroy(G, 10f);

        SourceOne.clip = Hit_Ground;
        SourceOne.Play();

        SourceTwo.clip = RocksFalling;
        SourceTwo.Play();

        CS.CanShake = true;

         Distnace = Vector3.Distance(Player.transform.position, transform.position);

        if(Distnace < Range)
        {
            float DamageVar = DamageRate * 2 * (Range - Distnace) / Range;
          
           Player.GetComponent<PlayerHealth>().Health -= DamageVar;

        }

        foreach (var S in Shards)
        {
            foreach (var P in Points)
            {
                GameObject Rocks = Instantiate(S, P.transform.position, Quaternion.identity);

                Destroy(Rocks, 2f);
                GameObject Effetcs = Instantiate(HitGroundEffects, P.transform.position, Quaternion.identity);
                Destroy(Effetcs, 2f);

               
            }




        }



    }
    public void ThrowProjectile()
    {
      
      foreach (var Rocks in Shards)
        {

                  GameObject G = Instantiate(Rocks, ThrowPoint.transform.position, transform.rotation);

                G.GetComponent<Rigidbody>().velocity =  ((Player.transform.position - G.transform.position)  * ThrowSpeed * Time.deltaTime);
                Destroy(G, 5f);

        
        }



    }
    public void GroundStomp()
    {
        SourceOne.clip = Hit_Ground;
        SourceOne.Play();

        SourceTwo.clip = RocksFalling;
        SourceTwo.Play();

        GameObject G = Instantiate(BossHitEffects, transform.position, Quaternion.identity);
        Destroy(G, 10f);



        foreach (var S in Shards)
        {
            foreach (var P in Points)
            {
                GameObject Rocks = Instantiate(S, P.transform.position, Quaternion.identity);
                Destroy(Rocks, 2f);
                GameObject Effetcs = Instantiate(HitGroundEffects, P.transform.position, Quaternion.identity);
                Destroy(Effetcs, 2f);
                
            }




        }



    }
    private void OnDrawGizmos()
    {


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);

    }
    void SmallAttackOne()
    {
    BM.Rb.AddForce(transform.up * BM.Forces.y * Time.deltaTime, ForceMode.VelocityChange);
     
    }
   
    void DisableAllAnimations()
    {

        BM.RandSmall = true;
        BM.DisabledAllAnimations();
    }



    void Headbeam()
    {

        Beam.SetActive(true);


        SourceOne.clip = Snarling;
        SourceOne.Play();
        foreach (var E in Points)
        {
           
                GameObject G = Instantiate(Minions, E.transform.position, Quaternion.identity);


        }

    }


    void Swing()
    {

        SourceOne.clip = SwingSound;
        SourceOne.Play();





    }
    void DisbaleHeadBeam()
    {
        SourceOne.Stop();
        Beam.SetActive(false);


    }




    void BossWalking()
    {


        foreach (var W in WalkingPoints)
        {


            GameObject G = Instantiate(WalkingEffects, W.transform.position, Quaternion.identity);
            Destroy(G, 2f);
        }

    }



    void WalkingSound()
    {


        SourceTwo.clip = WalkingSounds[Random.Range(0, WalkingSounds.Length)];
        SourceTwo.Play();

    }

    


    void BuildingUpSounds()
    {


        SourceTwo.clip = BuildUpSound;
        SourceTwo.Play();
        StartCoroutine(Call());
    }



    IEnumerator Call()
    {


        yield return new WaitForSeconds(6f);
        LoopSound.clip = Buildingup;
        LoopSound.Play();

    }




    void StopLoopingSound()
    {

        if(LoopSound.isPlaying)
        {

            LoopSound.Stop();
        }


    }


    void Hurt()
    {
        foreach (var W in Weakpoints)
        {

            W.SetActive(true);
            


        }
          Anime.SetBool("Hurt", false);
           DisableAllAnimations();
            BM.CanFollow = false;


    }



    void DisableWeakPoints()
    {


        foreach (var W in Weakpoints)
        {

            W.SetActive(false);



        }
    }


    void RestartWalk()
    {


        BM.CanFollow = true;


    }


    void Death()
    {



        Anime.SetBool("Death", false);
    }
}
