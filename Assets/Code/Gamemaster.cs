using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour {
	public GameObject Player;
	public GameObject Cactus;
	public GameObject Bird;
	public GameObject Ground;
	public GameObject Cloud;

	int Cactus_Counter = 0;
	int Bird_Counter = 0;
	int Ground_Counter = 0;
	int Cloud_Counter = 0;

	public string Game_State = "Title";
	float Ground_Start;
	float Enemy_Max_Distance = 8.00f;
	float Start_Time;
	int Score = 0;

	public GameObject Game_End;
	public UnityEngine.UI.Text Score_Text;
	public UnityEngine.UI.Text Highscore_Text;

	// Use this for initialization
	void Start () {
		Ground_Start = Random.Range(-20.000f, 20.000f);
		StartCoroutine(Ground_Generation(Ground_Start));
		Highscore_Text.text = "";
		if (PlayerPrefs.GetInt("Highscore") != 0) {
			Highscore_Text.text = "HI " + Zeros(PlayerPrefs.GetInt("Highscore")) + " ";
		}
		Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && Game_State == "Title") {
			StartCoroutine(Game_Start());
		}else if (Game_State == "Game") {
			Score = Mathf.RoundToInt((Time.timeSinceLevelLoad - Start_Time) * 10);
			if (PlayerPrefs.GetInt("Highscore") != 0) {
				Highscore_Text.text = "HI " + Zeros(PlayerPrefs.GetInt("Highscore")) + " ";
			}
			Score_Text.text = Zeros(Score) + " ";
		}else if (Game_State == "End") {
			if (Score > PlayerPrefs.GetInt("Highscore")) {
				PlayerPrefs.SetInt("Highscore", Score);
			}
			Game_State = "Death";
			Game_End.SetActive(true);
		}
	}

	string Zeros (int Input) {
		string Output = Input.ToString();
		if (Input < 0) {
			Output = "00000";
		}else if (Input < 10) {
			Output = "0000" + Input.ToString();
		}else if (Input < 100) {
			Output = "000" + Input.ToString();
		}else if (Input < 1000) {
			Output = "00" + Input.ToString();
		}else if (Input < 10000) {
			Output = "0" + Input.ToString();
		}else if (Input > 99999) {
			Output = "99999";
		}
		return Output;
	}

	IEnumerator Enemy_Generation (float x) {
		int i = 0;
		if (Game_State == "Game") {
			GameObject Creation = null;
			if (Score < 100) {
				Creation = Cacti_Generation(Creation, x, i);
			}else if (Score < 200) {
				Creation = Cacti_Generation(Creation, x, i);
				if (Random.Range(1, 4) == 1) {
					i++;
					Creation = Cacti_Generation(Creation, x, i);
				}
				Enemy_Max_Distance = 17.00f;
			}else if (Score < 400) {
				Creation = Cacti_Generation(Creation, x, i);
				if (Random.Range(1, 3) == 1) {
					i++;
					Creation = Cacti_Generation(Creation, x, i);
					if (Random.Range(1, 3) == 1) {
						i++;
						Creation = Cacti_Generation(Creation, x, i);
					}
				}
				Enemy_Max_Distance = 15.00f;
			}else if (Score < 800) {
				if (Random.Range(1, 6) == 1) {
					Creation = Bird_Generation(Creation, x, i);
				}else {
					Creation = Cacti_Generation(Creation, x, i);
					if (Random.Range(1, 3) == 1) {
						i++;
						Creation = Cacti_Generation(Creation, x, i);
						if (Random.Range(1, 3) == 1) {
							i++;
							Creation = Cacti_Generation(Creation, x, i);
						}
					}
				}
				Enemy_Max_Distance = 13.00f;
			}else if (Score < 1600) {
				if (Random.Range(1, 4) == 1) {
					Creation = Bird_Generation(Creation, x, i);
				}else {
					Creation = Cacti_Generation(Creation, x, i);
					if (Random.Range(1, 2) == 1) {
						i++;
						Creation = Cacti_Generation(Creation, x, i);
						if (Random.Range(1, 3) == 1) {
							i++;
							Creation = Cacti_Generation(Creation, x, i);
						}
					}
				}
				Enemy_Max_Distance = 11.00f;
			}
		}
		while (Player.transform.position.x * 1f <= x - 16f){
			yield return new WaitForSecondsRealtime(.0001f);
		}
		StartCoroutine(Enemy_Generation(x + Random.Range(8.00f, Enemy_Max_Distance + (1.31f * i))));
	}

	GameObject Cacti_Generation (GameObject Creation, float x, int i) {
		Creation = Instantiate(Cactus,  new Vector2(x + (1.31f * i), 0), Quaternion.identity);
		Cactus_Counter++;
		Creation.name = "Cactus #" + Cactus_Counter;
		return Creation;
	}

	GameObject Bird_Generation (GameObject Creation, float x, int i) {
		Creation = Instantiate(Bird,  new Vector2(x, Random.Range(-2.25f, 1f)), Quaternion.identity);
		Bird_Counter++;
		Creation.name = "Bird #" + Bird_Counter;
		return Creation;
	}

	IEnumerator Ground_Generation (float x) {
		GameObject Creation = Instantiate(Ground,  new Vector3(x, -4.5f, .1f), Quaternion.identity);
		Ground_Counter++;
		Creation.name = "Ground #" + Ground_Counter;
		while (Player.transform.position.x * 1f <= x -  83.79f){
			yield return new WaitForSecondsRealtime(.0001f);
		}
		if (Game_State == "Game") {
			StartCoroutine(Ground_Generation(x + 83.79f));
		}else {
			Ground_Start += 83.79f;
		}
	}

	IEnumerator Cloud_Generation (float x) {
		GameObject Creation = Instantiate(Cloud,  new Vector3(x, Random.Range(1f, 3.5f), 2f), Quaternion.identity);
		Cloud_Counter++;
		Creation.name = "Cloud #" + Cloud_Counter;
		float Distance = Random.Range(10.000f, 30.000f);
		while (Player.transform.position.x * 1f <= x - Distance){
			yield return new WaitForSecondsRealtime(.0001f);
		}
		if (Game_State == "Game") {
			StartCoroutine(Cloud_Generation(x + Distance));
		}
	}

	public void Restart () {
		Game_State = "Restart";
		StopAllCoroutines();
		Game_End.SetActive(false);
		Cactus_Counter = 0;
		Ground_Counter = 0;
		Player.transform.position = new Vector2(-6, -4);
		Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>().Dinosaur_2;
		Player.GetComponent<Player>().Speed = .1f;
		Ground_Start = Random.Range(-20.000f, 20.000f);
		StartCoroutine(Game_Start());
	}

	IEnumerator Game_Start () {
		yield return new WaitForSecondsRealtime(.0001f);
		Game_State = "Game";
		Start_Time = Time.timeSinceLevelLoad;
		Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		StartCoroutine(Enemy_Generation(Random.Range(10.000f, 16.000f)));
		StartCoroutine(Ground_Generation(Ground_Start));
		StartCoroutine(Cloud_Generation(Random.Range(10.000f, 30.000f)));
	}
}
