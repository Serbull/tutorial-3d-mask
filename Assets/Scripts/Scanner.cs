using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Transform _screenCenterPoint;
    [SerializeField] private ScannerDisplay _display;
    [SerializeField] private float _detectTime = 1;

    private Camera _camera;
    private HideItem _targetItem;
    private float _currentDetectTime;


    private void Start()
    {
        _camera = GetComponentInParent<Camera>();
    }

    private void Update()
    {
        Move();
        Scan();
        Detect();
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Ray r = _camera.ScreenPointToRay(Input.mousePosition);
            transform.position = r.origin + r.direction * 0.4f;
        }
    }

    private void Scan()
    {
        Vector3 scanDirection = _screenCenterPoint.position - _camera.transform.position;
        Ray ray = new Ray(_camera.transform.position, scanDirection);
        if (Physics.Raycast(ray, out RaycastHit hit, 20))
        {
            if (hit.collider.TryGetComponent(out HideItem hideItem))
            {
                _targetItem = hideItem;
                return;
            }
        }
        _targetItem = null;
    }

    private void Detect()
    {
        if (_targetItem == null)
        {
            if (_currentDetectTime > 0)
            {
                _display.StopDetect();
            }

            _currentDetectTime = 0;
            return;
        }

        if (_currentDetectTime == 0)
        {
            _display.StartDetect();
        }

        _currentDetectTime += Time.deltaTime;

        if (_currentDetectTime < _detectTime)
        {
            _display.Detecting();
            return;
        }

        _display.CompleteDetect();
        _targetItem.CompleteDetect();
    }
}