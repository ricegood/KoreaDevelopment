using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	public Image[] mapButton;
	public Text[] mapButtonText;
	public GameObject roadButton;
	public GameObject plantButton;
	public GameObject allRoad;
	public GameObject allApprRate;

	public const int DEFAULT = 0;
	public const int INDUSTRY = 1;
	public const int RESOURCE = 2;
	public const int ENVIRONMENT = 3;
	public const int SUPPORT = 4;

	public GameObject[] city;
	public static Dictionary<City, List<City>> graph = new Dictionary <City, List<City>>();
	public static Dictionary<City, List<City>> linkableGraph = new Dictionary <City, List<City>>();
	public static int type;

	private Color black = new Color (0f, 0f, 0f, 1f);
	private Color white = new Color (1f, 1f, 1f, 1f);

	// Use this for initialization
	void Start () {
		setButtonColor ();
		loadGraph ();

		/*
		foreach(KeyValuePair<City, List<City>> kv in graph) 
		{
			Debug.Log ("\n" + kv.Key.myname + ": ");
			for(int i=0; i<kv.Value.Count; i++)
				Debug.Log(kv.Value[i].myname+" "); 
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static bool isLinked(City a, City b){
		if (graph [a].Contains(b))
			return true;
		else
			return false;
	}

	public static bool isLinkable(City a, City b){
		if (linkableGraph [a].Contains(b))
			return true;
		else
			return false;
	}

	/*
	public static bool isConnected(City a, City b){
		return false;
	}

	private void setCityFlag(bool b){
		for(int i=0; i<city.Length; i++){
			City thisCity = city [i].GetComponent<City> ();
			thisCity.setFlag (b);
		}
	}
	*/

	public static Road getRoad(City a, City b){
		for (int i = 0; i < a.roadList.Length; i++) {
			if (b == a.roadList [i].GetComponent<Road> ().getAdgacencyCity (a)) {
				return a.roadList [i].GetComponent<Road> ();
			}
		}
		return null;
	}

	public void mapColorUpate(){
		for(int i=0; i<city.Length; i++){
			city [i].GetComponent<City> ().mapColorUpdate (type);
		}
	}
		
	public static Road getRoad(string cityName1, string cityName2){
		foreach(KeyValuePair<City, List<City>> kv in linkableGraph) 
		{
			if (kv.Key.titleName == cityName1) {
				for (int i = 0; i < kv.Value.Count; i++) {
					if (kv.Value [i].titleName == cityName2) {
						return getRoad (kv.Key, kv.Value [i]);
					}
				}
			}
		}
		return null;
	}

	// load graph
	public void loadGraph(){
		for (int i = 0; i < city.Length; i++) {
			City thisCity = city [i].GetComponent<City> ();
			graph.Add (thisCity, new List<City> ());
			linkableGraph.Add (thisCity, new List<City> ());
			for (int j = 0; j < thisCity.roadList.Length; j++) {
				// get roadList[j]
				Road thisRoad = thisCity.roadList[j].GetComponent<Road>();
				// if the road is completed, add the city to the graph.
				if(thisRoad.getCompleted()){
					graph [thisCity].Add (thisRoad.getAdgacencyCity(thisCity));
				}

				// always add the city to the LinkableGraph.
				linkableGraph [thisCity].Add (thisRoad.getAdgacencyCity(thisCity));
			}
		}
	}

	public static void addGraph(Road r){
		graph [r.getFirstCity()].Add (r.getSecondCity());
		graph [r.getSecondCity()].Add (r.getFirstCity());
	}

	// menu button..
	public void setType(int n){
		type = n;
	}

	public void resetIsRoadClicked(){
		for (int i = 0; i < city.Length; i++) {
			city [i].GetComponent<City> ().setIsRoadClicked (false);
			city [i].GetComponent<City> ().setRoadInteract (true);
		}
		RoadButton.secondChoice = false;
	}

	public void setButtonColor(){
		for (int i = 0; i < mapButton.Length; i++) {
			mapButton [i].color = white;
			mapButtonText [i].color = black;
		}

		mapButton [type].color = black;
		mapButtonText [type].color = white;


		switch (type) {
		case DEFAULT:
			roadButton.SetActive (true);
			plantButton.SetActive (false);
			allRoad.SetActive (true);
			allApprRate.SetActive (false);
			break;
		case ENVIRONMENT:
			plantButton.SetActive (true);
			roadButton.SetActive (false);
			allRoad.SetActive (false);
			allApprRate.SetActive (false);
			break;
		case SUPPORT:
			plantButton.SetActive (false);
			roadButton.SetActive (false);
			allRoad.SetActive (false);
			allApprRate.SetActive (true);
			break;
		case RESOURCE:
			plantButton.SetActive (false);
			roadButton.SetActive (false);
			allRoad.SetActive (false);
			allApprRate.SetActive (true);
			break;
		default:
			plantButton.SetActive (false);
			roadButton.SetActive (false);
			allRoad.SetActive (false);
			allApprRate.SetActive (false);
			break;
		}
	}

}
