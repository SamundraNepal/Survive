using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject BombBlastEffect;
    public float ExplosionForce;
    public float Radius;
    public float UpFroce;


    public GameObject AudioClip;
    public bool CanExplode;
    public bool Activated;

    public float DamageRate;

    GameObject Camera_Shake;
    SphereCollider SC;

    GameObject G;
  

    private void Awake()
    {
        SC = GetComponent<SphereCollider>();
        Camera_Shake = GameObject.FindGameObjectWithTag("MainCamera");
        CanExplode = false;
        StartCoroutine(Explode());

    }





    private void FixedUpdate()
    {




        if (Activated)
        {



            Collider[] col = Physics.OverlapSphere(transform.position, Radius);


            foreach (Collider hit in col)
            {
                float Distnace = Vector3.Distance(hit.transform.position, transform.position);

                Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();

                EnemyHealth Eh = hit.gameObject.GetComponent<EnemyHealth>();

                PlayerHealth Ph = hit.gameObject.GetComponent<PlayerHealth>();


                if (rb != null)
                {
                    rb.AddExplosionForce(ExplosionForce, transform.position, Radius, UpFroce, ForceMode.Impulse);

                }


                if(Distnace <= Radius)
                {
                    float Damager = DamageRate * 2f *(Radius - Distnace) / Radius;

                    if (Eh != null)
                    {
                        Eh.Health -= Damager;
                    }
                    if (Ph != null)
                    {

                        Ph.Health -= Damager;
                    }

                  

                }


               
             


                Destroy(this.gameObject, 1f);
            }



        }





    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CanExplode)
        {



            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBody" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Boss")
            {

                Camera_Shake.GetComponent<CameraShake>().CanShake = true;


                GameObject G = Instantiate(BombBlastEffect, transform.position, Quaternion.identity);
                Destroy(G, 5f);

                GameObject AC = Instantiate(AudioClip, transform.position, Quaternion.identity);
                Destroy(AC, 2f);



                Activated = true;



                BossHealth BH = collision.gameObject.GetComponent<BossHealth>();

                if (BH != null)
                {


                    collision.gameObject.GetComponent<Animator>().SetBool("Hurt", true);
                    BH.BHealth -= 100f;
                }


            }
        }



    }


    IEnumerator Explode()
    {

        yield return new WaitForSeconds(0.5f);
        CanExplode = true;
        SC.isTrigger = false;


    }








}
