using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float DamageRate;


    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.tag == "Player")
        {


            PlayerHealth Ph = other.GetComponent<PlayerHealth>();

            if(Ph!=null)
            {
                other.transform.GetComponent<Animator>().SetBool("Hurt", true);
                other.transform.GetComponent<PlayerMovement>().IsPlayerHurt = true;

                Ph.Health -= DamageRate;

            }

            
        }

    }


}
