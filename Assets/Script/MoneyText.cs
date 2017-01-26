using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyText : MonoBehaviour {
	private string myText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		myText = Util.printIntValue(Character.getMoney());
		Util.setText (this.GetComponent<Text>(), myText);
	}


}
