using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoadButton : MonoBehaviour {
	public const int ROADMONEY = 5000;	// per 1 distance
	public const int ROADTIME = 5; // sec per 1 distance

	public GameObject moneyPanel;
	public GameObject thisObject;
	// private static City firstChoosed;

	public static bool roadPopup;
	public static bool secondChoice;

	public static bool isRoadBuilding;
	public static float roadBuildingEndTime;
	public static string buildingRoadName1, buildingRoadName2;

	// Use this for initialization
	void Start () {
		isRoadBuilding = (PlayerPrefs.GetString ("isRoadBuilding") == "True");
		roadBuildingEndTime = Util.GetFloat(PlayerPrefs.GetString ("roadBuildingEndTime"), 0.0f);
		buildingRoadName1 = PlayerPrefs.GetString ("buildingRoadName1");
		buildingRoadName2 = PlayerPrefs.GetString ("buildingRoadName2");

		if (isRoadBuilding) {
			thisObject.GetComponent<Button> ().interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		// Check the road building
		if(isRoadBuilding && (myTime.getNow() > roadBuildingEndTime)){
			isRoadBuilding = false;
			PlayerPrefs.SetString ("isRoadBuilding", isRoadBuilding.ToString());

			// road completed!
			Debug.Log(buildingRoadName1 + buildingRoadName2);
			Road thisRoad = Map.getRoad(buildingRoadName1, buildingRoadName2);
			thisRoad.setCompleted (true);
			thisObject.GetComponent<Button> ().interactable = true;
			Map.addGraph (thisRoad);
		}
	}

	public void setRoadPopup(bool b){
		roadPopup = b;
	}

	// save, load !!!!!!
	public void buildRoad(Road road){
		if (!isRoadBuilding && !road.getCompleted()) {
			if (Country.setMoney (Country.getMoney () - road.distance*ROADMONEY)) {
				// Building !
				isRoadBuilding = true;
				PlayerPrefs.SetString ("isRoadBuilding", isRoadBuilding.ToString ());
				roadBuildingEndTime = myTime.getNow () + road.distance*ROADTIME;
				PlayerPrefs.SetString ("roadBuildingEndTime", roadBuildingEndTime.ToString ());
				buildingRoadName1 = road.getFirstCityName();
				buildingRoadName2 = road.getSecondCityName();
				PlayerPrefs.SetString ("buildingRoadName1", buildingRoadName1);
				PlayerPrefs.SetString ("buildingRoadName2", buildingRoadName2);
				thisObject.GetComponent<Button> ().interactable = false;
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
