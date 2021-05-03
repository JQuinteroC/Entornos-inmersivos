using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static generate_objects;
using static text_handler;

public class disable_button : MonoBehaviour
{
    public GameObject b;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableButton(){
        generate_objects.on = true;
        text_handler.onT = true;
        b.GetComponent<Button>().interactable = false;
    }

}
