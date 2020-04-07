using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource _audioSource;
    public  static float[] _samples = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }


}

