using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour {
	public GameObject loadingImage;
	public Text text;
	public GameObject[] lines;
	public static int page;

	// Use this for initialization
	void Start () {
		page = 0;
		text.text = "In the inauguration speech, president " + Character.getName() + " vowed to transform Korea into an economic powerhouse through sustainable development...";
		lines[0].SetActive (true);
	}

	public void nextPage(){
		if (page < 2) {
			lines [page++].SetActive (false);
			lines [page].SetActive (true);
		} else if (page == 2) {
			loadingImage.SetActive (true);
			SceneManager.LoadScene ("Main");
		}
	}

}