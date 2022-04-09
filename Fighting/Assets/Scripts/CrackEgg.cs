using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackEgg : MonoBehaviour
{

    public GameObject HitEffects;
    public GameObject Clip;
    TrailRenderer Tr;
    GameObject Boss; 
    public bool hit;
    public bool Hit2;
 
    private void Start()
    {

        Boss = GameObject.FindGameObjectWithTag("Boss");
        Tr = GetComponent<TrailRenderer>();
        hit = false;
        Hit2 = false;
    }



   


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Ground" || collision.transform.tag == "Environments")
        {

            if(Boss.GetComponent<DiffucltyLevel>().Intro == true)
            {
            Boss.GetComponent<DiffucltyLevel>().Intro = false;

            }


            PlayerHealth ph = collision.transform.GetComponent<PlayerHealth>();


            if(ph!=null)
            {


                ph.Health -= 10f;

            }
            Tr.enabled = false;
            Hit2 = true;
            hit = true;
            GameObject C = Instantiate(Clip, transform.position, Quaternion.identity);
            Destroy(C, 3f);
            GameObject G = Instantiate(HitEffects, transform.position, transform.rotation);
            Destroy(G, 2f);
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;

            Destroy(this.gameObject, 5f);
        }

    }
}
