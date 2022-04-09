using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTurn : MonoBehaviour
{
    public string GrandeThrow;
    public bool Once = true;




    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            if (Once)
            {



                //     other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                other.gameObject.GetComponent<Animator>().SetBool(GrandeThrow, true);
                Once = false;

            }
        }

    }
}
