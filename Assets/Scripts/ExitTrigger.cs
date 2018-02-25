using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {

	[SerializeField] private GameObject pauseLH;
	[SerializeField] private GameObject pauseRH;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject == pauseLH || other.gameObject == pauseRH) {
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(500);

			if (Input.GetKeyDown (KeyCode.JoystickButton7)) {
				SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse (500);
				Application.Quit ();
			}
		}
	}
}
