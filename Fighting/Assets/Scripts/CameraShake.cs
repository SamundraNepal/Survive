using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{


    public float MaxAngle = 10f;
    public bool CanShake;
    public Properties P;
    public CameraFollow CF;
    IEnumerator CurrentShake;


    private void Update()
    {

   
        if(CanShake &&CF.CanFOllow)
        {
            StartShake(P);
            CanShake = false;

        }
    }


    public void StartShake(Properties P_P)
    {

        if (CurrentShake != null)
        {

            StopCoroutine(CurrentShake);
        }

        CurrentShake = Shake(P_P);
        StartCoroutine(CurrentShake);  


    }





    IEnumerator Shake(Properties PP)
    {


        float CompletionPercent = 0;
        float MovePercent = 0;


        float Angle_Radians = PP.Angle * Mathf.Deg2Rad - Mathf.PI ;
        Vector3 PreviousWayPoints = Vector3.zero;
        Vector3 CurrentWaypoint = Vector3.zero;
        float MoveDistance = 0;


        Quaternion TargetRotaion = Quaternion.identity;
        Quaternion PreviousRotation = Quaternion.identity;





        do
        {
            if (MovePercent >= 1 || CompletionPercent == 0)
            {

                float DamingFacor = DampingCurve(CompletionPercent, PP.DamingPercent);
                float NoiseAngle = (Random.value - .5f) * Mathf.PI;

                Angle_Radians += Mathf.PI + NoiseAngle * PP.NoisePercent;

                CurrentWaypoint = new Vector3(Mathf.Cos(Angle_Radians), Mathf.Sin(Angle_Radians)) * PP.Strength * DamingFacor;

                PreviousWayPoints = transform.localPosition;


                MoveDistance = Vector3.Distance(CurrentWaypoint, PreviousWayPoints);

                TargetRotaion = Quaternion.Euler(new Vector3(CurrentWaypoint.y, CurrentWaypoint.x).normalized * PP.RotationPercent * DamingFacor * MaxAngle);
                PreviousRotation = transform.localRotation;

                MovePercent = 0;
            }
            CompletionPercent += Time.deltaTime / PP.Duration;
            MovePercent += Time.deltaTime / MoveDistance * PP.Speed;
            transform.localPosition = Vector3.Lerp(PreviousWayPoints, CurrentWaypoint, MovePercent);
            transform.localRotation = Quaternion.Slerp(PreviousRotation, TargetRotaion, MovePercent);


            yield return null;

        }
        while (MovePercent > 0);


    }


    float DampingCurve(float X , float Damping_Percent)
    {



        X = Mathf.Clamp01(X);
        float a = Mathf.Lerp(2, .25f, Damping_Percent);
        float b = 1 - Mathf.Pow(X, a);
        return b * b * b;

    }

    [System.Serializable]
    public class Properties
    {



        public float Angle;
        public float Strength;
        public float Speed;
        public float Duration;

        [Range(0f, 1f)]
        public float NoisePercent;
        [Range(0f, 1f)]
        public float DamingPercent;
        [Range(0f, 1f)]
        public float RotationPercent;

    }


}
