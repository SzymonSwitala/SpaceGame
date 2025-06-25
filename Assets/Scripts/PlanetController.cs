using Unity.VisualScripting;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    private Rigidbody2D rb;
    private float rotationSpeed;
    private bool rotateRight;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    private void FixedUpdate()
    {
        float direction = rotateRight ? 1f : -1f;
        float newRotation = rb.rotation + direction * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(newRotation);
    }
  
    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void SetPlanetSize(float size)
    {
       
        transform.localScale = new Vector2(size, size);
    }
    public void SetDirection(bool direction)
    {
        rotateRight = direction;
    }
}
