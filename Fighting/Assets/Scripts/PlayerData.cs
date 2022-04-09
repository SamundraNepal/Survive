using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{


    public bool IsBoosLevel;





    public PlayerData (BossMotor Bm)
    {


        IsBoosLevel = Bm.IsBossActive;
     

    }


}
