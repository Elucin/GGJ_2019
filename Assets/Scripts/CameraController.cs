using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float shiftTime;
    public Vector3 staticOffset;

    public Vector3 dynamicOffset = Vector3.zero;
    private Transform player;

    private Vector2 currentVolocity;
	void Start ()
    {
        player = GameObject.Find("Player").transform;
	}
	
	void Update ()
    {
        /*
        if (Input.GetAxis("Horizontal") > 0)
            dynamicOffset += Vector3.right * shiftSpeed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") < 0)
            dynamicOffset += Vector3.left * shiftSpeed * Time.deltaTime;
        if (Input.GetAxis("Vertical") > 0)
            dynamicOffset += Vector3.up * shiftSpeed * Time.deltaTime;
        if (Input.GetAxis("Vertical") < 0)
            dynamicOffset += Vector3.down * shiftSpeed * Time.deltaTime;
        */
        if (Input.GetAxis("Horizontal") > 0 )
            dynamicOffset.x = Mathf.SmoothDamp(dynamicOffset.x, 2, ref currentVolocity.x, shiftTime);
        if (Input.GetAxis("Horizontal") < 0)
            dynamicOffset.x = Mathf.SmoothDamp(dynamicOffset.x, -2, ref currentVolocity.x, shiftTime);
        if (Input.GetAxis("Vertical") > 0)
            dynamicOffset.y = Mathf.SmoothDamp(dynamicOffset.y, 2, ref currentVolocity.y, shiftTime);
        if (Input.GetAxis("Vertical") < 0)
            dynamicOffset.y = Mathf.SmoothDamp(dynamicOffset.y, -2, ref currentVolocity.y, shiftTime);

        if (Input.GetAxis("Horizontal") == 0 && (dynamicOffset.x > 0.01f || dynamicOffset.x < -0.01f))
            dynamicOffset = new Vector3(Mathf.SmoothDamp(dynamicOffset.x, 0, ref currentVolocity.x, shiftTime ), dynamicOffset.y);

        if (Input.GetAxis("Vertical") == 0 && (dynamicOffset.y > 0.01f || dynamicOffset.y < -0.01f))
            dynamicOffset = new Vector3(dynamicOffset.x, Mathf.SmoothDamp(dynamicOffset.y, 0, ref currentVolocity.y, shiftTime));


        dynamicOffset = Vector3.ClampMagnitude(dynamicOffset, 2);


        transform.position = new Vector3(player.position.x, 0, player.position.z);
        transform.position = transform.TransformPoint(staticOffset + dynamicOffset);
    }
}
