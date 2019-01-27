using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryObject : WorldObject {

	public GameObject secondaryObject;
	public GameObject spawnParticle;

	public GameObject newObject;

	public float yOffset;

	


	
	// Update is called once per frame
	

	public override void Interact(){
		if (newObject !=null){
			Instantiate(newObject, transform.position,Quaternion.identity);
		}
		Instantiate(secondaryObject, transform.position+ Vector3.up*yOffset,Quaternion.identity);
		Instantiate(spawnParticle, transform.position,Quaternion.identity);
		
		Destroy(this.gameObject);
	}
}
