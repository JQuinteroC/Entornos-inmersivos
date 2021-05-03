using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using static generate_objects;

public class text_handler : MonoBehaviour
{
    private static string[]
        words =
        { "Chair", "Table", "Shelf", "Clock", "Lamp", "Tv", "Computer", "Bed" };

    public static bool onT = false;

    public static bool left = true;

    public TextMeshPro textMesh;

    // Start is called before the first frame update
    void Start()
    {
        if (onT)
        {
            int choice = (int) Random.Range(0, 8);
            string new_text = words[choice];
            textMesh.text = new_text;
            onT = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onT)
        {
            left = false;
            for (int i = 0; i < 8; i++)
            {
                if (words[i] != "-1")
                {
                    left = true;
                }
            }
            if (left)
            {
                int choice;
                do
                {
                    choice = (int) Random.Range(0, 8);
                }
                while (words[choice] == "-1");
                string new_text = words[choice];
                textMesh.text = new_text;
            }
            else
            {
                textMesh.text = "Finalizado";
                on = false;
            }
            onT = false;
        }
    }

    public static void removeWord(string name)
    {
        for (int i = 0; i < 8; i++)
        {
            if (words[i] == name)
            {
                words[i] = "-1";
                Debug.Log("Removiendo: " + name);
                break;
            }
        }
        onT = true;
    }
}
