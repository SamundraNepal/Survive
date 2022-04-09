using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    public Transform Cam;

    public Vector3 InitialSpeed;

    public Rigidbody Projectile;

    public Transform ThrowPoint;

    public Vector3 LandingPositio;


    public int InitialSpeedX, InitialSpeedY;


    public float Horizontal;
    public float Vertical;

    public float D;

    public float Angle;
  

    Rigidbody G;


    public float AngleSpeedX;
    public float AngleSpeedY;

    public float TimeX;
    public float TimeY;


    public float AverageVelocity;
    public float HorizontalDistnace;

    public float height;



    public LineRenderer Lr;




    public float VerticalY;
    public float HorizontaxX;
     

    private void Start()
    {

        Lr.positionCount = 3;

    }

    private void Update()
    {




        CalculateEquations();

     




        if (Input.GetKeyUp(KeyCode.Mouse1))
        {



            G = Instantiate(Projectile, ThrowPoint.position, transform.rotation);
      

            G.velocity = (  new Vector3(Horizontal, Vertical , 0f));

          

        }

        
        

     









        if (Input.GetKey(KeyCode.Mouse1))
        {

            RotateTowardsTheThrowPOint();

           

        }


    

    }
   

    void CalculateEquations()
    {
        Horizontal = InitialSpeed.x * Time.fixedDeltaTime + 0.5f * -9.8f * AngleSpeedX * AngleSpeedX;

        Vertical = InitialSpeed.y * Time.fixedDeltaTime + 0.5f * -9.8f  * AngleSpeedY;


        LandingPositio = new Vector3(Horizontal, Vertical, 0f);









        Lr.SetPosition(0, transform.position);


        Lr.SetPosition(1, new Vector3(0f, LandingPositio.y, 0f));


        Lr.SetPosition(2, new Vector3(LandingPositio.x, 0f, 0f));


      //  ANgle;
          Vector3 OurPosition = transform.position;
           Vector3 LookPosition = Camera.main.transform.position;


           Vector3 Direction = (LookPosition - OurPosition).normalized;

           Angle = Vector3.Angle(-Direction,transform.forward);


           AngleSpeedX = InitialSpeedX * Mathf.Cos(Angle * Mathf.Deg2Rad);

           AngleSpeedY = InitialSpeedY * Mathf.Sin(Angle * Mathf.Deg2Rad);


           TimeX = 0 - AngleSpeedX / -9.8f;
           TimeY = 0 - AngleSpeedY / -9.8f;



           AverageVelocity = 1 / 2 * AngleSpeedY;


            height = AverageVelocity * TimeY;

           float TotalTrip = 2 * AngleSpeedY;

           HorizontalDistnace = TotalTrip * AngleSpeedX;









    }

    void RotateTowardsTheThrowPOint()
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


       




    }
}
