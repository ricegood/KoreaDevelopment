using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
	public Text infoText;
	// Use this for initialization
	void Start () {
		Util.popup = true;
		infoText.text = "Population : " + Country.getPopulation () + "\nGDP : " + Country.getAvgGDP () + "\nFine Dust Concentration : " + Country.getAvgEnvironment () + "\nApproval Rating : " + Country.getAvgApprRate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
