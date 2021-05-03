using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using static interface_text;
using static text_handler;

public class collision : MonoBehaviour
{
    public TextMeshPro textMesh;

    //public generate_objects scriptName;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindWithTag("maintx");
        textMesh = go.GetComponent<TextMeshPro>();
        //GameObject gob = GameObject.FindWithTag("oh");
        //scriptName = gob.GetComponent<Generate_objects>();
        //Debug.Log(scriptName);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "maintx")
        {
            Debug.Log("Colisiona");
            if (textMesh.text == tag)
            {
                Debug.Log("Objeto asociado correctamente");
                addPoints();
            }
            else
            {
                Debug.Log("Objeto no asociado correctamente");
                restPoints();
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
                transform.position = new Vector3(x, y, z);
                this.gameObject.GetComponent<Rigidbody>().velocity =
                    new Vector3(0, 0, 0);
                this.gameObject.GetComponent<Rigidbody>().angularVelocity =
                    new Vector3(0, 0, 0);
            }
            removeWord(textMesh.text);
            Destroy(GameObject.FindWithTag(textMesh.text));
        }
        else
        {
            Debug.Log("Colisión entre objetos");
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
            transform.position = new Vector3(x, y, z);
            this.gameObject.GetComponent<Rigidbody>().velocity =
                new Vector3(0, 0, 0);
            this.gameObject.GetComponent<Rigidbody>().angularVelocity =
                new Vector3(0, 0, 0);
        }
    }
}
