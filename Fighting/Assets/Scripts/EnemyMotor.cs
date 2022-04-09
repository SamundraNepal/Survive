using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMotor : MonoBehaviour
{
      public bool CanRotate;
      public float RotationSpeed;

       public enum Enemybrain { Following , Attacking , AttackExecuated , PushedBack }
       public Enemybrain EnenmyState;
    public bool Wait;




       GameObject Player;
       public   NavMeshAgent Agent;
       Animator ANime;
       float Speed;
      EnemyHealth Eh;




    [Header("Animations")]
    public string Attack1;
    public string Attack2;
    public string Attack3;
    public bool IsAttacking;
    public float NextAttackTimer;


    GameObject[] Eggs;

    private void Start()
    {
        IsAttacking = false;
        Eh = GetComponent<EnemyHealth>();
        ANime = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
       
    }

    private void Update()
    {
        if (Agent != null)
        {



            if (Agent.isOnNavMesh)
            {

                Speed = Mathf.Clamp(Speed, 0f, 2f);
                RotateTowardsPlayer();
            }
            else
            {
                if (Eh.Health > 0)
                {
                    Destroy(this.gameObject);

                }

            }

        }
    }



    


    void RotateTowardsPlayer()
    {


        if (Eh.Health > 0)
        {



            if (CanRotate)
            {
                EnemyFollow();
                EnemyBehaviour();

                Vector3 V = Player.transform.position;

                Vector3 Rotation = (V - transform.position);
                Rotation.y = 0f;

                Quaternion LookRotation = Quaternion.LookRotation(Rotation);

                transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, RotationSpeed * Time.deltaTime);


            }


        }

    }




    void EnemyFollow()
    {
        if (IsAttacking == false)
        {



            Vector3 PlayerPosition = Player.transform.position;



            if (Vector3.Distance(PlayerPosition, transform.position) > 1.5f)
            {
                EnenmyState = Enemybrain.Following;
                Agent.destination = (PlayerPosition);

            }
            else if(Vector3.Distance(PlayerPosition , transform.position ) <= 1.5f)
            {

                EnenmyState = Enemybrain.Attacking;

            }
        }
    }



    void EnemyBehaviour()
    {


        if (Wait!=true)
        {



            if (EnenmyState == Enemybrain.Following)
            {

                Agent.isStopped = false;
                ANime.SetBool("Run", true);
            }
            else if (EnenmyState == Enemybrain.Attacking)
            {




                Agent.isStopped = true;

                ANime.SetBool("Run", false);
                EnemyAttacks();

            }
        }
      
    }




    void EnemyAttacks()
    {
        if (IsAttacking == false)
        {


            EnenmyState = Enemybrain.Attacking;

            int RandomNUmber = Random.Range(1, 4);




            if (RandomNUmber == 1)
            {

                ANime.SetBool(Attack1, true);




            }
            else if (RandomNUmber == 2)
            {
                ANime.SetBool(Attack2, true);



            }
            else if (RandomNUmber == 3)
            {


                ANime.SetBool(Attack3, true);


            }

            IsAttacking = true;

        }


    }


    public void  Callme()
    {

            ANime.SetBool(Attack3, false);
            ANime.SetBool(Attack2, false);
            ANime.SetBool(Attack1, false);
            IsAttacking = false;
    }
  



    IEnumerator AddingUp()
    {

        yield return new WaitForSeconds(0.1f);
        transform.GetComponent<Rigidbody>().isKinematic = false;


    }

}
