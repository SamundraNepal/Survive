using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{

    public bool IsRealGrounded;
    public PlayerMovement Player;


    public float Test;
  

    

    private void Update()
    {

    








        RaycastHit Hit;
        if(Physics.Raycast(transform.position ,  -transform.up, out Hit , 1f))
        {

            Debug.DrawRay(transform.position, -transform.up * 1f, Color.green);

            if(Hit.transform.tag == "Ground")
            {



                IsRealGrounded = true;



            } 


        } else
        {

            Debug.DrawRay(transform.position, -transform.up * 1f, Color.white);
            IsRealGrounded = false;

        }









    }





}
