using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Script : MonoBehaviour
{
    public Camera _camera;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    
    void Update()
    {
        _transform.position = new Vector3(_transform.position.x, _camera.transform.position.y + 121, _transform.position.z);
    }

    private void FixedUpdate()
    {
    }
}
