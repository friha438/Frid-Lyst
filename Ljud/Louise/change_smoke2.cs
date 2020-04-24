using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_smoke : MonoBehaviour
{
    //partikelsystemen 
    private ParticleSystem pssmoke;
    private ParticleSystem pssmoke1;
    private ParticleSystem pssmoke2;
    private ParticleSystem pssmoke3;
    private ParticleSystem pssmoke4;

    public float sizeChange = 4f;
    public float activate = 0; //måste vara public för vi ska kunna nå den från changeColor

    public GameObject smoke1;
    public GameObject smoke2;
    public GameObject smoke3;
    public GameObject smoke4;

    private Gradient grad_used_smoke = new Gradient();
    AnimationCurve used_smoke = new AnimationCurve();

    // Start is called before the first frame update
    void Start()
    {
        pssmoke = this.GetComponent<ParticleSystem>();
        pssmoke1 = smoke1.GetComponent<ParticleSystem>();
        pssmoke2 = smoke2.GetComponent<ParticleSystem>();
        pssmoke3 = smoke3.GetComponent<ParticleSystem>();
        pssmoke4 = smoke4.GetComponent<ParticleSystem>();

        grad_used_smoke.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.0f, 0.0f, 0.0f), 0.0f),
                                                        new GradientColorKey(new Color(0.0f, 0.01f, 0.0f), 1.0f) },
                                new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f),
                                                        new GradientAlphaKey(0.0f, 0.28f),
                                                        new GradientAlphaKey(0.0f, 0.53f),
                                                        new GradientAlphaKey(0.0f, 1.0f) });

        used_smoke = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.0f),
                                                         new Keyframe(0.5f, 0.0f),
                                                         new Keyframe(1f, 0.0f)});

    }

    // Update is called once per frame
    void Update()
    {
        _changeSmoke(pssmoke, activate); _changeSizeSmoke(pssmoke, activate);
        _changeSmoke(pssmoke1, activate); _changeSizeSmoke(pssmoke1, activate);
        _changeSmoke(pssmoke2, activate); _changeSizeSmoke(pssmoke2, activate);
        _changeSmoke(pssmoke3, activate); _changeSizeSmoke(pssmoke3, activate);
        _changeSmoke(pssmoke4, activate); _changeSizeSmoke(pssmoke4, activate);
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
            //blågrön
            grad_loud.SetKeys(new GradientColorKey[] {new GradientColorKey(new Color(0.4f, 0.4f, 1.0f), 0.0f),
                                                      new GradientColorKey(new Color(0.21f, 0.47f, 0.22f), 1.0f) },
                              new GradientAlphaKey[] {new GradientAlphaKey(0.0f, 0.0f),
                                                    new GradientAlphaKey(0.46f, 0.28f),
                                                    new GradientAlphaKey(0.64f, 0.538f),
                                                    new GradientAlphaKey(0.309f, 1.0f)});

            grad_temp_smoke = grad_loud;
            
        }
        else
        {
            
            //vanlig
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
        col.color = grad;
    }

    void _changeSizeSmoke(ParticleSystem pSystem, float ac)
    {

        var psSol = pSystem.sizeOverLifetime;
        psSol.enabled = true;

        AnimationCurve curve;

        float lerpValue = Time.deltaTime * 0.01f;

        if (ac == 1)
        {                                               //värdena får Louise sätta
            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke[0].value, 1.5f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke[1].value, 0.9f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke[2].value, 0.6f, lerpValue))});
            used_smoke = curve;
        }
        else
        {                                               //värdena får Louise sätta
            curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, Mathf.Lerp(used_smoke[0].value, 0.3f, lerpValue)),
                                                        new Keyframe(0.5f, Mathf.Lerp(used_smoke[1].value, 0.2f, lerpValue)),
                                                        new Keyframe(1f, Mathf.Lerp(used_smoke[2].value, 0.2f, lerpValue))});
            used_smoke = curve;
        }

        psSol.size = new ParticleSystem.MinMaxCurve(sizeChange, used_smoke);

        //Livslängd 
        float lifetime = 5.0f;
        var main = pSystem.main;
        main.startLifetime = lifetime;
    }


}
