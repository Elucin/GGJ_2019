using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float shiftSpeed;
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
        if (Input.GetAxis("Horizontal") > 0)
            dynamicOffset += Vector3.right * shiftSpeed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") < 0)
            dynamicOffset += Vector3.left * shiftSpeed * Time.deltaTime;
        if (Input.GetAxis("Vertical") > 0)
            dynamicOffset += Vector3.up * shiftSpeed * Time.deltaTime;
        if (Input.GetAxis("Vertical") < 0)
            dynamicOffset += Vector3.down * shiftSpeed * Time.deltaTime;

        /*
        if (Input.GetAxis("Horizontal") == 0)
            dynamicOffset = new Vector3(Mathf.MoveTowards(dynamicOffset.x, 0, shiftSpeed * Time.deltaTime) , dynamicOffset.y);
        if (Input.GetAxis("Vertical") == 0)
            dynamicOffset = new Vector3(dynamicOffset.x, Mathf.MoveTowards(dynamicOffset.y, 0, shiftSpeed * Time.deltaTime));
        */

        if (Input.GetAxis("Horizontal") == 0 && (dynamicOffset.x > 0.01f || dynamicOffset.x < -0.01f))
            dynamicOffset = new Vector3(Mathf.SmoothDamp(dynamicOffset.x, 0, ref currentVolocity.x, 1 ), dynamicOffset.y);
        if (Input.GetAxis("Vertical") == 0 && (dynamicOffset.y > 0.01f || dynamicOffset.y < -0.01f))
            dynamicOffset = new Vector3(dynamicOffset.x, Mathf.SmoothDamp(dynamicOffset.y, 0, ref currentVolocity.y, 1));


        dynamicOffset = Vector3.ClampMagnitude(dynamicOffset, 2);


        transform.position = player.position;
        transform.position = transform.TransformPoint(staticOffset + dynamicOffset);

        /*
        dynamicOffset = new Vector3(0, 0, staticOffset.z);

        if (Input.GetAxis("Horizontal") > 0)
            dynamicOffset += Vector3.right * 2;
        if (Input.GetAxis("Horizontal") < 0)
            dynamicOffset += Vector3.left * 2;
        if (Input.GetAxis("Vertical") > 0)
            dynamicOffset += Vector3.up * 2;
        if (Input.GetAxis("Vertical") < 0)
            dynamicOffset += Vector3.down * 2;

        if (Input.GetAxis("Horizontal") == 0)
            dynamicOffset.x = 0;
        if (Input.GetAxis("Vertical") == 0)
            dynamicOffset.y = 0;


        transform.position = player.position;
        transform.position = transform.TransformPoint(staticOffset + dynamicOffset);
        */
    }
}
