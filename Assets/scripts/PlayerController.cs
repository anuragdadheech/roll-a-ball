using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rbBall;
	private int count;
	public Text countText;
	public Text winText;
	public Text timerText;
	private float t;
	private bool updateTime;
	// Use this for initialization
	void Start () {
		rbBall = GetComponent<Rigidbody>();
		count = 0;
		setCountText ();
		winText.text = "";
		timerText.text = "0";
		t = 0;
		updateTime = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (updateTime) {
			t += Time.deltaTime;
			timerText.text = t.ToString ();
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 ballMovement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rbBall.AddForce (ballMovement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);
			count++;
			setCountText();
		}
	}

	void setCountText() {
		countText.text = "Counter: " + count.ToString ();
		if (count == 10) {
			winText.text = t.ToString() ;
			updateTime = false;
		}
	}
}
