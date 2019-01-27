using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour {
  public int currentChoice = 1;
  public Button[] climate_buttons;
  public Button[] terrain_buttons;
  public Button[] supplies_buttons;
  public Button[] misc_buttons;
  public Button[] targets;
  public Button[] end_targets;
  public Button[] first_selected;
  public GameObject terrain, supplies, misc;
  public Image title;
  public Image fade;

  private EventSystem eventSystem;
  private Button[] user_choices = new Button[4];
  private float speed = 5.0f;
  private int FADE_TIME = 1;
  private float startTime = 0.0f;
  private float journeyLength = 0.0f;
  bool changing = false;
  float changeTime = 0f;

	void Start () {
    eventSystem = GameObject.FindObjectOfType<EventSystem>();
    setListeners();
    
	}

  void Update(){
    if(changing){
        float a = (Time.time - changeTime) / 3.0f;
        Color fade_col = new Color(0, 0, 0, a);
        fade.color = fade_col;
    }
  }

  void setListeners() {
    foreach (Button btn in climate_buttons){
      btn.onClick.AddListener(() => choiceMade(btn, 1));
    }

    foreach (Button btn in terrain_buttons){
      btn.onClick.AddListener(() => choiceMade(btn, 2));
    }

    foreach (Button btn in supplies_buttons){
      btn.onClick.AddListener(() => choiceMade(btn, 3));
    }

    foreach (Button btn in misc_buttons){
      btn.onClick.AddListener(() => choiceMade(btn, 4));
    }
  }
  
	void choiceMade (Button choice, int numChoice) {
    startTime = Time.time;
    user_choices[numChoice-1] = choice;
    if(numChoice != 4){
      eventSystem.SetSelectedGameObject(first_selected[numChoice-1].gameObject);
    }
    sideEffects(choice, numChoice);
	}

	void sideEffects (Button choice, int numChoice) {
    Transform start, end = null;
    Button[] notChosen = null;
    start = choice.transform;

    switch (numChoice) {
      case 1:
        notChosen = notChosenButtons(climate_buttons, choice);
        end = targets[numChoice-1].transform;
        break;
      case 2:
        notChosen = notChosenButtons(terrain_buttons, choice);
        end = targets[numChoice-1].transform;
        break;
      case 3:
        notChosen = notChosenButtons(supplies_buttons, choice);
        end = targets[numChoice-1].transform;
        break;
      case 4:
        notChosen = notChosenButtons(misc_buttons, choice);
        end = targets[numChoice-1].transform;
        break;
    }

    journeyLength = Vector3.Distance(start.position, end.position);
    StartCoroutine(MoveTo(choice, start, end));
    StartCoroutine(Fade(notChosen));
	}

  Button[] notChosenButtons(Button[] buttons, Button choice) {
    int arrayLen = buttons.Length - 1;
    Button[] notChosen = new Button[arrayLen];
    int nc = 0;

    foreach (Button btn in buttons){
      disableButton(btn);

      if (btn.name != choice.name) {
        notChosen[nc] = btn;
        nc++;
      }
    }

    return notChosen;
  }

  void disableButton(Button btn) {
    btn.interactable = false;
  }

  IEnumerator MoveTo(Button choice, Transform start, Transform end) {
    float distCovered = 0.0f;
    float fracJourney = 0.0f;

    while(fracJourney < 0.07) {
      yield return null;
      distCovered = (Time.time - startTime) * speed;
      fracJourney = distCovered / journeyLength;
      choice.transform.position = Vector3.Lerp(start.position, end.position, fracJourney);
    }

    choice.transform.position = end.position;
    currentChoice++;

    switch (currentChoice) {
      case 2:
        terrain.SetActive(true);
        break;
      case 3:
        supplies.SetActive(true);
        break;
      case 4:
        misc.SetActive(true);
        break;
      case 5:
        title.GetComponent<Image>().CrossFadeAlpha(0,1,false);
        journeyLength = Vector3.Distance(user_choices[0].transform.position, end_targets[0].transform.position);
        startTime = Time.time;
        StartCoroutine(MoveTo(user_choices[0], user_choices[0].transform, end_targets[0].transform));
        StartCoroutine(MoveTo(user_choices[1], user_choices[1].transform, end_targets[1].transform));
        StartCoroutine(MoveTo(user_choices[2], user_choices[2].transform, end_targets[2].transform));
        StartCoroutine(MoveTo(user_choices[3], user_choices[3].transform, end_targets[3].transform));
        break;
      case 6:
        StartCoroutine(ChangeScenes("WorldMap"));
        changing = true;
        changeTime = Time.time;
        break;
    }
  }

  IEnumerator Fade(Button[] buttons) {
    foreach (Button btn in buttons){
      btn.GetComponent<Image>().CrossFadeAlpha(0,FADE_TIME,false);
    }
    yield return null;
  }
  IEnumerator GameStart(){
		yield return new WaitForSeconds(3f);
    Debug.Log("Game Start");
		GameManager.gameHasStarted = true;
	}
  IEnumerator ChangeScenes(string scene){
    yield return new WaitForSeconds(4f);
    StartCoroutine(GameStart());
    UnityEngine.SceneManagement.SceneManager.LoadScene("WorldMap");
    
  }
}
