using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SimpleAI : MonoBehaviour {
	private float wanderInterval = 10f;
	private float wanderDistance = 3f;
	private float wanderTime = 0f;
	private float wanderSpeed = 2f;
	private float followSpeed = 10f;
	public bool swim = false;
	bool follow = false;
	private NavMeshAgent navMeshAgent;
	Vector3 startingPos;
	// Use this for initialization
	void Start () {
		//navMeshAgent = GetComponent<NavMeshAgent>();
		NavMeshHit closestHit;
		int layerhit = swim ? 1 << 3 : 1;
		if( NavMesh.SamplePosition(  transform.position, out closestHit, 500, layerhit ) ){
			startingPos = transform.position = closestHit.position;
			navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
			navMeshAgent.height = GetComponent<BoxCollider>().size.y;
			navMeshAgent.baseOffset = 0f;
			navMeshAgent.stoppingDistance = 0.5f;
			
			//navMeshAgent.agentTypeID = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		navMeshAgent.speed = follow ? followSpeed : wanderSpeed;
		if(follow){
			
			return;
		}
		else{
			
			if(wanderTime <= 0f){
				Wander();
			}
			else
				wanderTime -= Time.deltaTime;
			Debug.DrawLine(transform.position, navMeshAgent.destination, Color.red, Time.deltaTime);
		}
	}

	void Wander(){
		Vector2 randomPoint = Random.insideUnitCircle * wanderDistance;
		Vector3 wanderPoint = new Vector3(randomPoint.x, 0, randomPoint.y);
		NavMeshHit hit;
		//Debug.DrawLine(transform.position, transform.position + wanderPoint, Color.red, Time.deltaTime);
		if(NavMesh.SamplePosition(startingPos + wanderPoint, out hit, 5, NavMesh.AllAreas)){
			navMeshAgent.SetDestination(hit.position);
			wanderTime = wanderInterval + Random.Range(-3f, 3f);
		}
	}
}
