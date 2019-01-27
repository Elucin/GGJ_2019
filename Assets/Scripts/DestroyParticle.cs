using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {

	public ParticleSystem p;
	
	// Use this for initialization
	void Start () {
		if(p==null){p = GetComponent<ParticleSystem> ();}
	}
	
	// Update is called once per frame
	void Update () {
		if (p) {
			if (!p.IsAlive ()) {
				Destroy (gameObject);
			}
		}
	}
}
