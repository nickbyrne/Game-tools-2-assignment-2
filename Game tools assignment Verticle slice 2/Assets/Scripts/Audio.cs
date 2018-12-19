using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public AudioSource DBZ;
    public AudioSource Fortnite;

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            DBZ.Play();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Fortnite.Play();
        }
    }
}
