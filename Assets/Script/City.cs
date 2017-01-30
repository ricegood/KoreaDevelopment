using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class City : MonoBehaviour {
	public string myname;
	public string titleName;
	public SpriteRenderer map; 
	public GameObject detailPanel;

	private int devValue;
	private int population;
	private int apprRate;
	private int investment;

	private int resource;
	private int environment;
	private int taxRate;


	// Use this for initialization
	void Start () {
		load ();
	}

	// Update is called once per frame
	void Update () {
		if (detailPanel.activeSelf)
			this.GetComponent<PolygonCollider2D> ().enabled = false;
		else this.GetComponent<PolygonCollider2D> ().enabled = true;

		save ();
		population += 1;
		devValue = (int)(population * 0.4 + investment * 0.4)/250;
		apprRate = (int)(devValue * 0.5);
		mapColorUpdate (this.GetComponent<SpriteRenderer>(), Map.type);

		// Click(touch) Event
		if (Input.GetMouseButtonDown (0)) {
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if (hitInfo && hitInfo.transform.gameObject.name == myname) {
				touchEvent ();

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

	public void touchEvent(){
		switch (Map.type) {
		case Map.DEFAULT:
			// Open the Detail Panel
			detailPanel.GetComponent<CityDetail>().setCity(this.GetComponent<City>());
			detailPanel.GetComponent<CityDetail>().imageUpdate ();
			detailPanel.GetComponent<CityDetail>().textUpdate ();
			detailPanel.SetActive(true);
			break;
		case Map.INDUSTRY:
			break;
		case Map.RESOURCE:
			break;
		case Map.ENVIRONMENT:
			break;
		case Map.SUPPORT:
			break;
		}
	}

	public string getName(){
		return myname;
	}

	public string getTitleName(){
		return titleName;
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

	public int getResource(){
		return resource;
	}

	public int getEnvironment(){
		return environment;
	}

	public int getTaxRate(){
		return taxRate;
	}
		
	public void invest(){
		investment += 1000;
		Character.setMoney (Character.getMoney() - 1000);
	}

	public void save(){
		PlayerPrefs.SetInt (myname + "DevValue", devValue);
		PlayerPrefs.SetInt (myname + "Population", population);
		PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		PlayerPrefs.SetInt (myname + "Investment", investment);
		PlayerPrefs.SetInt (myname + "Resource", resource);
		PlayerPrefs.SetInt (myname + "Environment", environment);
		PlayerPrefs.SetInt (myname + "TaxRate", taxRate);
	}

	public void load(){
		devValue = PlayerPrefs.GetInt (myname + "DevValue");
		population = PlayerPrefs.GetInt (myname + "Population");
		apprRate = PlayerPrefs.GetInt (myname + "ApprRate");
		investment = PlayerPrefs.GetInt (myname + "Investment");
		resource = PlayerPrefs.GetInt (myname + "Resource");
		environment = PlayerPrefs.GetInt (myname + "Environment");
		taxRate = PlayerPrefs.GetInt (myname + "TaxRate");
	}
}
