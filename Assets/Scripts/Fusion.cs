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
		Fusion[] logs = FindObjectsOfType<Fusion>();
		for(int i = 0; i < logs.Length; i++){
			if(logs[i] == this)
				return;
			if(Vector3.SqrMagnitude(transform.position - logs[i].transform.position) < 9f){
				if(!inactive){
					Fusion f = logs[i].gameObject.GetComponent<Fusion>();
					f.inactive = true;
					Fuse(logs[i].gameObject);
				}
			}
		}
	}

	void Fuse(GameObject other){
		Destroy(other.gameObject);
		Instantiate(fusionResult,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
