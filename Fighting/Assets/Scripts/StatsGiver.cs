using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGiver : MonoBehaviour
{

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


        Y = Mathf.Sin(Time.time * 5f) * 0.01f;
        transform.position = new Vector3(X, transform.position.y - Y, Z);
    }



    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {

            PlayerHealth Ph = other.gameObject.GetComponent<PlayerHealth>();

            PlayerStat stat = other.gameObject.GetComponent<PlayerStat>();

            if (stat != null)
            {

                stat.NumberOfGranade += 20f;
                stat.NumberOfSpecailAttacks += 10f;
            }

            if (Ph != null)
            {


                Ph.GunAmmuniations += 10f;

                Destroy(this.gameObject);
            }


        }

    }

}
