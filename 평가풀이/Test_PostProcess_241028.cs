using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Test_PostProcess_241028 : MonoBehaviour
{
    Volume volume;
    Vignette vignette;
    float direction = 1;
    float speed = 0.5f;

    private void Start()
    {
        volume = FindAnyObjectByType<Volume>();
        volume.profile.TryGet<Vignette>(out vignette);
        vignette.rounded.value = true;
    }

    private void Update()
    {
        vignette.intensity.value += Time.deltaTime * direction * speed;
        if (vignette.intensity.value >= 1 || vignette.intensity.value <= 0)
        {
            direction *= -1;
        }
    }
}
