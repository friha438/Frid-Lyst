using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*INSTUKTIONER FÖR ATT FÅ SCRIPTET ATT FUNGERA
 * 1. HA EN PC
 * 2. File --> build settings --> Universal Windows Platform, om du inte har den måste den laddas ner --> Inspector scrolla ner till Capabilites -->
 *    tryck i mikrofon
 * 3. Skapa ett objekt och lägg till en aduio source
 * 4. Plugga in en extern mikrofon för då blir det inget eko som uppstår annars
 */

 //detta är alltså det enklaste fallet 
public class Sound : MonoBehaviour
{
    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audio;
    Vector3 postions;
    Renderer rend;
    float n=0.01f;
    void Start()
    {
        rend = GetComponent<Renderer> ();
        rend.material.shader = Shader.Find("Unlit/perlin");
        print(rend.material.GetFloat("_a"));
        //postions = this.GetComponent<Rigidbody>().transform.position;
        //this.GetComponent<Renderer>().material.color = Color.blue;
        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100); //deviceName, loop bool, secounds, frequency
        _audio.loop = true;
        _audio.mute = true;
        rend.material.SetFloat("_a", 0);

        while (!(Microphone.GetPosition(null) > 0))
        {

        }
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;

        rend.material.SetFloat("_a",n );
Debug.Log(n);
n+=loudness/10f;


    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}

