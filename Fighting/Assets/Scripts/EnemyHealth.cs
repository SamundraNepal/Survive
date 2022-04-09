using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public float Health;
    public Image HealthImage;
    public float  damage;
    public GameObject Canvas;

    [Header("Pick up Objects")]
    public GameObject[] Objects;
    public CapsuleCollider Col;


     NavMeshAgent Agent;
     Animator anime;


    private void Start()
    {
        anime = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CallHead());
    }





    

    private void Update()
    {

        Health = Mathf.Clamp(Health, 0f, 100f);

        HealthImage.fillAmount = Health / 100f;


        if(Health <= 0)
        {
            transform.GetComponent<CapsuleCollider>().enabled = false;
            
            Canvas.SetActive(false);
            Agent.enabled = false;
            anime.enabled = false;
            anime.applyRootMotion = false;
            Destroy(this.gameObject,2f);
        }
    }




    IEnumerator CallHead()
    {


        yield return new WaitForSeconds(1f);
        Col.enabled = true;

    }

}
