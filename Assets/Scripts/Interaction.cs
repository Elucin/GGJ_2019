using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {
	float maxDistance = 1.75f;
	public static GameObject player;
	public static Collectable held = null;
	// Use this for initialization
	public ParticleSystem interactParticle;
	public SoundPlay interactSound;
	void Start () {
		player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		HighlightClosest(WorldObject.lstWorldObjects);

		if(Input.GetButtonDown("Interact")){
			if(!interactParticle.isPlaying){
				interactParticle.Play();
				interactSound.Play();
			}
			if(held != null){
				held.Interact();
				held = null;
			}
			else if(WorldObject.highlighted != null){
				WorldObject.highlighted.Interact();
			}
		}

		if(Input.GetButtonDown("Throw")){
			if(held != null){
				held.Drop(true);
				held = null;
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
			directionToTarget.y = 0;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
		if(closestDistanceSqr < maxDistance * maxDistance){
			bestTarget.Highlight();
		}
		else{
			WorldObject.highlighted = null;
		}
        //return bestTarget;
    }
}
