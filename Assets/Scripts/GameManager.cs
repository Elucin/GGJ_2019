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
	public Status ClimateObjective{
		get{
			return (int)Objectives.CLIMATE == 15 ? (Status)0 : (Status)objectives[(int)Objectives.CLIMATE];
		}
		set{
			objectives[(int)Objectives.CLIMATE] = (int)value;
		}
	}
	public Status TerrainObjective{
		get{
			return (int)Objectives.TERRAIN == 15 ? (Status)4 : (Status)objectives[(int)Objectives.TERRAIN];
		}
		set{
			objectives[(int)Objectives.TERRAIN] = (int)value;
		}
	}
	public Status ThingsObjective{
		get{
			return (int)Objectives.THINGS == 15 ? (Status)8 : (Status)objectives[(int)Objectives.THINGS];
		}
		set{
			objectives[(int)Objectives.THINGS] = (int)value;
		}
	}
	public Status MiscObjective{
		get{
			return (int)Objectives.MISC == 15 ? (Status)12 : (Status)objectives[(int)Objectives.MISC];
		}
		set{
			objectives[(int)Objectives.MISC] = (int)value;
		}
	}
	[SerializeField]
	public int[] objectives = new int[4];

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
	public bool[] statuses = new bool[15];

	public static bool gameHasStarted = false;

	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);

		for(int i = 0; i < 14; i++){
			statuses[i] = false;
		}
		for(int i = 0; i < 4; i++){
			if(UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("WorldMap")){
				objectives[i] = i * 4;
			}
			else
				objectives[i] = 15;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameHasStarted){
			if(achievedAll){
				Win();
			}
		}
		
	}

	void Win(){
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Victory");
		Debug.Log("You Win!");
		return;
	}
	
	public void Lose(){
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Lose");
		Debug.Log("You Lose!");
	}

	public void SetStatus(Status s, bool setToTrue=true){
		statuses[(int)s] = setToTrue;
		if(statuses[(int)Status.WARM])
			statuses[(int)Status.COOL] = false;
	}

	public void SetObjective(int selection){
		if(ClimateObjective == (Status)15)
			ClimateObjective = (Status)selection;
		else if(TerrainObjective == (Status)15)
			TerrainObjective = (Status)selection;
		else if(ThingsObjective == (Status)15)
			ThingsObjective = (Status)selection;
		else if(MiscObjective == (Status)15)
			MiscObjective = (Status)selection;
	} 
}
