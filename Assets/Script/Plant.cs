using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Plant : MonoBehaviour {
	public const int TREEMONEY = 100000;		// per 1000 tree
	public const int TREETIME = 5;				// sec per 1000 tree
	public const int TREEVALUE = 5;			// environment pollution decreased by TREEVALUE, per 1000 tree.

	public Text text;
	public Text timeText;
	public GameObject thisObject;
	public GameObject moneyPanel;
	public Map map;

	private int treeNum = 1000;
	private int remainTime;

	private static bool isPlanting;
	private static float plantingEndTime;
	private static int plantedTreeNum;

	// Use this for initialization
	void Start () {
		isPlanting = (PlayerPrefs.GetString ("isPlanting") == "True");
		plantedTreeNum = (PlayerPrefs.GetInt ("plantedTreeNum"));
		plantingEndTime = Util.GetFloat(PlayerPrefs.GetString ("plantingEndTime"), 0.0f);
		if (isPlanting) {
			thisObject.GetComponent<Button> ().interactable = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!myTime.timeStop) {
			if (isPlanting) {
				remainTime = (int)(plantingEndTime - myTime.getNow ());
				timeText.text = remainTime + " s";
			}

			// Check the planting
			if (isPlanting && (myTime.getNow () > plantingEndTime)) {
				isPlanting = false;
				PlayerPrefs.SetString ("isPlanting", isPlanting.ToString ());

				// planting completed!
				betterEnvironment (plantedTreeNum);
				thisObject.GetComponent<Button> ().interactable = true;
				timeText.text = "";
			}
		}
	}

	public void betterEnvironment(int n){
		/*
		for(int i=0; i<map.city.Length; i++){
			City thisCity = map.city [i].GetComponent<City> ();
			thisCity.addTreeNum (n);
		}
		*/
		City.addTreeNum (n);
	}

	public void plant(){
		if (!isPlanting) {
			if (Country.setMoney (Country.getMoney () - (treeNum/1000)*TREEMONEY)) {
				isPlanting = true;
				PlayerPrefs.SetString ("isPlanting", isPlanting.ToString ());
				plantingEndTime = myTime.getNow () + (treeNum/1000)*TREETIME;
				PlayerPrefs.SetString ("plantingEndTime", plantingEndTime.ToString ());

				plantedTreeNum = treeNum;
				PlayerPrefs.SetInt ("plantedTreeNum", plantedTreeNum);
				thisObject.GetComponent<Button> ().interactable = false;
			} else {
				// no money
				Util.popup = true;
				moneyPanel.SetActive (true);
			}
		} else {
			// already mining
			Debug.Log("Already planting");
		}
	}

	public void increaseNum(){
		treeNum += 1000;
	}

	public void decreaseNum(){
		if (treeNum > 1000) {
			treeNum -= 1000;
		}
	}

	public void textUpdate(){
		text.text = "Determine the number of trees the plant.\n\n" + treeNum + "\n\nCost : " + Util.printIntValue((treeNum / 1000) * TREEMONEY) + "\nEstimated Time : " + ((treeNum / 1000) * TREETIME) + "s";
	}

	public void textReset(){
		treeNum = 1000;
		textUpdate ();
	}

}
