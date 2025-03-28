using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float acceleration = 10f;
    public Rigidbody2D rb;
    public Animator animator;

    // For crate pulling
    private bool isHoldingCrate = false;
    private Rigidbody2D crateRb;
    private FixedJoint2D crateJoint;
    public float grabDistance = 1f;  // Adjustable grab distance

    private Vector2 moveInput;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput.magnitude > 0)
        {
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);
        }
        animator.SetBool("isMoving", moveInput.magnitude > 0);

        if (Input.GetKeyDown(KeyCode.F))
            TryGrabCrate();
        else if (Input.GetKeyUp(KeyCode.F))
            ReleaseCrate();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        Vector2 targetSpeed = moveInput.normalized * maxSpeed;
        Vector2 speedDifference = targetSpeed - rb.linearVelocity;
        Vector2 forceVector = speedDifference * acceleration;

        rb.AddForce(forceVector, ForceMode2D.Force);
    }

    void TryGrabCrate()
    {
        // Detect crates within grab distance
        Collider2D crateCollider = Physics2D.OverlapCircle(transform.position, grabDistance, LayerMask.GetMask("Crate"));

        if (crateCollider != null)
        {
            crateRb = crateCollider.GetComponent<Rigidbody2D>();

            if (crateRb != null)
            {
                crateJoint = gameObject.AddComponent<FixedJoint2D>();
                crateJoint.connectedBody = crateRb;
                crateJoint.autoConfigureConnectedAnchor = false;
                crateJoint.connectedAnchor = Vector2.zero;
                crateJoint.frequency = 5f;
                crateJoint.dampingRatio = 1f;

                isHoldingCrate = true;

                Debug.Log("?? Crate grabbed successfully.");
            }
        }
        else
        {
            Debug.Log("?? No crate within grabbing range.");
        }
    }

    void ReleaseCrate()
    {
        if (isHoldingCrate)
        {
            if (crateJoint != null)
                Destroy(crateJoint);

            crateRb = null;
            isHoldingCrate = false;

            Debug.Log("?? Crate released.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabDistance);
    }
}
