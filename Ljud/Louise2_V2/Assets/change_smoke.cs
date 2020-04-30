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

    public float sizeChange = 2f;

    public float activate = 0; //måste vara public för vi ska kunna nå den från changeColor

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
    AnimationCurve used_smoke3 = new AnimationCurve();

    float randA;
    float randB;
    float randC;

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
        used_smoke3 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
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

    }

    // Update is called once per frame
    void Update()
    {

        _changeSmoke(ps, activate); _changeSizeSmoke(ps, activate);
        _changeSmoke(ps1, activate); _changeSizeSmoke(ps1, activate);
        _changeSmoke(ps2, activate); _changeSizeSmoke(ps2, activate);

        _changeSmoke(ps3, activate); _changeSizeSmoke(ps3, activate);
        _changeSmoke(ps4, activate); _changeSizeSmoke(ps4, activate);
        _changeSmoke(ps5, activate); _changeSizeSmoke(ps5, activate);

        _changeSmoke(ps6, activate); _changeSizeSmoke(ps6, activate);
        _changeSmoke(ps7, activate); _changeSizeSmoke(ps7, activate);
        _changeSmoke(ps8, activate); _changeSizeSmoke(ps8, activate);

    }

    void _changeSmoke(ParticleSystem pSystem, float ac)
    {

        Gradient grad_temp_smoke = new Gradient();

        Gradient grad_loud = new Gradient();
        Gradient grad_low = new Gradient();
        Gradient grad = new Gradient();

        var col = pSystem.colorOverLifetime;
        float t = Time.deltaTime * 0.01f;

        if (ac == 1)
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


    void _changeSizeSmoke(ParticleSystem pSystem, float ac)
    {

        randA = Random.Range(0.0f, 1.0f);
        randB = Random.Range(0.0f, 1.0f);
        randC = Random.Range(0.0f, 1.0f);

        var psSol = pSystem.sizeOverLifetime;
        psSol.enabled = true;

        AnimationCurve curve;

        float lerpValue = Time.deltaTime * 0.1f;
        
        if (ac == 1)
        {                                               //värdena får Louise sätta
            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke[0].value, randA+0.4f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke[1].value, randB+0.4f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke[2].value, randC+0.4f, lerpValue))});
            used_smoke = curve;
        }
        else
        {                                               //värdena får Louise sätta
            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke[0].value, randA, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke[1].value, randB, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke[2].value, randC, lerpValue))});
            used_smoke = curve;
        }

        psSol.size = new ParticleSystem.MinMaxCurve(sizeChange, used_smoke);

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem.main;
        main.startLifetime = lifetime;
    }

    void _changeSizeSmoke2(ParticleSystem pSystem2, float loud)
    {

        var psSol2 = pSystem2.sizeOverLifetime;
        psSol2.enabled = true;

        AnimationCurve curve2;

        randA = Random.Range(1.0f, 1.6f);
        randB = Random.Range(1.0f, 1.5f);
        randC = Random.Range(1.0f, 1.5f);

        float lerpValue = Time.deltaTime * 0.1f;

        if (loud > 0.5)
        {                                               //värdena får Louise sätta
            curve2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke2[0].value, randA+1.2f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke2[1].value, randB+1.2f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke2[2].value, randC+1.2f, lerpValue))});
            used_smoke2 = curve2;
        }
        else
        {                                               //värdena får Louise sätta
            curve2 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke2[0].value, randA+0.2f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke2[1].value, randB+0.2f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke2[2].value, randC+0.2f, lerpValue))});
            used_smoke2 = curve2;
        }

        psSol2.size = new ParticleSystem.MinMaxCurve(sizeChange, used_smoke2); //sizeChange kan ändras i unity.

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem2.main;
        main.startLifetime = lifetime;
    }

    void _changeSizeSmoke3(ParticleSystem pSystem3, float loud)
    {
        var psSol3 = pSystem3.sizeOverLifetime;
        psSol3.enabled = true;

        AnimationCurve curve3;

        randA = Random.Range(0.0f, 0.1f);
        randB = Random.Range(0.0f, 0.1f);
        randC = Random.Range(0.0f, 0.1f);


        float lerpValue = Time.deltaTime * 0.1f;

        if (loud > 0.5)
        {                                               //värdena får Louise sätta
            curve3 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke3[0].value, randA+1.2f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke3[1].value,randB+1.2f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke3[2].value, randC+1.2f, lerpValue))});
            used_smoke3 = curve3;
        }
        else
        {                                               //värdena får Louise sätta
            curve3 = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke3[0].value, randA+0.2f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke3[1].value, randB+0.2f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke3[2].value, randC+0.2f, lerpValue))});
            used_smoke3 = curve3;
        }

        psSol3.size = new ParticleSystem.MinMaxCurve(sizeChange, used_smoke3); //sizeChange kan ändras i unity.

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem3.main;
        main.startLifetime = lifetime;
    }

    /*float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }*/
}


  
  