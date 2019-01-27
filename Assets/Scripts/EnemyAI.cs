using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : SimpleAI {
	public void Kill(){
		//Set the player to safe
		GameManager.instance.SetStatus(GameManager.Status.SAFETY);
		
		//Sounds + Particle Systems
		Destroy(gameObject);
	}
}
