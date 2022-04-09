using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushForce : MonoBehaviour
{


    public float PushedForce;
    Animator anime;
    Rigidbody Rb;
    EnemyMotor EM;

    private void Start()
    {
        EM = GetComponent<EnemyMotor>();
        anime = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();

    }


  

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "HighKick")
        {

            EM.EnenmyState = EnemyMotor.Enemybrain.PushedBack;
            transform.tag = "PushedBackedEnemy";
            anime.applyRootMotion = false;
            Rb.velocity =  (-transform.forward * PushedForce * Time.deltaTime);
            anime.SetBool("PushedBack", true);
            StartCoroutine(PushedbackFalse());
        }


        if(other.gameObject.tag == "HighJumpAttack")
        {
            EM.CanRotate = false;

            anime.SetBool("HitGround", true);
            Rb.AddForce(transform.up * 500f * Time.deltaTime);
            StartCoroutine(HitgroundFalse());
        }



    }


    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "PushedBackedEnemy")
        {


            anime.applyRootMotion = false;
            Rb.velocity = (-transform.forward * PushedForce * Time.deltaTime);
            anime.SetBool("PushedBack", true);
            StartCoroutine(PushedbackFalse());

        }

    }



    IEnumerator PushedbackFalse()
    {


        yield return new WaitForSeconds(2f);
        transform.tag = "Enemy";

        anime.SetBool("PushedBack", false);
        anime.applyRootMotion = true;


    }


    IEnumerator HitgroundFalse ()
    {


        yield return new WaitForSeconds(5f);
        anime.SetBool("HitGround", false);
    }




    public void EnemyCanRotate()
    {
        EM.CanRotate = true;



    }

}
