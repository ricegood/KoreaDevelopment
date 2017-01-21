using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	private static int order;
	private static int money;
	private static string myname;

	public static int getOrder(){
		return order;
	}

	public static int getMoney(){
		return money;
	}

	public static string getName(){
		return myname;
	}

	public static void setOrder(int n){
		order = n;
	}

	public static void setMoney(int n){
		money = n;
	}

	public static void setName(string n){
		myname = n;
	}

	public static void save(){
		PlayerPrefs.SetInt ("order", order);
		PlayerPrefs.SetInt ("money", money);
		PlayerPrefs.SetString ("name", myname);
	}

	public static void load(){
		order = PlayerPrefs.GetInt ("order");
		money = PlayerPrefs.GetInt ("money");
		myname = PlayerPrefs.GetString ("name");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
