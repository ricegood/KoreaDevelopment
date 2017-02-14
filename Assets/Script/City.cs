using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class City : MonoBehaviour {
	private const int INVESTMONEY = 10000;
	private const int MININGMONEY = 50000;
	private const int MININGPROFIT = 50000; // per 1kt.
	private const int MININGTIME = 5; // required time for mining (sec)
	private const int POPMIN = 1000;
	private const int POPMAX = 5000;

	public string myname;
	public string titleName;
	public int initDevValue; // initial GDP
	public float initResource; // initial resource

	public Map allMap;
	public SpriteRenderer map; 
	public GameObject detailPanel;
	public GameObject moneyPanel;
	public GameObject miningPanel;
	public GameObject openCheckPanel;
	public GameObject[] roadList;
	public Text apprRateText;
	public AudioSource click;

	private int devValue;		// GDP
	private float population; // initial value
	private int apprRate = 50;	// 0~100 (%) initial value
	private int investment = 0; // initial value
	private float resource;		// unit : [t]
	private int environment = 0;
	private int taxRate = 10;	// GDP * taxRate/100
	private int roadNumber;

	private int prevDevValue;
	private int prevEnvironment;
	private int prevTaxRate = 10;

	private int dDevValue;
	private int dEnvironment;
	private int dTaxRate;
	private float dResource;

	private bool isMining;
	private float miningEndTime;

	private bool isRoadClicked;
	public static City firstChoosed;
	public static City secondChoosed;
	private bool roadInteract = true;

	private int savedMonth;
	private static int treeNumber;



	// Use this for initialization
	void Start () {
		map = this.GetComponent<SpriteRenderer> ();
		devValue = initDevValue;
		prevDevValue = initDevValue;
		resource = initResource;
		savedMonth = myTime.getMonth ();
		System.Random rnd = new System.Random();
		population = rnd.Next(POPMIN, POPMAX); // population number initialization
		setRoadNum(roadList);
		load ();

		//delta value
		dResource = initResource - resource;
		dDevValue = devValue - prevDevValue;
		dEnvironment = environment - prevEnvironment;
		dTaxRate = taxRate - prevTaxRate;
		//Debug.Log (myname + " delta value : " + dDevValue + " / " + dEnvironment + " / " + dTaxRate);
	}


	// Update is called once per frame

	void Update () {
		if (!myTime.timeStop) {
			
			// get money every 1 month.
			if (savedMonth != myTime.getMonth ()) {
				savedMonth = myTime.getMonth ();
				Country.setMoney (Country.getMoney () + (int)(((float)taxRate / 100) * devValue * 100));
			}


			// <! ------ Value Update Start --------> 
			setDevValue (initDevValue, population, investment, roadNumber);
			dResource = initResource - resource;
			setEnvironment (investment, roadNumber, dResource, treeNumber);

			dDevValue = devValue - prevDevValue;
			dEnvironment = environment - prevEnvironment;
			dTaxRate = taxRate - prevTaxRate;

			setApprRate (dDevValue, dTaxRate, dEnvironment);
			setPopulation();
			// <! ------ Value Update End -------->


			// Check the mining
			if (isMining && (myTime.getNow () > miningEndTime)) {
				isMining = false;
				PlayerPrefs.SetString (myname + "IsMining", isMining.ToString ());

				// get profit
				Country.setMoney (Country.getMoney () + (int)(10 * MININGPROFIT));
			}


			// Map Color Update
			if (!RoadButton.roadPopup) {
				mapColorUpdate (Map.type);
			} else if (RoadButton.secondChoice) {
				updateEnableCity ();
			}

			// Click(touch) Event
			if (Input.GetMouseButtonDown (0)) {
				Vector2 pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
				// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
				if (!Util.popup && !detailPanel.activeSelf && hitInfo && hitInfo.transform.gameObject.name == myname) {
					touchEvent ();
				}
			}


		}

	}


	public void mapColorUpdate(int type){
		switch (type) {
		case Map.DEFAULT:
			if (!RoadButton.roadPopup) {
				map.color = new Color ((float)(1 - devValue * 0.0005), 1f, (float)(1 - devValue * 0.0005), 1f);
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
			if (investment > 400000) map.color = new Color (0.3f, 0.3f, 1f, 1f);
			else map.color = new Color(1-(float)(investment*0.0000025)*0.7f, 1-(float)(investment*0.0000025)*0.7f, 1f, 1f);
			break;
		case Map.RESOURCE:
			if (isMining) {
				map.color = new Color (1f, 0.55f, 0.55f, 1f);
				apprRateText.text = (int)(miningEndTime - myTime.getNow ()) + "s";
			} else {
				apprRateText.text = resource+"kt";
				map.color = new Color (1 - (float)((float)resource / 160) * 0.7f, 1 - (float)((float)resource / 160) * 0.7f, 1 - (float)((float)resource / 160) * 0.7f, 1f);
			}
			break;
		case Map.ENVIRONMENT:
			map.color = new Color(1f, (float)(1-environment*0.005), (float)(1-environment*0.005), 1f);
			break;
		case Map.SUPPORT:
			map.color = new Color (1f, (float)(1 - apprRate * 0.001), (float)(1 - apprRate * 0.01), 1f);
			apprRateText.text = apprRate.ToString() + "%";
			break;
		}

	}

	public void touchEvent(){
		switch (Map.type) {
		case Map.DEFAULT:
			// Open the Detail Panel
			if (!RoadButton.roadPopup) {
				click.Play ();
				detailPanel.GetComponent<CityDetail> ().setCity (this);
				detailPanel.GetComponent<CityDetail> ().imageUpdate ();
				detailPanel.GetComponent<CityDetail> ().textUpdate ();
				detailPanel.SetActive (true);
			}

			// if the road button is clicked -> create road.  (take 10 minutes)
			else if(roadInteract){
				// first choice
				click.Play ();
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
					Util.popup = true;
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
			click.Play ();
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

	public float getPopulation(){
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

	public int getGDP(){
		return devValue * 1000;
	}


	public void increaseTaxRate(){
		if (taxRate < 100) {
			taxRate++;
			PlayerPrefs.SetInt (Character.order + myname + "TaxRate", taxRate);
		}
	}

	public void decreaseTaxRate(){
		if (taxRate > 0) {
			taxRate--;
			PlayerPrefs.SetInt (Character.order + myname + "TaxRate", taxRate);
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
			click.Play ();
			if (Country.setMoney (Country.getMoney () - MININGMONEY)) {
				if (resource > 0) {
					// have resource
					// resource -= initResource * 0.1f;
					resource -= 10;
					if (resource <= 0) resource = 0;
					PlayerPrefs.SetString (myname + "Resource", resource.ToString());
					isMining = true;
					PlayerPrefs.SetString (myname + "IsMining", isMining.ToString ());
					miningEndTime = myTime.getNow () + MININGTIME;
					PlayerPrefs.SetString (myname + "MiningEndTime", miningEndTime.ToString ());
				} else {
					// no resource
					Util.popup = true;
					miningPanel.SetActive(true);
				}
			} else {
				// no money
				Util.popup = true;
				moneyPanel.SetActive (true);
			}
		} else {
			// already mining
			//Debug.Log("Already Mining.");
		}
	}

	public static void addTreeNum(int n){
		if(n > 0) treeNumber += n;
		PlayerPrefs.SetInt ("TreeNumber", treeNumber);
	}

	public static int getTreeNum(){
		return treeNumber;
	}

	public void setRoadNum(GameObject[] roadList){
		int count = 0;

		for (int i = 0; i < roadList.Length; i++) {
			if (roadList [i].GetComponent<Road> ().getCompleted ())
				count++;
		}
		roadNumber = count;
	}

	public int getRoadNum(){
		return roadNumber;
	}


	/********************************************/
	/* <! ------ Value Update Function--------> */
	/********************************************/

	private void setPopulation(){
		population = population + 0.01f;
		PlayerPrefs.SetFloat (myname + "Population", population);
	}

	private void setEnvironment(int investment, int roadNumber, float dResource, int treeNumber){
		int result = (int)(investment*0.0004 + roadNumber*10 + dResource) - (int)(treeNumber / 1000) * Plant.TREEVALUE;

		if (result < 0) {
			environment = 0;
			PlayerPrefs.SetInt (Character.order + myname + "Environment", environment);
		} else if (result > 200) {
			environment = 200;
			PlayerPrefs.SetInt (Character.order + myname + "Environment", environment);
		} else{
			environment = result;
			PlayerPrefs.SetInt (Character.order + myname + "Environment", environment);
		}
	}

	private void setDevValue(int initDevValue, float population, int investment, int roadNumber){
		devValue = initDevValue + (int)(population * 0.05 + investment * 0.5 + roadNumber * 7500) / 100;
		PlayerPrefs.SetInt (Character.order + myname + "DevValue", devValue);
	}

	private void setApprRate(int devValue, int taxRate, int environment){
		int result;

		if (taxRate >= 0) {
			result = (int)(50 + (devValue * 0.02) - Math.Pow (taxRate, 1.25) - environment * 0.15);
		} else {
			result = (int)(50 + (devValue * 0.02) + Math.Pow (-taxRate, 1.25) - environment * 0.15);
		}

		if (result < 0) {
			apprRate = 0;
			PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		} else if (result > 100) {
			apprRate = 100;
			PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		} else{
			apprRate = result;
			PlayerPrefs.SetInt (myname + "ApprRate", apprRate);
		}
	}




	/********************************************/
	/* <! ------Data   Load   Function--------> */
	/********************************************/

	private void load(){
		if (PlayerPrefs.HasKey (Character.getOrder () + myname + "DevValue")) {
			devValue = PlayerPrefs.GetInt (Character.getOrder () + myname + "DevValue");
		}
		else if (PlayerPrefs.HasKey ((Character.getOrder () - 1) + myname + "DevValue")) {
			devValue = PlayerPrefs.GetInt ((Character.getOrder () - 1) + myname + "DevValue");
			PlayerPrefs.SetInt ((Character.getOrder ()) + myname + "DevValue", devValue);
		}
				
		if(PlayerPrefs.HasKey(myname + "Population"))
			population = PlayerPrefs.GetFloat (myname + "Population");
		if(PlayerPrefs.HasKey(myname + "ApprRate"))
			apprRate = PlayerPrefs.GetInt (myname + "ApprRate");
		if(PlayerPrefs.HasKey(myname + "Investment"))
			investment = PlayerPrefs.GetInt (myname + "Investment");
		if(PlayerPrefs.HasKey(myname + "Resource"))
			resource = Util.GetFloat(PlayerPrefs.GetString (myname + "Resource"), 0.0f);
		
		if (PlayerPrefs.HasKey (Character.getOrder () + myname + "Environment"))
			environment = PlayerPrefs.GetInt (Character.getOrder () + myname + "Environment");
		else if (PlayerPrefs.HasKey ((Character.getOrder () - 1) + myname + "Environment")) {
			environment = PlayerPrefs.GetInt ((Character.getOrder () - 1) + myname + "Environment");
			PlayerPrefs.SetInt ((Character.getOrder ()) + myname + "Environment", environment);
		}
				
		if (PlayerPrefs.HasKey (Character.getOrder () + myname + "TaxRate")) {
			taxRate = PlayerPrefs.GetInt (Character.getOrder () + myname + "TaxRate");
		}
		else if (PlayerPrefs.HasKey ((Character.getOrder () - 1) + myname + "TaxRate")) {
			taxRate = PlayerPrefs.GetInt ((Character.getOrder () - 1) + myname + "TaxRate");
			PlayerPrefs.SetInt ((Character.getOrder ()) + myname + "TaxRate", taxRate);
		}
		
		isMining = (PlayerPrefs.GetString (myname + "IsMining") == "True");
		miningEndTime = Util.GetFloat (PlayerPrefs.GetString (myname + "MiningEndTime"), 0.0f);
		treeNumber = PlayerPrefs.GetInt ("TreeNumber");

		if (PlayerPrefs.HasKey ((Character.getOrder ()-1) + myname + "DevValue"))
			prevDevValue = PlayerPrefs.GetInt ((Character.getOrder () - 1) + myname + "DevValue");
		
		if(PlayerPrefs.HasKey ((Character.getOrder ()-1) + myname + "Environment"))
			prevEnvironment = PlayerPrefs.GetInt((Character.order-1) + myname + "Environment" );

		if(PlayerPrefs.HasKey ((Character.getOrder ()-1) + myname + "TaxRate"))
			prevTaxRate = PlayerPrefs.GetInt ((Character.order-1) + myname + "TaxRate");

	}
}
