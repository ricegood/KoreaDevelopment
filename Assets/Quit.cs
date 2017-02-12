using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
	public GameObject UI_Quit;

	void start(){
	}
	void Update()
	{
		if (Input.GetKey("escape"))
		{
			UI_Quit.SetActive(true);
		}
	}
}