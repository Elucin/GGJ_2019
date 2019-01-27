using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryObject : WorldObject {

	public GameObject secondaryObject;
	public GameObject spawnParticle;

	public GameObject newObject;

	


	
	// Update is called once per frame
	

	public override void Interact(){
		Instantiate(secondaryObject, this.transform.position,Quaternion.identity);
		Instantiate(spawnParticle, this.transform.position,Quaternion.identity);
		if (newObject !=null){
			Instantiate(newObject, this.transform.position,Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
}
