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

        Debug.Log($"Selected color name: {randomColor}");

        Color selectedColor = vfx.GetVector4(randomColor);

        Debug.Log($"Retrieved color: {selectedColor}");

        vfx.SetVector4("MainColor", selectedColor);
        vfx.SetVector4("EmissionColor", selectedColor);

        Debug.Log($"Main color: {vfx.GetVector4("MainColor")}");
        Debug.Log($"Emission color: {vfx.GetVector4("EmissionColor")}");

        vfx.Play();
    }
}
