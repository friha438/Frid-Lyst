using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_smoke : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem ps1;
    private ParticleSystem ps2;
    private ParticleSystem ps3;
    private ParticleSystem ps4;
    private ParticleSystem ps5;
    private ParticleSystem ps6;
    private ParticleSystem ps7;
    private ParticleSystem ps8;

    public float sizeChange = 4f;

    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audio;

    public GameObject smoke1;
    public GameObject smoke2;
    public GameObject smoke3;
    public GameObject smoke4;
    public GameObject smoke5;
    public GameObject smoke6;
    public GameObject smoke7;
    public GameObject smoke8;


    private Gradient grad_used_smoke = new Gradient();
    AnimationCurve used_smoke = new AnimationCurve();
    AnimationCurve used_smoke2 = new AnimationCurve();

    // Start is called before the first frame update
    void Start()
    {

        grad_used_smoke.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.0f, 0.0f, 0.0f), 0.0f),
                                                        new GradientColorKey(new Color(0.0f, 0.01f, 0.0f), 1.0f) },
                                new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f),
                                                        new GradientAlphaKey(0.0f, 0.28f),
                                                        new GradientAlphaKey(0.0f, 0.53f),
                                                        new GradientAlphaKey(0.0f, 1.0f) });

        used_smoke = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
                                                         new Keyframe(0.5f, 0.0f),
                                                         new Keyframe(1f, 0.0f)});
        used_smoke2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
                                                         new Keyframe(0.5f, 0.0f),
                                                         new Keyframe(1f, 0.0f)});


        ps = this.GetComponent<ParticleSystem>();
        ps1 = smoke1.GetComponent<ParticleSystem>();
        ps2 = smoke2.GetComponent<ParticleSystem>();
        ps3 = smoke3.GetComponent<ParticleSystem>();
        ps4 = smoke4.GetComponent<ParticleSystem>();
        ps5 = smoke5.GetComponent<ParticleSystem>();
        ps6 = smoke6.GetComponent<ParticleSystem>();
        ps7 = smoke7.GetComponent<ParticleSystem>();
        ps8 = smoke8.GetComponent<ParticleSystem>();


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

        _changeSmoke(ps, loudness); _changeSizeSmoke(ps, loudness);
        _changeSmoke(ps1, loudness); _changeSizeSmoke(ps1, loudness);
        _changeSmoke(ps2, loudness); _changeSizeSmoke(ps2, loudness);
        _changeSmoke(ps3, loudness); _changeSizeSmoke(ps3, loudness);
        _changeSmoke(ps4, loudness); _changeSizeSmoke(ps4, loudness);

        _changeSmoke(ps5, loudness); _changeSizeSmoke2(ps5, loudness);
        _changeSmoke(ps6, loudness); _changeSizeSmoke2(ps6, loudness);
        _changeSmoke(ps7, loudness); _changeSizeSmoke2(ps7, loudness);
        _changeSmoke(ps8, loudness); _changeSizeSmoke2(ps8, loudness);

    }

    void _changeSmoke(ParticleSystem pSystem, float loud)
    {

        Gradient grad_temp_smoke = new Gradient();

        Gradient grad_loud = new Gradient();
        Gradient grad_low = new Gradient();
        Gradient grad = new Gradient();

        var col = pSystem.colorOverLifetime;
        float t = Time.deltaTime * 0.1f;

        if (loud > 0.5)
        {
            //test: blå/grön
            grad_loud.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.4f, 0.4f, 1.0f), 0.0f),
                                                 new GradientColorKey(new Color(0.21f, 0.47f, 0.22f), 1.0f) },
                         new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f),
                                                 new GradientAlphaKey(0.46f, 0.28f),
                                                 new GradientAlphaKey(0.64f, 0.538f),
                                                 new GradientAlphaKey(0.309f, 1.0f) });

            grad_temp_smoke = grad_loud;
        }
        else
        {
            //vanlig färg

            //vanlig färg
            grad_low.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(1.0f, 0.69f, 0.03f), 0.0f),
                                                     new GradientColorKey(new Color(0.75f, 0.7f, 0.63f), 1.0f)},
                             new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f),
                                                     new GradientAlphaKey(0.46f, 0.28f),
                                                     new GradientAlphaKey(0.64f, 0.538f),
                                                     new GradientAlphaKey(0.309f, 1.0f)});
            grad_temp_smoke = grad_low;
        }
        
        grad.SetKeys(new GradientColorKey[] {new GradientColorKey(Color.Lerp(grad_used_smoke.colorKeys[0].color, grad_temp_smoke.colorKeys[0].color, t), 0.0f),
                                             new GradientColorKey(Color.Lerp(grad_used_smoke.colorKeys[1].color, grad_temp_smoke.colorKeys[1].color, t), 1.0f)},
                     new GradientAlphaKey[] {new GradientAlphaKey(Mathf.Lerp(grad_used_smoke.alphaKeys[0].alpha, grad_temp_smoke.alphaKeys[0].alpha, t), 0.0f),
                                             new GradientAlphaKey(Mathf.Lerp(grad_used_smoke.alphaKeys[1].alpha, grad_temp_smoke.alphaKeys[1].alpha, t), 0.28f),
                                             new GradientAlphaKey(Mathf.Lerp(grad_used_smoke.alphaKeys[2].alpha, grad_temp_smoke.alphaKeys[2].alpha, t), 0.538f),
                                             new GradientAlphaKey(Mathf.Lerp(grad_used_smoke.alphaKeys[3].alpha, grad_temp_smoke.alphaKeys[3].alpha, t), 1.0f)});
        
        grad_used_smoke = grad;
        //test: blå / gul
        //grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.4f, 0.4f, 1.0f), 0.0f), new GradientColorKey(new Color(0.57f, 0.56f, 0.5f), 1.0f) },
        //             new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(0.46f, 0.28f), new GradientAlphaKey(0.64f, 0.538f), new GradientAlphaKey(0.309f, 1.0f) });
         col.color = grad;
    }
    void _changeSizeSmoke(ParticleSystem pSystem, float loud)
    {

        var psSol = pSystem.sizeOverLifetime;
        psSol.enabled = true;

        AnimationCurve curve;

        float lerpValue = Time.deltaTime * 0.1f;
        
        if (loud > 0.5)
        {                                               //värdena får Louise sätta
            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke[0].value, 2.5f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke[1].value, 1.9f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke[2].value, 0.6f, lerpValue))});
            used_smoke = curve;
        }
        else
        {                                               //värdena får Louise sätta
            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke[0].value, 1f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke[1].value, 0.6f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke[2].value, 0.8f, lerpValue))});
            used_smoke = curve;
        }

        psSol.size = new ParticleSystem.MinMaxCurve(sizeChange, used_smoke);

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem.main;
        main.startLifetime = lifetime;
    }

    //Värdena MÅSTE LOUISE HJÄLPA TILL MED
    void _changeSizeSmoke2(ParticleSystem pSystem2, float loud)
    {

        var psSol2 = pSystem2.sizeOverLifetime;
        psSol2.enabled = true;

        AnimationCurve curve2;

        float lerpValue = Time.deltaTime * 0.1f;

        if (loud > 0.5)
        {                                               //värdena får Louise sätta
            curve2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke2[0].value, 2.5f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke2[1].value, 1.9f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke2[2].value, 0.6f, lerpValue))});
            used_smoke2 = curve2;
        }
        else
        {                                               //värdena får Louise sätta
            curve2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke2[0].value, 1.5f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke2[1].value, 1f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke2[2].value, 0.5f, lerpValue))});
            used_smoke2 = curve2;
        }

        psSol2.size = new ParticleSystem.MinMaxCurve(sizeChange, used_smoke2); //sizeChange kan ändras i unity.

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem2.main;
        main.startLifetime = lifetime;
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


  
  