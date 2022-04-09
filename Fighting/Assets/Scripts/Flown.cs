using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flown : MonoBehaviour
{

    public string HurtAnimations;

    public bool Once;



    private void Start()
    {

        Once = true;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (Once)
            {



                PlayerHealth Ph = collision.transform.GetComponent<PlayerHealth>();

                if (Ph != null)
                {
                    Ph.Health -= 10f;
                    collision.transform.GetComponent<Animator>().SetBool(HurtAnimations, true);

                }

                Once = false;
            }
        }


    }
   

}
