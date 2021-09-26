using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Am : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;

    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public InputField passwordRegisterVerifyField;
    
    [Header("Message")]
    public GameObject panelMessage;
    public Text textMessage;


    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string email, string password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Inicio de sesion fallido";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "El correo electronico no puede estar vacio";
                    emailLoginField.Select();
                    break;
                case AuthError.MissingPassword:
                    message = "La contrasena no puede estar vacia";
                    passwordLoginField.Select();
                    break;
                case AuthError.WrongPassword:
                    message = "Contrasena incorrecta";
                    break;
                case AuthError.InvalidEmail:
                    message = "Correo electronico invalido";
                    break;
                case AuthError.UserNotFound:
                    message = "Usuario no encontrado";
                    break;
            }
            UIManager.instance.MessageScreen();
            textMessage.text = message;
        }
        else
        {
            // Login completo con exito
            User = LoginTask.Result;
            Debug.LogFormat("Inicio de sesion satisfactorio: {0} ({1})", User.DisplayName, User.Email);
            //textMessage.text = "Bienvenido " + User.DisplayName;
            //UIManager.instance.MessageScreen();
            SceneManager.LoadScene(3);
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if (username == "")
        {
            textMessage.text = "El nombre del nuevo usuario esta vacio";
            UIManager.instance.MessageScreen();
            usernameRegisterField.Select();
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            textMessage.text = "Las contrasenas no coinciden";
            UIManager.instance.MessageScreen();
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Registro de nuevo usuario fallido";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "El correo electronico no puede estar vacio";
                        emailRegisterField.Select();
                        break;
                    case AuthError.MissingPassword:
                        message = "La contrasena no puede estar vacia";
                        passwordRegisterField.Select();
                        break;
                    case AuthError.WeakPassword:
                        message = "La contrasena es muy debil, por favor escriba una contrasena con mas de 6 caracteres";
                        passwordRegisterVerifyField.text = "";
                        passwordRegisterField.text = "";
                        passwordRegisterField.Select();
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "El correo electronico ya se encuentra registrado";
                        break;
                    case AuthError.InvalidEmail:
                        message = "El correo electronico no tiene el formato correcto";
                        emailRegisterField.Select();
                        break;
                }
                textMessage.text = message;
                UIManager.instance.MessageScreen();
            }
            else
            {
                // EL nuevo usuario se registro con exito
                User = RegisterTask.Result;

                if (User != null)
                {
                    // Configurar nombre de usario
                    UserProfile profile = new UserProfile { DisplayName = username };

                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        textMessage.text = "No fue posible asignar el nombre del nuevo usuario";
                        UIManager.instance.MessageScreen();
                    }
                    else
                    {
                        UIManager.instance.LoginScreen();
                        textMessage.text = "";
                    }
                }
            }
        }
    }
}

