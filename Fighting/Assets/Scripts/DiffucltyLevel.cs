using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffucltyLevel : MonoBehaviour
{


    public bool Intro;
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;







    public GameObject RIght;
    public GameObject Left;
    public GameObject Middle;
    public GameObject Info;

    public  float Timer1;
    public float Timer2;
    public float Timer3;

    public float ThisValue;

    public bool Finish;


    float Timer;
    bool Once = true;
    bool OnceTwo = true;



    private void Start()
    {

        Info.SetActive(false);

    }
    public void Update()
    {
     

        if (Intro == false && Once) 
        {
          if(Timer < 30f)
            {

                Timer += 1f * Time.fixedDeltaTime;
            } else
            {

                Once = false;
                Level1 = true;
            }
              
                
            
        }


        if (Intro == false)
        {

            if (!Finish)
            {

            

                if (Timer1 < 10f)
                {
                    RIght.SetActive(true);

                    Timer1 += Time.fixedDeltaTime * 1f;
                    Timer1 = Mathf.Round(Timer1 * 100.0f) * 0.01f;

                }

                if (Timer1 == ThisValue)
                {
                   // Debug.Log("Second Time");
                    RIght.SetActive(false);

                    Left.SetActive(true);

                    if(Timer2 < 10f)
                    {
                    Timer2 += Time.fixedDeltaTime * 1f;
                        Timer2 = Mathf.Round(Timer2 * 100.0f) * 0.01f;

                    }

                }



                if (Timer2 ==ThisValue)
                {
                    Left.SetActive(false);
                    Middle.SetActive(true);

                   
                 Timer3 += Time.fixedDeltaTime * 1f;
                    
                }

                if (Timer3 > 20f)
                {

                    Middle.SetActive(false);
                    if(OnceTwo)
                    {
                    Info.SetActive(true);
                        OnceTwo = false;
                    }
                    Finish = true;
                }

            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {

            Info.SetActive(false);
        }
    }





}
