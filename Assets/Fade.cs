using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {

	public GameObject thisObject;
	public GameObject nextObject;
	public string nextScene;
	private Text thisText;
	private float now;
	private float end;

	// Use this for initialization
	void Start () {
		thisText = thisObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (now <= 5) {
			now += Time.deltaTime;
			thisText.color = new Color (1f, 1f, 1f, now / 2);
		}

		else if (now >= 5 && end < 2) {
			end += Time.deltaTime;
			thisText.color = new Color (1f, 1f, 1f, 1 - end/2);
		}

		else if (end >= 2) {
			if (nextScene != "") {
				SceneManager.LoadScene ("Main");
			} else {
				thisObject.SetActive (false);
				nextObject.SetActive (true);
			}
		}
	}
}
