using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : WorldObject {
	private const float THROW_FORCE = 10.0f;
	private bool isCollected = false;
	public Collectable Collect(){
		isCollected = true;
		return this; 
	}

	public void Drop(){
		isCollected = false;
		rb.AddForce(Vector3.up * THROW_FORCE / 4f);
	}

	public void Throw(Vector3 direction){
		isCollected = false;
		rb.AddForce(direction * THROW_FORCE, ForceMode.Impulse);
	}

}
