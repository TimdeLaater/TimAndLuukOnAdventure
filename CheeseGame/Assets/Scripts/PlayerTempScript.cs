using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTempScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    private Rigidbody rb;
    public TextMeshProUGUI text;

    public TurnManager tm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject startCurb = GameObject.FindGameObjectWithTag("StartCurb");
        Collider colliderCurb = startCurb.GetComponent<Collider>();
        Physics.IgnoreCollision(colliderCurb,GetComponent<Collider>());
        tm.initializeTurns();

        text.text = "Player " + (tm.playerData.turn + 1).ToString() + " score: " + tm.playerData.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.G))
        {
            tm.switchTurns();
            text.text = "Player " + (tm.playerData.turn + 1).ToString() + " score: " + tm.playerData.score.ToString();
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Score"))
        {
            ScoringScript sc = other.gameObject.GetComponent<ScoringScript>();
            tm.playerData.score += sc.points;
            tm.switchTurns();
            text.text = "Player " + (tm.playerData.turn + 1).ToString() + " score: " + tm.playerData.score.ToString();
           
        }
    }


}
