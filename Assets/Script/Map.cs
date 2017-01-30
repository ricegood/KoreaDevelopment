using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {
	public Image[] mapButton;
	public Text[] mapButtonText;

	public const int DEFAULT = 0;
	public const int INDUSTRY = 1;
	public const int RESOURCE = 2;
	public const int ENVIRONMENT = 3;
	public const int SUPPORT = 4;

	public GameObject[] city;
	public static int type;

	private Color black = new Color (0f, 0f, 0f, 1f);
	private Color white = new Color (1f, 1f, 1f, 1f);

	// Use this for initialization
	void Start () {
		setButtonColor ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setType(int n){
		type = n;
	}

	public void setButtonColor(){
		for (int i = 0; i < mapButton.Length; i++) {
			mapButton [i].color = white;
			mapButtonText [i].color = black;
		}

		mapButton [type].color = black;
		mapButtonText [type].color = white;
	}

}
