using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DevInfo : MonoBehaviour {
	private string myText;
	public City myCity;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		myText = "발전수치  : " + myCity.getDevValue ();
		Util.setText (this.GetComponent<Text>(), myText);
	}

}
