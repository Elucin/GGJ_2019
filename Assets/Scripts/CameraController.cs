using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float shiftSpeed;
    public Vector3 staticOffset;

    private Vector3 dynamicOffset = Vector3.zero;
    private Transform player;
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

        if (Input.GetAxis("Horizontal") == 0)
            dynamicOffset = new Vector3(Mathf.MoveTowards(dynamicOffset.x, 0, shiftSpeed * Time.deltaTime) , dynamicOffset.y);
        if (Input.GetAxis("Vertical") == 0)
            dynamicOffset = new Vector3(dynamicOffset.x, Mathf.MoveTowards(dynamicOffset.y, 0, shiftSpeed * Time.deltaTime));


        dynamicOffset = Vector3.ClampMagnitude(dynamicOffset, 2);


        transform.position = player.position;
        transform.position = transform.TransformPoint(staticOffset + dynamicOffset);
	}
}
