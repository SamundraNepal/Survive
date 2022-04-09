using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public LayerMask CollideLayer;
    public float CollideRadius;

    public Transform Cam;

    public Vector3 InitialVelocity;

    public float Angle;
    public float Max, Min;
    public bool IsProjectileReady;

    public float HorizonatlVelocity;
    public float VerticalVelocity;



    public Rigidbody Projectile;
    public Transform Point;



    public float VerticalTime, HorizontalTime;



    [Header("Player Line of Throw")]
    public LineRenderer Lr;
    public int NumberOfPoint;
    public float TimeBetweenPoints;
  


    [Header("ANimations")]
    public string GThrowPose;
    public string GThrow;
    Animator Anime;
    public GameObject Gun;



    [Header("Bools")]
    public bool GranadeThrowPose;
    PlayerAttack PA;
    AnimationEvent AE;
    PlayerStat Ps;


    public GameObject Sphere;
    public GameObject G;
     Vector3 NEwPoint;
    private void Start()
    {
        Ps = GetComponent<PlayerStat>();
        AE = GetComponent<AnimationEvent>();
        PA = GetComponent<PlayerAttack>();
        Anime = GetComponent<Animator>();

    }



    private void Update()
    {
        if(IsProjectileReady == false)
        {


          if(G!=null)
            {
                Destroy(G);
            }
        }

        if (PA.AttackMode == 2)
        {
            if (Ps.NumberOfGranade >= 10f)
            {




                if (Input.GetKey(KeyCode.Mouse1))
                {
                    Anime.SetFloat("Movement", 0f);
                    IsProjectileReady = true;

                    Anime.SetBool(GThrowPose, true);


                    Gun.SetActive(false);

                    GranadeThrowPose = true;


                    CalculateANgle();

                    Lr.enabled = true;

                    DrawArc();

                    Anime.SetLayerWeight(Anime.GetLayerIndex("Shooting OverRide"), 0f);
                    Anime.SetLayerWeight(Anime.GetLayerIndex("Granade OverRide"), 1f);



                }
                else
                {
                    IsProjectileReady = false;
                    Anime.SetBool(GThrowPose, false);

                    Lr.enabled = false;
                }

                Vector3 OurPosition = transform.position;
                Vector3 LookPosition = Cam.transform.position;

                Vector3 Ang = (OurPosition - LookPosition).normalized;
                Angle = Vector3.Angle(Ang, Cam.transform.forward);


                Angle = Mathf.Clamp(Angle, Min, Max);

                CalculateXandY();


                if (Input.GetKeyUp(KeyCode.Mouse1) && AE.ThrowGranadeNow)
                {
                    Anime.SetBool(GThrow, true);
                    Destroy(G);
                    Rigidbody R = Instantiate(Projectile, Point.transform.position, Quaternion.identity);
                    R.velocity = (-transform.forward * HorizontalTime + transform.up * VerticalTime);
                    Ps.NumberOfGranade -= 20f;
                    GranadeThrowPose = false;

                }
            }

        }
        else
        {
            Lr.enabled = false;
            Anime.SetLayerWeight(Anime.GetLayerIndex("Granade OverRide"), 0f);

            Anime.SetBool(GThrow, false);

            Gun.SetActive(true);
        }

    }



    void CalculateXandY()
    {

        HorizonatlVelocity = InitialVelocity.x * Mathf.Cos(Angle * Mathf.Deg2Rad);

        VerticalVelocity = InitialVelocity.y * Mathf.Sin(Angle * Mathf.Deg2Rad);



        VerticalTime = VerticalVelocity * VerticalVelocity / (2 * 9.8f);
        HorizontalTime = 2 * HorizonatlVelocity * VerticalVelocity / -9.8f;


    }




    void CalculateANgle()
    {


        Vector3 OurPosition = transform.position;
        Vector3 LookPosition = Camera.main.transform.position;


        Vector3 FinalPosition = LookPosition - OurPosition;



        FinalPosition.y = 0f;


        Quaternion Look = Quaternion.LookRotation(-FinalPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, Look, 10f * Time.deltaTime);







    }


    void DrawArc()
    {


        Lr.positionCount = NumberOfPoint;
        List<Vector3> Points = new List<Vector3>();
        Vector3 StartingPosition = Point.transform.position;
        Vector3 StartingVelocity = (-transform.forward * HorizontalTime + transform.up * VerticalTime);


        for (float i = 0; i < NumberOfPoint; i += TimeBetweenPoints)
        {

            NEwPoint = StartingPosition + i * StartingVelocity;
            NEwPoint.y = StartingPosition.y + StartingVelocity.y * i + Physics.gravity.y / 2f * i * i;
            Points.Add(NEwPoint);


            if (Physics.OverlapSphere(NEwPoint, CollideRadius, CollideLayer).Length > 0f)
            {
               
                if(G== null)
                {

                    G = Instantiate(Sphere, NEwPoint, Quaternion.identity);

                }

                if(G!=null)
                {

                    G.transform.position = NEwPoint;

                }

          

                    Lr.positionCount = Points.Count;
                    break;
                








            }




        }
            Lr.SetPositions(Points.ToArray());

    }

  
}
