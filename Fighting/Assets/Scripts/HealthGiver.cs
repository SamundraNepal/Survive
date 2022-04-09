using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGiver : MonoBehaviour
{

    public float Swing;

    GameObject Player;



    private void Start()
    {


        Player = GameObject.FindGameObjectWithTag("Player");


    }

    private void Update()
    {


        Vector3 V = transform.position;
        Vector3 T = Player.transform.position;



        Vector3 Dis = T - V;

        Dis.y = 0f;

        Quaternion Rot = Quaternion.LookRotation(-Dis);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rot, 10f * Time.deltaTime);



        float Y = transform.position.y;
        float X = transform.position.x;
        float Z = transform.position.z;


        Y = Mathf.Sin(Time.time * 5f) *Swing;
        transform.position = new Vector3(X, transform.position.y - Y, Z);
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {



            PlayerHealth Ph = other.gameObject.GetComponent<PlayerHealth>();
            PlayerStat stat = other.gameObject.GetComponent<PlayerStat>();


            if (Ph != null)
            {

                Ph.Health += 10f;


            }

            if (stat != null)
            {

                stat.PlayerStatmina += 10f;
                Destroy(this.gameObject);

            }
        }

    }
   
}
