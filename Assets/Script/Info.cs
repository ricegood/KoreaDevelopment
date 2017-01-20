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
		myText = Character.getOrder() + "대\n" + Character.getName ();
		setText (myText);
	}

	public void setText(string s){
		this.GetComponent<Text> ().text = s;
	}
}
