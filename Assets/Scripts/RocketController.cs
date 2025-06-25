using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 10f;

    [Header("Camera Settings")]
    [SerializeField] private float attachedZoomValue;
    [SerializeField] private float detachedZoomValue;

    [Header("References")]
   // [SerializeField] private CameraController cameraController;
    [SerializeField] private GameManager gameManager;

    private Rigidbody2D rb;
    private FixedJoint2D joint2D;
    private bool isAttachedToPlanet=true;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        joint2D = GetComponent<FixedJoint2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) DetachFromPlanet();

    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        if (isAttachedToPlanet == true) return;

        currentSpeed += acceleration * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + (Vector2)transform.up * currentSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Planet"))
        {
            GameObject planet = collision.gameObject;
            AttachToPlanet(planet);
        }
    }

    private void AttachToPlanet(GameObject planet)
    {

        if (isAttachedToPlanet) return;
        ResetRocketPhysics();
        rb.bodyType = RigidbodyType2D.Dynamic;

        AlignRocketToPlanet(planet);

        joint2D.enabled = true;
        joint2D.connectedBody = planet.GetComponent<Rigidbody2D>();

       // cameraController.SetNewTarget(planet.transform);
       // cameraController.SetNewZoomValue(attachedZoomValue);
        isAttachedToPlanet = true;

        gameManager.ReplacePlanet();

    }

    private void DetachFromPlanet()
    {
        if (isAttachedToPlanet == false) return;

        joint2D.connectedBody = null;
        joint2D.enabled = false;

        ResetRocketPhysics();

       // cameraController.SetNewTarget(transform);
       // cameraController.SetNewZoomValue(detachedZoomValue);
        isAttachedToPlanet = false;
    }

    private void ResetRocketPhysics()
    {
        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        currentSpeed = 0;
    }

    private void AlignRocketToPlanet(GameObject planet)
    {
        Vector2 directionToPlanet = (planet.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlanet.y, directionToPlanet.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }
}
