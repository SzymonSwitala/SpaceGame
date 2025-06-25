using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera cam { get; private set; }
    public FollowTargetCamera followTargetCamera { get; private set; }
    public TwoTargetCamera twoTargetCamera { get; private set; }
  
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        followTargetCamera = GetComponent<FollowTargetCamera>();
        twoTargetCamera = GetComponent<TwoTargetCamera>();
    }
    public bool IsOffscreen(Transform target)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(target.position);
        bool isOffScreen = viewportPos.x < 0 || viewportPos.x > 1 ||
                           viewportPos.y < 0 || viewportPos.y > 1;
        return isOffScreen;
    }
}
