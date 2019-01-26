using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {
	private static Color HIGHLIGHT_COLOR = Color.green;
	public static List<WorldObject> lstWorldObjects = new List<WorldObject>();

	//Highlight
	public static WorldObject highlighted = null;
	private bool isHighlighted = false;

	//Components
	protected Rigidbody rb; 
	private Material material;
	
	void OnEnable(){
		lstWorldObjects.Add(this);
	}

	void OnDisable(){
		lstWorldObjects.Remove(this);
	}

	void Start(){
		material = GetComponent<MeshRenderer>().material;
		material.EnableKeyword("_EmissionColor");
		rb = GetComponent<Rigidbody>();
	}

	public virtual void Interact(){
		return;
	}

	public void Highlight(){
		highlighted = this;
		isHighlighted = true;
		material.SetColor ("_EmissionColor", HIGHLIGHT_COLOR);
		return;
	}

	protected virtual void Update(){
		if(highlighted != this && isHighlighted){
			isHighlighted = false;
			material.SetColor ("_EmissionColor", Color.black);
		}
	}
}
