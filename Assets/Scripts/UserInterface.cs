using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour {
	public static bool pauseMenuIsUp = false;

	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject restart;
	[SerializeField] private GameObject exit;
	[SerializeField] private GameObject resume;
	[SerializeField] private GameObject pauseRH;
	[SerializeField] private GameObject pauseLH;

	RestartTrigger restartTrigger;
	ResumeTrigger resumeTrigger;
	ExitTrigger exitTrigger;

	Vector3 previousVelocity;
	Vector3 previousAngularVelocity;

	//Object[] objects = FindObjectsOfType (typeof(GameObject));
	// Use this for initialization
	void Start () {
		restartTrigger = restartTrigger.GetComponent<RestartTrigger> ();
		resumeTrigger = resumeTrigger.GetComponent<ResumeTrigger> ();
		exitTrigger = exitTrigger.GetComponent <ExitTrigger> ();

		pauseMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.JoystickButton2)) {
			pauseMenuIsUp = true;
		}

		if (pauseMenuIsUp) {
			// pause gameplay for all objects except pause menu, raycast objects
			previousVelocity = gameObject.GetComponent<Rigidbody> ().velocity;
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;

			previousAngularVelocity = gameObject.GetComponent<Rigidbody> ().angularVelocity;
			gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;

			gameObject.GetComponent<Rigidbody> ().Sleep ();

			// set rayCast objects to active, require receiver

			// pull up pause menu
			pauseMenu.SetActive (true);	
			pauseRH.SetActive (true);
			pauseLH.SetActive (true);


			// grab collider
			// if grab Restart, restart game
			// if grab resume, unpause game
			// if grab exit, exit game
			// ALL CALLED AUTOMATICALLY THROUGH onTriggerEnter


		} else {
			pauseMenu.SetActive (false);

			gameObject.GetComponent<Rigidbody> ().velocity = previousVelocity;
			gameObject.GetComponent<Rigidbody> ().angularVelocity = previousAngularVelocity;
		}
	}
}
