using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongitudinalMovement : MonoBehaviour {
	public const int maxZVelocity = 100;
	public const int ZVelocityIncrement = 5; // amount by which to increment ZVelocity each flap
	public const int maxXVelocity = 100;
	public const int XVelocityScalingFactor = 0.2; // scales deltaY of controller from ground / lower collider

    // use this for initialization
    void Start () {
    }

    //Update is called once per frame
    void Update () {
		float xLeft = 0;
		float xRight = 0;
		float deltax = xLeft - xRight;
		if (this.GetComponent<Rigidbody> ().velocity.x < maxXVelocity && this.GetComponent<Rigidbody>.velocity.x > -1 * maxXVelocity) {
			this.GetComponent<Rigidbody> ().AddForce (deltay * XVelocityScalingFactor, 0.0f, 0.0f, ForceMode.VelocityChange);
		}
    }

    void onTriggerEnter(Collider other) {
		if (this.GetComponent<Rigidbody>().velocity.z < maxZVelocity){
			this.GetComponent<Rigidbody>().AddForce (0.0f, 0.0f, ZVelocityIncrement, ForceMode.VelocityChange);
		}
    }
}
