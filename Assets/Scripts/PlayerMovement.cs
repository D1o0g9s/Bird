using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public const float maxXVelocity = 10.0f;
    public const float XVelocityScalingFactor = 0.01f; // scales deltaY of controller from ground / lower collider

    private long lLowerTick = 0;
    private long lUpperTick = 0;

    private long rLowerTick = 0;
    private long rUpperTick = 0;

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject lowerCollider;
    [SerializeField] private GameObject upperCollider;
    [SerializeField] private GameObject leftControllerCollider;
    [SerializeField] private GameObject rightControllerCollider;

    TriggerTime lowerColliderScript;
    TriggerTime upperColliderScript;

	// Use this for initialization
	void Start () {
        lowerColliderScript = lowerCollider.GetComponent<TriggerTime>();
        upperColliderScript = upperCollider.GetComponent<TriggerTime>();
        //gameObject.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, 10.0f, ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        lUpperTick = upperColliderScript.leftCollisionTime;

        if (lLowerTick != lowerColliderScript.leftCollisionTime) {
            lLowerTick = lowerColliderScript.leftCollisionTime;

            //Debug.Log("left " + 0.3f / (float) (lLowerTick - lUpperTick) * (float) TimeSpan.TicksPerSecond);

            gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(2.0f / (float) (lLowerTick - lUpperTick) * (float)TimeSpan.TicksPerSecond, 50.0f)), 0.0f, ForceMode.Impulse);
        }

        rUpperTick = upperColliderScript.rightCollisionTime;

        if (rLowerTick != lowerColliderScript.rightCollisionTime) {
            rLowerTick = lowerColliderScript.rightCollisionTime;

            //Debug.Log("right " + 0.3f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond);

            gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(2.0f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond, 50.0f)), 0.0f, ForceMode.Impulse);
        }

        Vector3 forwardDir = new Vector3(camera.transform.forward.x, 0.0f, camera.transform.forward.z) / 8.0f;
        Vector3 upwardDir = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 turnOffset = Vector3.zero;
        float rotAngle = 0;

        float deltaLeftY = leftControllerCollider.transform.position.y - lowerCollider.transform.position.y - 1;
        float deltaRightY = rightControllerCollider.transform.position.y - lowerCollider.transform.position.y - 1;
        //Debug.Log("deltaLeftY: " + deltaLeftY);
        //Debug.Log("deltaRightY: " + deltaRightY);

        if (deltaLeftY > 0 ^ deltaRightY > 0) {
            turnOffset = Vector3.Cross(forwardDir, upwardDir).normalized * XVelocityScalingFactor * (deltaRightY - deltaLeftY);
            if (turnOffset.magnitude > forwardDir.magnitude) {
                turnOffset = turnOffset.normalized * forwardDir.magnitude;
            }
            rotAngle = 5 * Mathf.Atan(turnOffset.magnitude / forwardDir.magnitude);
            if (deltaLeftY < deltaRightY)
                rotAngle *= -1;
        }

        Debug.Log("forwardDir: " + forwardDir);
        Debug.Log("turnOffset: " + turnOffset);

        transform.position +=  2.0f * (forwardDir - turnOffset).normalized * Time.deltaTime;
        transform.Rotate(upwardDir, rotAngle);
    }
}
