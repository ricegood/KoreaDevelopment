using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {
	public string myname;
	private int devValue;
	private int population;
	private int apprRate;
	private int investment;
	private int roadLv;

	// Use this for initialization
	void Start () {
		load ();
	}

	// Update is called once per frame
	void Update () {
		save ();
		population += 1;
		devValue = (int)(population * 0.2 + investment * 0.4 + roadLv * 0.4)/1000;
		apprRate = (int)(devValue * 0.5);
	}

	public int getDevValue(){
		return devValue;
	}

	public int getPopulation(){
		return population;
	}

	public int getApprRate(){
		return apprRate;
	}

	public int getInvestment(){
		return investment;
	}
		
	public int getRoadLv(){
		return roadLv;
	}

	public void invest(){
		investment += 1000;
		Character.setMoney (Character.getMoney() - 1000);
	}

	public void roadUpgrade(){
		roadLv++;
		Character.setMoney (Character.getMoney() - 1000);
	}

	public void save(){
		PlayerPrefs.SetInt (myname + "DevValue", devValue);
		PlayerPrefs.SetInt (myname + "Population", population);
		PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		PlayerPrefs.SetInt (myname + "Investment", investment);
		PlayerPrefs.SetInt (myname + "RoadLv", roadLv);
	}

	public void load(){
		devValue = PlayerPrefs.GetInt (myname + "DevValue");
		population = PlayerPrefs.GetInt (myname + "Population");
		apprRate = PlayerPrefs.GetInt (myname + "ApprRate");
		investment = PlayerPrefs.GetInt (myname + "Investment");
		roadLv = PlayerPrefs.GetInt (myname + "RoadLv");
	}
}
