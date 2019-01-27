using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    int lmTerrain;
    public static Movement instance;

    public float fallMultiplier;

    public float speed;
    public float acceleration;
    public float jumpHight;

    private bool grounded = true;
    private bool canJump = true;
    new private Rigidbody rigidbody;
    new private Collider collider;
    Camera cam;
    Vector3 camForward;
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        cam = Camera.main;
        instance = this;
        lmTerrain = LayerMask.GetMask("Terrain");
	}
	
	void FixedUpdate ()
    {
        //Get input into Vector3 for ease of use
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Check grounded
        grounded = IsGrounded();

        //if (grounded == false && rigidbody.velocity.y < 0)
          //  rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y * (1 + fallMultiplier * Time.deltaTime), rigidbody.velocity.z);


        if (input.magnitude > 0.05f)
        {
            collider.material.bounciness = 0;
            //Only jump if we've reset 'canJump' (every 0.05s) and we're on the ground
            if (grounded == true && canJump){
                rigidbody.AddForce(Vector3.up * jumpHight * Time.deltaTime, ForceMode.Impulse);
                StartCoroutine(DelayJump());
            }
        }
        else
            collider.material.bounciness = 1;

        //Orient the movement relative to the camera
        Vector3 direction = cam.transform.TransformDirection(input);
        direction.y = 0;
        direction.Normalize();

        //Turn the player in the direction they're moving
        if(direction.magnitude > 0)
            transform.forward = direction;

        //Set the velocity
        Vector3 vel = rigidbody.velocity + acceleration * direction * Time.deltaTime;
        vel = Vector3.ClampMagnitude(vel, speed);

        if (grounded == false && rigidbody.velocity.y < 0)
            vel.y = rigidbody.velocity.y * (1 + fallMultiplier * Time.deltaTime);

        rigidbody.velocity = vel;
	}

    bool IsGrounded(){
        return(Physics.BoxCast(transform.position, new Vector3(0.5f, 0.05f, 0.5f), Vector3.down, transform.rotation, 0.5f, lmTerrain, QueryTriggerInteraction.Ignore));
    }

    IEnumerator DelayJump(){
        canJump = false;
        yield return new WaitForSeconds(0.05f);
        canJump = true;
    }
}
