using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public Image CrossHair;

    public Image HumanHealth;
    public float Health;


    public Image GunAmmo;
    public float GunAmmuniations;
    public float GunDecreaseRate;

    public float DivedeRate;


    public GameObject HealthUI;
    public GameObject PlayerdeathTrans;
    public string SceneName;
    public AudioClip PlayerDeath;
    public AudioSource AS;
    bool Once = true;




    private void Update()
    {

       GunAmmuniations = Mathf.Clamp(GunAmmuniations, 0f, 100f);
        Health = Mathf.Clamp(Health, 0f, 100f);


        if(GunAmmuniations <= 0)
        {


            CrossHair.color = Color.red;


        } else
        {

            CrossHair.color = Color.green;


        }


        HumanHealth.fillAmount = (Health / 100);
        GunAmmo.fillAmount = (GunAmmuniations/100);



        if(Health < 50f)
        {
            HealthUI.SetActive(true);


        } else
        {
            HealthUI.SetActive(false);

        }


        if(Health <= 0)
        {
            if(Once)
            {
            AS.clip = PlayerDeath;
            AS.Play();
                Once = false;
            }
            LoadSceneAfterPlayerDeath();


        }


    }





    public void LoadSceneAfterPlayerDeath()
    {



        StartCoroutine(CallLoadScene());


    }


    IEnumerator CallLoadScene()
    {


        yield return new WaitForSeconds(2f);
        PlayerdeathTrans.SetActive(true);
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(SceneName);
    }

}

