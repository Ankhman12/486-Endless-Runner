using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerObj;
    public float speed;
    public float jumpForce;
    public Rigidbody rb;
    public Vector3 rotationAmount;
    public int coinsCollected;
    public GameObject currentGround;
    public float gravForce;
    public float maxJumpTime = 0.4f;
    public LayerMask groundLayer;

    bool turningLeft;
    bool turningRight;
    [SerializeField] bool grounded;
    Vector3 gravity;
    bool isHoldingJump;
    float jumpTimer = 0f;


    // Update is called once per frame
    void Update()
    {
        //Move the player forward
        Vector3 currPos = this.transform.position;
        Vector3 newPos = currPos + (this.transform.forward * speed * Time.deltaTime);
        this.transform.position = newPos;

        //Attract the rigidbody to the ground
        gravity = new Vector3(rb.transform.position.x - currentGround.transform.position.x, rb.transform.position.y - currentGround.transform.position.y, 0f).normalized;
        gravity *= gravForce;
        rb.AddForce(gravity);

        //Ground check
        RaycastHit hit;
        //Physics.Raycast(playerObj.transform.position, -playerObj.transform.up, out hit, 10f);
        //Debug.DrawRay(playerObj.transform.position, -playerObj.transform.up, Color.red);
        if (Physics.Raycast(playerObj.transform.position, -playerObj.transform.up, out hit))
        {
            Debug.Log("fdfsff");
            if (hit.collider.gameObject.layer == groundLayer)
            {
                grounded = true;
            }
        }
        else {
            grounded = false;
        }

        //Allow Player to rotate
        if (Input.GetKeyDown(KeyCode.A)) {
            turningLeft = true; 
        }
        if (Input.GetKeyUp(KeyCode.A)) { 
            turningLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            turningRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            turningRight = false;
        }

        if (turningLeft && !turningRight) {
            this.transform.Rotate(rotationAmount);
        }
        if (!turningLeft && turningRight)
        {
            this.transform.Rotate(-rotationAmount);
        }

        //Allow Player to jump
        jumpTimer += Time.deltaTime;
        if (grounded && Input.GetButtonDown("Jump")) 
        {
            isHoldingJump = true;
        }
        if (Input.GetButtonUp("Jump") || jumpTimer >= maxJumpTime)
        {
            jumpTimer = 0f;
            isHoldingJump = false;
        }
        if (isHoldingJump) {
            rb.AddForce(jumpForce * playerObj.transform.up);
        }
    }
}
