using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Collectable {

	 int lmTerrain;
	bool grounded = false;

	void OnCollisionEnter(Collision c){
		lmTerrain = LayerMask.GetMask("Terrain");
		if(Interaction.held == this)
			return;
		if(c.transform.tag == "FruitTree"){
			c.gameObject.GetComponent<FruitTree>().DropFruit();
			return;
		}
		else if(c.transform.tag == "Enemy" && !IsGrounded()){
			c.gameObject.GetComponent<EnemyAI>().Kill();
			Debug.Log("Kill");
			return;
		}
	}

	bool IsGrounded() {
		
        return (Physics.Raycast(transform.position, Vector3.down, 1f, lmTerrain, QueryTriggerInteraction.Ignore));
    }
}
