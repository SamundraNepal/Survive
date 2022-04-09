using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEveything : MonoBehaviour
{
    public BossMotor BossPosition;



    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Enemy")
        {


            EnemyHealth eh = other.gameObject.GetComponent<EnemyHealth>();

            if(eh!=null)
            {

                eh.Health = 0f;
            }






        }


        if (other.gameObject.tag == "Player")
        {


            PlayerHealth eh = other.gameObject.GetComponent<PlayerHealth>();

            if (eh != null)
            {

                eh.Health = 0f;
            }






        }




        if (other.gameObject.tag == "Boss")
        {


            other.transform.position = BossPosition.BossPosition;





        }





    }
}
