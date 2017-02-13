using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CityDetail : MonoBehaviour {
	public static City thisCity;

	public Text title;
	public Text info1;
	public Text info2;
	public Text info3;
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
		info1.text = "Population : " + Util.printIntValue((int)(thisCity.getPopulation ())) + "\nGDP : " + Util.printIntValue(thisCity.getGDP ());
		info2.text = "Industry Type: " + printIndustry (thisCity.getDevValue ()) + "\nInvestment Capital : " + Util.printIntValue (thisCity.getInvestment ()) + "\nMineral Deposit : " + thisCity.getResource () + " kt" + "\nFine Dust Concentration : " + thisCity.getEnvironment () + " ㎍/㎥";
		info3.text = "Approval Rating : " + thisCity.getApprRate() + "%\nTax Rate : " + thisCity.getTaxRate() + "%";
	}

	public void imageUpdate(){
		mapImage.sprite = thisCity.map.sprite;
		mapImage.rectTransform.sizeDelta = new Vector2(thisCity.map.sprite.rect.width, thisCity.map.sprite.rect.height);
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
		if (n <= 500) {
			return "Agriculture";
		} else if (n <= 4000) {
			return "Light Industry";
		} else if (n <= 10000) {
			return "Heavy Industry";
		} else {
			return "High-tech Industry";
		}
	}
}
