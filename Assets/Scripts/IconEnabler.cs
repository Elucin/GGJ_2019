using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconEnabler : MonoBehaviour {
	[SerializeField]
	public Image[] icons;
	public bool playerObjectives = false;
	private Canvas canvas;
	public Canvas controls;
	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
		if(playerObjectives){
			for(int i = 0;i<4;i++){
				//Debug.Log(GameManager.instance);
				icons[GameManager.instance.objectives[i]].gameObject.SetActive(true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(playerObjectives){
			controls.enabled = canvas.enabled = Input.GetButton("Info");
			transform.LookAt(Camera.main.transform);
			transform.position = Movement.instance.transform.position + Vector3.up * 2;
			return;
		}

		for(int i = 0;i<icons.Length;i++){
			icons[i].gameObject.SetActive(GameManager.instance.statuses[i]);
		}
	}
}
