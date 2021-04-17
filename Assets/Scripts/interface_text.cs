using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interface_text : MonoBehaviour
{

    public generate_objects Handler;
    public TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = "Puntos: " + Handler.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Puntos: " + Handler.points.ToString();
    }
}
