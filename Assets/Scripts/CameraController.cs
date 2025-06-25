using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float zoomValue;
    [SerializeField] private float zoomSpeed;
    private float currentVelocity;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {

        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + (Vector3)offset;
        transform.position = targetPosition;

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomValue, ref currentVelocity, zoomSpeed * Time.deltaTime);
    }
    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void SetNewZoomValue(float value)
    {
        zoomValue = value;
    }
}
