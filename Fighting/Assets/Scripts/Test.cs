using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform Player;

    public Rigidbody Rb;

    public float VerticalTime;

    public float ToatalDistnaceSquare;

    public float Multiply;

    public float Dis;

    public float finalTime;

    public float ANgle;
    public float Total_Distnace;

    public float Gravity;

    private void Update()
    {


        LookAtPlayer();
        // Two Positions.



        Vector3 OurPosition = transform.position;
        Vector3 PlayerPosition = Player.transform.position;

        // Substracting Point A with Point B.
        Vector3 NewPositon = (PlayerPosition - OurPosition);
        // Saving the Height Value.
        float H = NewPositon.y;

     


        // Total Distance Between them.
     float  DistanceBewteenPlayerandENemy = NewPositon.magnitude;
         Total_Distnace = DistanceBewteenPlayerandENemy ;

        Debug.DrawLine(OurPosition, NewPositon * Total_Distnace, Color.red);

        //Finding the angle between Point A and Point B.
        ANgle = Vector3.Angle(transform.forward, NewPositon);

        // Converting angle to radian.
        float A = ANgle * Mathf.Deg2Rad;

        //Getting Initial Velocity in X direction. 
      float  HorizontalVelocity = Mathf.Cos(A);


        //Getting Initial Velocity in Y direction. 

      float  VerticalVelocity = Mathf.Sin(A);


        // Projectile Equaation in Y direction
         Gravity = -4.9f;
         ToatalDistnaceSquare = Mathf.Pow(Total_Distnace, 2);
        float cosine = Mathf.Pow(HorizontalVelocity, 2);
        float FinalOutput = ToatalDistnaceSquare / cosine;
         Multiply = Gravity * FinalOutput;
         Dis = Total_Distnace * Mathf.Tan(A);
         finalTime = Mathf.Sqrt(-Multiply) / Mathf.Sqrt(Dis);
         VerticalTime = finalTime;

        Debug.Log(VerticalTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rb.velocity = (transform.forward * VerticalTime + transform.up * VerticalTime);

            
        }

    }


    void LookAtPlayer()
    {


        Vector3 OurPosition = transform.position;
        Vector3 PlayerPosition = Player.transform.position;

        Vector3 TotalDis = PlayerPosition - OurPosition;
        TotalDis.y = 0f;
        Quaternion rot = Quaternion.LookRotation(TotalDis);

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 50f * Time.deltaTime);



    }




}
