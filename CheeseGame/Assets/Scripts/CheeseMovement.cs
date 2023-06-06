using UnityEngine;

public class CheeseMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed; // Speed of the cheese
    public float rollSpeedThreshold; // Minimum speed required to prevent the cheese from falling
    public float rollingResistance; // Resistance to slow down the roll
    public float throwForceScale = 0.2f; // Scaling factor for throw force
    public float maxVerticalThrowForce = 5f; // Maximum value for the throw force in the vertical direction
    public BoxCollider boxCollider; // Reference to the BoxCollider component

    private Vector3 mouseDownPosition; // Mouse position when mouse button is pressed down
    private bool isMousePressed; // Flag to track if the mouse button is pressed
    private bool hasThrown; // Flag to track if the cheese has been thrown

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMousePressed = false;
        hasThrown = false;
    }

    private void Update()
    {
        if (!hasThrown && Input.GetMouseButtonDown(0))
        {
            // Store the mouse position when the mouse button is pressed down
            mouseDownPosition = Input.mousePosition;
            isMousePressed = true;
        }
        else if (isMousePressed && Input.GetMouseButtonUp(0))
        {
            // Calculate the difference between the current mouse position and the mouse position when the button was pressed down
            Vector3 mouseUpPosition = Input.mousePosition;
            Vector3 throwForce = (mouseUpPosition - mouseDownPosition) * throwForceScale;

            // Limit the vertical throw force
            throwForce.y = Mathf.Clamp(throwForce.y, -maxVerticalThrowForce, maxVerticalThrowForce);

            // Apply the throw force to the cheese
            rb.AddForce(throwForce, ForceMode.Impulse);

            isMousePressed = false;
            hasThrown = true;
        }
    }

    private void FixedUpdate()
    {
        if (!hasThrown)
        {
            // Get the horizontal and vertical inputs
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Create a vector3 for the movement
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

            // Apply the movement force to the cheese within the box collider
            if (boxCollider.bounds.Contains(transform.position))
            {
                rb.AddForce(movement * speed, ForceMode.Force);
            }

            // Apply rolling resistance to slow down the roll
            Vector3 angularVelocity = rb.angularVelocity;
            Vector3 resistanceTorque = -angularVelocity.normalized * rollingResistance;
            rb.AddTorque(resistanceTorque);

            // Check if the roll speed falls below the threshold
            if (rb.angularVelocity.magnitude < rollSpeedThreshold)
            {
                // Disable constraints and allow the cheese to fall over
                rb.constraints = RigidbodyConstraints.None;
            }
            else
            {
                // Enable constraints to keep the cheese rolling
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
