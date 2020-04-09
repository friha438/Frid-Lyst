using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public float sizeChange = 15f;

    // Start is called before the first frame update
    void Start()
    {
       ParticleSystem ps = GetComponent<ParticleSystem>();

        //färg
        var col = ps.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.95f, 0.75f, 0.122f), 0.0f), new GradientColorKey( new Color(0.95f, 0.13f, 0.13f), 1.0f) },
                     new GradientAlphaKey[] { new GradientAlphaKey(0.77f, 0.0f), new GradientAlphaKey(1.0f, 0.447f), new GradientAlphaKey(0.14f, 1.0f) });
        col.color = grad;

        //Storlek
        var psSol = ps.sizeOverLifetime;
        psSol.enabled = true;

        // skapar keys för sizeoverlifetime
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 2.0f);
        curve.AddKey(0.6f, 1.65f);
        curve.AddKey(1.0f, 0.0f);

        psSol.size = new ParticleSystem.MinMaxCurve(sizeChange, curve);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
