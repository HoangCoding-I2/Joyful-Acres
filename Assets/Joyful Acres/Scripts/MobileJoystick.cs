using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    public void ClickedOnJoystickZoneCallback()
    {
        Vector3 clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;
    }
}
