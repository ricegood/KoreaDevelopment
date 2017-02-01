using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoadButton : MonoBehaviour {
	private const int ROADMONEY = 5000;	// per 1 distance
	private const int ROADTIME = 180; // sec

	public GameObject moneyPanel;
	public GameObject thisObject;

	public static bool roadPopup;
	public static bool secondChoice;

	private static bool isRoadBuilding;
	private static float roadBuildingEndTime;

	// Use this for initialization
	void Start () {
		isRoadBuilding = (PlayerPrefs.GetString ("isRoadBuilding") == "True");
		roadBuildingEndTime = Util.GetFloat(PlayerPrefs.GetString ("roadBuildingEndTime"), 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoadBuilding) {
			thisObject.GetComponent<Button> ().interactable = false;
		}
	}

	public static void openCheckPopup(){
		
	}

	public void setRoadPopup(bool b){
		roadPopup = b;
	}

	// save, load !!!!!!
	public void roadBuild(Road road){
		if (!isRoadBuilding && !road.getCompleted()) {
			if (Country.setMoney (Country.getMoney () - road.distance*ROADMONEY)) {
				// Building !
				isRoadBuilding = true;
				PlayerPrefs.SetString ("isRoadBuilding", isRoadBuilding.ToString ());
				roadBuildingEndTime = myTime.getNow () + ROADTIME;
				PlayerPrefs.SetString ("roadBuildingEndTime", roadBuildingEndTime.ToString ());
			} else {
				// no money
				Util.popup = true;
				moneyPanel.SetActive (true);
			}
		} else {
			// already mining
			Debug.Log("Already Building. && Builded");
		}
	}

}
