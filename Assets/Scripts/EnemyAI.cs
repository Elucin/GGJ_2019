using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : SimpleAI {

	public GameObject deathParticle;
	public void Kill(){
		//Set the player to safe
		GameManager.instance.SetStatus(GameManager.Status.SAFETY);
		
		Instantiate(deathParticle,transform.position,Quaternion.identity);
		//Sounds + Particle Systems
		Destroy(gameObject);
	}
}
