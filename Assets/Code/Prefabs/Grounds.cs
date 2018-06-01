using UnityEngine;

public class Grounds : MonoBehaviour {
	void Update () {
		if (this.gameObject.transform.position.x < GameObject.Find("Player").transform.position.x - 60 || GameObject.Find("Canvas").GetComponent<Gamemaster>().Game_State == "Restart") {
			Destroy(this.gameObject);
		}
	}
}