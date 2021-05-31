using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class authManager : MonoBehaviour
{
    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Auth.FirebaseUser user;
    private string displayName;

    public InputField inputFieldEmail;
    public InputField inputFieldPass;

    // Start is called before the first frame update
    void Start()
    {
        InitializeFirebase();
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Cerró sesión " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Inicio de sesión " + user.UserId);
                displayName = user.DisplayName ?? "";
               // emailAddress = user.Email ?? "";
               // photoUrl = user.PhotoUrl ?? "";
            }
        }

    }

    // Crete user
    public void createUserByEmail ()
    {
        string email = inputFieldEmail.text;
        string password = inputFieldPass.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
        if (task.IsCanceled) {
            Debug.LogError("Creación de usuario cancelado.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("Problema en la creación de usuario en: " + task.Exception);
            return;
        }

        // Firebase user has been created.
        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
        });
    }

    // Actual Session
    public void ActivateSession()
    {

        void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
        }
    }
}
