﻿using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {
	public string myname;

	public int devValue;
	public int population;
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
		devValue = (int)(population * 0.4 + investment * 0.4 + roadLv * 0.4)/250;
		apprRate = (int)(devValue * 0.5);
		mapColorUpdate (this.GetComponent<SpriteRenderer>(), Map.type);

		// Click(touch) Event
		if (Input.GetMouseButtonDown (0)) {
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if(hitInfo.transform.gameObject.name == myname)
			{
				Debug.Log( hitInfo.transform.gameObject.name + " Clicked" );
				// Here you can check hitInfo to see which collider has been hit, and act appropriately.
			}
		}

	}

	public void mapColorUpdate(SpriteRenderer map, int type){
		switch (type) {
		case Map.DEFAULT:
			map.color = new Color((float)(1-devValue*0.1), 1f, (float)(1-devValue*0.1), 1f);
			break;
		case Map.INDUSTRY:
			map.color = new Color((float)(1-devValue*0.1), (float)(1-devValue*0.1), 1f, 1f);
			break;
		case Map.RESOURCE:
			map.color = new Color((float)(1-devValue*0.1), (float)(1-devValue*0.1), (float)(1-devValue*0.1), 1f);
			break;
		case Map.ENVIRONMENT:
			map.color = new Color(1f, (float)(1-devValue*0.1), (float)(1-devValue*0.1), 1f);
			break;
		case Map.SUPPORT:
			break;
		}

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
