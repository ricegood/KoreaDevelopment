﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey("order")) {
			SceneManager.LoadScene("Intro");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
