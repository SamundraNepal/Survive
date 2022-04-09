using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMotor : MonoBehaviour
{
    [Header("Ammo Giver")]
    public PlayerHealth Ph;
    public Transform[] Points;
    public GameObject[] Stats;
    public bool CanSpawn;

    public PickUpAndThrow PAT;
    public Vector3 BossPosition;
    public enum Boss_States
    {
        Small_Range,
        Medium_Range,
        Large_Range,
        Chase,
        Walk,

    }
    public Boss_States BossBrain;
    public Transform Player;
    public bool IsBossActive;
    public AudioSource Source;
    public Rigidbody Rb;
    Animator Anime;
    [Header("Distnace Check")]
    public float Distnace;
    public float SmallRnage;
    public float MediumRange;
    public float LongRange;
    public Vector3 Velocity;
    public float InAirCounter;
    public float JumpCouter;
    public float WaitforJump;
    public GameObject Fire;
    [Range(0f, 5f)]
    public float DistanceFromTheGround;
    public LayerMask IgnoreLayers;
    public bool IsGrounded;
    public bool Once;
    public bool Isjumping;
    [Header("Attack Attributes")]
    public Vector3 Forces;
    public Vector3 PlayerPositionTracker;
    float CountPositionTimer;
    [Header("SmallRange")]
    public float SmallRangeTimer;
    public string SmallRangeAttackOne;
    public string SmallRangeAttackTwo;
    public bool RandSmall;
    [Header("LongRange")]
    public float LongRangeTimer;
    public string ThrowProjectile;
    public string LongRangeAttack1;
    public string LongRangeAttack2;
    public bool LongRangeAttackRand;

    [Header("Medium Attacks")]
    public bool MediumAttack;
    public string WalkAnimations;
    public string MediumAttack2;
    public float MaxMediumAttackTime;
    public bool RandMediumAttackNUmber;
    public bool CanFollow;

    public BossHealth BH;
    public bool BossData;

    private void Start()
    {

        Source.enabled = false;

        RandSmall = true;
        Fire.SetActive(false);

        Anime = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(0, 9);
        BH = GetComponent<BossHealth>();
        LoadData();

    }
    private void Update()
    {
        if (BH.BHealth > 0f)
        {


            if (IsBossActive)
            {
                SaveSystem.SaveData(this);
                Rb.isKinematic = false;
                IsPlayerOutOfAmmo();
                Source.enabled = true;
                Anime.SetBool("Jump", true);
                RotateTowardsThePlayer();
                BossStates();




            }
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {


        if (Anime)
        {


            Anime.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            Anime.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            Anime.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            Anime.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);


            // Left Foot;


            RaycastHit hit;
            Ray ray = new Ray(Anime.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceFromTheGround + 1f, IgnoreLayers))
            {

                if (hit.transform.tag == "Ground")
                {



                    Vector3 FootPostion = hit.point;
                    FootPostion.y += DistanceFromTheGround;
                    Anime.SetIKPosition(AvatarIKGoal.LeftFoot, FootPostion);
                    Anime.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));


                }

            }





            //Right foot

            ray = new Ray(Anime.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceFromTheGround + 1f, IgnoreLayers))
            {

                if (hit.transform.tag == "Ground")
                {

                    Vector3 FootPostion = hit.point;
                    FootPostion.y += DistanceFromTheGround;
                    Anime.SetIKPosition(AvatarIKGoal.RightFoot, FootPostion);
                    Anime.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));


                }

            }


        }


    }
    void RotateTowardsThePlayer()
    {
        Vector3 EnemyPos = transform.position;
        Vector3 PlayerPos = Player.transform.position;




        Vector3 DistnaceInBetween = PlayerPos - transform.position;

        DistnaceInBetween.y = 0f;


        Quaternion Rotaion = Quaternion.LookRotation(DistnaceInBetween);

        transform.rotation = Quaternion.Slerp(transform.rotation, Rotaion, 10f * Time.deltaTime);





    }
    private void OnCollisionEnter(Collision collision)
    {



        if (collision.gameObject.tag == "Ground")
        {
            BossPosition = transform.position;

            Anime.SetBool(SmallRangeAttackOne, false);
            Anime.SetBool("Jump", false);
            Anime.SetBool("Jump To attack", false);
            Anime.SetBool("StandingUp", true);

            IsGrounded = true;

        }

    }
    private void OnCollisionExit(Collision collision)
    {


        if (collision.gameObject.tag == "Ground")
        {

            IsGrounded = false;

        }
    }
    public void BossStates()
    {






        Vector3 MyPosition = transform.position;
        Vector3 PlayerPosition = Player.transform.position;


        Vector3 V = PlayerPosition - MyPosition;


        Distnace = ((int)Vector3.Distance(PlayerPosition, MyPosition));


        if (InAirCounter <= 0 && BossBrain != Boss_States.Walk && MediumAttack == false)
        {


            if (Distnace < SmallRnage)
            {
                BossBrain = Boss_States.Small_Range;


            }
            else if (Distnace > SmallRnage && Distnace < MediumRange)
            {

                if (MaxMediumAttackTime < 5)
                {

                    MaxMediumAttackTime += 1.5f * Time.fixedDeltaTime;
                }

                BossBrain = Boss_States.Medium_Range;

            }
            else if (Distnace > SmallRnage && Distnace > MediumRange && Distnace < LongRange)
            {
                BossBrain = Boss_States.Large_Range;



            }
            else if (Distnace > LongRange)
            {

                BossBrain = Boss_States.Chase;

            }
            else
            {

                MaxMediumAttackTime = 0f;
            }

        }

        if (BossBrain == Boss_States.Chase)
        {


            if (IsGrounded)
            {
                if (JumpCouter < 5)
                {
                    JumpCouter += 1f * Time.fixedDeltaTime;

                }

                JumpCouter = Mathf.Round(JumpCouter * 100.0f) * 0.01f;



                if (JumpCouter == 5)
                {
                    Anime.SetBool("Jump To attack", true);

                    Rb.AddForce(transform.up * Forces.y * Time.deltaTime, ForceMode.VelocityChange);
                }



            }

            if (JumpCouter == 5)
            {



                if (WaitforJump < 5)
                {

                    WaitforJump += 1f * Time.fixedDeltaTime;
                }
            }

            if (WaitforJump > 3f)
            {


                Isjumping = true;
                JumpCouter = 0f;
                WaitforJump = 0f;

            }
        }

        if (Isjumping == true)
        {


            float D = V.magnitude;

            InAirCounter += 1f * Time.deltaTime;

            if (InAirCounter > 1f)
            {
                Fire.SetActive(true);

                Anime.SetBool("JumpAttackPose", true);
                Rb.velocity = Velocity;
            }

            if (InAirCounter < 8f)
            {

                PlayerPositionTracker = Player.transform.position;

            }

            if (InAirCounter > 10f)
            {
                Vector3 v = PlayerPositionTracker - transform.position;


                float Distnace = v.magnitude;
                if (Distnace > 2f)
                {



                    Rb.AddForce(v * 150f * Time.fixedDeltaTime, ForceMode.VelocityChange);
                    //  transform.position += V * 8f * Time.deltaTime;

                    //  transform.position = Vector3.Lerp(transform.position, V, 50f * Time.deltaTime);

                }
                else
                {
                    InAirCounter = 0f;
                    Isjumping = false;
                    Anime.SetBool("JumpAttackPose", false);
                    Anime.SetBool("Jump To attack", false);
                    Fire.SetActive(false);

                }



            }






        }


        if (BossBrain == Boss_States.Small_Range)
        {

            if (RandSmall)
            {
                int SmallNumber = Random.Range(1, 4);

                switch (SmallNumber)
                {

                    case 1:
                        Anime.SetBool(SmallRangeAttackOne, true);
                        break;
                    case 2:
                        Anime.SetBool(SmallRangeAttackTwo, true);
                        break;
                    case 3:
                        Anime.SetBool(MediumAttack2, true);
                        break;


                }

                RandSmall = false;
            }


        }

        if (BossBrain == Boss_States.Large_Range)
        {




            if (LongRangeTimer < 5f && LongRangeAttackRand == false)
            {

                LongRangeTimer += 1.5f * Time.fixedDeltaTime;

            }


            if (LongRangeTimer >= 5f)
            {
                LongRangeAttackRand = true;

                if (LongRangeAttackRand)
                {
                    int Rand = Random.Range(1, 4);

                    Anime.applyRootMotion = true;

                    switch (Rand)
                    {

                        case 1:
                            Anime.SetBool(ThrowProjectile, true);
                            break;

                        case 2:
                            Anime.SetBool(LongRangeAttack1, true);
                            break;

                        case 3:
                            Anime.SetBool(LongRangeAttack2, true);
                            break;


                    }

                    LongRangeAttackRand = false;
                    LongRangeTimer = 0f;
                }









            }

        }

        if (BossBrain == Boss_States.Walk)
        {
            EnemyWalk();
        }

        if (BossBrain == Boss_States.Medium_Range && MaxMediumAttackTime >= 5f)
        {
            MediumAttack = true;

            if (MediumAttack && CanFollow)
            {
                Vector3 Pos = transform.position;
                Vector3 TargetPos = Player.transform.position;

                Vector3 FinalPos = TargetPos - Pos;

                float Distnace = FinalPos.magnitude;

                if (Distnace > 8f)
                {
                    Anime.SetBool(WalkAnimations, true);
                    transform.position += FinalPos * 0.5f * Time.deltaTime;
                }
                else if (Distnace <= 8f)
                {
                    Anime.SetBool(WalkAnimations, false);
                    MaxMediumAttackTime = 0f;
                    MediumAttack = false;
                    CanFollow = false;

                }


            }

        }




    }
    public void EnemyWalk()
    {

        Vector3 PlayerPos = Player.transform.position;
        Vector3 OurPosition = transform.position;

        Vector3 FinalPosition = PlayerPos - OurPosition;

        float Distnace = FinalPosition.magnitude;


        if (Distnace > 10 && CanFollow)
        {


            transform.position += FinalPosition * 2f * Time.deltaTime;

        }



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, SmallRnage);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MediumRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LongRange);





    }
    public void DisabledAllAnimations()
    {
        Anime.applyRootMotion = false;

        Anime.SetBool(SmallRangeAttackOne, false);
        Anime.SetBool(SmallRangeAttackTwo, false);
        Anime.SetBool(MediumAttack2, false);
        Anime.SetBool(LongRangeAttack1, false);
        Anime.SetBool(LongRangeAttack2, false);
        Anime.SetBool(ThrowProjectile, false);

    }


    void DisableRootMotions()
    {

        Anime.applyRootMotion = false;


    }

    public void Followpllayer()
    {

        CanFollow = true;
    }



    public void IsPlayerOutOfAmmo()
    {

        if (CanSpawn)
        {


            if (Ph.GunAmmuniations < 1f || Ph.Health < 10f)
            {

                foreach (var pos in Points)
                {
                  if (pos.GetComponent<CheckPickUps>().Ishere == false)
                    {
                        int Num = Random.Range(0, Stats.Length);

                        GameObject G = Instantiate(Stats[Num], pos.position, Quaternion.identity);
                        CanSpawn = false;
                    }
                }



            }
           
        }


        if(Ph.GunAmmuniations > 0)
        {

            CanSpawn = true;


        }

    }



    public void LoadData()
    {

        PlayerData Data = SaveSystem.LoadData();
        IsBossActive = Data.IsBoosLevel;
    }
}
