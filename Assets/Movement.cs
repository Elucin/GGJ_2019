using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    public float acceleration;
    public float jumpHight;

    private bool grounded = true;
    new private Rigidbody rigidbody;
    new private Collider collider;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
	}
	
	void FixedUpdate ()
    {
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
        {
            collider.material.bounciness = 0;

            if (grounded == true)
                rigidbody.AddForce(Vector3.up * jumpHight * Time.deltaTime);
        }
        else
            collider.material.bounciness = 1;

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log("1: " + input);
        input.Normalize();
        Debug.Log("2: " + input);


        rigidbody.velocity = new Vector3(
                Mathf.Clamp(rigidbody.velocity.x + acceleration * input.x * Time.deltaTime, -speed, speed),
                rigidbody.velocity.y,
                Mathf.Clamp(rigidbody.velocity.z + acceleration * input.y * Time.deltaTime, -speed, speed));
	}

    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
