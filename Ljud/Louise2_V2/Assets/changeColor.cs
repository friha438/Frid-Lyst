using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
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


    public float sizeChange = 2f;

    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audio;
 
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject fire4;
    public GameObject fire5;
    public GameObject fire6;
    public GameObject fire7;
    public GameObject fire8;


    private Gradient grad_used = new Gradient();
    AnimationCurve used = new AnimationCurve();
    AnimationCurve used2 = new AnimationCurve();
    AnimationCurve used3 = new AnimationCurve();

    float randA;
    float randB;
    float randC;

    //koppling till smoke_scriptet
    change_smoke change_smoke_script;
    public GameObject smoke;
    private float activ;

    // Start is called before the first frame update
    void Start()
    {
        

        grad_used.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.0f, 0.0f, 0.0f), 0.0f),
                                                      new GradientColorKey(new Color(0.0f, 0.01f, 0.0f), 1.0f) },
                                 new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(0.0f, 0.0f),
                                                      new GradientAlphaKey(0.0f, 0.53f),
                                                      new GradientAlphaKey(0.0f, 1.0f) });

        used = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
                                                   new Keyframe(0.5f, 0.0f),
                                                   new Keyframe(1f, 0.0f)});
        used2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
                                                   new Keyframe(0.5f, 0.0f),
                                                   new Keyframe(1f, 0.0f)});
        used3 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
                                                   new Keyframe(0.5f, 0.0f),
                                                   new Keyframe(1f, 0.0f)});
        change_smoke_script = smoke.GetComponent<change_smoke>();

        ps = this.GetComponent<ParticleSystem>();
        ps1 = fire1.GetComponent<ParticleSystem>();
        ps2 = fire2.GetComponent<ParticleSystem>();
        ps3 = fire3.GetComponent<ParticleSystem>();
        ps4 = fire4.GetComponent<ParticleSystem>();
        ps5 = fire5.GetComponent<ParticleSystem>();
        ps6 = fire6.GetComponent<ParticleSystem>();
        ps7 = fire7.GetComponent<ParticleSystem>();
        ps8 = fire8.GetComponent<ParticleSystem>();


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

        activ = _changeColor(ps, loudness);     _changeSize(ps, loudness);
        activ = _changeColor(ps1, loudness);    _changeSize(ps1, loudness);
        activ = _changeColor(ps2, loudness);    _changeSize(ps2, loudness);

        activ = _changeColor(ps3, loudness);    _changeSize(ps3, loudness);
        activ = _changeColor(ps4, loudness);    _changeSize(ps4, loudness);
        activ = _changeColor(ps5, loudness); _changeSize(ps5, loudness);

        activ = _changeColor(ps6, loudness); _changeSize(ps6, loudness);
        activ = _changeColor(ps7, loudness); _changeSize(ps7, loudness);
        activ = _changeColor(ps8, loudness); _changeSize(ps8, loudness);

        change_smoke_script.activate = activ; //skickar till change_smoke så den också aktiveras och ändras
    }

    float _changeColor(ParticleSystem pSystem, float loud)
    {
        Gradient grad_temp = new Gradient();
        Gradient grad_loud = new Gradient();
        Gradient grad_low = new Gradient();
        Gradient grad = new Gradient();
        float activation; 
        var col = pSystem.colorOverLifetime;
        float t = Time.deltaTime * 0.01f;
       
        if (loud > 0.5)
        {
            //test: blå/grön
            //Varför har denna fyra Alpha keys?
            grad_loud.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.0f, 0.5f, 1.0f), 0.0f),
                                                      new GradientColorKey(new Color(0.0f, 0.81f, 0.4f), 1.0f) },
                              new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f),
                                                      new GradientAlphaKey(0.46f, 0.28f),
                                                      new GradientAlphaKey(0.64f, 0.538f),
                                                      new GradientAlphaKey(0.309f, 1.0f) });
            grad_temp = grad_loud;
            activation = 1;
        }
        else
        {
            grad_low.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.95f, 0.75f, 0.122f), 0.0f),
                                                     new GradientColorKey(new Color(0.95f, 0.13f, 0.13f), 1.0f) },
                             new GradientAlphaKey[] {new GradientAlphaKey(0.77f, 0.0f),
                                                     new GradientAlphaKey(0.46f, 0.28f), 
                                                     new GradientAlphaKey(1.0f, 0.447f), 
                                                     new GradientAlphaKey(0.14f, 1.0f) });

            grad_temp = grad_low;
            activation = 0;
        }

        grad.SetKeys(new GradientColorKey[] {new GradientColorKey(Color.Lerp(grad_used.colorKeys[0].color, grad_temp.colorKeys[0].color, t), 0.0f),
                                             new GradientColorKey(Color.Lerp(grad_used.colorKeys[1].color, grad_temp.colorKeys[1].color, t), 1.0f)},
                     new GradientAlphaKey[] {new GradientAlphaKey(Mathf.Lerp(grad_used.alphaKeys[0].alpha, grad_temp.alphaKeys[0].alpha, t), 0.0f),
                                             new GradientAlphaKey(Mathf.Lerp(grad_used.alphaKeys[1].alpha, grad_temp.alphaKeys[1].alpha, t), 0.28f),
                                             new GradientAlphaKey(Mathf.Lerp(grad_used.alphaKeys[2].alpha, grad_temp.alphaKeys[2].alpha, t), 0.538f),
                                             new GradientAlphaKey(Mathf.Lerp(grad_used.alphaKeys[3].alpha, grad_temp.alphaKeys[3].alpha, t), 1.0f)});

        grad_used = grad;
        //test: blå/gul
        //grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.4f, 0.4f, 1.0f), 0.0f), new GradientColorKey(new Color(0.99f, 0.7f, 0.05f), 1.0f) },
        //            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(0.46f, 0.28f), new GradientAlphaKey(0.64f, 0.538f), new GradientAlphaKey(0.309f, 1.0f) });

        col.color = grad;
        return activation;
    }


    void _changeSize(ParticleSystem pSystem, float loud)
    {
        var psSol = pSystem.sizeOverLifetime;
        psSol.enabled = true;
        AnimationCurve curve; 

        randA = Random.Range(0.0f, 1.0f);
        randB = Random.Range(0.0f, 1.0f);
        randC = Random.Range(0.0f, 1.0f); 

        float lerpValue = Time.deltaTime*0.1f;

        if (loud > 0.5)
        {
            /*curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used[0].value, 2f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used[1].value, 1.8f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used[2].value, 1.3f, lerpValue))});*/

            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used[0].value, randA + Random.Range(0.0f, 1.0f), lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used[1].value, randB + Random.Range(0.0f, 1.0f), lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used[2].value, randC + Random.Range(0.0f, 1.0f), lerpValue))});
            used = curve; 
         }
         else
         {
            /*curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used[0].value, 0.5f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used[1].value, 0.3f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used[2].value, 0.4f, lerpValue))});*/


            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used[0].value, randA, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used[1].value, randB, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used[2].value, randC, lerpValue))});

            used = curve;
         }
        
        psSol.size = new ParticleSystem.MinMaxCurve(sizeChange, used);

        //Livslängd 
        float lifetime = 5.0f;
        //float lifetime = 0f;
        var main = pSystem.main;
        main.startLifetime = lifetime;

    }

    void _changeSize2(ParticleSystem pSystem2, float loud)
    {
        var psSol2 = pSystem2.sizeOverLifetime;
        psSol2.enabled = true;
        AnimationCurve curve2;

        randA = Random.Range(1.0f, 1.6f);
        randB = Random.Range(1.0f, 1.5f);
        randC = Random.Range(1.0f, 1.5f);

        float lerpValue = Time.deltaTime * 0.1f;

        if (loud > 0.5)
        {
            curve2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used2[0].value,randA+1, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used2[1].value,randB+1, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used2[2].value, randC+1, lerpValue))});
            used2 = curve2;
        }
        else
        {
            curve2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used2[0].value, randA, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used2[1].value, randB, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used2[2].value, randC, lerpValue))});
            used2 = curve2;
        }

        psSol2.size = new ParticleSystem.MinMaxCurve(sizeChange, used2);

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem2.main;
        main.startLifetime = lifetime;

    }

    void _changeSize3(ParticleSystem pSystem3, float loud)
    {
        var psSol3 = pSystem3.sizeOverLifetime;
        psSol3.enabled = true;
        AnimationCurve curve3;

        randA = Random.Range(0.0f, 0.1f);
        randB = Random.Range(0.0f, 0.1f);
        randC = Random.Range(0.0f, 0.1f);

        float lerpValue = Time.deltaTime * 0.1f;

        if (loud > 0.5)
        {
            curve3 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used3[0].value, randA+1, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used3[1].value, randB+1, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used3[2].value, randC+1, lerpValue))});
            used3 = curve3;
        }
        else
        {

            curve3 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used3[0].value, randA, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used3[1].value, randB, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used3[2].value, randC, lerpValue))});
            used3 = curve3;
        }

        psSol3.size = new ParticleSystem.MinMaxCurve(sizeChange, used3);

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem3.main;
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
