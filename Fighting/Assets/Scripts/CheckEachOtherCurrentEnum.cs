using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEachOtherCurrentEnum : MonoBehaviour
{



    public float WaitTime;

    public Transform NearEnemy;


   


    private void OnTriggerEnter(Collider other)
    {


       if(other.gameObject.tag == "Enemy")
        {
             

             NearEnemy = other.gameObject.transform;

            if (NearEnemy.gameObject.tag != this.gameObject.tag)
            {


                if (NearEnemy != null)
                {

                    if (NearEnemy.GetComponent<EnemyHealth>().Health > 0)
                    {

                        if (NearEnemy.GetComponent<EnemyMotor>().EnenmyState == EnemyMotor.Enemybrain.Attacking)
                        {


                            NearEnemy.GetComponent<EnemyMotor>().Wait = true;
                            StartCoroutine(FinishWaitTime());
                        }
                    }



                }

            }


        }

    }






    IEnumerator FinishWaitTime()
    {


        yield return new WaitForSeconds(WaitTime);
        if (NearEnemy != null)
        {
        NearEnemy.GetComponent<EnemyMotor>().Wait = false;
            NearEnemy = null;

        }

    }

}
