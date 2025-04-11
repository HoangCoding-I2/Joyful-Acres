using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Transform _cropRenderer;
    public void ScaleUp()
    {


        _cropRenderer.gameObject.LeanScale(Vector3.one, 1).setEase(LeanTweenType.easeOutBack);
        
    }

  
}
