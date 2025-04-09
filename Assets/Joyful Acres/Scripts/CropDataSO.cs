using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Crop Data",menuName ="Scriptable Objects/Crop Data")]
public class CropDataSO : ScriptableObject
{
    [Header(" Settings ")]
    public Crop CropPrefab;
}
