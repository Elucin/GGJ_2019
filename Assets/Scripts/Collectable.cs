using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : WorldObject {
	private const float FORWARD_DROP_FORCE = 0f;
	private const float FORWARD_THROW_FORCE = 15f;
	private const float UP_FORCE = 4f;
	public bool isCollected = false;

	public override void Interact(){
		if(!isCollected){
			Interaction.held = this;
			isCollected = true;
		}
		else{
			Drop();
		}
	}

	public void Drop(bool thrown = false){
		float forward_force = thrown ? FORWARD_THROW_FORCE : FORWARD_DROP_FORCE;

		//No longer considered to be the collected object
		isCollected = false;

		//Toss me
		//What?
		//I cannot jump such a distance you'll have to toss me!
		//Uh-h-h-h... don't tell the elf.
		//Not a word
		rb.AddForce((transform.up * UP_FORCE + Vector3.up * Interaction.player.GetComponent<Rigidbody>().velocity.y) + Interaction.player.transform.forward * forward_force, ForceMode.VelocityChange);
	}

	protected override void Update(){
		base.Update();
		if(isCollected){
			transform.position = Interaction.player.transform.position + Vector3.up;
		}
	}

}
