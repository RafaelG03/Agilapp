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
    private DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://agilapp-61259-default-rtdb.firebaseio.com/");
        userID = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        if (nickname == null || pronoun == null || email_address == null || password == null ||
        confirm_password == null || contact_number == null || birthday == null)
        {
            Debug.LogError("One or more input fields are null.");
            return;
        }

        // Check that all input fields have non-empty values
        if (string.IsNullOrEmpty(nickname.text) || string.IsNullOrEmpty(pronoun.text) ||
            string.IsNullOrEmpty(email_address.text) || string.IsNullOrEmpty(password.text) ||
            string.IsNullOrEmpty(confirm_password.text) || string.IsNullOrEmpty(contact_number.text) ||
            string.IsNullOrEmpty(birthday.text))
        {
            Debug.LogError("One or more input fields are empty.");
            return;
        }

        // Try to parse the birthday input as an int
        int parsedBirthday;
        if (!int.TryParse(birthday.text, out parsedBirthday))
        {
            Debug.LogError("Birthday input could not be parsed as an int.");
            return;
        }

        // Create the new user object
        User newUser = new User(nickname.text, pronoun.text, email_address.text, password.text, confirm_password.text, contact_number.text, parsedBirthday);
        string json = JsonUtility.ToJson(newUser);

        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
}
