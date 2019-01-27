using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Shepaca : SimpleAI {
	bool getApple = false;
	Interaction player;
	WorldObject apple;

	public ParticleSystem hearts;
	// Use this for initialization
	public override void Start () {
		base.Start();
		player = Interaction.player.GetComponent<Interaction>();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(Interaction.held != null){
			if(Vector3.SqrMagnitude(transform.position - player.transform.position) < interestRange && Interaction.held.tag == "Fruit"){
				getApple = true;
				apple = Interaction.held;
			}
		}

		if(getApple){
			GetApple();
		}

	}

	void GetApple(){
		NavMeshHit hit;
		if(NavMesh.SamplePosition(apple.transform.position, out hit, 30f, NavMesh.AllAreas))
			navMeshAgent.SetDestination(hit.position);
		navMeshAgent.stoppingDistance = apple == Interaction.held ? 5 : 1;
	}

	void OnCollisionEnter(Collision c){
		if(getApple){
			if(c.gameObject == apple.gameObject){
				Destroy(apple.gameObject);
				hearts.Play();
				follow = true;
				getApple = false;
				zone.SetActive(true);
			}
		}
		
	}
}
