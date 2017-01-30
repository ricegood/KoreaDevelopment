using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CityDetail : MonoBehaviour {
	public static City thisCity;

	public Text title;
	public Text info1;
	public Text info2;
	public Image mapImage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		textUpdate ();
	}

	public void textUpdate(){
		title.text = thisCity.getName ();
		info1.text = "인구수 : " + thisCity.getPopulation () + "\nGDP : " + thisCity.getDevValue ();
		info2.text = "XXXX 산업" + "\n투자 금액 : " + thisCity.getInvestment() + "\n자원 매장량 : " + thisCity.getResource() + "\n환경 오염도 : " + thisCity.getEnvironment() + "\n지지율 : " + thisCity.getApprRate() + "\n세율 : " + thisCity.getTaxRate();
	}

	public void imageUpdate(){
		mapImage.sprite = thisCity.map.sprite;
	}

	public void setCity(City city){
		thisCity = city;
	}
}
