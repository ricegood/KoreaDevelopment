using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class City : MonoBehaviour {
	private const int INVESTMONEY = 1000;

	public string myname;
	public string titleName;
	public SpriteRenderer map; 
	public GameObject detailPanel;
	public GameObject moneyPanel;

	private int devValue;		// GDP
	private int population = 0; // initial value
	private int apprRate = 50;	// 0~100 (%) initial value
	private int investment = 0; // initial value
	private int resource;		// unit : [t]
	private int environment = 0;
	private int taxRate = 10;	// GDP * taxRate/100

	private int savedMonth;


	// Use this for initialization
	void Start () {
		// Initialize (* 나중에는 상수로 해야 할듯.)
		switch (myname) {
		case "Hambuk":
			devValue = 30;
			resource = 100;
			break;
		case "Hamnam":
			devValue = 30;
			resource = 10;
			break;
		case "Yanggangdo":
			devValue = 30;
			resource = 50;
			break;
		case "Jagangdo":
			devValue = 30;
			resource = 30;
			break;
		case "Pyeongbuk":
			devValue = 30;
			resource = 100;
			break;
		case "Pyeongnam":
			devValue = 40;
			resource = 70;
			break;
		case "Hwangbuk":
			devValue = 30;
			resource = 100;
			break;
		case "Hwangnam":
			devValue = 30;
			resource = 10;
			break;
		case "Kangwondo":
			devValue = 30;
			resource = 100;
			break;
		case "Gyeonggido":
			devValue = 80;
			resource = 5;
			break;
		case "Chungbuk":
			devValue = 50;
			resource = 10;
			break;
		case "Chungnam":
			devValue = 50;
			resource = 30;
			break;
		case "Jeonbuk":
			devValue = 50;
			resource = 30;
			break;
		case "Jeonnam":
			devValue = 50;
			resource = 30;
			break;
		case "Kyeongbuk":
			devValue = 50;
			resource = 30;
			break;
		case "Kyeongnam":
			devValue = 60;
			resource = 30;
			break;
		case "Jejudo":
			devValue = 50;
			resource = 30;
			break;
		case "Ulleungdo":
			devValue = 40;
			resource = 40;
			break;
		case "Dokdo":
			devValue = 10;
			resource = 90;
			break;
		}
		savedMonth = myTime.getMonth ();
		load ();

	}

	// Update is called once per frame
	void Update () {
		// get money every 1 month.
		if(savedMonth != myTime.getMonth()){
			savedMonth = myTime.getMonth ();
			Country.setMoney (Country.getMoney () + (int)(((float)taxRate/100) * devValue*100));
		}
	
		// Value Setting Functions
		setEnvironment ((int)(devValue * 0.5 - resource * 0.5));
		setApprRate (devValue,taxRate);
		population += 1;
		devValue = (int)(population * 0.1 + investment * 0.9)/50;

		save ();

		// Map Color Update
		mapColorUpdate (this.GetComponent<SpriteRenderer>(), Map.type);

		// Click(touch) Event
		if (Input.GetMouseButtonDown (0)) {
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if (!Util.popup && !detailPanel.activeSelf && hitInfo && hitInfo.transform.gameObject.name == myname) {
				touchEvent ();

			}
		}

	}

	public void mapColorUpdate(SpriteRenderer map, int type){
		switch (type) {
		case Map.DEFAULT:
			map.color = new Color((float)(1-devValue*0.001), 1f, (float)(1-devValue*0.001), 1f);
			break;
		case Map.INDUSTRY:
			// modify [ 4000 , 0.000025 ] values to change limitation.
			if (investment > 40000)
				map.color = new Color (0.3f, 0.3f, 1f, 1f);
			else map.color = new Color(1-(float)(investment*0.000025)*0.7f, 1-(float)(investment*0.000025)*0.7f, 1f, 1f);
			break;
		case Map.RESOURCE:
			map.color = new Color(1-(float)((float)resource/100)*0.7f, 1-(float)((float)resource/100)*0.7f, 1-(float)((float)resource/100)*0.7f, 1f);
			break;
		case Map.ENVIRONMENT:
			map.color = new Color(1f, (float)(1-environment*0.01), (float)(1-environment*0.01), 1f);
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
			// investment (take 10 minutes)
			invest();
			break;
		case Map.RESOURCE:
			break;
		case Map.ENVIRONMENT:
			// nothing
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

	public void increaseTaxRate(){
		if (taxRate < 100) {
			taxRate++;
			PlayerPrefs.SetInt (myname + "TaxRate", taxRate);
		}
	}

	public void decreaseTaxRate(){
		if (taxRate > 0) {
			taxRate--;
			PlayerPrefs.SetInt (myname + "TaxRate", taxRate);
		}
	}
		
	public void invest(){
		if (Country.setMoney (Country.getMoney () - INVESTMONEY)) {
			investment += INVESTMONEY;
			PlayerPrefs.SetInt (myname + "Investment", investment);
		} else {
			Util.popup = true;
			moneyPanel.SetActive (true);
		}
	}

	public void setEnvironment(int n){
		if (n < 0) {
			environment = 0;
		} else if (n > 200) {
			environment = 200;
		} else{
			environment = n;
			PlayerPrefs.SetInt (myname + "Environment", environment);
		}
	}

	public void setApprRate(int devValue, int taxRate){
		int result;

		if (taxRate < 20) {
			result = (int)((devValue * 0.5) - taxRate);
		} else{
			result = (int)((devValue * 0.5) - 20 - (taxRate - 20) / 0.5);
		}

		if (result < 0) {
			// ** Game Over !!!!! **
			apprRate = 0;
		} else if (result > 100) {
			apprRate = 100;
		} else{
			apprRate = result;
			PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		}
	}



	public void save(){
		PlayerPrefs.SetInt (myname + "DevValue", devValue);
		PlayerPrefs.SetInt (myname + "Population", population);
		//PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		// PlayerPrefs.SetInt (myname + "Investment", investment);
		PlayerPrefs.SetInt (myname + "Resource", resource);
		// PlayerPrefs.SetInt (myname + "Environment", environment);
		// PlayerPrefs.SetInt (myname + "TaxRate", taxRate);
	}

	public void load(){
		if(PlayerPrefs.HasKey(myname + "DevValue"))
			devValue = PlayerPrefs.GetInt (myname + "DevValue");
		if(PlayerPrefs.HasKey(myname + "Population"))
		population = PlayerPrefs.GetInt (myname + "Population");
		if(PlayerPrefs.HasKey(myname + "ApprRate"))
			apprRate = PlayerPrefs.GetInt (myname + "ApprRate");
		if(PlayerPrefs.HasKey(myname + "Investment"))
			investment = PlayerPrefs.GetInt (myname + "Investment");
		if(PlayerPrefs.HasKey(myname + "Resource"))
			resource = PlayerPrefs.GetInt (myname + "Resource");
		if(PlayerPrefs.HasKey(myname + "Environment"))
			environment = PlayerPrefs.GetInt (myname + "Environment");
		if(PlayerPrefs.HasKey(myname + "TaxRate"))
			taxRate = PlayerPrefs.GetInt (myname + "TaxRate");
	}
}
