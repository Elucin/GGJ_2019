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
    private Coroutine squachAndSquich;

    public ParticleSystem dustParticle;
    public ParticleSystem splashParticle;
    Camera cam;
    Vector3 camForward;

    bool inWater = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        cam = Camera.main;
        instance = this;
        lmTerrain = LayerMask.GetMask("Terrain");
    }

    void FixedUpdate()
    {
        //Get input into Vector3 for ease of use
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Check grounded
        grounded = IsGrounded();

        

        if (squachAndSquich == null && grounded == false){
            
            squachAndSquich = StartCoroutine(SquachAndStrech());
        }

        if (input.magnitude > 0.05f)
        {
            if(grounded){
                if(!dustParticle.isPlaying && !splashParticle.isPlaying){
                    if(!inWater) {dustParticle.Play();}
                    else {splashParticle.Play();}
                }
            }
            collider.material.bounciness = 0;
            //Only jump if we've reset 'canJump' (every 0.05s) and we're on the ground
            if (grounded == true && canJump) {
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
        if (direction.magnitude > 0)
            transform.forward = direction;

        //Set the velocity
        Vector3 vel = rigidbody.velocity + acceleration * direction * Time.deltaTime;
        vel = Vector3.ClampMagnitude(vel, speed);

        if (grounded == false && rigidbody.velocity.y < 0)
            vel.y = rigidbody.velocity.y * (1 + fallMultiplier * Time.deltaTime);

        rigidbody.velocity = vel;
    }

    bool IsGrounded() {
        return (Physics.BoxCast(transform.position, new Vector3(0.5f, 0.05f, 0.5f), Vector3.down, transform.rotation, 0.5f, lmTerrain, QueryTriggerInteraction.Ignore));
    }

    IEnumerator DelayJump() {
        canJump = false;
        yield return new WaitForSeconds(0.05f);
        canJump = true;
    }

    IEnumerator SquachAndStrech()
    {
        yield return new WaitForSeconds(0.05f);

        while (grounded == false)
            yield return null;

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 1/1.2f, transform.localScale.z);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 1.2f, transform.localScale.z);

        squachAndSquich = null;
    }

    void OnTriggerStay(Collider c){
        
        if(c.tag == "Water"){inWater = true;}
    }

     void OnTriggerExit(Collider c){
        
        if(c.tag == "Water"){inWater = false;}
    }
}
