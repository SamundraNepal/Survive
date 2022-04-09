using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public float DamageRate;




    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag =="Enemy")
        {
            EnemyHealth EH = other.transform.GetComponent<EnemyHealth>();
            if(EH!=null)
            {
                other.transform.GetComponent<Animator>().SetBool("Hurt", true);
                EH.Health -= DamageRate; 
            }

        }


    }
}
