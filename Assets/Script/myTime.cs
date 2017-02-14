using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class myTime : MonoBehaviour {
	public Text timeText;
	public GameObject gameClearPanel;
	private static int year;
	private static int month;
	private static float now;

	private static int prevYear;
	private static int prevMonth;

	public static bool timeStop;

	private const int SEC = 10; // after 5 seconds, 1 month passed.

	// Use this for initialization
	void Start () {
		prevMonth = ((int)(now / SEC) % 12) + 1;
		prevYear = ((int)(now / SEC) / 12) + 2050;
		load ();
		month = ((int)(now / SEC) % 12) + 1;
		year = ((int)(now / SEC) / 12) + 2050;
		timeText.text = printMonth(month) + " " + year.ToString ();
	}

	// Update is called once per frame
	void Update () {
		if (!timeStop) {
			now += Time.deltaTime;
			month = ((int)(now / SEC) % 12) + 1;
			year = ((int)(now / SEC) / 12) + 2050;
			timeText.text = printMonth(month) + " " + year.ToString ();
			save ();

			if (year*12 + month >= prevYear*12 + prevMonth + 60) {
				gameClear ();
			}
		}
	}

	public static string printMonth(int month){
		switch (month) {
		case 1: 
			return "JAN";
		case 2: 
			return "FEB";
		case 3:
			return "MAR";
		case 4:
			return "APR";
		case 5:
			return "MAY";
		case 6:
			return "JUN";
		case 7:
			return "JUL";
		case 8:
			return "AUG";
		case 9:
			return "SEP";
		case 10:
			return "OCT";
		case 11:
			return "NOV";
		case 12:
			return "DEC";
		default :
			return "";
		}
	}

	public void gameClear(){
		Debug.Log("GAME CLEAR!");
		Debug.Log ("avgApprRate = " + Country.getAvgApprRate());
		Debug.Log ("sumGDPValue = " + Country.getSumGDP());
		timeStop = true;
		gameClearPanel.SetActive (true);
		Util.popup = true;
		Util.record (Character.getOrder (), Character.getName (), Country.getPopulation (), Country.getSumGDP (), Country.getAvgEnvironment (), Country.getAvgApprRate (), true);

	}

	public static int getYear(){
		return year;
	}

	public static int getMonth(){
		return month;
	}

	public static float getNow(){
		return now;
	}

	private void save(){
		PlayerPrefs.SetString ("playTime", now.ToString());
	}
	private void load(){
		now = Util.GetFloat(PlayerPrefs.GetString ("playTime"), 0.0f);
		int order = PlayerPrefs.GetInt ("order");
		if(PlayerPrefs.HasKey((order-1) + "Year"))
			prevYear = PlayerPrefs.GetInt ((order-1) + "Year");

		if(PlayerPrefs.HasKey((order-1) + "Month"))
			prevMonth = PlayerPrefs.GetInt ((order-1) + "Month");
	}


}
