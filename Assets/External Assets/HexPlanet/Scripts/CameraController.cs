using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public Transform centerPoint;      //center point of the planet that the camera rotates around
    public Transform cameraPivot;                // camera pivot transform. zoom rotations are applied here
    public Transform cameraPosition;            // camera position
    public float distanceToTarget;
    public float rotateSpeed = 1f;

    public bool invertHoriz;
    public bool invertVertical;
    private Quaternion targetRot;
    private Quaternion prevRot;

    [SerializeField] private float maxZoomOut = 5f;
    [SerializeField] private float maxZoomIn = 5f;
    [SerializeField] private float zoomInAngleChangepoint = 5f;    

    void Start()
    {
        cameraPivot.LookAt(centerPoint);
    }

    void LateUpdate()
    {
        distanceToTarget = Vector3.Distance(centerPoint.position, cameraPosition.position);
       
        //zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distanceToTarget < maxZoomOut)
            {
                cameraPosition.position = cameraPosition.position + ((cameraPosition.forward * -1) * (distanceToTarget * 0.1f));
            }

            UpdateZoomAngle();
        }

        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distanceToTarget > maxZoomIn)
            {
                cameraPosition.position = cameraPosition.position + ((cameraPosition.forward) * (distanceToTarget * 0.1f));
            }

            UpdateZoomAngle();

        }

        if (Input.GetMouseButton(0))
        {
            float speed = rotateSpeed * distanceToTarget * Time.deltaTime;
            var x = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
            Quaternion yaw = Quaternion.Euler(0f, Input.GetAxis("Mouse X") * speed, 0f);
            centerPoint.rotation = yaw * centerPoint.rotation; // yaw on the left.
            var y = new Vector3(Input.GetAxis("Mouse Y") * -1, 0.0f, 0.0f);

            Quaternion pitch = Quaternion.Euler(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
            centerPoint.rotation = centerPoint.rotation * pitch; // pitch on the right.
        }
    }

    private void UpdateZoomAngle()
    {
        var angle = Mathf.Lerp(0, -30, (zoomInAngleChangepoint-distanceToTarget+maxZoomIn) / zoomInAngleChangepoint);
        Debug.Log(angle);

         cameraPivot.rotation = cameraPosition.rotation;

        cameraPivot.Rotate(Vector3.right, angle);
    }
}
