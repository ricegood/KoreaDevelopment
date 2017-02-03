using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class myTime : MonoBehaviour {
	public Text timeText;
	public GameObject gameClearPanel;
	private static int year;
	private static int month;
	private static float now;
	public static bool timeStop;

	private const int SEC = 5; // after 5 seconds, 1 month passed.

	// Use this for initialization
	void Start () {
		load ();
		month = ((int)(now / SEC) % 12) + 1;
		year = ((int)(now / SEC) / 12) + 1;
		timeText.text = year.ToString() + "년 " + month.ToString() + "월";
	}

	// Update is called once per frame
	void Update () {
		if (!timeStop) {
			now += Time.deltaTime;
			month = ((int)(now / SEC) % 12) + 1;
			year = ((int)(now / SEC) / 12) + 1;
			timeText.text = year.ToString () + "년 " + month.ToString () + "월";
			save ();

			if ((year == 6) && (month == 1)) {
				gameClear ();
			}
		}
	}

	public void gameClear(){
		Debug.Log("GAME CLEAR!");
		Debug.Log ("avgApprRate = " + Country.getAvgApprRate());
		Debug.Log ("avgDevValue = " + Country.getAvgDevValue());
		timeStop = true;
		gameClearPanel.SetActive (true);
		Util.popup = true;
		Util.record (Character.getOrder (), Character.getName (), Country.getPopulation (), Country.getAvgGDP (), Country.getAvgEnvironment (), Country.getAvgApprRate (), true);

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
	}


}
