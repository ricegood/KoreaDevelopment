using UnityEngine;
using System.Collections;

public class Country : MonoBehaviour {
	public GameObject map;
	public GameObject moneyPanel;
	public GameObject[] cityList;

	private static int money;

	private static int devValue;
	private static int population;
	private static int apprRate;
	private static int taxRate;

	// Use this for initialization
	void Start () {
		load ();
	}
	
	// Update is called once per frame
	void Update () {
		//Character.save ();
	}

	public static int getMoney(){
		return money;
	}

	public static bool setMoney(int n){
		if (n >= 0) {
			money = n;
			PlayerPrefs.SetInt ("money", money);
			return true;
		} else {
			return false;
		}
	}

	private static void load(){
		Character.load ();
		money = PlayerPrefs.GetInt ("money");
	}

}
