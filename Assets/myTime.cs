using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class myTime : MonoBehaviour {
	public Text timeText;
	private static int year;
	private static int month;
	private static float now;
	// Use this for initialization
	void Start () {
		load ();
		month = ((int)(now / 5) % 12) + 1;
		year = ((int)(now / 5) / 12) + 1;
		timeText.text = year.ToString() + "년 " + month.ToString() + "월";
	}

	// Update is called once per frame
	void Update () {
		now += Time.deltaTime;
		month = ((int)(now / 5) % 12) + 1;
		year = ((int)(now / 5) / 12) + 1;
		timeText.text = year.ToString() + "년 " + month.ToString() + "월";
		save ();
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
		now = GetFloat(PlayerPrefs.GetString ("playTime"), 0.0f);
	}

	private float GetFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}
}
