using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Util : MonoBehaviour {
	private int order;
	public static bool popup = false;

	private int DEFAULT_MONEY;

	void Start(){
		popup = false;
		myTime.timeStop = false;
		RoadButton.roadPopup = false;

		Debug.Log(PlayerPrefs.GetInt ("order"));
		order = PlayerPrefs.GetInt ("order");
		Screen.SetResolution(900, 1600, true); 
	}

	public static void record(int order, string name, int population, int GDP, int environment, int apprRate, bool clear){
		PlayerPrefs.SetString (order + "name", name);
		PlayerPrefs.SetInt (order + "population", population);
		PlayerPrefs.SetInt (order + "GDP", GDP);
		PlayerPrefs.SetInt (order + "environment", environment);
		PlayerPrefs.SetInt (order + "apprRate", apprRate);
		PlayerPrefs.SetString (order + "clear", clear.ToString());

		PlayerPrefs.SetInt (order + "Month", myTime.getMonth ());
		PlayerPrefs.SetInt (order + "Year", myTime.getYear ());
	}

	public void quit()
	{
		Application.Quit();
	}

	public void moveScene(string scene){
		popup = false;
		myTime.timeStop = false;
		RoadButton.roadPopup = false;

		SceneManager.LoadScene(scene);
	}

	public void setLevel(Dropdown d){
		Character.setLevel (d.value);
	}

	public void reset(){
		PlayerPrefs.DeleteAll ();
	}

	public void createCharacter(Text text){
		popup = false;
		myTime.timeStop = false;
		RoadButton.roadPopup = false;

		switch (Character.getLevel()) {
		case 0:
			DEFAULT_MONEY = 100000;
			break;
		case 1:
			DEFAULT_MONEY = 50000;
			break;
		case 2:
			DEFAULT_MONEY = 30000;
			break;
		}

		string name = text.text.ToString();
		Character.setOrder (order+1);
		Character.setName (name);
		Country.setMoney (DEFAULT_MONEY);
	}

	public void increaseOrder(){
		Character.setOrder (order+1);
	}

	public static void setText(Text t, string s){
		t.text = s;
	}

	public void setPopup(bool b){
		popup = b;
	}

	public static string printIntValue(int n){
		if (n < 1000) {
			return n.ToString ();
		} else if (n < 1000000) {
			return (n / 1000).ToString () + "." + ((n % 1000).ToString ()+"0000").Substring(0,4) + "K";
		} else if (n < 1000000000) {
			return (n / 1000000).ToString () + "." + ((n % 1000000).ToString ()+"0000").Substring(0,4) + "M";
		}
		else return (n / 1000000000).ToString () + "." + ((n % 1000000000).ToString ()+"0000").Substring(0,4) + "B";
	}

	public static float GetFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}

}