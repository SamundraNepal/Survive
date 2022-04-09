using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{

    public UnityEngine.Rendering.Volume Vol;

    private UnityEngine.Rendering.Universal.DepthOfField Dph;
    private UnityEngine.Rendering.Universal.MotionBlur MB;



    public GameObject Yes;
    public GameObject No;
    public GameObject Hmm;


    public Transform[] Menus;

    public Transform Selection;


    public int Index;
    public int MaxIndex;
    public bool IsPressed;

    public GameObject MenuMenu;
    public GameObject OptionsMenu;
    public GameObject LevelSelectMenu;


    public GameObject TransitionScreen;
    public GameObject QuitScreen;
    public string SceneName;

    public Animator Anime;
    public AnimeEvent AE;



    public GameObject MotionBlur;
    public GameObject DOF;
    bool IsActive;
    bool IsDofActive;


    public GameObject Play_;
    public GameObject Options_;
    public GameObject LoadLevel;
    public GameObject Levels_;
    

    public bool BossData;
    public bool Data;

    private void Start()
    {
        

     

        EventSystem.current.SetSelectedGameObject(Play_);
        Vol.profile.TryGet(out MB);
        Vol.profile.TryGet(out Dph);



           Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;

        PlayerData Data = SaveSystem.LoadData();
        BossData = Data.IsBoosLevel;

        if (BossData)
        {
            LoadLevel.SetActive(true);

        }
        else
        {
            LoadLevel.SetActive(false);

        }


    }

    private void Update()
    {

        Index = Mathf.Clamp(Index, 0, MaxIndex);


        if (Input.GetAxis("Vertical") != 0)
        {

            if (!IsPressed)
            {



                if (Input.GetAxis("Vertical") < 0)
                {
                    if (Index < MaxIndex)
                    {
                        Index++;

                    }
                    else
                    {
                        Index = 0;
                    }



                }
                else if (Input.GetAxis("Vertical") > 0)
                {

                    if (Index > 0)
                    {

                        Index--;


                    }
                    else
                    {


                        Index = MaxIndex;
                    }

                }

                IsPressed = true;
            }


        }
        else
        {

            IsPressed = false;
        }



    }




void MEnuSelection()
    {


        for (int i = 0; i < Menus.Length; i++)
        {


            




        }
    }



    public void Play()
    {

        Anime.SetBool(AE.StandUp, true);
        Yes.SetActive(true);
        StartCoroutine(SceneLoader());


    }



    public void Quit()
    {
        Anime.SetBool(AE.SayBye, true);
        No.SetActive(true);
        StartCoroutine(quititng());


    }

    IEnumerator quititng()
    {
        yield return new WaitForSeconds(2f);
        QuitScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        Application.Quit();



    }

    IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(2f);
        TransitionScreen.SetActive(true);


        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneName);



    }



    public void Options()
    {
        Hmm.SetActive(true);
        MenuMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Options_);


    }


    public void Back()
    {
        LevelSelectMenu.SetActive(false);
        Hmm.SetActive(false);
        MenuMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(Play_);


    }


    public void MotionBulrActivated()
    {

        if(!IsActive)
        {
          MotionBlur.SetActive(true);
            MB.active = false;
            StartCoroutine(Active());

        }

        if(IsActive)
        {
            MotionBlur.SetActive(false);
            MB.active = true;
            IsActive = false;
        }



    }




    public void DepthOfField()
    {

        if(!IsDofActive)
        {
            DOF.SetActive(true);
            Dph.active = false;
            StartCoroutine(DOFFIED());
        }

        if(IsDofActive)
        {

            DOF.SetActive(false);
            Dph.active = true;
            IsDofActive = false;

        }



    }


    public void LevelSelectOptions()
    {

        MenuMenu.SetActive(false);
        LevelSelectMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Levels_);

    }


    public void LoadLevelNOw()
    {
        
        Play();
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(1f);
        IsActive = true;
    }


    IEnumerator DOFFIED()
    {
        yield return new WaitForSeconds(1f);
        IsDofActive = true;


    }
}
