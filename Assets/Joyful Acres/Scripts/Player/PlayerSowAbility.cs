using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSowAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator _playerAnimator;

    [Header(" Settings ")]
    private CropField _currentCropField;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        SeedParticles.OnSeedsCollided += SeedsColliedCallback;
        CropField.OnFullSown += CropFieldFullySownCallback;
    }

    private void OnDestroy()
    {
        SeedParticles.OnSeedsCollided -= SeedsColliedCallback;
        CropField.OnFullSown -= CropFieldFullySownCallback;
    }

    private void CropFieldFullySownCallback(CropField cropField)
    {
        if (cropField == _currentCropField)
        {
            _playerAnimator.StopSowAnimation();
        }
    }

    
    private void SeedsColliedCallback(Vector3[] seedPositions)
    {
        if (_currentCropField == null)
        {
            return;
        }
        _currentCropField.SeedsCollidedCallback(seedPositions);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            _playerAnimator.PlaySowAnimation();
            _currentCropField = other.GetComponent<CropField>();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopSowAnimation();
            _currentCropField = null;
        }
    }
}
