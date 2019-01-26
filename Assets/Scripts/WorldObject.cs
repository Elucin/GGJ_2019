using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {
	private static Color HIGHLIGHT_COLOR = Color.green;
	public static WorldObject[] lstWorldObjects;

	//Highlight
	public static WorldObject highlighted = null;
	private bool isHighlighted = false;

	//Components
	protected Rigidbody rb; 
	private Material material;

	//For Testing
	public bool doHighlight = false;
	
	void Start(){
		material = GetComponent<MeshRenderer>().material;
		material.EnableKeyword("_EmissionColor");
	}

	public void Highlight(){
		highlighted = this;
		isHighlighted = true;
		material.SetColor ("_EmissionColor", HIGHLIGHT_COLOR);
		return;
	}

	void Update(){
		if(doHighlight){
			doHighlight = false;
			Highlight();
		}

		if(highlighted != this && isHighlighted){
			isHighlighted = false;
			material.SetColor ("_EmissionColor", Color.black);
		}
	}
}
