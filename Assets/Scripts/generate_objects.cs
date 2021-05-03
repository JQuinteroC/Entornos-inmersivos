using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_objects : MonoBehaviour
{
    public int timeP = 1;

    public static int points = 500;

    public static bool on = false;

    private int nextP = 0;

    public GameObject chair;

    public GameObject table;

    public GameObject shelf;

    public GameObject clock;

    public GameObject lamp;

    public GameObject tv;

    public GameObject computer;

    public GameObject bed;

    private List<GameObject> elements = new List<GameObject>();

    //private List<Vector3> rotations = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        //Rotaciones de los objetos
        //rotations.Add(new Vector3(0, 0, 0));
        //rotations.Add(new Vector3(-90, 0, 0));
        //generateObj();
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            if (Time.time >= nextP)
            {
                nextP = Mathf.FloorToInt(Time.time) + timeP;
                points -= 5;
            }
        }
    }

    public void generateObj()
    {
        foreach (GameObject item in elements)
        {
            Destroy (item);
        }

        int count = 0;

        elements.Add (chair);
        elements.Add (table);
        elements.Add (shelf);
        elements.Add (clock);
        elements.Add (lamp);
        elements.Add (tv);
        elements.Add (computer);
        elements.Add (bed);

        foreach (GameObject item in elements)
        {
            float
                x,
                y,
                z;
            do
            {
                x = Random.Range(-2.0f, 2.0f);
                y = Random.Range(0.3f, 0.7f);
                z = Random.Range(-2.0f, 2.0f);
            }
            while (x <= 4f &&
                x >= 0.9f &&
                y >= 0.15f &&
                y <= 0.9f &&
                z <= 0.4f &&
                z >= -0.8
            );
            if (item == table)
            {
                Instantiate(item,
                new Vector3(x, y, z),
                Quaternion.Euler(-90, 0, 0));
            }
            else
            {
                Instantiate(item, new Vector3(x, y, z), Quaternion.identity);
            }

            count += 1;
        }
    }
}
