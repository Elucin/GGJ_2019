using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SoundArea : MonoBehaviour {

	public AudioSource player;
	// Use this for initialization

	bool inArea = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	void OnTriggerEnter(Collider c){
		if(c.name == "Player"){player.Play();}
	}

	void OnTriggerExit(Collider c){
		if(c.name == "Player"){player.Stop();}
	}
}
