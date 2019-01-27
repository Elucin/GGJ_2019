using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {
	public List<GameManager.Status> statuses = new List<GameManager.Status>();
	public List<GameManager.Status> remove_statuses = new List<GameManager.Status>();
	void OnTriggerStay(Collider c){
		foreach(GameManager.Status status in statuses){
			GameManager.instance.SetStatus(status);
		}
		foreach(GameManager.Status status in remove_statuses){
			GameManager.instance.SetStatus(status, false);
		}
	}

	void OnTriggerExit(Collider c){
		foreach(GameManager.Status status in statuses){
			GameManager.instance.SetStatus(status, false);
		}
		foreach(GameManager.Status status in remove_statuses){
			GameManager.instance.SetStatus(status);
		}
	}
}
