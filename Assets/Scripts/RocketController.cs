using UnityEngine;

public class RocketController : MonoBehaviour
{

    [SerializeField] private float acceleration = 10f;

    private bool isAttachedToPlanet = true;
    private float currentSpeed;

    private Rigidbody2D rb;
    private FixedJoint2D joint2D;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint2D = GetComponent<FixedJoint2D>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) DetachFromPlanet();

    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        if (isAttachedToPlanet == true) return;

        currentSpeed += acceleration * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + (Vector2)transform.up * currentSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Planet"))
        {
            AttachToPlanet(collision);
        }
    }

    private void AttachToPlanet(Collision2D planet)
    {

        if (isAttachedToPlanet) return;

        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        currentSpeed = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;

        Vector2 directionToPlanet = (planet.transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlanet.y, directionToPlanet.x) * Mathf.Rad2Deg;
        float targetAngle = baseAngle + 90f;
        rb.rotation = targetAngle;

        joint2D.enabled = true;
        joint2D.connectedBody = planet.rigidbody;

        isAttachedToPlanet = true;

    }

    void DetachFromPlanet()
    {
        if (isAttachedToPlanet == false) return;

        joint2D.connectedBody = null;
        joint2D.enabled = false;

        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;

        isAttachedToPlanet = false;
    }
}
