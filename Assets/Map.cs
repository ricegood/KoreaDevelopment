using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public const int DEFAULT = 0;
	public const int INDUSTRY = 1;
	public const int RESOURCE = 2;
	public const int ENVIRONMENT = 3;
	public const int SUPPORT = 4;

	public GameObject[] city;
	public static int type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

}
