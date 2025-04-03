using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSowAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CropField"))
        {
            _playerAnimator.PlaySowAnimation();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            _playerAnimator.StopSowAnimation();
        }
    }
}
