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
                        inputFieldEmail.text = "";
                        inputFieldEmail.Select();
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
                        inputFieldPass.text = "";
                        inputFieldPass.Select();
                    });
                    return;
                }
                // Correo ya registrado en el sistema
                if (CheckError(task.Exception, (int)Firebase.Auth.AuthError.EmailAlreadyInUse))
                {
                    UnityMainThread.wkr.AddJob(() =>
                    {
                        panelMessage.SetActive(true);
                        textErrorMessage.text = "El correo electronico ya se encuentra en uso";
                        inputFieldEmail.Select();
                    });
                    return;
                }


                Debug.Log("Error sin excepci�n programada: " + task.Exception.InnerExceptions[0]);
                return;
            }

            // Firebase user has been created.
            user = task.Result;
        });

    }

    // Actualizaci�n del displayName para usuairos nuevos
    private void UpdateUserDisplayName()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        string name = inputFieldName.text;

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
                Debug.Log("diplay name");
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
        auth.SignOut();
    }

    // Validaci�n de inicios de sesi�n
 

    public void RegisterButton()
    {
        CreateUserWithEmailAndPasswordAsync();
        UpdateUserDisplayName();
        auth.SignOut();
        
        SceneManager.LoadScene("Login");
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Login");
    }
}
