using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{

    public TextMeshProUGUI PopUp;

    public float Speed;
    Transform Cam;

    private void Start()
    {
        Cam = Camera.main.transform;
    }
    public void Update()
    {

        transform.LookAt(Cam.transform.position);
        transform.rotation = Quaternion.LookRotation(Cam.transform.forward);

        transform.position += new Vector3(0f, Speed, 0f) * Time.deltaTime;

    }
    public void DamePop(int Damage)
    {


        PopUp.SetText(Damage.ToString());


    }





}
