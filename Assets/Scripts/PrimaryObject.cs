using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryObject : WorldObject {

	public GameObject secondaryObject;
	public GameObject spawnParticle;

	public GameObject newObject;
	public Mesh newModel;

	public float yOffset;

	


	
	// Update is called once per frame
	

	public override void Interact(){
		Instantiate(secondaryObject, transform.position+ Vector3.up*yOffset,Quaternion.identity);
		Instantiate(spawnParticle, transform.position,Quaternion.identity);
		if (newObject !=null){
			Instantiate(newObject, transform.position,Quaternion.identity);
		}
		else if(newModel != null){
			GetComponent<MeshFilter>().mesh = newModel;
			Destroy(this);
			GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
			return;
		}

		Destroy(this.gameObject);
		
		
	}
}
