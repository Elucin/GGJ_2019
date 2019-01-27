using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float shiftTime;
    public Vector3 staticOffset;

    public Vector3 dynamicOffset = Vector3.zero;
    private Transform player;

    private Vector2 smoothVolocity;
    private float lastHit = 0;
    private float currentHit;
    private float initial_zoom;
    private float zoom_out = 20f;
	void Start ()
    {
        initial_zoom = Camera.main.orthographicSize;
        player = GameObject.Find("Player").transform;
        currentHit = player.position.y;
	}
	
	void Update ()
    {
        /*RaycastHit hit;
        Physics.Raycast(player.position, Vector3.down, out hit, 20, LayerMask.GetMask("Terrain"));

        if (hit.point.y != lastHit)
            StartCoroutine(SmoothHeight(hit.point.y));

        lastHit = hit.point.y;
        */

        if(Input.GetButton("Zoom")){
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom_out, Time.deltaTime * 2);
        } 
        else{
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initial_zoom, Time.deltaTime * 4);
        }

        if (Input.GetAxis("Horizontal") > 0 )
            dynamicOffset.x = Mathf.SmoothDamp(dynamicOffset.x, 2, ref smoothVolocity.x, shiftTime);
        if (Input.GetAxis("Horizontal") < 0)
            dynamicOffset.x = Mathf.SmoothDamp(dynamicOffset.x, -2, ref smoothVolocity.x, shiftTime);
        if (Input.GetAxis("Vertical") > 0)
            dynamicOffset.y = Mathf.SmoothDamp(dynamicOffset.y, 2, ref smoothVolocity.y, shiftTime);
        if (Input.GetAxis("Vertical") < 0)
            dynamicOffset.y = Mathf.SmoothDamp(dynamicOffset.y, -2, ref smoothVolocity.y, shiftTime);

        if (Input.GetAxis("Horizontal") == 0 && (dynamicOffset.x > 0.01f || dynamicOffset.x < -0.01f))
            dynamicOffset = new Vector3(Mathf.SmoothDamp(dynamicOffset.x, 0, ref smoothVolocity.x, shiftTime ), dynamicOffset.y);

        if (Input.GetAxis("Vertical") == 0 && (dynamicOffset.y > 0.01f || dynamicOffset.y < -0.01f))
            dynamicOffset = new Vector3(dynamicOffset.x, Mathf.SmoothDamp(dynamicOffset.y, 0, ref smoothVolocity.y, shiftTime));

       

        //transform.position = new Vector3(player.position.x, currentHit, player.position.z);
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.position = transform.TransformPoint(staticOffset + dynamicOffset);
    }

    IEnumerator SmoothHeight(float hit)
    {
        float smoothHit = 0;

        while (hit == lastHit)
        {
            currentHit = Mathf.SmoothDamp(currentHit, hit, ref smoothHit, shiftTime);
            Debug.Log(currentHit);
            yield return null;
        }
        
    }
}