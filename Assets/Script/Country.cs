using UnityEngine;
using System.Collections;

public class Country : MonoBehaviour {
	public const int EASY_GAMEOVER_VALUE = 30;
	public const int INTER_GAMEOVER_VALUE = 35;
	public const int HARD_GAMEOVER_VALUE = 40;

	public Map map;
	public GameObject GameOverPanel;

	public Achieve[] achievements;
	public GameObject achieveImage;

	private static int money;

	private static int sumDevValue;
	private static int population;
	private static int avgApprRate;
	private static int avgEnvironment;

	private int savedMonth;

	// Use this for initialization
	void Start () {
		load ();
		checkAchievement ();
		population = getPop (map.city);
		sumDevValue = getSumDevValue (map.city);
		avgApprRate = sumApprRate (map.city) / population;
		avgEnvironment = sumEnvironment (map.city) / map.city.Length;
		savedMonth = myTime.getMonth ();
	}

	// Update is called once per frame
	void Update () {
		if (!myTime.timeStop) {
			population = getPop (map.city);
			sumDevValue = getSumDevValue (map.city);
			avgApprRate = sumApprRate (map.city) / population;
			avgEnvironment = sumEnvironment (map.city) / map.city.Length;

			// check the accomplishments every 1 month.
			if (!Util.popup) {
				if (savedMonth != myTime.getMonth ()) {
					savedMonth = myTime.getMonth ();
					checkAchievement ();
				}
			}

			switch (Character.getLevel ()) {
			case 0:
				if (avgApprRate < EASY_GAMEOVER_VALUE) {
					// GAME OVER
					gameOver ();
				}
				break;
			case 1:
				if (avgApprRate < INTER_GAMEOVER_VALUE) {
					// GAME OVER
					gameOver ();
				}
				break;
			case 2:
				if (avgApprRate < HARD_GAMEOVER_VALUE) {
					// GAME OVER
					gameOver ();
				}
				break;
			}
		}
	}

	public static int getAvgApprRate(){
		return avgApprRate;
	}

	public static int getAvgEnvironment(){
		return avgEnvironment;
	}

	public static int getSumGDP(){
		return sumDevValue*1000;
	}

	public static int getPopulation(){
		return population;
	}

	private void gameOver(){
		myTime.timeStop = true;
		GameOverPanel.SetActive (true);
		Util.popup = true;
		Util.record (Character.getOrder (), Character.getName (), getPopulation (), getSumGDP (), getAvgEnvironment (), getAvgApprRate (), false);
	}

	private void checkAchievement(){
		for (int i = 0; i < achievements.Length; i++) {
			achievements [i].checkComplete ();
			if (achievements [i].getComplete () && !achievements[i].getGetReward()) {
				achieveImage.SetActive (true);
			}
		}
	}

	private int getPop(GameObject[] cityList){
		int sum = 0;
		for(int i=0; i<cityList.Length; i++){
			City thisCity = cityList [i].GetComponent<City> ();
			sum += (int)(thisCity.getPopulation ());
		}
		return sum;
	}

	private int sumApprRate(GameObject[] cityList){
		int sum = 0;
		for(int i=0; i<cityList.Length; i++){
			City thisCity = cityList [i].GetComponent<City> ();
			sum += (int)thisCity.getPopulation ()*thisCity.getApprRate();
		}
		return sum;
	}

	private int sumEnvironment(GameObject[] cityList){
		int sum = 0;
		for(int i=0; i<cityList.Length; i++){
			City thisCity = cityList [i].GetComponent<City> ();
			sum += thisCity.getEnvironment();
		}
		return sum;
	}

	private int getSumDevValue(GameObject[] cityList){
		int sum = 0;
		for(int i=0; i<cityList.Length; i++){
			City thisCity = cityList [i].GetComponent<City> ();
			sum += (int)thisCity.getDevValue();
		}
		return sum;
	}

	public static int getMoney(){
		return money;
	}

	public static bool setMoney(int n){
		if (n >= 0) {
			money = n;
			PlayerPrefs.SetInt ("money", money);
			return true;
		} else {
			return false;
		}
	}

	private void load(){
		Character.load ();
		money = PlayerPrefs.GetInt ("money");

		for (int i = 0; i < achievements.Length; i++) {
			achievements [i].load ();
		}
	}

}
