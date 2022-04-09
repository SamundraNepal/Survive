using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTimeManager : MonoBehaviour
{

    public float SlowDownTimeFactor;
    public float SlowDOwnTimeLength;

    public float time;
    PauseMenu paused;

    private void Start()
    {


        paused = GetComponent<PauseMenu>();
    }
    private void Update()
    {

        if (paused.IsMenuPaused == false)
        {


            time = Time.fixedDeltaTime;


            Time.timeScale += (1f / SlowDOwnTimeLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void DoSlowDownTime()
    {



        Time.timeScale = SlowDownTimeFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;



    }


    public void FinishSlowDownTime()
    {

        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
        



    }
}
