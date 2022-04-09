using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTest : MonoBehaviour
{

    Rigidbody RB;

    public Transform Player;

    public float TimeInY;

    public float HorizontalTime;

    private float Ydir;
     Vector3 totadis;

    float angle;
    private void Start()
    {



        RB = GetComponent<Rigidbody>();
    }



    private void Update()
    {


        Testing();


        if(Input.GetKeyDown(KeyCode.E))
        {


            RB.velocity = transform.forward * HorizontalTime + transform.up * TimeInY;

        }
    }


    void Testing()
    {


        Vector3 Playerpos = Player.transform.position;
        Vector3 OurPOs = transform.position;





        Vector3 totadis = Playerpos - OurPOs;

        // float angle = Mathf.Atan2(totadis.y, totadis.x);



        Quaternion Rot = Quaternion.LookRotation(totadis);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rot, 50f * Time.deltaTime);

        float Ydir = OurPOs.y - Playerpos.y;


        totadis.y = 0f;




        Vector3 Dis = new Vector3(totadis.x * totadis.x, totadis.y * totadis.y, totadis.z * totadis.z);
        float Cosine = Mathf.Pow(Mathf.Cos(totadis.x / totadis.magnitude), 2);
        //float C = Cosine  * mth

        Vector3 Divide = Dis / Cosine;

        Vector3 Multipy = -4.9f * Divide;

        float tan = Mathf.Tan(Ydir / totadis.x) * totadis.magnitude;


        float Minus = Multipy.magnitude - tan;

        float SquareRoot = Mathf.Sqrt(Minus / Ydir);

        TimeInY = SquareRoot;

        HorizontalTime = totadis.magnitude / SquareRoot * Mathf.Sin(totadis.y / totadis.magnitude);





    }




}
