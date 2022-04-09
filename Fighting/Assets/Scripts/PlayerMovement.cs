using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerStat Ps;
    public bool IsPlayerHurt;

    [Header("Animations")]
    public string Movemennt;
    public string Jump;




    [Header("Movements")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float RotationSpeed;
    public float JumpForce;
    public float RollForce;
    public bool IsGrounded;
    public bool IsSprinting;
    public float Momentum;

    public bool IsJumping;
    public bool IsShooting;
    private float TrunSmoothVelocity;
    public Vector3 Gravity;

    public float DashSpeed;


    [Header("Animations")]
    public float Speed;
    private float AnimationsSpeed;
    public float Min, Max;



    [Header("Valuting")]
    public Vector3 StartPoint;
    public LayerMask GroundMask;






    private Animator Anime;
    public Rigidbody Rb;
    AnimationEvent AE;
    PlayerAttack PA;
    PlayerHealth Ph;
    ProjectileMotion PM;

    [Header("Stats")]
    public float StaminaDecreaseRate;

    public void Start()
    {
        Debug.Log(Application.persistentDataPath);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Ps = GetComponent<PlayerStat>();
        PM = GetComponent<ProjectileMotion>();
        Ph = GetComponent<PlayerHealth>();
        AE = GetComponent<AnimationEvent>();
        Rb = GetComponent<Rigidbody>();
        PA = GetComponent<PlayerAttack>();
        Anime = GetComponent<Animator>();




    }

    private void Update()
    {
        if (!AE.IsAttacking && !IsPlayerHurt && Ph.Health > 0)
        {
          
            AnimationsSpeed = Mathf.Clamp(AnimationsSpeed, Min, Max);
            Momentum = Mathf.Clamp(Momentum, Min, Max);
            if (IsGrounded)
            {


                if (Input.GetKey(KeyCode.Space))
                {

                    IsJumping = true;
                    Anime.SetBool(Jump, true);
                    IsGrounded = false;


                }

            }
        }


    }
    public void FixedUpdate()
    {

        if (!AE.IsAttacking && !IsPlayerHurt && Ph.Health > 0 && !PM.IsProjectileReady)
        {
            PlayerMovements();
            GroundCheck();
        }


    }



    void PlayerMovements()
    {

        if (!IsJumping && IsGrounded && !IsShooting)
        {


            float Hor = Input.GetAxisRaw("Horizontal");
            float Ver = Input.GetAxisRaw("Vertical");


            Vector3 MoveDirection = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0) * new Vector3(Hor, 0, Ver).normalized;



            if (MoveDirection.magnitude >0f)
            {

                float Angle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg;
                float TargetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref TrunSmoothVelocity, RotationSpeed);
                transform.rotation = Quaternion.Euler(0f, TargetAngle, 0f);
               
                
                if (Input.GetKey(KeyCode.LeftShift) && Ps.PlayerStatmina > 0f)
                {
                    IsSprinting = true;
                    Momentum += 0.5f * Time.deltaTime;
                    Ps.PlayerStatmina -= StaminaDecreaseRate * Time.deltaTime;
                    Rb.velocity = MoveDirection * SprintSpeed * Time.deltaTime ;
                    Max = 2f;
                }
                else
                {
                    IsSprinting = false;
                    Rb.velocity = MoveDirection * MoveSpeed * Time.deltaTime;

                    Max = 1f;

                }
            }


            if (Hor != 0 || Ver != 0)
            {
                AnimationsSpeed += Speed * Time.deltaTime;

                Anime.SetFloat(Movemennt, AnimationsSpeed);

            }
            else
            {


                AnimationsSpeed -= 5f * Time.deltaTime;
                Anime.SetFloat(Movemennt, AnimationsSpeed);

            }

        }


      
    }
    



    void GroundCheck()
    {

   
        if (Physics.Raycast(transform.position, -transform.up, 0.1f,GroundMask))
        {



                Debug.DrawRay(transform.position, -transform.up * 0.1f, Color.red);
                IsJumping = false;
                IsGrounded = true;
                Anime.SetBool(Jump, false);
               Anime.SetBool("InAir", false);


        }
        else
        {
            Anime.SetBool("InAir", true);
            IsGrounded = false;


        }

    }


  





}
