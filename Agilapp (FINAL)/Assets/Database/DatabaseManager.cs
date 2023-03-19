using Firebase;
using Firebase.Database;
using Firebase.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public InputField nickname;
    public InputField email_address;
    public InputField password;
    public InputField confirm_password;
    public InputField contact_number;
    public Dropdown pronoun;
    public Dropdown month;
    public Dropdown day;
    private Text pronounout;
    private Text monthout;
    private Text dayout;
    public InputField year;

    private string eaddress;
    private string userID;
    private string usead;

    DatabaseReference reference;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pronoun: " + pronoun);
        Debug.Log("Month: " + month);
        Debug.Log("Day: " + day);

        pronounout = pronoun.GetComponentInChildren<Text>();
        monthout = month.GetComponentInChildren<Text>();
        dayout = day.GetComponentInChildren<Text>();
        
        
        FirebaseApp.GetInstance("https://agilapp-61259-default-rtdb.firebaseio.com/");
        userID = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        Dropdown1(pronoun);
        Dropdown2(month);
        Dropdown3(day);

        pronoun.onValueChanged.AddListener(delegate { Dropdown1(pronoun); });
        month.onValueChanged.AddListener(delegate { Dropdown2(month); });
        day.onValueChanged.AddListener(delegate { Dropdown3(day); });
    }

    void Dropdown1(Dropdown pronoun)
    {
        int prv = pronoun.value;
        pronounout.text = pronoun.options[prv].text;
        Debug.Log(pronounout.text);
    }


    void Dropdown2(Dropdown month)
    {
        int mtv = month.value;
        monthout.text = month.options[mtv].text;
        Debug.Log(monthout.text);
    }

    void Dropdown3(Dropdown day)
    {
        int dav = day.value;
        dayout.text = day.options[dav].text;
        Debug.Log(dayout.text);
    }

    public void CreateUser()
    {
        User newUser = new User(nickname.text, pronounout.text, email_address.text, password.text, confirm_password.text, contact_number.text, monthout.text, int.Parse(dayout.text), int.Parse(year.text));
        string json = JsonUtility.ToJson(newUser);
        eaddress = nickname.text;
        string usead = eaddress + "." + userID;
         
        reference.Child("users").Child(userID).Child(eaddress).SetRawJsonValueAsync(json);
    }
}
