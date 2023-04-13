using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoSingleton<CameraMove>
{
    Touch touch;
    Vector2 _firstPos;
    [SerializeField] Transform _target;
    Vector2 _targetPos;
    public Vector2 maxTargetPos;
    [SerializeField] float _CamDistance;
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
                    ObjectManager.Instance.isFree = true;
                    break;

                case TouchPhase.Moved:
                    if (30 < Mathf.Abs(_firstPos.y - touch.position.y))
                    {
                        _targetPos.y -= (_firstPos.y - touch.position.y) / (Camera.main.pixelHeight);
                        _targetPos.y = Mathf.Clamp(_targetPos.y, -maxTargetPos.y, 0);
                        _target.position = new Vector3(_target.position.x, _targetPos.y, _target.position.z);
                    }

                    if (10 < Mathf.Abs(_firstPos.x - touch.position.x))
                    {
                        _targetPos.x -= (touch.position.x - _firstPos.x) / (Camera.main.pixelWidth / 200);
                        _target.transform.rotation = Quaternion.Euler(new Vector3(0, _targetPos.x, 0));
                    }

                    _firstPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    _firstPos = Vector2.zero;
                    ObjectManager.Instance.isFree = false;
                    break;
            }
        }
    }
}
