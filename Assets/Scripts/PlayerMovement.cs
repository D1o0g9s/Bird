using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public const float maxXVelocity = 10.0f;
    public const float XVelocityScalingFactor = 0.01f; // scales deltaY of controller from ground / lower collider

    public float speed = 10.0f;

    private long lLowerTick = 0;
    private long lUpperTick = 0;

    private long rLowerTick = 0;
    private long rUpperTick = 0;

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject lowerCollider;
    [SerializeField] private GameObject upperCollider;
    [SerializeField] private GameObject leftControllerCollider;
    [SerializeField] private GameObject rightControllerCollider;
    //[SerializeField] private GameObject player; 

    TriggerTime lowerColliderScript;
    TriggerTime upperColliderScript;

    private Vector3 forwardDir;
    private Vector3 upwardDir = new Vector3(0.0f, 1.0f, 0.0f);


    // Use this for initialization
    void Start () {
        lowerColliderScript = lowerCollider.GetComponent<TriggerTime>();
        upperColliderScript = upperCollider.GetComponent<TriggerTime>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 2.0f); 
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
        //gameObject.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, 10.0f, ForceMode.VelocityChange);
    }

	// Update is called once per frame
	void FixedUpdate () {
        //forwardDir = new Vector3(camera.transform.forward.x, 0.0f, camera.transform.forward.z);
        

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

        

        //float leftY = leftControllerCollider.transform.position.y;
        //Debug.Log("leftY: " + leftY);
        //float rightY = rightControllerCollider.transform.position.y;
        //Debug.Log("rightY: " + rightY);
        //float leftX = leftControllerCollider.transform.position.x;
        //Debug.Log("leftX: " + leftX);
        //float rightX = rightControllerCollider.transform.position.x;
        //Debug.Log("rightX: " + rightX);
        //float centerX = Mathf.Lerp(leftX, rightX, 0.5f);
        //Debug.Log("centerX: " + centerX);
        //float centerY = Mathf.Lerp(leftY, rightY, 0.5f);
        //Debug.Log("centerY: " + centerY);

        float dy = leftControllerCollider.transform.localPosition.y - rightControllerCollider.transform.localPosition.y;
        Debug.Log("dy: " + dy);
        float dx = Mathf.Sqrt(Mathf.Pow(leftControllerCollider.transform.localPosition.x - rightControllerCollider.transform.localPosition.x, 2) +
            Mathf.Pow(leftControllerCollider.transform.localPosition.z - rightControllerCollider.transform.localPosition.z, 2));
        Debug.Log("dx: " + dx);

        //float rotAngleLeft = Mathf.Atan((leftY - centerY) / (leftX - centerX));
        //Debug.Log("rotAngleLeft: " + rotAngleLeft);
        //float rotAngleRight = Mathf.Atan((rightY - centerY) / (rightX - centerX));
        //Debug.Log("rotAngleRight: " + rotAngleRight);
        //float rotAngle = Mathf.Lerp(rotAngleLeft, rotAngleRight, 0.5f);
        float rotAngle = Mathf.Atan(dy / dx);
        Debug.Log("rotAngle: " + rotAngle);

        //float deltaLeftY = leftControllerCollider.transform.position.y - lowerCollider.transform.position.y - 1;
        //float deltaRightY = rightControllerCollider.transform.position.y - lowerCollider.transform.position.y - 1;

        //Debug.Log("deltaLeftY: " + deltaLeftY);
        //Debug.Log("deltaRightY: " + deltaRightY);
        /*
        if (deltaLeftY > 0 ^ deltaRightY > 0) {
            turnOffset = Vector3.Cross(forwardDir, upwardDir).normalized * XVelocityScalingFactor * (deltaRightY - deltaLeftY);
            if (turnOffset.magnitude > forwardDir.magnitude) {
                turnOffset = turnOffset.normalized * forwardDir.magnitude;
            }
            rotAngle = 5 * Mathf.Atan(turnOffset.magnitude / forwardDir.magnitude);
            if (deltaLeftY < deltaRightY)
                rotAngle *= -1;
        }
        */
        
        //turn right
        //if ((leftY > centerY) && (rightY < centerY)){
            Debug.Log("before turn right " + gameObject.GetComponent<Rigidbody>().velocity);
            gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, rotAngle * Time.deltaTime * 15.0f , 0) * gameObject.GetComponent<Rigidbody>().velocity;
            gameObject.transform.Rotate(upwardDir, rotAngle * Time.deltaTime * 15.0f);
            Debug.Log("turn right " + gameObject.GetComponent<Rigidbody>().velocity);
            Debug.Log("deltaTime " + Time.deltaTime);
        /*} else {
            Debug.Log("before turn left " + gameObject.GetComponent<Rigidbody>().velocity);
            gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, rotAngle * -1 * Time.deltaTime * 50.0f, 0) * gameObject.GetComponent<Rigidbody>().velocity;
            //gameObject.transform.Rotate(upwardDir, rotAngle * Time.deltaTime * -50.0f);
            Debug.Log("turn left " + gameObject.GetComponent<Rigidbody>().velocity);
            Debug.Log("deltaTime " + Time.deltaTime);
        }*/
        /*
        Debug.Log("forwardDir: " + forwardDir);
        Debug.Log("turnOffset: " + turnOffset);

        transform.position +=  2.0f * (forwardDir - turnOffset).normalized * Time.deltaTime;
        transform.Rotate(upwardDir, rotAngle);
        */
    }
}
