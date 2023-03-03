using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerObj;
    private float speed;
    public float acceleration;
    public float topSpeed;
    public float startSpeed;
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

    public GameObject ball;
    public GameObject energy;
    public GameObject sparks;
    public GameObject trail;
    public ParticleSystem fizzleOut;

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    float distTravelled;
    PathCreator oldPath;

    public Transform restPos;

    private void Start()
    {
        speed = startSpeed;
    }
    private void Update()
    {
        //pathCreator = currentGround.GetComponentInChildren<PathCreator>();
        if (GameManager.Instance.gameRunning)
        {
            speed += Time.deltaTime * acceleration;

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
                SoundManager.Instance.PlaySound(SoundManager.Instance.jumpSound);
            }
            if (Input.GetButtonUp("Jump") || jumpTimer >= maxJumpTime)
            {
                jumpTimer = 0f;
                isHoldingJump = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.gameRunning)
        {

            //Move the player forward
            //Vector3 currPos = this.transform.position;
            //Vector3 newPos = currPos + (this.transform.forward * speed * Time.fixedDeltaTime);
            //this.transform.position = newPos;
            if (pathCreator != null)
            {
                distTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distTravelled, end);

                //Attract the rigidbody to the ground
                gravity = new Vector3(rb.transform.position.x - pathCreator.path.GetPointAtDistance(distTravelled, end).x, rb.transform.position.y - pathCreator.path.GetPointAtDistance(distTravelled, end).y, 0f).normalized;
                gravity *= gravForce;
                if (rb.transform.localPosition.y >= restPos.localPosition.y)
                {
                    Debug.Log("RB: " + rb.transform.localPosition.y + " | REST: " + restPos.localPosition.y);
                    Debug.Log(gravity);
                    rb.AddForce(gravity);
                }
            }
            else
            {
                Collider[] hits = Physics.OverlapSphere(playerObj.transform.position, 3f, groundLayer);
                foreach (Collider c in hits)
                {
                    PathCreator newPath = c.gameObject.GetComponentInChildren<PathCreator>();
                    //Debug.Log(newPath);
                    if (newPath != null && !newPath.Equals(pathCreator) && !newPath.Equals(oldPath))
                    {
                        //Debug.Log("Bboooopp");
                        oldPath = pathCreator;
                        pathCreator = newPath;
                        distTravelled = 0f;
                    }
                }
            }
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
            else
            {
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

            if (isHoldingJump)
            {
                rb.AddForce(jumpForce * playerObj.transform.up);
            }
            
            // *******
           // Vector3 localVelocity = rb.transform.InverseTransformDirection(rb.velocity);
            //localVelocity.x = 0;
            //localVelocity.y = 0;

            //rb.velocity = transform.TransformDirection(localVelocity);
            // *******

            //Move Player back to rest position if not jumping
            if (grounded && rb.position != restPos.position) {
                rb.MovePosition(restPos.localPosition);
                //Debug.Log("Unstuck: " + rb.position);
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (other.gameObject.layer == 6) //Ground layer is 6
        {
            Debug.Log("Boop");
            PathCreator newPath = other.gameObject.GetComponentInChildren<PathCreator>();
            if (newPath != null && !newPath.Equals(pathCreator) && !newPath.Equals(oldPath)) {
                Debug.Log("Bboooopp");
                oldPath = pathCreator;
                pathCreator = newPath;
                distTravelled = 0f;
            }
        } 
    }
}
