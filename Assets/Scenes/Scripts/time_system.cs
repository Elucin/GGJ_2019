using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_system : MonoBehaviour {
	public bool isSun = false;
	public Color start;
	public Color midday;
	public Color sunset;

	float half_day = 5f;
	
	private float startTime = 0f;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float elapsedTime = Time.time - startTime;
		Color lightColor;
		if(isSun){
			float t = 0f;
			if(elapsedTime < half_day){
				t = elapsedTime / half_day;
				lightColor = Color.Lerp(start, midday, t);
			}
			else{
				t = (elapsedTime - half_day) / half_day;
				lightColor = Color.Lerp(midday, sunset, t);
			}
			GetComponent<Light>().color = lightColor;
		}

		transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime);
		transform.LookAt(Vector3.zero);
	}
}
