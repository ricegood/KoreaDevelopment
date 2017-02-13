using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
	public Text infoText;
	public GameObject thisPanel;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (thisPanel.activeSelf) {
			Util.popup = true;
			infoText.text = "Population : " + Country.getPopulation () + "\nGDP : " + Country.getSumGDP () + "\nFine Dust Concentration : " + Country.getAvgEnvironment () + "\nApproval Rating : " + Country.getAvgApprRate ();
		}
	}
}
