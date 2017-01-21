using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoadInfo : MonoBehaviour {
	private string myText;
	public City myCity;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		myText = "도로 : " + Util.printIntValue(myCity.getRoadLv()) + "\n투자금액 : " + Util.printIntValue(myCity.getInvestment());
		Util.setText (this.GetComponent<Text>(), myText);
	}

}
