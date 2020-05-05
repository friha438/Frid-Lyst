
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audio;

    Renderer rend;
    float newDesity;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Unlit/perlin");
        //Debug.Log(rend.material.GetFloat("_Density"));
        newDesity = rend.material.GetFloat("_c");

        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100); //deviceName, loop bool, secounds, frequency
        _audio.loop = true;
        _audio.mute = true;
        while (!(Microphone.GetPosition(null) > 0))
        {

        }
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
        DensityCange(loudness);
    }

    void DensityCange(float loud)
    {
        float Density1 = 0.7f;
        float Density2 = 1f;

        if (loud > 0.5)
        {
            newDesity = Density1;
        }
        else
        {
            newDesity = Density2;
        }

        newDesity = Mathf.Lerp(rend.material.GetFloat("_c"), newDesity, 2f * Time.deltaTime);
        rend.material.SetFloat("_c", newDesity);
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
