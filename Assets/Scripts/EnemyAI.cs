using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : SimpleAI {
	public Frend terrorizing;
	public GameObject deathParticle;
	public void Kill(){
		//Set the player to safe
		GameManager.instance.SetStatus(GameManager.Status.SAFETY);
		if(terrorizing != null)
			terrorizing.enemies.Remove(this);
		Instantiate(deathParticle,transform.position,Quaternion.identity);
		//Sounds + Particle Systems
		Destroy(gameObject);
	}
}
