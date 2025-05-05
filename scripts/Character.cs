using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform _head;                                                     
    [SerializeField] private Transform _body;                                                    
    [SerializeField] private float _horizontalSens = 9.0f;                                       
    [SerializeField] private float _verticalSens = 9.0f;                                         
    [SerializeField] private float _minVerticalAngle = -45.0f;                                   
    [SerializeField] private float _maxVerticalAngle = 45.0f;                                    
    [SerializeField] private bool _invertYAxis = false;                                          

    private float _mousePositionX = 0f;                                                          
    private float _mousePositionY = 0f;                                                          

    private void Awake()                                                                        
    {
        _mousePositionX = transform.eulerAngles.y;                                               
        _mousePositionY = transform.eulerAngles.x;                                               // 11.2
    }

    private void Update()                                                                        // 12.0
    {
        LookInput();                                                                             // 12.1
    }

    private void LookInput()                                                                     // 13.0
    {
        _mousePositionX += Input.GetAxis("Mouse X") * _horizontalSens;                           // 13.1

        _mousePositionY += Input.GetAxis("Mouse Y") * _verticalSens * (_invertYAxis ? 1f : -1f); // 13.2
        _mousePositionY = Mathf.Clamp(_mousePositionY, _minVerticalAngle, _maxVerticalAngle);    // 13.3

        _head.localEulerAngles = new Vector3(_mousePositionY, 0, 0);                             // 13.4

        _body.eulerAngles = new Vector3(0, _mousePositionX, 0);                                  // 13.5
    }
}
