using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{


    public Transform player;


    public Vector3 Playerpos;

    public float MoveSpeed;


    public float DetectionRange;


    public Transform[] CheckPoints;
    public LayerMask Obs;
    public bool Avoid;



    private void Update()
    {

        Playerpos = player.position;

        if(Avoid)
        {
        calculatePath();

        }

        AvoidObsticals();
    }






    void AvoidObsticals()
    {

        Vector3 DistanceInBetween = (Playerpos - transform.position).normalized;
        DistanceInBetween.y = 0f;
     


        foreach (var O in CheckPoints)
        {
            RaycastHit hit;

           if(Physics.Raycast(O.transform.position , O.transform.forward,out hit ,DetectionRange , Obs))
            {

                Avoid = false;
                DistanceInBetween += hit.normal * 20f;
               

                Debug.DrawRay(O.transform.position, O.transform.forward * DetectionRange, Color.red);



            } else
            {

                Avoid = true;
                Debug.DrawRay(O.transform.position, O.transform.forward * DetectionRange, Color.green);

            }




        }

        Quaternion LookRot = Quaternion.LookRotation(DistanceInBetween);

        transform.rotation = Quaternion.Slerp(transform.rotation, LookRot, 5f * Time.deltaTime);

    }







    void calculatePath()
    {

        float Dis = Vector3.Distance(transform.position,Playerpos);

        Vector3 DistanceInBetween = (Playerpos - transform.position).normalized;



        Debug.DrawRay(transform.position + transform.up * 0.3f, DistanceInBetween * Dis, Color.red);


       DistanceInBetween = Vector3.forward * MoveSpeed * Time.deltaTime;



    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, DetectionRange);


    }



 
}
