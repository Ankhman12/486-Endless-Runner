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
    public LayerMask playerLayer;

    bool turningLeft;
    bool turningRight;
    [SerializeField] bool grounded;
    Vector3 gravity;
    [SerializeField] bool isHoldingJump;
    [SerializeField] float jumpTimer = 0f;

    int health = 3;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject energy;
    [SerializeField] GameObject sparks;
    [SerializeField] ParticleSystem fizzleOut;

    private void Update()
    {
        //Allow Player to rotate
        if (Input.GetKeyDown(KeyCode.A))
        {
            turningLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
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

       

        //Allow Player to jump
        jumpTimer += Time.deltaTime;
        if (grounded && Input.GetButtonDown("Jump"))
        {
            isHoldingJump = true;
            jumpTimer = 0f;
        }
        if (Input.GetButtonUp("Jump") || jumpTimer >= maxJumpTime)
        {
            jumpTimer = 0f;
            isHoldingJump = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move the player forward
        Vector3 currPos = this.transform.position;
        Vector3 newPos = currPos + (this.transform.forward * speed * Time.fixedDeltaTime);
        this.transform.position = newPos;

        //Attract the rigidbody to the ground
        gravity = new Vector3(rb.transform.position.x - currentGround.transform.position.x, rb.transform.position.y - currentGround.transform.position.y, 0f).normalized;
        gravity *= gravForce;
        rb.AddForce(gravity);

        //Ground check
        RaycastHit hit;
        //Physics.Raycast(playerObj.transform.position, -playerObj.transform.up, out hit, 10f);
        //Debug.DrawRay(playerObj.transform.position, -playerObj.transform.up, Color.red);
        if (Physics.Raycast(playerObj.transform.position, -playerObj.transform.up, out hit, .1f, ~playerLayer))
        {
            //Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == 6) //ground layer is layer 6
            {
                //Debug.Log("fSKfhosdhfo;AWHR");
                grounded = true;
            }
        }
        else {
            grounded = false;
        }

        if (turningLeft && !turningRight)
        {
            this.transform.Rotate(rotationAmount);
        }
        if (!turningLeft && turningRight)
        {
            this.transform.Rotate(-rotationAmount);
        }

        if (isHoldingJump) {
            rb.AddForce(jumpForce * playerObj.transform.up);
        }
    }

    public int PlayerHit() 
    {
        health--;
        switch (health) {
            case 2:
                sparks.SetActive(false);
                break;
            case 1:
                ball.SetActive(false);
                break;
            default:
                break;
        }

        return health;
    }

    public bool Fizzle() 
    {
        energy.SetActive(false);
        Instantiate(fizzleOut, this.transform);
        Destroy(this, .1f);
        return true;
    }
}
