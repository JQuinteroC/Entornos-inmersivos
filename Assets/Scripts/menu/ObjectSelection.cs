using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjectSelection : MonoBehaviour
{

    public int length = 8;
    public GameObject[] objects;
    public int selected = 0;
    public AudioSource[] sounds;
    public UnityEngine.Video.VideoPlayer[] videos;
    public bool enabled = false;
    public string[] names = {
        "Bed", "Clock", "Laptop", "Lamp", 
        "Shelf", "Table", "Chair", "TV"
    };
    public string[] namesE = {
        "Cama", "Reloj", "Portátil", "Lámpara",
        "Estante", "Mesa", "Silla", "Televisor"
    };

    public TextMeshPro textM;
    public TextMeshPro textME;

    void Start(){
        objects[selected].SetActive(true);
        Debug.Log("Iniciado");
        for(int i = 1; i < length; i++){
            objects[i].SetActive(false);
        }
        textM.text = names[selected];
        textME.text = namesE[selected];
    }

    public void playVideo(){
        enabled = !enabled;
        if(enabled){
            videos[selected].enabled = enabled;
            videos[selected].Play();
        }else{
            videos[selected].Pause();
            videos[selected].enabled = enabled;
        }
    }

    public void playSound(){
        sounds[selected].Play();
    }

    public void nextObject(){
        Debug.Log(selected);
        objects[selected].SetActive(false);
        selected = (selected + 1) % length;
        objects[selected].SetActive(true);
        textM.text = names[selected];
        textME.text = namesE[selected];
        Debug.Log(selected);
    }

    public void previousObject(){
        objects[selected].SetActive(false);
        selected --;
        if(selected < 0){
            selected += length;
        }
        objects[selected].SetActive(true);
        textM.text = names[selected];
        textME.text = namesE[selected];
        Debug.Log(selected);
    }

    public void startG(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
