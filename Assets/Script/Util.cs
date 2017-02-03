using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Util : MonoBehaviour {
	private int order;
	public static bool popup = false;

	private int DEFAULT_MONEY;

	void Start(){
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
	}

	public void moveScene(string scene){
		SceneManager.LoadScene(scene);
	}

	public void setLevel(Dropdown d){
		Character.setLevel (d.value);
	}

	public void createCharacter(Text text){
		PlayerPrefs.DeleteAll ();

		switch (Character.getLevel()) {
		case 0:
			DEFAULT_MONEY = 10000;
			break;
		case 1:
			DEFAULT_MONEY = 5000;
			break;
		case 2:
			DEFAULT_MONEY = 3000;
			break;
		}

		string name = text.text.ToString();
		Character.setOrder (order+1);
		Character.setName (name);
		Country.setMoney (DEFAULT_MONEY);
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
		}
		else if(n < 1000000){
			return (n/1000).ToString() + "." + (n%1000).ToString() + "K" ;
		}
		else return (n/1000000).ToString() + "." + (n%1000000).ToString() + "M" ;
	}

	public static float GetFloat(string stringValue, float defaultValue)
	{
		float result = defaultValue;
		float.TryParse(stringValue, out result);
		return result;
	}

}