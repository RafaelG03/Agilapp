using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthUIManager : MonoBehaviour
{
    public static AuthUIManager instance;

    [Header("References")]
    [SerializeField]
    private GameObject checkingforAccountUI;
    [SerializeField]
    private GameObject loginUI;
    [SerializeField]
    private GameObject registerUI;
    [SerializeField]
    private GameObject verifyEmailUI;
    [SerializeField]
    private Text verifyEmailText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }    
    }

    private void ClearUI()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        FirebaseManager.instance.ClearOutput();
    }

    public void LoginScreen()
    {
        ClearUI();
        loginUI.SetActive(true);
    }

    public void RegisterScreen()
    {
        ClearUI();
        registerUI.SetActive(true);
    }
}
