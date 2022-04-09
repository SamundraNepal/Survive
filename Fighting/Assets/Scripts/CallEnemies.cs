using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemies : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Enemy;
    public Rigidbody[] RB;
    public BoxCollider[] BC;
    public bool Once;

    public Transform[] Points;

    public Transform Player;


     CrackEgg CG;




    [Header("Levels")]
    public int level1;
    public int Level2;
    public int Level3;

    private void Start()
    {

        Boss = GameObject.FindGameObjectWithTag("Boss");
        CG = GetComponentInParent<CrackEgg>();
        Once = true;
        RB = GetComponentsInChildren<Rigidbody>();
        BC = GetComponentsInChildren<BoxCollider>();



        foreach (var R in RB)
        {



            R.isKinematic = true;


        }


        foreach (var B in BC)
        {



            B.enabled = false;


        }




    }

    



    private void Update()
    {
        if(CG.hit == true)
        {

            if (Once)
            {
                GameObject E = Instantiate(Enemy, transform.position + transform.forward * 1f, transform.rotation);


                if(Boss.GetComponent<DiffucltyLevel>().Level1)
                {
                    for (int i = 0; i < level1; i++)
                    {

                        GameObject D = Instantiate(Enemy, transform.position + transform.forward * 1f, transform.rotation);



                    }


                }


                if (Boss.GetComponent<DiffucltyLevel>().Level2)
                {
                    for (int i = 0; i < Level2; i++)
                    {

                        GameObject D = Instantiate(Enemy, transform.position + transform.forward * 1f, transform.rotation);



                    }


                }




                if (Boss.GetComponent<DiffucltyLevel>().Level3)
                {
                    for (int i = 0; i < Level3; i++)
                    {

                        GameObject D = Instantiate(Enemy, transform.position + transform.forward * 1f, transform.rotation);



                    }


                }


                Once = false;

            }


            foreach (var R in RB)
            {



                R.isKinematic = false;


            }


            foreach (var B in BC)
            {


                B.enabled = true;


            }

            Destroy(this.gameObject, 5f);


            CG.hit = false;
        }




       /* foreach (var P in Points)
        {

            RaycastHit hit;

            if (Physics.Raycast(P.transform.position, P.transform.forward, out hit, 2f))
            {

                if (hit.transform.tag == "Ground" || hit.transform.tag == "Environments")
                {


                    


                }


            }
        }*/





    }
   
}
