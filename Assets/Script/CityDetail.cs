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
		title.text = thisCity.getTitleName ();
		info1.text = "Population : " + Util.printIntValue(thisCity.getPopulation ()) + "\nGDP : " + Util.printIntValue(thisCity.getGDP ()) + " ($)";
		info2.text = printIndustry(thisCity.getDevValue()) + "\nInvestment Capital : " + Util.printIntValue(thisCity.getInvestment()) + "\nMineral Deposit : " + thisCity.getResource() + " t" + "\nFine Dust Concentration : " + thisCity.getEnvironment() + "\nApproval Rating : " + thisCity.getApprRate() + "%\n세율 : " + thisCity.getTaxRate() + "%";
	}

	public void imageUpdate(){
		mapImage.sprite = thisCity.map.sprite;
	}

	public void setCity(City city){
		thisCity = city;
	}

	public void increaseTaxRate(){
		thisCity.increaseTaxRate ();
	}

	public void decreaseTaxRate(){
		thisCity.decreaseTaxRate ();
	}

	private string printIndustry(int n){
		if (n <= 25) {
			return "농/어업 중심 산업";
		} else if (n <= 50) {
			return "섬유/가발 중심 경공업";
		} else if (n <= 75) {
			return "중공업";
		} else {
			return "첨단산업";
		}
	}
}
