using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	public GameObject MainMenu;
	public GameObject StartScene;

	public void gameStart(){
		if (!PlayerPrefs.HasKey("order")) {
			MainMenu.SetActive(false);
			StartScene.SetActive (true);
		}
		else SceneManager.LoadScene("Main");
	}
}
