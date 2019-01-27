using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frend : SimpleAI {
	public List<EnemyAI> enemies = new List<EnemyAI>();

	public ParticleSystem hearts;
	// Use this for initialization
	public override void Start () {
		base.Start();
		wanderDistance = 1f;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(enemies.Count == 0 && Vector3.SqrMagnitude(Movement.instance.transform.position - transform.position) < interestRange){
			Freedom();
		}
	}

	void Freedom(){
		zone.SetActive(true);
		follow = true;
		hearts.Play();
	}
}
