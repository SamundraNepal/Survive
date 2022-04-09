using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTraget, Target;
    public float MouseSensesitivity;
    public float Min, Max;
    float Yaw, pitch;
    public Vector2 pitchMinMax;
    Vector3 CurrentRotation;
    Vector3 RotationSmoothVelocity;
    public float RotationSmoothTime;
    public float DistnaceFromTar;
    public LayerMask Collsion;
    public PlayerHealth ph;
    public bool CanFOllow;




    private void Update()
    {
        if(ph.Health > 0f && CanFOllow)
        {
        CameraLook();

        }
        
        
    }

    void CameraLook()
    {
        


        Yaw += Input.GetAxisRaw("Mouse X") * MouseSensesitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * MouseSensesitivity;


        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);



        Vector3 targetRotation = new Vector3(pitch, Yaw);
        transform.eulerAngles = targetRotation;

        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, Yaw), ref RotationSmoothVelocity, RotationSmoothTime);
        transform.eulerAngles = CurrentRotation;

        
        
        transform.position = Target.position - transform.forward * DistnaceFromTar;

        


    }


    


}
