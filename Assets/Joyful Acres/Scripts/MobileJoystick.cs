using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform _joystickOutline;
    [SerializeField] private RectTransform _joystickKnob;

    [Header(" Settings ")]
    [SerializeField] private float _moveFactor;
    private Vector3 _move;
    private Vector3 clickedPosition;
    private bool _canControl;

    private void Start()
    {
        
        HideJoystick();
    }
    private void Update()
    {
        if (_canControl)
        {
            ControlJoystick();
        }
    }

    private void ControlJoystick()
    {
        var currentPosition = Input.mousePosition;
        var direction = currentPosition - clickedPosition;

        float moveMagnitude = direction.magnitude * _moveFactor / Screen.width;
        //Chia cho Screen.width trong trường hợp này là để chuẩn hóa giá trị di chuyển của joystick,
        //giúp đảm bảo trải nghiệm người dùng nhất quán trên mọi kích thước màn hình

        moveMagnitude = Mathf.Min(moveMagnitude, _joystickOutline.rect.width / 2);

        _move = direction.normalized * moveMagnitude;
        Vector3 targetPos = clickedPosition + _move;

        _joystickKnob.position = targetPos;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }
  

    public void ClickedOnJoystickZoneCallback()
    {
         clickedPosition = Input.mousePosition;
        _joystickOutline.position = clickedPosition;
        ShowJoystick();
    }
    private void ShowJoystick()
    {
        _joystickOutline.gameObject.SetActive(true);
        _canControl = true;
    }
    private void HideJoystick()
    {
        _joystickOutline.gameObject.SetActive(false);
        _canControl = false;

        _move = Vector3.zero;
    }

    public Vector3 GetMoveVector()
    {
        return _move;
    }
}
