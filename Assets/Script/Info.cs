using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Info : MonoBehaviour {
	string myText;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		myText = getOrder(Character.getOrder()) + " President: " + Character.getName ();
		Util.setText (this.GetComponent<Text>(), myText);
	}


	public static string getOrder(int n){
		int temp = n % 10;
		if (temp == 1) {
			return (n + "st");
		} else if (temp == 2) {
			return (n + "nd");
		} else if (temp == 3) {
			return (n + "rd");
		} else {
			return (n + "th");
		}
	}

}
