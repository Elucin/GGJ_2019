using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconEnabler : MonoBehaviour {
	[SerializeField]
	public Image[] icons;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0;i<icons.Length;i++){
			icons[i].gameObject.SetActive(GameManager.instance.statuses[i]);
		}
	}
}
