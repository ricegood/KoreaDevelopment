using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameRecord : MonoBehaviour {
	public Text infoText;
	public GameObject prev;
	public GameObject next;
	public Text titleText;
	public Text subTitleText;

	private int lastPage;
	private int page;

	// Use this for initialization
	void Start () {
		lastPage = Character.getOrder () - 1;
		if (lastPage > 1) {
			page = 1;
			pageUpdate ();
			prev.SetActive (false);
			next.SetActive (true);
		} else if (lastPage == 1) {
			page = 1;
			pageUpdate ();
			prev.SetActive (false);
			next.SetActive (false);
		} else {
			infoText.text="";
			next.SetActive (false);
			subTitleText.text="There are no previous president records in the Blue House.";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextPage(){
		prev.SetActive (true);
		page++;
		if (page == lastPage)
			next.SetActive (false);
		pageUpdate ();
	}

	public void prevPage(){
		next.SetActive (true);
		page--;
		if (page == 1)
			prev.SetActive (false);
		pageUpdate ();
	}

	private void pageUpdate(){
		titleText.text = Info.getOrder(page) + " " + PlayerPrefs.GetString (page + "name");
		if(PlayerPrefs.GetString (page + "clear") == "True"){
			// Game Clear
			subTitleText.text = "You have successfully finished the term of presidency.\n ~ " + printPeriod();
		}
		else {
			// Game Over
			subTitleText.text = "You have been impeached by Congress due to growing unpopularity.\n ~ " + printPeriod();
		}
		infoText.text = "Population : " + PlayerPrefs.GetInt (page + "population") + "\nGDP : " + PlayerPrefs.GetInt (page + "GDP") + "\nFine Dust Concentration : " + PlayerPrefs.GetInt (page + "environment") + "\nApproval Rating : " + PlayerPrefs.GetInt (page + "apprRate");
	}

	private string printPeriod(){
		return myTime.printMonth (PlayerPrefs.GetInt (page + "Month")) + " " + PlayerPrefs.GetInt (page + "Year");
	}
}
