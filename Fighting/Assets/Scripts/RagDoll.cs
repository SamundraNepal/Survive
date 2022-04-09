using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RagDoll : MonoBehaviour
{




    public Rigidbody[] Rb;
    public Collider[] Col;

    public EnemyHealth EH;

    public  Animator Anime;

    public float deadforce;
    public float Upforce;

    public GameObject[] PickUps;


    bool Forces;
    bool Once;
    bool GivePickups;


    [Header("Enemy DeathEffects")]
    public GameObject DeathEffects;



    private void Awake()
    {
        Once = true;
        GivePickups = true;

        EH = GetComponentInParent<EnemyHealth>();

    }
    public  void Start()
    {
     

        Forces = true;
        Col = GetComponentsInChildren<Collider>();
        Rb = GetComponentsInChildren<Rigidbody>();
        foreach (var R in Rb)
        {

            R.isKinematic = true;
            R.mass = 0.5f;

        }

        foreach (var C in Col)
        {



            C.enabled = false;

        }


    }


    private void Update()
    {
        
        if(EH.Health <= 0)
        {
            if (GivePickups)
            {

                  
                    Instantiate(PickUps[Random.Range(0 , PickUps.Length)], new Vector3(transform.position.x , transform.position.y + 1f , transform.position.z), Quaternion.identity);

              

                GivePickups = false;

            }
        }

    }

    private void FixedUpdate()
    {
        
        if(EH.Health <= 0)
        {


            StartCoroutine(DestroyEnemyBody());

          

            transform.parent = null;
            StartCoroutine(CallForce());

            foreach (var R in Rb)
            {

                R.isKinematic = false;
                if (Forces)
                {


                    R.AddForce(-transform.forward * deadforce * Time.fixedDeltaTime);
                    R.AddForce(transform.up * Upforce * Time.fixedDeltaTime);
                }
            }

            foreach (var C in Col)
            {



                C.enabled = true;

            }

        }

    }



    IEnumerator DestroyEnemyBody()
    {
        yield return new WaitForSeconds(8f);
        if(Once)
        {
            GameObject G = Instantiate(DeathEffects, transform.position, DeathEffects.transform.rotation);
            G.transform.parent = transform;
            Destroy(G, 5f);
            Once = false;
        }

        yield return new WaitForSeconds(1f);
      
            Destroy(this.gameObject);


    }

    IEnumerator CallForce()
    {



        yield return new WaitForSeconds(3f);
        Forces = false;
   
    }
}
