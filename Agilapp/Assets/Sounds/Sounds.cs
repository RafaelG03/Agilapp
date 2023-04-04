using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{

    public AudioSource normal;
    public AudioSource enter;
    public AudioSource exit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playThisNormal()
    {
        normal.Play();
    }

    public void playThisEnter()
    {
        enter.Play();
    }

    public void playThisExit()
    {
        exit.Play();
    }
}
