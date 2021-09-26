using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectSelection : MonoBehaviour
{

    public int length = 8;
    public GameObject[] objects;
    public int selected = 0;

    void Start(){
        objects[selected].SetActive(true);
        Debug.Log("Iniciado");
        for(int i = 1; i < length; i++){
            objects[i].SetActive(false);
        }
    }


    public void nextObject(){
        Debug.Log(selected);
        objects[selected].SetActive(false);
        selected = (selected + 1) % length;
        objects[selected].SetActive(true);
        Debug.Log(selected);
    }

    public void previousObject(){
        objects[selected].SetActive(false);
        selected --;
        if(selected < 0){
            selected += length;
        }
        objects[selected].SetActive(true);
        Debug.Log(selected);
    }

    public void startG(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
