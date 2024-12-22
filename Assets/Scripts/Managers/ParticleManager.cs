using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class ParticleManager : MonoBehaviour
{
    public void PlayRandomColor(VisualEffect vfx)
    {
        string[] colors = { "green", "red", "yellow", "violet", "blue" };
        string randomColor = colors[Random.Range(0, colors.Length)];

        Color selectedColor = vfx.GetVector4(randomColor);

        vfx.SetVector4("MainColor", selectedColor);
        vfx.SetVector4("EmissionColor", selectedColor);

        vfx.Play();
    }
}
