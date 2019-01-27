using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Collectable {

	 int lmTerrain;

	public override void Start(){
		base.Start();
		lmTerrain = LayerMask.GetMask("Terrain", "AI");
	}

	void OnCollisionEnter(Collision c){
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
        return (Physics.Raycast(transform.position, Vector3.down,  0.3f, lmTerrain, QueryTriggerInteraction.Ignore));
    }
}
