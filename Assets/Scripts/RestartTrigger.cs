using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RestartTrigger : MonoBehaviour {

	[SerializeField] private GameObject pauseLH;
	[SerializeField] private GameObject pauseRH;

	private void OnTriggerStay(Collider other) {
		if (other.gameObject == pauseLH || other.gameObject == pauseRH) {
            //SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(500);
            Debug.Log("restart entered");
            this.GetComponent<Text>().color = Color.red;

			if (Input.GetKeyDown (KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.JoystickButton15)) {
				//SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse (500);
				UserInterface.pauseMenuIsUp = false;
				SceneManager.LoadScene( SceneManager.GetActiveScene().name );

			}
		}
	}
}
