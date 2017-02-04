using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class Country : MonoBehaviour {
	public const int EASY_GAMEOVER_VALUE = 30;
	public const int INTER_GAMEOVER_VALUE = 35;
	public const int HARD_GAMEOVER_VALUE = 40;

	public Map map;
	public GameObject GameOverPanel;
	private static int money;

	private static int avgDevValue;
	private static int population;
	private static int avgApprRate;
	private static int avgEnvironment;

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey("order")) {
			SceneManager.LoadScene("Intro");
		}
		load ();
	}

	// Update is called once per frame
	void Update () {
		if (!myTime.timeStop) {
			population = getPop (map.city);
			avgApprRate = sumApprRate (map.city) / population;
			avgDevValue = sumDevValue (map.city) / population;
			avgEnvironment = sumEnvironment (map.city) / map.city.Length;

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

	public static int getAvgDevValue(){
		return avgDevValue;
	}

	public static int getAvgGDP(){
		return avgDevValue * 1000;
	}

	public static int getPopulation(){
		return population;
	}

	private void gameOver(){
		myTime.timeStop = true;
		GameOverPanel.SetActive (true);
		Util.popup = true;
		Util.record (Character.getOrder (), Character.getName (), getPopulation (), getAvgGDP (), getAvgEnvironment (), getAvgApprRate (), false);
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

	private int sumDevValue(GameObject[] cityList){
		int sum = 0;
		for(int i=0; i<cityList.Length; i++){
			City thisCity = cityList [i].GetComponent<City> ();
			sum += (int)thisCity.getPopulation ()*thisCity.getDevValue();
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

	private static void load(){
		Character.load ();
		money = PlayerPrefs.GetInt ("money");
	}

}
