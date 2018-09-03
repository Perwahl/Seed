using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float distanceToTarget;
    public Transform target;
    public float rotateSpeed = 1f;
    public float transitionSpeed;

    public bool invertHoriz;
    public bool invertVertical;

    private Transform prevTarget;
    private bool changingTarget;
    private Quaternion targetRot;
    private Quaternion prevRot;

    private bool movingToPoint;
    private Vector3 targetPos;

    private float t;
    [SerializeField] private float maxZoomOut = 5f;
    [SerializeField] private float maxZoomIn = 5f;
    private float targetAngle;

    // Update is called once per frame

    void Start()
    {
        cam.transform.LookAt(target);
        prevTarget = target;
    }

    void LateUpdate()
    {
        distanceToTarget = Vector3.Distance(transform.position, cam.transform.position);

        if (changingTarget)
        {
            t += Time.deltaTime * transitionSpeed;
            transform.rotation = Quaternion.Slerp(prevRot, targetRot, t);

            if (target.position == Vector3.zero)
            {
                //Zoom out
                Camera.main.fieldOfView = Camera.main.fieldOfView + Time.deltaTime * transitionSpeed * 3f;
                Camera.main.orthographicSize = Camera.main.orthographicSize + Time.deltaTime * transitionSpeed * 3f;
                Vector3 normalizedTransform = transform.position;
                normalizedTransform = normalizedTransform.normalized * 15f;
                transform.position = normalizedTransform;
            }
            else
            {
                //Zoom in
                Camera.main.fieldOfView = Camera.main.fieldOfView - Time.deltaTime * transitionSpeed * 3f;
                Camera.main.orthographicSize = Camera.main.orthographicSize - Time.deltaTime * transitionSpeed * 3f;
            }
            if (Camera.main.orthographicSize > maxZoomOut)
            {
                Camera.main.orthographicSize = maxZoomOut;
            }
            if (Camera.main.orthographicSize < 1.5f)
            {
                Camera.main.orthographicSize = 1.5f;
            }
            if (t >= 1)
            {
                changingTarget = false;
                prevTarget = target;
            }
            return;
        }
        if (movingToPoint)
        {
            //t += Time.deltaTime * transitionSpeed * .3f;
            targetAngle -= Time.deltaTime * transitionSpeed * 25f;
            //Debug.Log("Current Angle: " + targetAngle);
            transform.position = Vector3.RotateTowards(transform.position, targetPos, (Time.deltaTime * transitionSpeed * 25f) * Mathf.Deg2Rad, 0f);
            transform.LookAt(Vector3.zero);

            if (targetAngle <= 0)
            {
                movingToPoint = false;
            }
            return;
        }

        //zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distanceToTarget < maxZoomOut)
            {
                cam.transform.position = cam.transform.position + (cam.transform.forward * -1) * (distanceToTarget * 0.1f);
            }            
        }

        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distanceToTarget > maxZoomIn)
            {
                cam.transform.position = cam.transform.position + (cam.transform.forward) * (distanceToTarget * 0.1f);
            }
        }

        if (Input.GetMouseButton(0))
        {
            float speed = rotateSpeed * distanceToTarget * Time.deltaTime;
            var x = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
            Quaternion yaw = Quaternion.Euler(0f, Input.GetAxis("Mouse X") * speed, 0f);
            transform.rotation = yaw * transform.rotation; // yaw on the left.
            var y = new Vector3(Input.GetAxis("Mouse Y") * -1, 0.0f, 0.0f);

            Quaternion pitch = Quaternion.Euler(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
            transform.rotation = transform.rotation * pitch; // pitch on the right.
        }
    }

    /// <summary>
    /// Moves the camera to focus over a certain point on the surface of the sphere.
    /// </summary>
    /// <param name="pos">Position on sphere to move to.</param>
    public void moveToPointOnSphere(Vector3 pos)
    {
        if (target.position != Vector3.zero)
        {
            //setTarget(Vector3.zero);		
        }
        targetAngle = Vector3.Angle(transform.position, pos);

        t = targetAngle * Time.deltaTime * (1f / transitionSpeed);
        prevRot = transform.rotation;
        targetPos = pos;
        //t = 0f;
        movingToPoint = true;
    }

    /// <summary>
    /// Sets the new rotational center of camera to focus on different celestial body.
    /// </summary>
    /// <param name="pos">Position of new rotational center.</param>
    public void setTarget(Transform newTarget)
    {
        target = newTarget;
        moveToNewTarget();
    }

    private void moveToNewTarget()
    {
        if (target != prevTarget)
        {
            //PlayerUI.instance.showCamResetButton();
            Vector3 dir = target.position - transform.position;
            targetRot = Quaternion.LookRotation(dir, transform.up);
            prevRot = transform.rotation;
            t = 0f;
            changingTarget = true;
        }
    }
}
