using UnityEngine;
using System.Collections;

public class Country : MonoBehaviour {
	public GameObject map;
	public GameObject[] cityMapList;
	private static int devValue;
	private static int population;
	private static int apprRate;
	private static int taxRate;

	// Use this for initialization
	void Start () {
		Character.load ();
	}
	
	// Update is called once per frame
	void Update () {
		//Character.save ();
	}

	public void openCityMap(int n){
		map.SetActive (false);
		cityMapList [n].SetActive (true);
	}

	public void closeCityMap(int n){
		cityMapList [n].SetActive (false);
		map.SetActive (true);
	}
}
