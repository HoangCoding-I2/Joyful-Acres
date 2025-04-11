using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;

    [Header(" Settings ")]
    private CropField _currentCropField;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();

        WaterParticles.OnWatersCollided += WaterColliedCallback;
        CropField.OnFullWatered += CropFieldFullyWateredCallback;
        _playerToolSelector.OnToolSelected += ToolSelectedCallback;
    }


    private void OnDestroy()
    {
        WaterParticles.OnWatersCollided -= WaterColliedCallback;
        CropField.OnFullWatered -= CropFieldFullyWateredCallback;
        _playerToolSelector.OnToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanWater())
        {
            _playerAnimator.StopWaterAnimation();
        }
    }
    private void CropFieldFullyWateredCallback(CropField cropField)
    {
        if (cropField == _currentCropField)
        {
            _playerAnimator.StopWaterAnimation();
        }
    }


    private void WaterColliedCallback(Vector3[] waterPositions)
    {
        if (_currentCropField == null)
        {
            return;
        }
        _currentCropField.WaterCollidedCallback(waterPositions);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }

    }
    private void EnteredCropField(CropField cropField)
    {
        if (_playerToolSelector.CanWater())
        {
            _playerAnimator.PlayWaterAnimation();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopWaterAnimation();
            _currentCropField = null;
        }
    }
}
