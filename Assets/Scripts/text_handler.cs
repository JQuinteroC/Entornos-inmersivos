using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text_handler : MonoBehaviour
{

    private string[] words = {"Chair", "Table", "Shelf", "Clock", "Lamp", "Tv", "Computer", "Bed"};
    public TextMeshPro textMesh;
    private int nextUpdate = 0;
    private int lastChoice = -1;
    public int timeE = 5;


    // Start is called before the first frame update
    void Start()
    {
        int choice = (int) Random.Range(0, 8);
        lastChoice = choice;
        string new_text = words[choice];
        textMesh.text = new_text;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=nextUpdate){
            int choice;
            nextUpdate=Mathf.FloorToInt(Time.time) + timeE;
            do{
                choice = (int) Random.Range(0, 8);
            }while(lastChoice == choice);
            lastChoice = choice;
            string new_text =  words[choice];
            textMesh.text = new_text;
        }
    }
}
