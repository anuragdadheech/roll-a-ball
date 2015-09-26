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

	//accelerometer code
	private Vector3 curAc;
	private Vector3 zeroAc;
	private float speedAc = 10;
	private float GetAxisV = 0;
	private float GetAxisH = 0;
	private float sensV = 10;
	private float sensH = 10;
	private float smooth = 0.5f;
	// Use this for initialization
	void Start () {
		rbBall = GetComponent<Rigidbody>();
		count = 0;
		setCountText ();
		winText.text = "";
		timerText.text = "0";
		t = 0;
		updateTime = true;
		ResetAxes();
	}
	
	// Update is called once per frame
	void Update () {
		if (updateTime) {
			t += Time.deltaTime;
			timerText.text = t.ToString ();
		}
	}

	void FixedUpdate () {
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 ballMovement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rbBall.AddForce (ballMovement * speed);
		} else {
			curAc = Vector3.Lerp(curAc, Input.acceleration-zeroAc, Time.deltaTime/smooth);
			GetAxisV = Mathf.Clamp(curAc.y * sensV, -1, 1);
			GetAxisH = Mathf.Clamp(curAc.x * sensH, -1, 1);
			// now use GetAxisV and GetAxisH instead of Input.GetAxis vertical and horizontal
			// If the horizontal and vertical directions are swapped, swap curAc.y and curAc.x
			// in the above equations. If some axis is going in the wrong direction, invert the
			// signal (use -curAc.x or -curAc.y)
			
			Vector3 movement = new Vector3 (GetAxisH, 0.0f, GetAxisV);
			rbBall.AddForce(movement * speedAc);
		}
			
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

	void ResetAxes(){
		zeroAc = Input.acceleration;
		curAc = Vector3.zero;
	}
}
