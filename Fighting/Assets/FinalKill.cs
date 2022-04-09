using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKill : MonoBehaviour
{

    public GameObject Bomb;

    public Transform Boss;

    public BossHealth Bh;

    public Transform ThrowPoint;

    public Transform Final;

    public float Dis;
    public bool Once;

    public float JumpSpeed , ForwardForce;
    public float RunSpeed;
    public Vector3 Velocity_;
    public Animator ANime;
    Rigidbody Rb;
    bool CanMOve;
    public GameObject SecondCamera;
    public bool SecondCamraTue;
    public GameObject FirstCamera;

   
    private void Start()
    {
        ANime = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        Once = true;
    }
    public void Update()
    {

        if (Bh.BHealth <= 0f)
        {

            RunTowardsTheBoss();
        }


    }



    void RunTowardsTheBoss()
    {
        if (CanMOve == false)
        {



            Vector3 Pos = transform.position;
            Vector3 BossPos = Boss.transform.position;



            Vector3 FinalDistnace = BossPos - Pos;




            Dis = FinalDistnace.magnitude;

            if (Once)
            {


                if (Dis > 15f)
                {
                    SecondCamera.transform.parent = null;
                    transform.position += FinalDistnace * RunSpeed * Time.deltaTime;
                    ANime.SetBool("Running", true);

                }
                else
                {
                    ANime.SetBool("Jump", true);
                    Rb.AddForce(transform.up * JumpSpeed * Time.deltaTime, ForceMode.Impulse);
                    Rb.velocity = Velocity_;

                    ANime.SetBool("Running", false);


                    Once = false;

                }
            }

            //    FinalDistnace.y = 0f;
            Quaternion rot = Quaternion.LookRotation(FinalDistnace);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }
        if(CanMOve)
        {


            Vector3 Pos = transform.position;
            Vector3 BossPos = Boss.transform.position;



            Vector3 FinalDistnace = BossPos - Pos;

            FinalDistnace.y = 0f;
            Quaternion rot = Quaternion.LookRotation(-FinalDistnace);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot , 10f * Time.deltaTime);



        }


       if(SecondCamraTue)
        {

            Vector3 Pos =Boss.position;
            Vector3 Cam = SecondCamera.transform.position;



            Vector3 FinalDistnace = Pos - Cam;
            FinalDistnace.y = 0f;
            Quaternion rot = Quaternion.LookRotation(FinalDistnace);

         SecondCamera.transform.rotation = Quaternion.Slerp(SecondCamera.transform.rotation, rot, 10f * Time.deltaTime);




            if(FinalDistnace.magnitude < 25f)
            {


                SecondCamera.transform.position -= FinalDistnace * Time.deltaTime;
            }
        }



    }




    void ThrowBomb()
    {

        for (int i = 0; i < 5; i++)
        {
        GameObject G = Instantiate(Bomb, ThrowPoint.position, Quaternion.identity);
            Destroy(G, 5f);
        }


     
      

    }



    void MoveForward()
    {



        CanMOve = true;
        ANime.SetBool("FinalToss", false);


    }



    void SecondCamera_()
    {

        FirstCamera.SetActive(false);
        SecondCamera.SetActive(true);
        SecondCamraTue = true;

    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Ground")
        {

            ANime.SetBool("FinalToss", false);
            ANime.SetBool("Jump", false);

            ANime.SetBool("HItGround", false);


        }

    }
}
