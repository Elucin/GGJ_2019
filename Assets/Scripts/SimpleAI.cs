using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SimpleAI : MonoBehaviour {
	private float wanderInterval = 10f;
	protected float wanderDistance = 20f;
	private float wanderTime = 0f;
	private float wanderSpeed = 2f;
	private float followSpeed = 10f;
	protected float interestRange = 50f;
	public bool swim = false;
	public GameObject zone;
	protected bool follow = false;
	protected NavMeshAgent navMeshAgent;
	Vector3 startingPos;
	// Use this for initialization
	public virtual void Start () {
		//navMeshAgent = GetComponent<NavMeshAgent>();
		NavMeshHit closestHit;
		
		int layerhit = swim ? 1 << NavMesh.GetAreaFromName("Swim") : 1;
		//Debug.Log(layerhit);
		if( NavMesh.SamplePosition(  transform.position, out closestHit, 500, layerhit ) ){
			startingPos = transform.position = closestHit.position;
			navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
			navMeshAgent.height = GetComponent<BoxCollider>().size.y;
			navMeshAgent.baseOffset = 0f;
			navMeshAgent.stoppingDistance = 2f;
			
			//navMeshAgent.agentTypeID = 1;
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
		navMeshAgent.speed = follow ? followSpeed : wanderSpeed;
		if(follow){
			Follow();
			navMeshAgent.speed = followSpeed;
			
			return;
		}
		else{
			navMeshAgent.speed = wanderSpeed;
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
		navMeshAgent.stoppingDistance = 2f;
		//Debug.DrawLine(transform.position, transform.position + wanderPoint, Color.red, Time.deltaTime);
		if(NavMesh.SamplePosition(startingPos + wanderPoint, out hit, 5, NavMesh.AllAreas)){
			navMeshAgent.SetDestination(hit.position);
			wanderTime = wanderInterval + Random.Range(-3f, 3f);
		}
	}
	void Follow(){
		navMeshAgent.stoppingDistance = 5f;
		navMeshAgent.SetDestination(Interaction.player.transform.position);
	}
}
