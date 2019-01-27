using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum Status{
		WARM,
		COOL,
		DRY,
		WET,	
		FOOD,
		LIGHT,
		COMFORT,
		DECORATION,
		FOREST,
		ALTITUDE,
		PLAINS,
		COAST,
		PARTNER,
		COMPANIONSHIP,
		SAFETY,
		NUM_OF_STATUSES
	}
	public enum Objectives{
		CLIMATE,
		TERRAIN,
		THINGS,
		MISC,
		NUM_OF_STATUSES
	}

	public static GameManager instance;
	private Status ClimateObjective{
		get{
			return (Status)objectives[(int)Objectives.CLIMATE];
		}
		set{
			objectives[(int)Objectives.CLIMATE] = (int)value;
		}
	}
	private Status TerrainObjective{
		get{
			return (Status)objectives[(int)Objectives.TERRAIN];
		}
		set{
			objectives[(int)Objectives.TERRAIN] = (int)value;
		}
	}
	private Status ThingsObjective{
		get{
			return (Status)objectives[(int)Objectives.THINGS];
		}
		set{
			objectives[(int)Objectives.THINGS] = (int)value;
		}
	}
	private Status MiscObjective{
		get{
			return (Status)objectives[(int)Objectives.MISC];
		}
		set{
			objectives[(int)Objectives.MISC] = (int)value;
		}
	}

	private int[] objectives = new int[4];

	//Checking objective completion
	private bool achievedClimate{
		get{
			return statuses[(int)ClimateObjective];
		}
	}
	private bool achievedTerrain{
		get{
			return statuses[(int)TerrainObjective];
		}
	}
	private bool achievedThings{
		get{
			return statuses[(int)ThingsObjective];
		}
	}
	private bool achievedMisc{
		get{
			return statuses[(int)MiscObjective];
		}
	}
	private bool achievedAll{
		get{
			return achievedClimate && achievedTerrain && achievedThings && achievedMisc; 
		}
	}
	[SerializeField]
	private bool[] statuses = new bool[15];
	// Use this for initialization
	void Start () {
		instance = this;
		for(int i = 0; i < 15; i++){
			statuses[i] = false;
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

	public void SetStatus(Status s, bool setToTrue=true){
		statuses[(int)s] = setToTrue;
		if(statuses[(int)Status.WARM])
			statuses[(int)Status.COOL] = false;
	}
}
