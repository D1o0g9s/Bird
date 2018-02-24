using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongitudinalMovement : MonoBehaviour {
	public const float maxZVelocity = 10.0f;
	public const float ZVelocityIncrement = 0.5f; // amount by which to increment ZVelocity each flap
	public const float maxXVelocity = 10.0f;
	public const float XVelocityScalingFactor = 0.2f; // scales deltaY of controller from ground / lower collider

    // use this for initialization
    void Start () {
    }

    //Update is called once per frame
    void Update () {
		float xLeft = 0;
		float xRight = 0;
		float deltax = xLeft - xRight;
		if (this.GetComponent<Rigidbody> ().velocity.x < maxXVelocity && this.GetComponent<Rigidbody>().velocity.x > -1 * maxXVelocity) {
			this.GetComponent<Rigidbody> ().AddForce (deltax * XVelocityScalingFactor, 0.0f, 0.0f, ForceMode.VelocityChange);
		}
    }

    void onTriggerEnter(Collider other) {
		if (this.GetComponent<Rigidbody>().velocity.z < maxZVelocity){
			this.GetComponent<Rigidbody>().AddForce (0.0f, 0.0f, ZVelocityIncrement, ForceMode.VelocityChange);
		}
    }
}
