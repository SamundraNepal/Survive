using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBomb : MonoBehaviour
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
    public bool CanMOve;

    private void Awake()
    {
        SC = GetComponent<SphereCollider>();
        CanExplode = false;
        StartCoroutine(Explode());

    }





    private void Update()
    {


        G = GameObject.FindGameObjectWithTag("Point");

        transform.position = Vector3.MoveTowards(transform.position, G.transform.position, 10f * Time.deltaTime);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (CanExplode)
        {



            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBody"  || collision.gameObject.tag == "Boss")
            {



                GameObject G = Instantiate(BombBlastEffect, transform.position, Quaternion.identity);
                Destroy(G, 5f);

                GameObject AC = Instantiate(AudioClip, transform.position, Quaternion.identity);
                Destroy(AC, 2f);



                Activated = true;



                BossHealth BH = collision.gameObject.GetComponent<BossHealth>();

                if (BH != null)
                {

                    BH.BHealth -= 100f;
                    BH.Once = false;
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
