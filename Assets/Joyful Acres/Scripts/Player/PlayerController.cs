using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private MobileJoystick _mobileJoystick;

    private PlayerAnimator _playerAnimator;
    private CharacterController _characterController;

    [Header(" Settings ")]
    [SerializeField] private float _moveSpeed;


    

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        
        ManageMovement();
    }

    private void ManageMovement()
    {
        Vector3 moveVector = _mobileJoystick.GetMoveVector() * _moveSpeed * Time.deltaTime / Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;

        _characterController.Move(moveVector);

        _playerAnimator.ManageAnimations(moveVector);

        
    }

   
}
