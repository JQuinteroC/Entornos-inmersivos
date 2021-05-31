using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class authRegister : MonoBehaviour
{
    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Auth.FirebaseUser user;
    private string displayName;
    private string emailAddress;

    public InputField inputFieldName;
    public InputField inputFieldEmail;
    public InputField inputFieldPass;

    public GameObject panelMessage;
    public GameObject panelError;
    public Text textErrorMessage;

    // Start is called before the first frame update
    void Start()
    {
        InitializeFirebase();
    }

    // Registro de nuevo usuario
    private void CreateUserWithEmailAndPasswordAsync()
    {
        string name = inputFieldName.text;
        string email = inputFieldEmail.text;
        string password = inputFieldPass.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Creaci�n de usuario con email y contrase�a fue cancelado.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Ha ocurrido un error en la creacion del usuario: " + task.Exception.InnerExceptions[0]);

                // Formato incorrecto del correo
                if (CheckError(task.Exception, (int)Firebase.Auth.AuthError.InvalidEmail))
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        panelMessage.SetActive(true);
                        textErrorMessage.text = "El correo electr�nico no tiene el formato correcto, por ejemplo: xxxx@xxxx.xx";
                    });
                    return;
                }
                // Contrase�a d�bil
                if (CheckError(task.Exception, (int)Firebase.Auth.AuthError.WeakPassword))
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        panelMessage.SetActive(true);
                        textErrorMessage.text = "La contrase�a es muy d�bil, por favor escriba una contrase�a con m�s de 6 caracteres";
                    });
                    return;
                }
                // Correo ya registrado en el sistema
                if (CheckError(task.Exception, (int)Firebase.Auth.AuthError.EmailAlreadyInUse))
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        panelMessage.SetActive(true);
                        textErrorMessage.text = "La contrase�a es muy d�bil, por favor escriba una contrase�a con m�s de 6 caracteres";
                    });
                    return;
                }


                Debug.Log("Error sin exception programada: " + task.Exception.InnerExceptions[0]);
                return;
            }

            // Firebase user has been created.
            user = task.Result;
        });

        UpdateUserDisplayName(name);
    }

    // Actualizaci�n del displayName para usuairos nuevos
    private void UpdateUserDisplayName(string name)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = name,
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Asignaci�n de displayname cancelada.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Error en la asignaci�n del displayname: " + task.Exception);
                    return;
                }
            });
        }

        Debug.LogFormat("Usuario creado correctamente: {0} ({1})", user.DisplayName, user.UserId);
    }

    // Validaci�n de errores del distema y errores de Firebase
    private bool CheckError(System.AggregateException exception, int firebaseExceptionCode)
    {
        Firebase.FirebaseException fbEx = null;
        foreach (Exception e in exception.Flatten().InnerExceptions)
        {
            fbEx = e as Firebase.FirebaseException;
            if (fbEx != null)
                break;
        }

        if (fbEx != null)
        {
            if (fbEx.ErrorCode == firebaseExceptionCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    // Inicializaci�n de servicios de firebase
    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Validaci�n de inicios de sesi�n
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
            }
        }
    }

    public void RegisterButton()
    {
        CreateUserWithEmailAndPasswordAsync();
        SceneManager.LoadScene("Login");
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Login");
    }
}
