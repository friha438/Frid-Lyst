using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{

    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat.SetColor("_mainColor", new Color(0f, 10f, 0f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
