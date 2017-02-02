using UnityEngine;
using System.Collections;

public class Ulleungdo : MonoBehaviour {
	public SpriteRenderer thisMap;
	public SpriteRenderer Kyeongbuk;

	// Use this for initialization
	void Start () {
		thisMap.color = Kyeongbuk.color;
	}
	
	// Update is called once per frame
	void Update () {
		thisMap.color = Kyeongbuk.color;
	}
}
