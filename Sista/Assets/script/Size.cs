
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
    float n=1;

    Shader shader_moln;
    Shader shader_fire;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        //Debug.Log(rend.material.GetFloat("_Density"));
        shader_moln= Shader.Find("Unlit/moln");
        shader_fire= Shader.Find("Unlit/fire2");
        newDesity = 0.14f;

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
        if(n<=0){
            loudness = GetAveragedVolume() * sensitivity;
            DensityCange(loudness);
        }
    }

    void DensityCange(float loud)
    {

        float Density1 = 0.08f;
        float Density2 = 0.18f;
        float Density3 = 0.02f;


        if(rend.material.shader==shader_fire){
        Density1 = 3.07f;
        Density2 = 3.6f;
        Density3 = 1.09f;
        }

        if (loud > 0.5)
        {
            newDesity = Density1;
        }if(loud>1f){
            newDesity= Density3;
        }else
        {
            newDesity = Density2;
        }

        newDesity = Mathf.Lerp(rend.material.GetFloat("_c"), newDesity, 0.8f * Time.deltaTime);
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

    public void buttonstart(){
        n=-1;
        _audio.mute= !_audio.mute;
    }
}
