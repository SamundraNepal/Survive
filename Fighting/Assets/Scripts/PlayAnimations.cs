using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayAnimations : MonoBehaviour
{

    public Animator Anime;
    public GameObject Marker;


    private void Start()
    {

        Anime.enabled = false;
        Marker.SetActive(false);


    }


    public void OnPointer()
    {

        Marker.SetActive(true);
        Anime.enabled = true;

  }


    public void OnPointerExit()
    {
        Marker.SetActive(false);

        Anime.enabled = false;


    }


 
}
