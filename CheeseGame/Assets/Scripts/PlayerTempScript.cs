using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTempScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    private int score;
    private Rigidbody rb;
    public TextMeshProUGUI text;

    void Start()
    {
        score = 0;
        rb = GetComponent<Rigidbody>();
        GameObject startCurb = GameObject.FindGameObjectWithTag("StartCurb");
        Collider colliderCurb = startCurb.GetComponent<Collider>();
        Physics.IgnoreCollision(colliderCurb,GetComponent<Collider>());
        
        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Score"))
        {
            ScoringScript sc = other.gameObject.GetComponent<ScoringScript>();
            score += sc.points;
            text.text = score.ToString();

        }
    }
}
