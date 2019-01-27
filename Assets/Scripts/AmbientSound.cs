using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class AmbientSound : MonoBehaviour {
	public float avgWait = 5f;
	public float diff = 2.5f;

	float timer;
	public AudioSource player;
	// Use this for initialization
	void Start () {
		timer = avgWait + Random.Range(-diff,diff);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer>0){
			timer-=Time.deltaTime;
		}else{
			player.Play();
			timer = avgWait + Random.Range(-diff,diff);
		}
	}
}
