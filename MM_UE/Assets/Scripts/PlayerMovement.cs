using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    public float MovementSpeed = 0.3f;
    public float MaxMovementSpeed = 5;
    public float JumpHeight = 3;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        handleMovement();
        handleJump();
    }

    void handleMovement()
    {
        Vector3 forceVector = new Vector3(Input.GetAxis("HumanHorizontal") * MovementSpeed, 0, Input.GetAxis("HumanVertical") * MovementSpeed);
        rigidbody.AddForce(forceVector, ForceMode.VelocityChange);
        Vector2 clampedXZ = Vector2.ClampMagnitude(new Vector2(rigidbody.velocity.x, rigidbody.velocity.z), MaxMovementSpeed);
        rigidbody.velocity = new Vector3(clampedXZ.x, rigidbody.velocity.y, clampedXZ.y);

        /*Vector3 currentPosition = rigidbody.transform.position;
            rigidbody.transform.position = new Vector3(currentPosition.x + Input.GetAxis("HumanHorizontal")*MOVEMENT_SPEED, 
            currentPosition.y, 
            currentPosition.z + Input.GetAxis("HumanVertical") * MOVEMENT_SPEED);*/
        
    }

    void handleJump()
    {
        Vector3 forceVector = new Vector3(0, Mathf.Max(Input.GetAxis("HumanJump") * JumpHeight,0), 0);
        rigidbody.AddForce(forceVector, ForceMode.Impulse);
    }

    
}
