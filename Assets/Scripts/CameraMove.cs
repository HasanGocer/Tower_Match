using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Touch touch;
    Vector2 _firstPos;
    Vector2 _targetPos;
    public Vector2 maxTargetPos;
    public float yDistance;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _firstPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (_firstPos.y != touch.position.y)
                    {
                        _targetPos.y += (touch.position.y - _firstPos.y) / (Camera.main.pixelHeight / 3);
                        _targetPos.y = Mathf.Clamp(_targetPos.y, 0, maxTargetPos.y);
                    }

                    if (_firstPos.x != touch.position.x)
                    {
                        transform.localRotation = Quaternion.Euler()
                    }

                    _firstPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    _firstPos = Vector2.zero;
                    break;
            }
        }
    }
}
