using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour {
	public Text text;
	public GameObject firstLine;

	// Use this for initialization
	void Start () {
		text.text = "In the inauguration speech, president " + Character.getName() + " vowed to transform Korea into an economic powerhouse through sustainable development...";
		firstLine.SetActive (true);
	}
}
