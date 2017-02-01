using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class openCheckPanel : MonoBehaviour {
	public RoadButton roadButton;
	public Text text;
	private Road road;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateText(){
		text.text = road.getFirstCityName () + "-" + road.getSecondCityName () + " 도로를 건설하시겠습니까?" + "\n비용 : " + Util.printIntValue(road.distance * RoadButton.ROADMONEY) + "\n소요시간 : " + (road.distance * RoadButton.ROADTIME) + "sec";
	}

	public void setRoad(Road r){
		road = r;
	}

	public void build(){
		roadButton.buildRoad (road);
	}
}
