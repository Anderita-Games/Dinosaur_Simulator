using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	public float Speed = -.01f;
	public Sprite[] Bird_Move;
	int x = 0;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = Bird_Move[1];
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State == "Game") {
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + Speed, this.gameObject.transform.position.y);

			if (x <= 20) {
				this.GetComponent<SpriteRenderer>().sprite = Bird_Move[0];
				x++;
			}else if (x >= 40) {
				this.GetComponent<SpriteRenderer>().sprite = Bird_Move[1];
				x = 0;
			}else {
				this.GetComponent<SpriteRenderer>().sprite = Bird_Move[1];
				x++;
			}
		}

		if (this.gameObject.transform.position.x < GameObject.Find("Player").transform.position.x - 10 || GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State == "Restart") {
			Destroy(this.gameObject);
		}
	}
}
