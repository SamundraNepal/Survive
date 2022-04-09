using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoid : MonoBehaviour
{

    EnemyHealth Eh;





    public void Start()
    {
        Eh = GetComponentInParent<EnemyHealth>();


    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {


            if (Eh.Health > 0)
            {

                Vector3 OurPos = transform.position;
                Vector3 FriendPos = other.transform.position;


                Vector3 AvoidDistance = (FriendPos - OurPos).normalized;


                other.transform.position += AvoidDistance * Time.deltaTime;




            }

        }

    }




   
        
 }



