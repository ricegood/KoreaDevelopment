using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	public Image[] mapButton;
	public Text[] mapButtonText;
	public GameObject roadButton;

	public const int DEFAULT = 0;
	public const int INDUSTRY = 1;
	public const int RESOURCE = 2;
	public const int ENVIRONMENT = 3;
	public const int SUPPORT = 4;

	public GameObject[] city;
	public Dictionary<City, List<City>> graph = new Dictionary <City, List<City>>();
	public static int type;

	private Color black = new Color (0f, 0f, 0f, 1f);
	private Color white = new Color (1f, 1f, 1f, 1f);

	// Use this for initialization
	void Start () {
		setButtonColor ();
		loadGraph ();

		foreach(KeyValuePair<City, List<City>> kv in graph) 
		{
			Debug.Log ("\n" + kv.Key.myname + ": ");
			for(int i=0; i<kv.Value.Count; i++)
				Debug.Log(kv.Value+" "); 
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void mapColorUpate(){
		for(int i=0; i<city.Length; i++){
			city [i].GetComponent<City> ().mapColorUpdate (type);
		}
	}

	// load graph
	public void loadGraph(){
		for (int i = 0; i < city.Length; i++) {
			City thisCity = city [i].GetComponent<City> ();
			graph.Add (thisCity, new List<City> ());
			for (int j = 0; j < thisCity.roadList.Length; j++) {
				// get roadList[j]
				Road thisRoad = thisCity.roadList[j].GetComponent<Road>();

				// if the road is completed, add the city to the graph.
				if(thisRoad.getCompleted()){
					graph [thisCity].Add (thisRoad.getAdgacencyCity(thisCity));
				}
			}
		}
	}

	// menu button..
	public void setType(int n){
		type = n;
	}

	public void setButtonColor(){
		for (int i = 0; i < mapButton.Length; i++) {
			mapButton [i].color = white;
			mapButtonText [i].color = black;
		}

		mapButton [type].color = black;
		mapButtonText [type].color = white;

		if (type == 0)
			roadButton.SetActive (true);
		else
			roadButton.SetActive (false);
	}

}
