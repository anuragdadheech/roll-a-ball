using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rbBall;
	// Use this for initialization
	void Start () {
		rbBall = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 ballMovement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rbBall.AddForce (ballMovement * speed);
	}
}
