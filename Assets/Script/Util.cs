using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Util : MonoBehaviour {
	private int order;

	private readonly int DEFAULT_MONEY = 1000;

	void start(){
		order = PlayerPrefs.GetInt ("order");
	}

	public void moveScene(string scene){
		SceneManager.LoadScene(scene);
	}

	public void createCharacter(Text text){
		string name = text.text.ToString();
		Character.setOrder (++order);
		Character.setName (name);
		Character.setMoney (DEFAULT_MONEY);
		PlayerPrefs.SetInt ("order", order);
	}

}