using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;
    [SerializeField] float zoomMargin = 1.5f;
    [SerializeField] float followSmoothness = 0.3f;
    [SerializeField] float zoomSmoothness = 0.3f;

    public Camera cam { get; private set; }
    private Vector3 velocity = Vector3.zero;
    private float targetRadious;
    private float target2Radious;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (target == null || target2 == null) return;

        Vector3 middlePoint = CalculateMiddlePoint();
        FollowTarget(middlePoint);

        float targetZoom = CalculateRequiredZoom();
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSmoothness * Time.deltaTime);
    }
    Vector3 CalculateMiddlePoint()
    {
        return (target.position + target2.position) * 0.5f;
    }
    float CalculateRequiredZoom()
    {

        float distance = Vector3.Distance(target.position, target2.position);
        float totalRadius = targetRadious + target2Radious;
        float requiredZoom = (distance + totalRadius) * 0.5f * zoomMargin;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        requiredZoom = Mathf.Max(requiredZoom, distance / (2f * screenRatio) * zoomMargin);

        return requiredZoom;
    }
    void FollowTarget(Vector3 targetPosition)
    {
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSmoothness);
    }

    public void SetTargets(Transform t1, Transform t2, float t1Radious, float t2Radious)
    {
        target = t1;
        target2 = t2;
        targetRadious = t1Radious;
        target2Radious = t2Radious;
    }

}
