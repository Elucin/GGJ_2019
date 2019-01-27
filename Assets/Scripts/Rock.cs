using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Collectable {

	void OnCollisionEnter(Collision c){
		if(c.transform.tag == "FruitTree"){
			c.gameObject.GetComponent<FruitTree>().DropFruit();
			return;
		}
		else if(c.transform.tag == "Enemy"){
			c.gameObject.GetComponent<EnemyAI>().Kill();
			return;
		}
	}
}
