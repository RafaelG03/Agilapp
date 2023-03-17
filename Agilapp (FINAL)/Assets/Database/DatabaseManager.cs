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
    public InputField pronoun;
    public InputField email_address;
    public InputField password;
    public InputField confirm_password;
    public InputField contact_number;
    public InputField birthday;

    private string userID;
    DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseDatabase.GetInstance("https://agilapp-61259-default-rtdb.firebaseio.com/");
        userID = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        User newUser = new User(nickname.text, pronoun.text, email_address.text, password.text, confirm_password.text, contact_number.text, int.Parse(birthday.text));
        string json = JsonUtility.ToJson(newUser);

        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
}
