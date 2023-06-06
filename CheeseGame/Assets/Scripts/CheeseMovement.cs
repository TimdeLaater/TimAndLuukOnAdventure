using UnityEngine;

public class CheeseMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed; // Speed of the cheese
    public float rollSpeedThreshold; // Minimum speed required to prevent the cheese from falling
    public float rollingResistance; // Resistance to slow down the roll
    public float horizontalTorqueScale = 0.5f; // Scaling factor for horizontal torque
    public float maxVelocity; // Maximum velocity limit
    public BoxCollider boxCollider; // Reference to the BoxCollider component

   
    private bool hasThrown; // Flag to track if the cheese has been thrown

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hasThrown = false;
        GameObject startCurb = GameObject.FindGameObjectWithTag("StartCurb");
        Collider colliderCurb = startCurb.GetComponent<Collider>();
        Physics.IgnoreCollision(colliderCurb, GetComponent<Collider>());
    }


    private void OnTriggerExit(Collider other)
    {
        hasThrown= true;
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
            
            rb.AddForce(movement * speed, ForceMode.Force);

            //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
            

            // Apply rolling resistance to slow down the roll
            Vector3 angularVelocity = rb.angularVelocity;

            // Reduce the effect of horizontal movement on angular velocity
            Vector3 horizontalTorque = Vector3.up * horizontal * horizontalTorqueScale;
            Vector3 resistanceTorque = -angularVelocity.normalized * rollingResistance;
            rb.AddTorque(resistanceTorque + horizontalTorque);

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
