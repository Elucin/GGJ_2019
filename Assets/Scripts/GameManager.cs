using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum Attributes{
		CLIMATE,
		TERRAIN,
		THINGS,
		MISC,
		NUM_OF_ATTRIBUTE

	}
	public enum Climate{
		WARM,
		COOL,
		DRY,
		WET,
		NUM_OF_CLIMATES
	}
	public enum Terrain{
		FOREST,
		ALTITUDE,
		PLAINS,
		COAST,
		NUM_OF_CLIMATES
	}
	public enum Things{
		FOOD,
		LIGHT,
		COMFORT,
		DECORATION,
		NUM_OF_THINGS
	}
	public enum Misc{
		PARTNER,
		COMPANIONSHIP,
		SAFETY,
		NUM_OF_MISC

	}
	private Climate climate_objective;
	private bool achievedClimate{
		get{
			return objectives[(int)Attributes.CLIMATE,(int)climate_objective];
		}
	}
	private bool achievedTerrain{
		get{
			return objectives[(int)Attributes.TERRAIN,(int)climate_objective];
		}
	}
	private bool achievedThings{
		get{
			return objectives[(int)Attributes.THINGS,(int)climate_objective];
		}
	}
	private bool achievedMisc{
		get{
			return objectives[(int)Attributes.MISC,(int)climate_objective];
		}
	}
	private bool achievedAll{
		get{
			return achievedClimate && achievedTerrain && achievedThings && achievedMisc; 
		}
	}

	public bool[,] objectives = new bool[4,4];
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 4; i++){
			for(int j = 0; j < 4; j++){
				objectives[i,j] = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(achievedAll){
			Win();
		}
	}

	void Win(){
		//Do win behaviour here. Fade out/Load new scene?
		return;
	}
}
