using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shepaca : SimpleAI {
	float tameDistanceSqr = 9f; 
	Interaction player;
	// Use this for initialization
	void Start () {
		player = Interaction.player.GetComponent<Interaction>();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(Interaction.held.tag == "Fruit"){
			if(Vector3.SqrMagnitude(transform.position - player.transform.position) < tameDistanceSqr){
				follow = true;
			}
		}
	}

	void Follow(){

	}
}
