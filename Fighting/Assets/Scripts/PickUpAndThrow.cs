using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndThrow : MonoBehaviour
{
    public int NumberOfObjects;

    public GameObject ObjectThrow;
    public Transform PickUpPos;


    public float Timer;
    public float TimeSpeed;

    public Vector3 PlayerCurrentPos;
    public Transform Player;
    public Transform ThrowPoint;
    public GameObject G;
    Animator Anime;



    Vector3 CurrentPos;



    [Header("Projectile Motion")]
    //public float AngleBetweenPlayerAndEnemy;
   // public float HorizontalVelocity;
   // public float VerticalVelocity;

    public float VerticalTime, HorizontalTime;

 //   public float DistanceBewteenPlayerandENemy;


    public float Gravity;

    DiffucltyLevel DL;
    BossMotor Bm;


    [Header("Level 1")]
    public float MaxThrowingTime;
    public float MaxTime;
    public GameObject RoundOne;


    [Header("Level 2")]
    public float MaxThrowingTimeLevel2;
    public float MaxTimeLevel2;
    public GameObject RoundTwo;


    [Header("Level 3")]

    public float MaxThrowingTimeLevel3;
    public float MaxTimeLelve3;
    public bool BossJump;
    float HorT;
    public GameObject RoundThree;
    bool KeepInCheck;
    public bool Isthrowing;

    private void Start()
    {
        BossJump = false;
        Bm = GetComponent<BossMotor>();
        DL = GetComponent<DiffucltyLevel>();

        Gravity = Physics.gravity.y;

        CurrentPos = transform.position;
        Anime = GetComponent<Animator>();
        
    }

    private void Update()
    {
      
      if(!Bm.IsBossActive)
        {

        if (Player.GetComponent<PlayerHealth>().Health > 0)
        {
            PlayerCurrentPos = Player.transform.position;
                ProjectileMotionCalculation();


            if (DL.Intro)
           {

                    Timer += TimeSpeed * Time.deltaTime;

                    if (Timer >= 5)
                    {

                        Anime.SetBool("Throw", true);
                        Timer = 0f;

                    }
            }

            if (DL.Level1)
                {

                    MaxThrowingTime += 1 * Time.deltaTime;

                    if (MaxThrowingTime < MaxTime)
                    {


                        if (MaxThrowingTime < 5)
                        {


                            RoundOne.SetActive(true);


                        }
                        else
                        {

                            RoundOne.SetActive(false);

                        }

                        if (MaxThrowingTime < MaxTime)
                        {
                            Timer += TimeSpeed * Time.deltaTime;

                            if (Timer >= 5)
                            {

                                Anime.SetBool("Throw", true);
                                Timer = 0f;

                            }


                        }
                    }

                    if(MaxThrowingTime > MaxTime)
                    {


                        DL.Level2 = true;
                        DL.Level1 = false;
                    }

                }
            
            if(DL.Level2)
                {
                    MaxThrowingTimeLevel2 += 1 * Time.deltaTime;

                    if (MaxThrowingTimeLevel2 < MaxTimeLevel2)
                    {


                        if (MaxThrowingTimeLevel2 < 5)
                        {


                            RoundTwo.SetActive(true);


                        }
                        else
                        {

                            RoundTwo.SetActive(false);

                        }

                        if (MaxThrowingTimeLevel2 < MaxTimeLevel2)
                        {
                            Timer += TimeSpeed * Time.deltaTime;

                            if (Timer >= 5)
                            {

                                Anime.SetBool("Throw", true);
                                Timer = 0f;

                            }


                        }
                    }

                    if (MaxThrowingTimeLevel2 > MaxTimeLevel2)
                    {


                        DL.Level3 = true;
                        DL.Level2 = false;
                    }



                }

             if(DL.Level3)
                {


                    MaxThrowingTimeLevel3 += 1 * Time.deltaTime;

                    if (MaxThrowingTimeLevel3 < MaxTimeLelve3)
                    {


                        if (MaxThrowingTimeLevel3 < 5)
                        {


                            RoundThree.SetActive(true);


                        }
                        else
                        {

                            RoundThree.SetActive(false);

                        }

                        if (MaxThrowingTimeLevel3 < MaxTimeLelve3)
                        {
                            Timer += TimeSpeed * Time.deltaTime;

                            if (Timer >= 5)
                            {

                                Anime.SetBool("Throw", true);
                                Timer = 0f;

                            }


                        }
                    }

                    if (MaxThrowingTimeLevel3 > MaxTimeLelve3)
                    {


                        DL.Level4 = true;
                        DL.Level3 = false;
                    }



                }

             if(DL.Level4)
                {
                    Anime.SetBool("Throw", false);
                    Bm.IsBossActive = true;
                    

                }

        }    

      } else
      {
            ProjectileMotionCalculation();

            if (BossJump == false)
            {


                transform.GetComponent<Rigidbody>().velocity = (ThrowPoint.forward * HorizontalTime + ThrowPoint.up * VerticalTime);
                BossJump = true;


            }

      }

    }





    void PickingUp()
    {

            G = Instantiate(ObjectThrow, PickUpPos.transform.position, transform.rotation);
          

    }




    void Throwing()
    {
    //    Isthrowing = true;
        G.GetComponent<TrailRenderer>().enabled = true;
        G.GetComponent<Rigidbody>().isKinematic = false;
        G.GetComponent<Rigidbody>().useGravity = true;
        Anime.SetBool("Throw", false);
         G.GetComponent<Rigidbody>().velocity = ThrowPoint.forward * HorizontalTime + ThrowPoint.up * VerticalTime;

    }




  



    void BackToOriginalPos()
    {


        transform.position = CurrentPos;


    }

    public void ProjectileMotionCalculation()
    {






        Vector3 Playerpos = Player.transform.position;
        Vector3 OurPOs = ThrowPoint.position;





        Vector3 totadis = Playerpos - OurPOs;



        Quaternion Rot = Quaternion.LookRotation(totadis);
        ThrowPoint.rotation = Quaternion.Slerp(ThrowPoint.rotation, Rot, 50f * Time.deltaTime);

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



        VerticalTime = SquareRoot;

        HorizontalTime = totadis.magnitude / SquareRoot * Mathf.Sin(totadis.y / totadis.magnitude);

    }


    /* void ProjectileMOtion()
     {




             // Two Positions.
             Vector3 OurPosition = transform.position;
             Vector3 PlayerPosition = Player.transform.position;

             // Substracting Point A with Point B.
             Vector3 NewPositon = PlayerPosition - OurPosition;
             // Saving the Height Value.
             float H = NewPositon.y;



             // Total Distance Between them.
             DistanceBewteenPlayerandENemy = NewPositon.magnitude;
             float TotalDistnace = DistanceBewteenPlayerandENemy;


             //Finding the angle between Point A and Point B.
             AngleBetweenPlayerAndEnemy = Vector3.Angle(transform.TransformDirection(Vector3.forward), NewPositon);

             // Converting angle to radian.
             float A = AngleBetweenPlayerAndEnemy * Mathf.Deg2Rad;

             //Getting Initial Velocity in X direction. 
             HorizontalVelocity = Mathf.Cos(NewPositon.x / NewPositon.magnitude);


             //Getting Initial Velocity in Y direction. 

             VerticalVelocity = Mathf.Sin(NewPositon.y / NewPositon.magnitude);


             // Projectile Equaation in Y direction
             HorT = TotalDistnace / HorizontalVelocity;
             float Ver = H + VerticalVelocity - 4.9f;
             float Time = Mathf.Pow(HorT, 2);
             // Total time in Air
             VerticalTime = Mathf.Sqrt(-Time / Ver);

             // Projectile Equaation in X direction

             float Hor = TotalDistnace + HorizontalVelocity * HorT;
             float HortTime = Mathf.Pow(HorT, 2);
             float T = Mathf.Sqrt(HortTime / Hor);

             // Total Time in Air.
             float T1 = VerticalTime;
             float T2 = T1 - T;
             HorizontalTime = T + T2;


     }*/


    /*void LookAtPlayer()
    {


        Vector3 OurPosition = transform.position;
        Vector3 PlayerPosition = Player.transform.position;

        Vector3 TotalDis = PlayerPosition - OurPosition;
        TotalDis.y = 0f;
        Quaternion rot = Quaternion.LookRotation(TotalDis);

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 50f * Time.deltaTime);



    }*/




    /*  void DrawArc()
      {
          Lr.positionCount = NumberOfPoint;
          List<Vector3> Points = new List<Vector3>();

          Vector3 StartingPosition = ThrowPoint.transform.position;
          Vector3 StartingVelocity = (transform.forward * HorizontalTime + transform.up * VerticalTime);


          for (float i = 0; i < NumberOfPoint; i += TimeBetweenPoints)
          {

              Vector3 NEwPoint = StartingPosition + i * StartingVelocity;
              NEwPoint.y = StartingPosition.y + StartingVelocity.y * i + Physics.gravity.y / 2f * i * i;
              Points.Add(NEwPoint);

              Lr.SetPositions(Points.ToArray());





          }





      }*/

}
