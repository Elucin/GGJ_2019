using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour {
	public List<GameObject> fruit = new List<GameObject>();
	public void DropFruit(){
		GameObject f = fruit[fruit.Count - 1];
		f.transform.parent = null;
		f.AddComponent<Collectable>();
		f.GetComponent<Rigidbody>().isKinematic = false;
		fruit.Remove(f);
		
	}
}
