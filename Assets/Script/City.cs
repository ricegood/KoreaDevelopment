using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class City : MonoBehaviour {
	private const int INVESTMONEY = 1000;
	private const int MININGMONEY = 5000;
	private const int MININGPROFIT = 5000; // per 1t.
	private const int MININGTIME = 10; // required time for mining (sec)

	public string myname;
	public string titleName;
	public int initDevValue; // initial GDP
	public float initResource; // initial resource

	public Map allMap;
	public SpriteRenderer map; 
	public GameObject detailPanel;
	public GameObject moneyPanel;
	public GameObject openCheckPanel;
	public GameObject[] roadList;

	private int devValue;		// GDP
	private int population = 0; // initial value
	private int apprRate = 50;	// 0~100 (%) initial value
	private int investment = 0; // initial value
	private float resource;		// unit : [t]
	private int environment = 0;
	private int taxRate = 10;	// GDP * taxRate/100

	private bool isMining;
	private float miningEndTime;

	private bool isRoadClicked;
	public static City firstChoosed;
	public static City secondChoosed;
	private bool roadInteract = true;

	private int savedMonth;
	private int treeNumber;


	// Use this for initialization
	void Start () {
		map = this.GetComponent<SpriteRenderer> ();
		devValue = initDevValue;
		resource = initResource;
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
		setEnvironment ((int)(devValue - resource * 0.5) - (int)(treeNumber/1000)*Plant.TREEVALUE);
		setApprRate (devValue,taxRate);
		population += 1;
		devValue = (int)(population * 0.1 + investment * 0.9)/50;

		save ();

		// Check the mining
		if(isMining && (myTime.getNow() > miningEndTime)){
			isMining = false;
			PlayerPrefs.SetString (myname + "IsMining", isMining.ToString ());

			// get profit
			Country.setMoney (Country.getMoney () + (int)((initResource * 0.1f) * MININGPROFIT));
		}

		// Map Color Update
		if (!RoadButton.roadPopup) {
			mapColorUpdate (Map.type);
		} else if (RoadButton.secondChoice) {
			updateEnableCity ();
		}

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

	public void mapColorUpdate(int type){
		switch (type) {
		case Map.DEFAULT:
			if (!RoadButton.roadPopup) {
				map.color = new Color ((float)(1 - devValue * 0.001), 1f, (float)(1 - devValue * 0.001), 1f);
			} else if (roadList.Length == 0) {
				map.color = new Color (1f, 1f, 1f, 0.5f);
				roadInteract = false;
			} else {
				map.color = new Color (1f, 1f, 1f, 1f);
				roadInteract = true;
			}
			break;
		case Map.INDUSTRY:
			// modify [ 4000 , 0.000025 ] values to change limitation.
			if (investment > 40000) map.color = new Color (0.3f, 0.3f, 1f, 1f);
			else map.color = new Color(1-(float)(investment*0.000025)*0.7f, 1-(float)(investment*0.000025)*0.7f, 1f, 1f);
			break;
		case Map.RESOURCE:
			if(isMining) map.color = new Color (1f, 0.55f, 0.55f, 1f);
			else map.color = new Color(1-(float)((float)resource/100)*0.7f, 1-(float)((float)resource/100)*0.7f, 1-(float)((float)resource/100)*0.7f, 1f);
			break;
		case Map.ENVIRONMENT:
			map.color = new Color(1f, (float)(1-environment*0.003), (float)(1-environment*0.003), 1f);
			break;
		case Map.SUPPORT:
			break;
		}

	}

	public void touchEvent(){
		switch (Map.type) {
		case Map.DEFAULT:
			// Open the Detail Panel
			if (!RoadButton.roadPopup) {
				detailPanel.GetComponent<CityDetail> ().setCity (this.GetComponent<City> ());
				detailPanel.GetComponent<CityDetail> ().imageUpdate ();
				detailPanel.GetComponent<CityDetail> ().textUpdate ();
				detailPanel.SetActive (true);
			}

			// if the road button is clicked -> create road.  (take 10 minutes)
			else if(roadInteract){
				// first choice
				if (!isRoadClicked && !RoadButton.secondChoice) {
					this.GetComponent<SpriteRenderer> ().color = new Color (1f, 0.55f, 0.55f, 1f);
					RoadButton.secondChoice = true;
					isRoadClicked = true;
					firstChoosed = this;
					updateEnableCity ();
				}

				// second choice
				else if(RoadButton.secondChoice && !isRoadClicked){
					this.GetComponent<SpriteRenderer> ().color = new Color (1f, 0.55f, 0.55f, 1f);
					isRoadClicked = true;
					openCheckPanel.SetActive(true);
					secondChoosed = this;

					// => then, choosed list reset when OK button
					Road thisRoad = Map.getRoad (firstChoosed, secondChoosed);
					openCheckPanel.SetActive (true);
					openCheckPanel.GetComponent<openCheckPanel>().setRoad (thisRoad);
					openCheckPanel.GetComponent<openCheckPanel> ().updateText ();

				}

				// double clicked
				else if(RoadButton.secondChoice && isRoadClicked){
					// back to original color
					this.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
					isRoadClicked = false;
					RoadButton.secondChoice = false;
					allMap.mapColorUpate ();
				}
			}
			break;
		case Map.INDUSTRY:
			// investment
			invest();
			break;
		case Map.RESOURCE:
			// mining (take 5 minutes)
			mining();
			break;
		case Map.ENVIRONMENT:
			// nothing
			break;
		case Map.SUPPORT:
			break;
		}
	}

	public void updateEnableCity(){
		if (this != firstChoosed && (Map.isLinked (firstChoosed, this) || !Map.isLinkable (firstChoosed, this))) {
			map.color = new Color (1f, 1f, 1f, 0.5f);
			roadInteract = false;
		}
	}

	public void setRoadInteract(bool b){
		roadInteract = b;
	}

	public void setIsRoadClicked(bool b){
		isRoadClicked = b;
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

	public float getResource(){
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

	public void mining(){
		if (!isMining) {
			if (Country.setMoney (Country.getMoney () - MININGMONEY)) {
				if (resource > 0) {
					// have resource
					resource -= initResource * 0.1f;
					isMining = true;
					PlayerPrefs.SetString (myname + "IsMining", isMining.ToString ());
					miningEndTime = myTime.getNow () + MININGTIME;
					PlayerPrefs.SetString (myname + "MiningEndTime", miningEndTime.ToString ());
					if (resource < 0)
						resource = 0;
				} else {
					// no resource
					Debug.Log ("resource 0!");
				}
			} else {
				// no money
				Util.popup = true;
				moneyPanel.SetActive (true);
			}
		} else {
			// already mining
			Debug.Log("Already Mining.");
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

	public void addTreeNum(int n){
		if(n > 0) treeNumber += n;
		PlayerPrefs.SetInt (myname + "TreeNumber", treeNumber);
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
		PlayerPrefs.SetString (myname + "Resource", resource.ToString());
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
			resource = Util.GetFloat(PlayerPrefs.GetString (myname + "Resource"), 0.0f);
		if(PlayerPrefs.HasKey(myname + "Environment"))
			environment = PlayerPrefs.GetInt (myname + "Environment");
		if(PlayerPrefs.HasKey(myname + "TaxRate"))
			taxRate = PlayerPrefs.GetInt (myname + "TaxRate");
		
		isMining = (PlayerPrefs.GetString (myname + "IsMining") == "True");
		miningEndTime = Util.GetFloat (PlayerPrefs.GetString (myname + "MiningEndTime"), 0.0f);
		treeNumber = PlayerPrefs.GetInt (myname + "TreeNumber");
	}
}
