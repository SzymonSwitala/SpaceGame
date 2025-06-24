using Unity.VisualScripting;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] bool rotateRight = true;

    Rigidbody2D rb;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rotateRight)
        {

            float newRotation = rb.rotation + rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(newRotation);

        }
        else
        {
            float newRotation = -rb.rotation + rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(newRotation);
        }
       
    }
}
