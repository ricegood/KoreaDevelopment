using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopulationInfo : MonoBehaviour {
	private string myText;
	public City myCity;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		myText = "인구수 : " + Util.printIntValue(myCity.getPopulation()) + "\n지지율 : " + myCity.getApprRate() + "%";
		Util.setText (this.GetComponent<Text>(), myText);
	}

}
