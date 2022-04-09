using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStat : MonoBehaviour
{
    public GameObject CrossHair;

    public GameObject AllInfo;
    bool IsInfoActive;
    float TimerInfo;

    public GameObject Shooting;
    public GameObject HandAttacks;
    public GameObject ShootingNames;
    public GameObject HandAttckNames;







    PlayerMovement Pm;
    PlayerAttack Pa;

    public Image Stamina;
    public Image Granade;
    public Image SpecialAttack;
    




    public float PlayerStatmina;
    public float NumberOfGranade;
    public float NumberOfSpecailAttacks;




    private void Start()
    {
        Pa = GetComponent<PlayerAttack>();
        Pm = GetComponent<PlayerMovement>();

    }
    private void Update()
    {

        OpenInventory();

        if(Pa.AttackMode == 1)
        {
            Shooting.SetActive(false);
            CrossHair.SetActive(false);
            HandAttacks.SetActive(true);
            HandAttckNames.SetActive(true);
            ShootingNames.SetActive(false);


        }
        else if(Pa.AttackMode == 2)
        {
            CrossHair.SetActive(true);
            Shooting.SetActive(true);
            HandAttacks.SetActive(false);
            HandAttckNames.SetActive(false);
            ShootingNames.SetActive(true);

        }

        PlayerStatmina = Mathf.Clamp(PlayerStatmina, 0f, 100f);
        NumberOfGranade = Mathf.Clamp(NumberOfGranade, 0f, 100f);
        NumberOfSpecailAttacks = Mathf.Clamp(NumberOfSpecailAttacks, 0f, 100f);



        Stamina.fillAmount = PlayerStatmina / 100;
        Granade.fillAmount = NumberOfGranade / 100;
        SpecialAttack.fillAmount = NumberOfSpecailAttacks / 100;


        if(PlayerStatmina < 100f && Pm.IsSprinting == false)
        {

            StartCoroutine(RestartStat());


        }



     
    }


    IEnumerator RestartStat()
    {


        yield return new WaitForSeconds(10f);
        if(Pm.IsSprinting ==false)
        {
        PlayerStatmina += 0.1f;

        }
    }




    public void OpenInventory()
    {



        if (Input.GetKeyDown(KeyCode.I))
        {

            IsInfoActive = true;
            AllInfo.SetActive(true);


        }

        if(IsInfoActive)
        {

            TimerInfo += 1f * Time.deltaTime;
            if (TimerInfo > 10f)
            {
                TimerInfo = 0f;
                IsInfoActive = false;
                AllInfo.SetActive(false);

            }

        }

    }
}
