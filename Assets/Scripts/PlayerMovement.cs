using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public const float maxXVelocity = 10.0f;
    public const float XVelocityScalingFactor = 0.2f; // scales deltaY of controller from ground / lower collider

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
	void Update () {
        lUpperTick = upperColliderScript.leftCollisionTime;

        if (lLowerTick != lowerColliderScript.leftCollisionTime) {
            lLowerTick = lowerColliderScript.leftCollisionTime;

            Debug.Log("left " + 0.3f / (float) (lLowerTick - lUpperTick) * (float) TimeSpan.TicksPerSecond);

            gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(0.3f / (float) (lLowerTick - lUpperTick) * (float)TimeSpan.TicksPerSecond, 10.0f)), 0.0f, ForceMode.VelocityChange);
        }

        rUpperTick = upperColliderScript.rightCollisionTime;

        if (rLowerTick != lowerColliderScript.rightCollisionTime) {
            rLowerTick = lowerColliderScript.rightCollisionTime;

            Debug.Log("right " + 0.3f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond);

            gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(0.3f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond, 10.0f)), 0.0f, ForceMode.VelocityChange);
        }

        float deltaLeftY = leftControllerCollider.transform.position.y - lowerCollider.transform.position.y - 1;
        float deltaRightY = rightControllerCollider.transform.position.y - lowerCollider.transform.position.y - 1;

        if (deltaLeftY < 0 && deltaRightY > 0 /*&& Mathf.Abs(deltaLeftY) > 0.5 && Mathf.Abs(deltaRightY) > 0.5*/) {
            Debug.Log("turning right");
            Vector3 forwardDir = camera.transform.forward;
            Vector3 upwardDir = new Vector3(0.0f, 1.0f, 0.0f);
            this.GetComponent<Rigidbody>().velocity += Vector3.Cross(forwardDir, upwardDir).normalized * XVelocityScalingFactor;
        } else if (deltaLeftY > 0 && deltaRightY < 0 /*&& Mathf.Abs(deltaLeftY) > 0.5 && Mathf.Abs(deltaRightY) > 0.5*/) {
            Debug.Log("turning left");
            Vector3 forwardDir = camera.transform.forward;
            Vector3 upwardDir = new Vector3(0.0f, 1.0f, 0.0f);
            this.GetComponent<Rigidbody>().velocity -= Vector3.Cross(forwardDir, upwardDir).normalized * XVelocityScalingFactor;
        }
    }
}
