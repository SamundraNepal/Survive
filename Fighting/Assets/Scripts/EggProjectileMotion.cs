using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggProjectileMotion : MonoBehaviour
{

    public float VerticalTime, HorizontalTime;

     GameObject Player;
     GameObject Boss;
     Rigidbody Rb;
    CrackEgg CE;


    private void Start()
    {
        CE = GetComponent<CrackEgg>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Boss = GameObject.FindGameObjectWithTag("Boss");
        Rb = GetComponent<Rigidbody>();

    }


    private void Update()
    {
        ProjectileMotionCalculation();

        if (CE.Hit2 == false)
        {


            if (Boss.GetComponent<PickUpAndThrow>().Isthrowing)
            {


                Rb.velocity = transform.forward * HorizontalTime + transform.up * VerticalTime;
                Boss.GetComponent<PickUpAndThrow>().Isthrowing = false;
            }

        }


    }

    public void ProjectileMotionCalculation()
    {






            Vector3 Playerpos = Player.transform.position;
            Vector3 OurPOs = transform.position;





            Vector3 totadis = Playerpos - OurPOs;

            // float angle = Mathf.Atan2(totadis.y, totadis.x);



            Quaternion Rot = Quaternion.LookRotation(totadis);
            transform.rotation = Quaternion.Slerp(transform.rotation, Rot, 50f * Time.deltaTime);

            float Ydir = OurPOs.y - Playerpos.y;


            totadis.y = 0f;




            float Dis = Mathf.Pow(totadis.magnitude, 2);
            float Cosine = Mathf.Pow(Mathf.Cos(totadis.x / totadis.magnitude), 2);
            //float C = Cosine  * mth

            float Divide = Dis / Cosine;

            float Multipy = -4.9f * Divide;

            float tan = Mathf.Tan(Ydir / totadis.x) * totadis.magnitude;


            float Minus = tan - Multipy;

            float SquareRoot = Mathf.Sqrt(Minus / Ydir);



            VerticalTime = SquareRoot;

            HorizontalTime = totadis.magnitude / SquareRoot * Mathf.Sin(totadis.y / totadis.magnitude);
        
    }


}
