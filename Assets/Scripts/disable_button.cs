using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        b.GetComponent<Button>().interactable = false;
    }

}
