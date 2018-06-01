using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameObject Camera;
	public GameObject Canvas;

	int x = 0;
	public Sprite Dinosaur_Start;
	public Sprite Dinosaur_1; //Jump
	public Sprite Dinosaur_2; //Run 1
	public Sprite Dinosaur_3; //Run 2
	public Sprite Dinosaur_4; //Dead

	bool Is_Jumping;

	public float Speed = .1f;

	// Update is called once per frame
	void Update () {
		if (Canvas.GetComponent<Gamemaster>().Game_State == "Game") {
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + Speed, this.gameObject.transform.position.y);
			Speed += .00001f;
			if (Is_Jumping == false) {
				if (Input.GetMouseButtonDown(0)) {
					Jump();
				}
				if (x <= 10) {
					this.GetComponent<SpriteRenderer>().sprite = Dinosaur_2;
					x++;
				}else if (x >= 20) {
					this.GetComponent<SpriteRenderer>().sprite = Dinosaur_3;
					x = 0;
				}else {
					this.GetComponent<SpriteRenderer>().sprite = Dinosaur_3;
					x++;
				}
			}else {
				this.GetComponent<SpriteRenderer>().sprite = Dinosaur_1;
			}
		}else if (Canvas.GetComponent<Gamemaster>().Game_State == "Death") {
			this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			this.GetComponent<SpriteRenderer>().sprite = Dinosaur_4;
		}

		Camera.transform.position = new Vector3(this.gameObject.transform.position.x + 6, 0, -1);
	}

	public void Jump () {
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(4f * new Vector2(0, 3.75f), ForceMode2D.Impulse);
		Is_Jumping = true;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Is_Jumping = false;
		if (collision.gameObject.tag == "Death") {
			GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State = "End";
		}
	}
}
