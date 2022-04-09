using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPickUps : MonoBehaviour
{
    public bool Ishere;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "PickUps")
        {


            Ishere = true;
        }






    }



    private void OnTriggerExit(Collider other)
    {




            Ishere = false;
        


    }
}
