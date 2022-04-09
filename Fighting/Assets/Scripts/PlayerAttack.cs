using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{





    public int AttackMode;


    public GameObject[] Enemies;
    private float Range;
    public GameObject BestTarget;

    [Header("Normal Atacks")]
    public string Attack1;
    public string Attack2;
    public string Attack3;
    public string JumpAttack;



    public Animator Anime;
    public int ATtackNumber;
    AnimationEvent AE;
    PlayerMovement Pm;
    PlayerStat Ps;



    [Header("Gun")]
    public float BulletLenght;
    public LayerMask IgnoreLayers , HeadMask , BossWeakPoints;
    public GameObject PartfileEffects;
    //  public GameObject HitEffects;
    public AudioSource Source;
    public AudioClip Shoot;
    ProjectileMotion Porjectile;
    PlayerHealth Ph;
    public float DamageRate;
    public float BossDamageRate;
    public GameObject DamagePopUp;
    bool Once;


    [Header("SecondaryATtacks")]

    public bool SecondaryHandAttack;





    private void Start()
    {
        Once = true;

        Ps = GetComponent<PlayerStat>();
        Ph = GetComponent<PlayerHealth>();
        Porjectile = GetComponent<ProjectileMotion>();

        Pm = GetComponent<PlayerMovement>();


        AE = GetComponent<AnimationEvent>();

    }


    private void Update()
    {
        if (Ph.Health > 0 && !Pm.IsPlayerHurt)
        {


            ChangeAttack();

            if (AttackMode == 1)
            {

                Porjectile.IsProjectileReady = false;

                Pm.IsShooting = false;

                SecondaryHandAttack = true;

                if (Input.GetKeyDown(KeyCode.Mouse1) && Ps.NumberOfSpecailAttacks >= 50f)
                {

                   
                    SecondaryHandAttack = true;
                    Anime.SetBool(JumpAttack, true);
                    Ps.NumberOfSpecailAttacks -=50f;

                }
                else
                {

                    SecondaryHandAttack = false;
                }

                Anime.SetBool("Gun Holding", false);


                if (AE.IsAttacking == false)
                {


                    if (Input.GetKeyDown(KeyCode.Mouse0) && !SecondaryHandAttack && Pm.IsGrounded)
                    {


                        SnapeRotation(Enemies);



                        if (ATtackNumber == 0)
                        {
                            ATtackNumber = 1;
                        }
                        else if (ATtackNumber == 1)
                        {
                            ATtackNumber = 2;

                        }
                        else if (ATtackNumber == 2)
                        {


                            ATtackNumber = 3;
                        }
                        else if (ATtackNumber == 3)
                        {

                            ATtackNumber = 1;
                        }

                        attacks();



                    }
                    else


                    {

                        Anime.SetBool(Attack1, false);
                        Anime.SetBool(Attack2, false);
                        Anime.SetBool(Attack3, false);
                    }

                }
            }


            if (AttackMode == 2)
            {

                Pm.IsShooting = true;

                SecondaryHandAttack = false;




                Anime.SetBool("Gun Holding", true);

                Anime.SetBool(Attack1, false);
                Anime.SetBool(Attack2, false);
                Anime.SetBool(Attack3, false);


                if (Ph.GunAmmuniations > 0f)
                {

                    
                    if (Input.GetKey(KeyCode.Mouse0) && !Porjectile.GranadeThrowPose)
                    {



                            Ph.GunAmmuniations -= Ph.GunDecreaseRate * Time.deltaTime;
                            if (!Source.isPlaying)
                            {
                                Source.clip = Shoot;
                                Source.Play();
                            }

                            Anime.SetFloat("Movement", 0f);

                            Pm.IsShooting = true;

                            Anime.SetLayerWeight(Anime.GetLayerIndex("Shooting OverRide"), 1f);

                            Vector3 v = transform.position - Camera.main.transform.position;

                            v.y = 0f;

                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(v), 10f * Time.deltaTime);



                            BulletSHoot();
                        WeakBoss();
                        HeadAttck();



                    }
                    else
                    {

                        if (Source.isPlaying)
                        {
                            Source.Stop();
                        }
                        Anime.SetLayerWeight(Anime.GetLayerIndex("Shooting OverRide"), 0f);

                        Pm.IsShooting = false;
                    }
                }
                else
                {
                    Anime.SetLayerWeight(Anime.GetLayerIndex("Shooting OverRide"), 0f);
                    Pm.IsShooting = false;

                }

            }
        }


      
    }


  

    void attacks()
    {

       

            AE.IsAttacking = true;

            Anime.applyRootMotion = true;




            if (ATtackNumber == 1)
            {



                Anime.SetBool(Attack1, true);

            }
            else if (ATtackNumber == 2)
            {


                Anime.SetBool(Attack2, true);



            }
            else if (ATtackNumber == 3)
            {

                Anime.SetBool(Attack3, true);

            }

        



    }




   public GameObject SnapeRotation(GameObject[] Enemies)
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");


        Range = Mathf.Infinity;

        foreach (var Ptarget in Enemies)
        {

            Vector3 LookAtRot = Ptarget.transform.position - transform.position;
            LookAtRot.y = 0f;
            float dis = LookAtRot.sqrMagnitude;


            if(dis < Range)
            {
               


                Range = dis;
                BestTarget = Ptarget;


                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAtRot), 100f * Time.deltaTime);
                
            }
        }

        return BestTarget;

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Range);


    }

     void BulletSHoot()
    {



        RaycastHit hit;


        

        if(Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * 0.5f , Camera.main.transform.forward , out hit , BulletLenght, ~IgnoreLayers))
        {


            EnemyHealth Eh = hit.transform.GetComponent<EnemyHealth>();
            BossHealth Bh = hit.transform.GetComponent<BossHealth>();

            
                  if(Eh!= null)
                   {

                    float Value = Eh.Health - DamageRate;
                    Value = Mathf.Clamp(Value,1f, 99f);

           

                  if(Once)
                  {

                    GameObject G = Instantiate(PartfileEffects, hit.point, transform.rotation);
                    Destroy(G, 1f);
                    GameObject D = Instantiate(DamagePopUp, hit.point, Quaternion.identity);
                           D.GetComponent<DamagePopUp>().DamePop(((int)Value));
                          Destroy(D, 1f);
                          StartCoroutine(RestartDropPopUp());
                          Once = false;

                  }

                hit.transform.GetComponent<EnemyHealth>().Health -= DamageRate * Time.deltaTime;

         

              }




      

                  if(Bh!=null)
                   {

                   float BossValue = Bh.BHealth - DamageRate;
                    BossValue = Mathf.Clamp(BossValue, 0f, Bh.MaxHealth);


                    if(Once)
                    {

                    GameObject G = Instantiate(PartfileEffects, hit.point, transform.rotation);
                    Destroy(G, 1f);

                    GameObject D = Instantiate(DamagePopUp, hit.point, Quaternion.identity);
                    D.GetComponent<DamagePopUp>().DamePop(((int)BossValue));
                    Destroy(D, 1f);
                    StartCoroutine(RestartDropPopUp());
                    Once = false;

                   }
                hit.transform.GetComponent<BossHealth>().BHealth -= BossDamageRate * Time.deltaTime;


            }

        }
        


    }


    IEnumerator RestartDropPopUp()
    {


        yield return new WaitForSeconds(0.3f);
        Once = true;
    }
    void ChangeAttack()
    {




        AttackMode = Mathf.Clamp(AttackMode, 1, 2);



        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            AttackMode += 1;

        } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            AttackMode -= 1;



        }



    }



    public void HeadAttck()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * 0.5f, Camera.main.transform.forward, out hit, BulletLenght, HeadMask))
        {


           EnemyHealth H = hit.transform.GetComponentInParent<EnemyHealth>();

            if(H!=null)
            {

                H.Health = 0f;

            }
           

        }


    }



    public void WeakBoss()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * 0.5f, Camera.main.transform.forward, out hit, BulletLenght, BossWeakPoints))
        {


            BossHealth H = hit.transform.GetComponentInParent<BossHealth>();

            if (H != null)
            {

                H.BHealth -= 60 * Time.deltaTime;

            }


        }


    }


}
