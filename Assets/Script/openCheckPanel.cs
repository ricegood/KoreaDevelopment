using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class openCheckPanel : MonoBehaviour {
	public RoadButton roadButton;
	public Text text;
	private static Road road;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateText(){
		text.text = "Construct a road from " + road.getFirstCityName () + " to " + road.getSecondCityName () + "?" + "\nCost : " + Util.printIntValue(road.distance * RoadButton.ROADMONEY) + "\nEstimated Time : " + (road.distance * RoadButton.ROADTIME) + "sec";
	}

	public void setRoad(Road r){
		road = r;
	}

	public void build(){
		roadButton.buildRoad (road);
	}
}
