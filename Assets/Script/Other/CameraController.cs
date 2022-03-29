using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _Target;
    private float _Smoothing = 5f;
    private Vector3 _Offset;

    void Start()
    {
        _Offset = transform.position - _Target.position;
    }

    void Update()
    {
        Vector3 targetCamPos = _Target.position + _Offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Time.deltaTime * _Smoothing);
    }
}
