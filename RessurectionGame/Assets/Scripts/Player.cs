using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f,100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f,100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f,100f)] private float maxAirAcceleration = 20f;
    
    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    void Update() {
        direction.x = Input.GetAxisRaw("Horizontal");
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
    }

    void FixedUpdate() {
        onGround = ground.GetOnGround();
        velocity = rb.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        rb.velocity = velocity;
    }
}
