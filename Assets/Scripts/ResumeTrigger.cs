using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeTrigger : MonoBehaviour {

	[SerializeField] private GameObject pauseLH;
	[SerializeField] private GameObject pauseRH;

    bool entered = false;

	private void OnTriggerStay(Collider other) {
		if (other.gameObject == pauseLH || other.gameObject == pauseRH) {
            //SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(500);
            Debug.Log("Resume Entered");
            this.GetComponent<Text>().color = Color.red;
            if (Input.GetKeyDown(KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.JoystickButton15)) {
                //SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse (500);
                UserInterface.pauseMenuIsUp = false;
			}
		}
	}
}
