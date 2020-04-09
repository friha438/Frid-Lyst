using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_smoke : MonoBehaviour
{
    public float sizeChange = 4f;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        //färg
        var col = ps.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1.0f, 0.69f, 0.03f), 0.0f), new GradientColorKey(new Color(0.75f, 0.7f, 0.63f), 1.0f) },
                     new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(0.46f, 0.28f), new GradientAlphaKey(0.64f, 0.538f), new GradientAlphaKey(0.309f, 1.0f) });

        col.color = grad;

        //storlek
        var psSol = ps.sizeOverLifetime;
        psSol.enabled = true;

        // skapar keys för sizeoverlifetime
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.25f);
        curve.AddKey(0.578f, 1.6f);
        curve.AddKey(1.0f, 0.0f);

        psSol.size = new ParticleSystem.MinMaxCurve(sizeChange, curve);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
