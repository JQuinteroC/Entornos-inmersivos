using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "MainText"){
            Debug.Log("Colisiona");
        }else{
            transform.position = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.5f, 1.0f), Random.Range(-2.0f, 2.0f));
        }
    }
}
