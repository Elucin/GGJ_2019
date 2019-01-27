using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour {
	public float waitTime = 15f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(waitTime>0){
			waitTime-=Time.deltaTime;
		}else{
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Intro");
		}
	}
}
