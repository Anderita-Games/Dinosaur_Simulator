using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour {
	public Sprite[] Cacti;

	void Start () {
		int Species = Random.Range(0, 10);
		this.GetComponent<SpriteRenderer>().sprite = Cacti[Species];
		if (Species <= 5) {
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, -4.01f);
		}else {
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, -3.73f);
		}
	}

	void Update () {
		if (this.gameObject.transform.position.x < GameObject.Find("Player").transform.position.x - 10 || GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State == "Restart") {
			Destroy(this.gameObject);
		}
	}
}
