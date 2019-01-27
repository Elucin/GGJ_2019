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

    public SoundPlay dustSound;
    public SoundPlay splashSound;

    bool inWater = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        cam = Camera.main;
        instance = this;
        lmTerrain = LayerMask.GetMask("Terrain", "AI");
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameOver) {
            return;
        }

        //Get input into Vector3 for ease of use
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Check grounded
        grounded = IsGrounded();

        

        if (squachAndSquich == null && grounded == false){
            
            squachAndSquich = StartCoroutine(SquishAndStrech());
        }

        if (input.magnitude > 0.05f)
        {
            if(grounded){
                if(!dustParticle.isPlaying && !splashParticle.isPlaying){
                    if(!inWater) {dustParticle.Play();dustSound.Play();}
                    else {splashParticle.Play();splashSound.Play();}
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
        return (Physics.BoxCast(transform.position, new Vector3(0.45f, 0.05f, 0.45f), Vector3.down, transform.rotation, 0.5f, lmTerrain, QueryTriggerInteraction.Ignore));
    }

    IEnumerator DelayJump() {
        canJump = false;
        yield return new WaitForSeconds(0.05f);
        canJump = true;
    }

    IEnumerator SquishAndStrech()
    {
        float squishAmount = 1;

        yield return new WaitForSeconds(0.05f);

        while (grounded == false)
        {
            squishAmount += 0.3f * Time.deltaTime;
            yield return null;
        }

        float target = transform.localScale.y / squishAmount;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 0.1f;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, target, t), transform.localScale.z);
            yield return null;
        }
        
        target = transform.localScale.y * squishAmount;
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 0.1f;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.MoveTowards(transform.localScale.y, target, t), transform.localScale.z);
            yield return null;
        }

        squachAndSquich = null;
    }

    void OnTriggerStay(Collider c){
        
        if(c.tag == "Water"){inWater = true;}
    }

     void OnTriggerExit(Collider c){
        
        if(c.tag == "Water"){inWater = false;}
    }
}
