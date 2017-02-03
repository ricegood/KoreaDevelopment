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
			subTitleText.text="역대 대통령의 기록이 없습니다.";
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
			subTitleText.text = "무사히 임기를 마쳤습니다.";
		}
		else {
			// Game Over
			subTitleText.text = "국민들의 지지율이 너무 낮아 탄핵되었습니다.";
		}
		infoText.text = "Population : " + PlayerPrefs.GetInt (page + "population") + "\nGDP : " + PlayerPrefs.GetInt (page + "GDP") + "\nFine Dust Concentration : " + PlayerPrefs.GetInt (page + "environment") + "\nApproval Rating : " + PlayerPrefs.GetInt (page + "apprRate");
	}
}
