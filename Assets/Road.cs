using UnityEngine;
using System.Collections;

public class Road : MonoBehaviour {
	public SpriteRenderer roadImage;
	public string myname;
	public City[] city = new City[2];
	public int distance;
	private bool isCompleted;

	// Use this for initialization
	void Start () {
		load ();
		if (isCompleted)
			roadImage.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public City getFirstCity(){
		return city [0];
	}

	public City getSecondCity(){
		return city[1];
	}

	public string getFirstCityName(){
		return city[0].titleName;
	}

	public string getSecondCityName(){
		return city[1].titleName;
	}

	public City getAdgacencyCity(City myCity){
		if (city [0] == myCity)
			return city [1];
		else
			return city [0];
	}

	public void setCompleted(bool n){
		if (n == true) {
			//mapping!
		}
		isCompleted = n;
		PlayerPrefs.SetString (myname + "IsCompleted", isCompleted.ToString ());
		roadImage.enabled = true;
	}

	public bool getCompleted(){
		return isCompleted;
	}

	private void save(){
		PlayerPrefs.SetString (myname + "IsCompleted", isCompleted.ToString ());
	}

	private void load(){
		isCompleted = (PlayerPrefs.GetString (myname + "IsCompleted") == "True");
	}
}
