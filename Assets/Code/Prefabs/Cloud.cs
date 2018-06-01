using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
	float Speed;

	// Use this for initialization
	void Start () {
		Speed = Random.Range(-.05f + GameObject.Find("Player").GetComponent<Player>().Speed, .05f + GameObject.Find("Player").GetComponent<Player>().Speed);
	}
	
	void Update () {
		if (GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State == "Game") {
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + Speed, this.gameObject.transform.position.y);
		}

		if (this.gameObject.transform.position.x < GameObject.Find("Player").transform.position.x - 10 || GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State == "Restart" || this.gameObject.transform.position.x > GameObject.Find("Player").transform.position.x + 50) {
			Destroy(this.gameObject);
		}
	}
}
