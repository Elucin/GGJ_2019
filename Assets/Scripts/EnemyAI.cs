using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : SimpleAI {
	public void Kill(){
		//Sounds + Particle Systems
		Destroy(gameObject);
	}
}
