using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weather", menuName = "ScriptableObjects/Weather")]
public class WeatherSO : ScriptableObject
{
    [field:SerializeField] public string Name;
    [field:SerializeField] public Material skyboxMat;
    [field:SerializeField] public Color sunColor;
    [field:SerializeField] public float sunIntensity;
}
