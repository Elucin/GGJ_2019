using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour {

	public GameObject fusionPartner;

	public GameObject fusionResult;
	public bool inactive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c){
		if (c.collider.name == fusionPartner.name){
			if(!inactive){
				Fusion f = c.gameObject.GetComponent<Fusion>();
				f.inactive = true;
				Fuse(c.gameObject);
			}
			
			
		}
	}

	void Fuse(GameObject other){
		Destroy(other.gameObject);
		Instantiate(fusionResult,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
