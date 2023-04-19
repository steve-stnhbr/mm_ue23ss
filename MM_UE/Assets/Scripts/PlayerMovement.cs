using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    public float MovementSpeed = 0.3f;
    public float MaxMovementSpeed = 5;
    public float JumpHeight = 3;

    // A LayerMask that contains all Layers to considered for the OnGroundCheck
    public LayerMask jumpRaycastMask;
    // The distance the player has to have to the ground to be able to perform a Jump
    public float jumpDistanceToFloor;
    // The Amount of time a Player is not able to jump after a jump was performed in seconds
    public float JumpCooldownReset;

    float jumpCooldown;
    bool onGround;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        handleMovement();
        handleJump();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGroundCollision(collision))
        {
            onGround = true; 
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (isGroundCollision(collision))
        {
            onGround = false;
        }
    }

    bool isGroundCollision(Collision collision)
    {
        return collision.gameObject
            && collision.gameObject.layer == LayerMask.NameToLayer("ground");
    }

    void handleMovement()
    {
        Vector3 forceVector = new Vector3(Input.GetAxis("HumanHorizontal") * MovementSpeed, 0, Input.GetAxis("HumanVertical") * MovementSpeed);
        rigidbody.AddForce(forceVector, ForceMode.VelocityChange);
        Vector2 clampedXZ = Vector2.ClampMagnitude(new Vector2(rigidbody.velocity.x, rigidbody.velocity.z), MaxMovementSpeed);
        rigidbody.velocity = new Vector3(clampedXZ.x, rigidbody.velocity.y, clampedXZ.y);
    }

    void handleJump()
    {
        float jumpMultiplier = Input.GetAxis("HumanJump");
        if (jumpMultiplier <= 0 || !canJump() || jumpCooldown > 0) return;
        jumpCooldown = JumpCooldownReset;
        Debug.Log("Jump");
        Vector3 forceVector = new Vector3(0, Mathf.Max(jumpMultiplier * JumpHeight,0), 0);
        rigidbody.AddForce(forceVector, ForceMode.Impulse);
    }

    bool canJump()
    {
        return onGround;
        //return Physics.Raycast(rigidbody.transform.position, Vector3.down, 1f + jumpDistanceToFloor, jumpRaycastMask);
    }

}
