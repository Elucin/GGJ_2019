using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 Distance;

    private Transform player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.position + Distance;
	}
}
