using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagDoll : MonoBehaviour
{



    public Rigidbody[] Rb;
    public Collider[] Col;

    PlayerHealth Ph;
    Animator Anime;


   





    private void Start()
    {

        Anime = GetComponentInParent<Animator>();

        Ph = GetComponentInParent<PlayerHealth>();

        Col = GetComponentsInChildren<Collider>();
        Rb = GetComponentsInChildren<Rigidbody>();
        foreach (var R in Rb)
        {

            R.isKinematic = true;
            R.mass = 1f;

        }

        foreach (var C in Col)
        {



            C.enabled = false;

        }
    }




    private void FixedUpdate()
    {





        if (Ph.Health <= 0)
        {

            Anime.enabled = false;

            foreach (var R in Rb)
            {

                R.isKinematic = false;
               
            }

            foreach (var C in Col)
            {

                C.enabled = true;

            }

        }



    }




}


