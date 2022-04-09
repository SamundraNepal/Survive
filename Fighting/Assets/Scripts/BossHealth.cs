using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BossHealth : MonoBehaviour
{



    public float BHealth;
    public float MaxHealth;

    public Image Health;

     Animator Anime;
    public string AnimeName;


    public GameObject Death;
    public PlayerMovement Pm;
    public PlayerAttack Pa;
    public ProjectileMotion PM;
    public PauseMenu Pause;
    public bool Once;
    public GameObject GameOver;
    private void Start()
    {

        Anime = GetComponent<Animator>();
        Death.SetActive(false);
        Once = true;

    }


    private void Update()
    {



        Health.fillAmount = BHealth / MaxHealth;



        if(BHealth <= 0f)
        {

            if (Once)
            {
               Anime.SetBool(AnimeName, true);
            }


        }



        if(BHealth <= 0f)
        {
            Pause.Canpuse = false;
            if(Once == false)
            {
            StartCoroutine(Call());

            }
            Pm.enabled = false;
            Pa.enabled = false;
            PM.enabled = false;

        }


    }


    IEnumerator Call()
    {

        yield return new WaitForSeconds(5f);
        EventSystem.current.SetSelectedGameObject(GameOver); 
        Death.SetActive(true);
    }




}
