using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_objects : MonoBehaviour
{
    public int timeP = 1;
    public int points = 500;
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
        if(Time.time >= nextP){
            nextP = Mathf.FloorToInt(Time.time) + timeP;
            points -= 5;
        }
    }

    public void generateObj(){
        foreach (
             GameObject item in elements)
        {
            Destroy(item);
        }


        int count = 0;

        elements.Add(chair);
        elements.Add(table);
        elements.Add(shelf);
        elements.Add(clock);
        elements.Add(lamp);
        elements.Add(tv);
        elements.Add(computer);
        elements.Add(bed);

        foreach (
             GameObject item in elements)
        {
            Instantiate(item, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.5f, 1.0f), Random.Range(-2.0f, 2.0f)),
                         Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            count += 1;
        }

    }
}
