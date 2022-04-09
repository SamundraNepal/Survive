using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{

    public bool IsMenuPaused;
    public GameObject Pause;
    public GameObject MenuTrasition;
    public string LevelName;
    public Transform Camera;
    public Transform Player;
    public CameraFollow CF;
    public float RotateSpeed;
    public bool Canpuse;
    public GameObject GameoverTrans;



    public GameObject PauseButton;

    private void Start()
    {

        Canpuse = true;
    }
    private void Update()
    {
        if (Canpuse)
        {


            if (IsMenuPaused)
            {

                StartCoroutine(RotateCamera());
                Time.timeScale = 0f;
                CF.CanFOllow = false;
            }
            else
            {

                Time.timeScale = 1f;

                CF.CanFOllow = true;

            }



            Pause_Menu();
        }
    }


    public void Pause_Menu()
    {



        if (Input.GetKeyUp(KeyCode.Escape) && IsMenuPaused == false)
        {
            EventSystem.current.SetSelectedGameObject(PauseButton);
            Pause.SetActive(true);
            IsMenuPaused = true;


        }


    }






    public void Resume()
    {
        IsMenuPaused = false;
        Pause.SetActive(false);
        Time.timeScale = 1f;



    }



    public void Menu()
    {

        MenuTrasition.SetActive(true);

        StartCoroutine(CallMenuTime());


    }



    public void GameOverMenu()
    {


        GameoverTrans.SetActive(true);

        StartCoroutine(CallMenuTime());

    }
    IEnumerator CallMenuTime()
    {


        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelName);
    }


    IEnumerator RotateCamera()
    {

        yield return new WaitForSecondsRealtime(0.5f);
        Camera.transform.RotateAround(Player.transform.position, -Vector3.up, RotateSpeed * Time.fixedUnscaledDeltaTime);



    }
}
