using UnityEngine;
public class TwoTargetCamera : MonoBehaviour
{
  
    [SerializeField] private float followSmoothness = 0.3f;
    [SerializeField] private float zoomSmoothness = 0.3f;
    [SerializeField] private float zoomMargin = 1.5f;

    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Transform target1;
    private Transform target2;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (target1 == null || target2 == null) return;

        Vector3 middlePoint = CalculateMiddlePoint();
        FollowTarget(middlePoint);

        float targetZoom = CalculateRequiredZoom();
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSmoothness * Time.deltaTime);

    }
    private Vector3 CalculateMiddlePoint()
    {
        return (target1.position + target2.position) * 0.5f;
    }
    void FollowTarget(Vector3 targetPosition)
    {
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSmoothness);
    }
    float CalculateRequiredZoom()
    {
        float distance = Vector3.Distance(target1.position, target2.position);
        float requiredZoom = distance * 0.5f * zoomMargin;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        requiredZoom = Mathf.Max(requiredZoom, distance / (2f * screenRatio) * zoomMargin);

        return requiredZoom;
    }
    public void SetTarget1(Transform target)
    {
        target1 = target;
    }
    public void SetTarget2(Transform target)
    {
        target2 = target;
    }
}