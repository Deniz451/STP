using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RndBloodCol : MonoBehaviour
{
    [SerializeField] VisualEffect visEff;

    public void ApplyColorBtn()
    {
        ApplyRandomColor(visEff);
    }

    public void ApplyRandomColor(VisualEffect vfx)
           {
               string[] colors = { "green", "red", "yellow", "violet", "blue" };
               string randomColor = colors[Random.Range(0, colors.Length)];

               Color selectedColor = vfx.GetVector4(randomColor);

               vfx.SetVector4("BaseColor", selectedColor);
               vfx.SetVector4("EmissionColor", selectedColor);
           }
}
