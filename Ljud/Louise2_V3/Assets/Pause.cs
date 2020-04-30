using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    changeColor play_fire;
    public GameObject fire;
    public bool play = false;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (play == true)
        {
            Debug.Log("hej");
        }
    }
}
