using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float speed;
    private Rigidbody rb;

    private bool reverse = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = reverse ? Vector3.right : Vector3.left;
        movement *= moveSpeed;

        // Apply the movement to the rigidbody
        rb.velocity = movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            reverse = !reverse;
        }
    }
}
