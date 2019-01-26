using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {
	float maxDistance = 2;
	public static GameObject player;
	Collectable held = null;
	// Use this for initialization
	void Start () {
		player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		HighlightClosest(WorldObject.lstWorldObjects);
		if(Input.GetButtonDown("Collect")){
			if(held != null){
				held.Drop();
				held = null;
			} else if(WorldObject.highlighted.GetType() == typeof(Collectable)){
				Debug.Log("Collect");
				Collectable c = WorldObject.highlighted as Collectable;
				held = c;
				c.Collect();
			}
		}
	}
	void HighlightClosest (List<WorldObject> objects)
    {
        WorldObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(WorldObject potentialTarget in objects)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
		if(closestDistanceSqr < maxDistance * maxDistance || (bestTarget.GetType() == typeof(Collectable) && !((Collectable)bestTarget).isCollected)){
			//Debug.Log(bestTarget.gameObject.name);
			bestTarget.Highlight();
		}
		else{
			WorldObject.highlighted = null;
		}
        //return bestTarget;
    }
}
