using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : WorldObject {
	private const float FORWARD_DROP_FORCE = 0f;
	private const float FORWARD_THROW_FORCE = 30f;
	private const float UP_FORCE = 15f;
	public bool isCollected = false;
	public override void Interact(){
		if(!isCollected){
			Interaction.held = this;
			isCollected = true;
            Outline outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineHidden;
            outline.OutlineColor = new Color32(0, 121, 255, 255);
            outline.OutlineWidth = 0.5f;
		}
		else{
			Drop();
		}
	}

	public void Drop(bool thrown = false){
		float forward_force = thrown ? FORWARD_THROW_FORCE : FORWARD_DROP_FORCE;

		//No longer considered to be the collected object
		isCollected = false;

        Destroy(GetComponent<Outline>());

		//Toss me
		//What?
		//I cannot jump such a distance you'll have to toss me!
		//Uh-h-h-h... don't tell the elf.
		//Not a word
		Vector3 applyForce = (Vector3.up * 1.5f + Interaction.player.transform.forward).normalized * forward_force;
		Debug.DrawLine(transform.position, transform.position + applyForce, Color.green, 3f);
		rb.AddForce(applyForce, ForceMode.VelocityChange);
	}

	protected override void Update(){
		base.Update();
		if(isCollected){
			transform.position = Interaction.player.transform.position + Vector3.up;
		}
	}

}
