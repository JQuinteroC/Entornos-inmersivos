using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static generate_objects;

public class interface_text : MonoBehaviour
{

    public TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = "Puntos: " + points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Puntos: " + points.ToString();
    }

    public static void addPoints(){
        points += 100;
        Debug.Log("Añadiendo puntos");
    }

    public static void restPoints(){
        points -= 100;
        Debug.Log("Restando puntos");
    }

    public static int getPoints(){
        return points;
    }
}
