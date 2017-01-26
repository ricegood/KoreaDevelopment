using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	private static int order;
	private static int money;
	private static string myname;
	private static int level;

	public static int getLevel(){
		return level;
	}

	public static int getOrder(){
		return order;
	}

	public static int getMoney(){
		return money;
	}

	public static string getName(){
		return myname;
	}

	public static void setLevel(int n){
		level = n;
		PlayerPrefs.SetInt ("level", n);
	}

	public static void setOrder(int n){
		order = n;
		PlayerPrefs.SetInt ("order", order);
	}

	public static void setMoney(int n){
		money = n;
		PlayerPrefs.SetInt ("money", money);
	}

	public static void setName(string n){
		myname = n;
		PlayerPrefs.SetString ("name", myname);
	}

	/*
	public static void save(){
		PlayerPrefs.SetInt ("order", order);
		PlayerPrefs.SetInt ("money", money);
		PlayerPrefs.SetString ("name", myname);
		PlayerPrefs.SetInt ("level", level);
	}
	*/
	public static void load(){
		order = PlayerPrefs.GetInt ("order");
		money = PlayerPrefs.GetInt ("money");
		myname = PlayerPrefs.GetString ("name");
		level = PlayerPrefs.GetInt ("level");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
