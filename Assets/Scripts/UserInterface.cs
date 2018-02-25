using System;
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
    [SerializeField] private GameObject player;

	RestartTrigger restartTrigger;
	ResumeTrigger resumeTrigger;
	ExitTrigger exitTrigger;

    private bool resolved = true;

	Vector3 previousVelocity;
	Vector3 previousAngularVelocity;

	//Object[] objects = FindObjectsOfType (typeof(GameObject));
	// Use this for initialization
	void Start () {
		restartTrigger = restart.GetComponent<RestartTrigger> ();
		resumeTrigger = resume.GetComponent<ResumeTrigger> ();
		exitTrigger = exit.GetComponent <ExitTrigger> ();

		pauseMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }

        if (Input.GetKeyDown (KeyCode.JoystickButton8) || Input.GetKeyDown(KeyCode.JoystickButton9)) {
			pauseMenuIsUp = !pauseMenuIsUp;
            Debug.Log("paused");
		}

		if (pauseMenuIsUp) {
			// pause gameplay for all objects except pause menu, raycast objects
			previousVelocity = player.GetComponent<Rigidbody> ().velocity;
			player.GetComponent<Rigidbody> ().velocity = Vector3.zero;

			//previousAngularVelocity = gameObject.GetComponent<Rigidbody> ().angularVelocity;
			//gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;

			//gameObject.GetComponent<Rigidbody> ().Sleep ();

			// set rayCast objects to active, require receiver

			// pull up pause menu
			pauseMenu.SetActive (true);	
			pauseRH.SetActive (true);
			pauseLH.SetActive (true);

            resolved = false;

			// grab collider
			// if grab Restart, restart game
			// if grab resume, unpause game
			// if grab exit, exit game
			// ALL CALLED AUTOMATICALLY THROUGH onTriggerEnter


		} else if (!resolved) {
			pauseMenu.SetActive (false);

			player.GetComponent<Rigidbody> ().velocity = previousVelocity;
            //gameObject.GetComponent<Rigidbody> ().angularVelocity = previousAngularVelocity;
            resolved = true;
        }
	}
}
