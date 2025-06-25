using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target,target2;
    public float zoomMargin = 1.5f;
    public float followSmoothness,zoomSmoothness = 0.3f;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (target == null|| target2==null) return;

        Vector3 middlePoint= CalculateMiddlePoint();
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
        float requiredZoom = distance * 0.5f * zoomMargin;

        return requiredZoom;
    }
    void FollowTarget(Vector3 targetPosition)
    {
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSmoothness);
    }

    public void SetTargets(Transform t1,Transform t2)
    {
        target = t1;
        target2 = t2;
    }

}
