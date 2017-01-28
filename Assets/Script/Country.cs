using UnityEngine;
using System.Collections;

public class Country : MonoBehaviour {
	public GameObject map;
	public GameObject[] cityList;
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
		cityList [n].SetActive (true);
	}

	public void closeCityMap(int n){
		cityList [n].SetActive (false);
		map.SetActive (true);
	}
}
