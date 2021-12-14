using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedFlyCamZoom : MonoBehaviour
{
    public float zoomSensitivity = 60.0f;
    public float zoomSpeed = 10.0f;
    public float zoomMin = 5.0f;
    public float zoomMax = 100.0f;

    private float zoom;
    public Camera cam;

    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float fastMoveFactor = 3;

    Vector3 input;

    void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
        zoom = cam.fieldOfView;
    }

    void Update()
    {
        // Zoom Camera
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        input = new Vector2( Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * input.x * Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * input.y * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * normalMoveSpeed * input.x * Time.deltaTime;
            transform.position += transform.right * normalMoveSpeed * input.y * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.E)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
    }

    void LateUpdate()
    {
        // Zoom Camera
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * zoomSpeed);
    }

}