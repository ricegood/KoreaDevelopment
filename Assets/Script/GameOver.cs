using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
	public Text infoText;
	// Use this for initialization
	void Start () {
		Util.popup = true;
		infoText.text = "인구수 : " + Country.getPopulation () + "\nGDP : " + Country.getAvgGDP () + "\n미세먼지 농도 : " + Country.getAvgEnvironment () + "\n지지율 : " + Country.getAvgApprRate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
