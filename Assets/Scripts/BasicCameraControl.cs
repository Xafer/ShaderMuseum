using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraControl : MonoBehaviour
{

    private Vector2 _movement2D;
    private Vector2 _mouseMovement;

    [SerializeField] private Camera _controlledCamera;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _movement2D = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _mouseMovement = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * Time.deltaTime * 300;

        _controlledCamera.transform.rotation *= Quaternion.Euler(_mouseMovement.y, _mouseMovement.x, 0);

        _controlledCamera.transform.eulerAngles = new Vector3(_controlledCamera.transform.eulerAngles.x, _controlledCamera.transform.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3();

        movement += _controlledCamera.transform.forward * _movement2D.y;
        movement += _controlledCamera.transform.right * _movement2D.x;

        movement *= Time.deltaTime * 4;

        transform.position += movement;
    }
}
