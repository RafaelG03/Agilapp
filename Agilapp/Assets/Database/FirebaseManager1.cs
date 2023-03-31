using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseManager1 : MonoBehaviour
{
    public static FirebaseManager1 instance;

    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    [Space(5f)]

    [Header("Register References")]
    [SerializeField]
    private InputField registerUsername;
    [SerializeField]
    private InputField registerEmail;
    [SerializeField]
    private InputField registerPassword;
    [SerializeField]
    private InputField registerConfirmPassword;
    [SerializeField]
    private Text registerOutputText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if(!signedIn && user != null)
            {
                Debug.Log("Signed Out");
            }

        user = auth.CurrentUser;
        if (signedIn)
        {
        Debug.Log($"Signed In: {user.DisplayName}");
        }
        }
        
    }

    public void ClearOutput()
    {
        registerOutputText.text = "";
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUsername.text, registerEmail.text, registerPassword.text, registerConfirmPassword.text));
    }

    private IEnumerator RegisterLogic(string _username, string _email, string _password, string _confirmPassword)
    {
        if (_username == "")
        {
            registerOutputText.text = "Please Enter a Username";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "Passwords Do Not Match";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unknown Error, Please Try Again";

                switch(error)
                {
                    case AuthError.InvalidEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "Email Already In Use";
                        break;
                    case AuthError.WeakPassword:
                        output = "Weak Password";
                        break;
                    case AuthError.MissingEmail:
                        output = "Please Enter Your Email";
                        break;
                    case AuthError.MissingPassword:
                        output = "Please Enter Your Password";
                        break;
                }
                registerOutputText.text = output;
            }
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username

                    //TODO ; Give Profile Default Photo
                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);

                if(defaultUserTask.Exception != null)
                {
                    user.DeleteAsync();
                    FirebaseException firebaseException =(FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unknown Error, Please Try Again";

                    switch(error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Cancelled";
                            break;
                        case AuthError.SessionExpired:
                            output = "Session has Expired. Please login again.";
                            break;
                    }
                    registerOutputText.text = output;
                }
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {user.DisplayName} ({user.UserId})");
                }
            }
        }
    }

}
