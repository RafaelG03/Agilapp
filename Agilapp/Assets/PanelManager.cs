using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public Button Button1;
    public Button Button2;
    public Button Button3;

    void Start()
    {
        Button btn1 = Button1.GetComponent<Button>();
        btn1.onClick.AddListener(OpenPanel1);

        Button btn2 = Button2.GetComponent<Button>();
        btn2.onClick.AddListener(OpenPanel2);

        Button btn3 = Button3.GetComponent<Button>();
        btn3.onClick.AddListener(OpenPanel3);
    }

    public void OpenPanel1()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
    }

    public void OpenPanel2()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
        Panel3.SetActive(false);
    }

    public void OpenPanel3()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(true);
    }

}
