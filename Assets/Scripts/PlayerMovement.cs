using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public const float maxXVelocity = 10.0f;
    public const float XVelocityScalingFactor = 0.01f; // scales deltaY of controller from ground / lower collider

    public float speed = 0.0f;

    private long lLowerTick = 0;
    private long lUpperTick = 0;

    private long rLowerTick = 0;
    private long rUpperTick = 0;

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject lowerCollider;
    [SerializeField] private GameObject upperCollider;
    [SerializeField] private GameObject leftControllerCollider;
    [SerializeField] private GameObject rightControllerCollider;

    [SerializeField] private GameObject headRotator;
    //[SerializeField] private GameObject startInstructions;// Text that says "Stretch your wings!"
    //[SerializeField] private GameObject player; 

    TriggerTime lowerColliderScript;
    TriggerTime upperColliderScript;

    private Vector3 prevforwardDir;
    private Vector3 actualforwardDir;
    private Vector3 forwardDir;
    private Vector3 upwardDir = new Vector3(0.0f, 1.0f, 0.0f);

    private float maxWingspan = 0.0f;
    private float wingspan = 0.0f; 
    private const float MIN_WINGSPAN = 1.0f;
    private int cyclesToRecordWingspan = 200; // test to see if this is a good number of cycles to start. 

    private const float MAX_Y_DECREMENT = 0.25f; // test to see if this ia a reasonable falling acceleration for each fixed update. 
    private const float MAX_VELOCITY = 15.0f; // Fastest this birdy can go in any direction. 
    private const float GLIDE_WINGSPAN = 0.8f; // amount your wings must be in order to take advantage of drag upward

    public bool dive = false;

    private float flap = 0.0f;

    // Use this for initialization
    void Start () {
        lowerColliderScript = lowerCollider.GetComponent<TriggerTime>();
        upperColliderScript = upperCollider.GetComponent<TriggerTime>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f); 

        //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
        //gameObject.GetComponent<Rigidbody>().AddForce(0.0f, 0.0f, 10.0f, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!UserInterface.pauseMenuIsUp) {
            forwardDir = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

            // store the local positions of the left and right controllers
            float leftX = leftControllerCollider.transform.localPosition.x;
            float leftY = leftControllerCollider.transform.localPosition.y;
            float leftZ = leftControllerCollider.transform.localPosition.z;

            float rightX = rightControllerCollider.transform.localPosition.x;
            float rightY = rightControllerCollider.transform.localPosition.y;
            float rightZ = rightControllerCollider.transform.localPosition.z;

            float centerX = Mathf.Lerp(leftX, rightX, 0.5f);
            float centerY = Mathf.Lerp(leftY, rightY, 0.5f);
            float centerZ = Mathf.Lerp(leftZ, rightZ, 0.5f);

            // Calculate the XZ distance between controllers
            float distanceXZ = Mathf.Sqrt(Mathf.Pow(leftX - rightX, 2.0f) + Mathf.Pow(leftZ - rightZ, 2.0f));

            //Debug.Log("XZ distance between controllers " + distanceXZ);

            if (cyclesToRecordWingspan <= 0) {
                //Debug.Log("gaming!! ");
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                //Debug.Log("initial v " + v);

                wingspan = distanceXZ / maxWingspan;


                //Debug.Log("wingspan: " + wingspan);
                if (wingspan > 1.0f) {
                    wingspan = 1.0f;
                }


                if (v.y < (GLIDE_WINGSPAN - 1.0f)) {
                    v = v - upwardDir * (GLIDE_WINGSPAN - wingspan) * MAX_Y_DECREMENT;
                } else {
                    v = v - upwardDir * (1.0f - wingspan) * MAX_Y_DECREMENT;
                }




                lUpperTick = upperColliderScript.leftCollisionTime;


                if (lLowerTick != lowerColliderScript.leftCollisionTime && lLowerTick != 0) {
                    lLowerTick = lowerColliderScript.leftCollisionTime;

                    //Debug.Log("left " + 0.2f / (float) (lLowerTick - lUpperTick) * (float) TimeSpan.TicksPerSecond);
                    flap += Mathf.Max(0.0f, Mathf.Min(0.1f / (float) (lLowerTick - lUpperTick) * (float) TimeSpan.TicksPerSecond, 3.0f));
                    //gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(2.0f / (float) (lLowerTick - lUpperTick) * (float) TimeSpan.TicksPerSecond, 50.0f)), 0.0f, ForceMode.Impulse);
                } else {
                    lLowerTick = lowerColliderScript.leftCollisionTime;

                }

                rUpperTick = upperColliderScript.rightCollisionTime;

                if (rLowerTick != lowerColliderScript.rightCollisionTime && rLowerTick != 0) {
                    rLowerTick = lowerColliderScript.rightCollisionTime;

                    //Debug.Log("right " + 0.2f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond);
                    flap += Mathf.Max(0.0f, Mathf.Min(0.1f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond, 3.0f));
                    //gameObject.GetComponent<Rigidbody>().AddForce(0.0f, Mathf.Max(0.0f, Mathf.Min(2.0f / (float) (rLowerTick - rUpperTick) * (float) TimeSpan.TicksPerSecond, 50.0f)), 0.0f, ForceMode.Impulse);
                } else {
                    rLowerTick = lowerColliderScript.rightCollisionTime;

                }

                while (flap > 0.0) {
                    v.y += (flap < 0.5f) ? flap : 0.5f;
                    flap -= (flap < 0.5f) ? flap : 0.5f;
                    speed += 0.005f;
                    Debug.Log("flapping " + flap);
                }

                

                float dy = leftControllerCollider.transform.localPosition.y - rightControllerCollider.transform.localPosition.y;
                //Debug.Log("dy: " + dy);
                //float dx = Mathf.Sqrt(Mathf.Pow(leftControllerCollider.transform.localPosition.x - rightControllerCollider.transform.localPosition.x, 2) +
                //   Mathf.Pow(leftControllerCollider.transform.localPosition.z - rightControllerCollider.transform.localPosition.z, 2));
                //Debug.Log("dx: " + dx);

                //float rotAngleLeft = Mathf.Atan((leftY - centerY) / (leftX - centerX));
                //Debug.Log("rotAngleLeft: " + rotAngleLeft);
                //float rotAngleRight = Mathf.Atan((rightY - centerY) / (rightX - centerX));
                //Debug.Log("rotAngleRight: " + rotAngleRight);
                //float rotAngle = Mathf.Lerp(rotAngleLeft, rotAngleRight, 0.5f);
                float rotAngle = Mathf.Atan(dy / distanceXZ);
                //Debug.Log("rotAngle: " + rotAngle);

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
                //Debug.Log("before turn right " + gameObject.GetComponent<Rigidbody>().velocity);
                v = Quaternion.Euler(0, rotAngle * Time.deltaTime * 15.0f, 0) * v;
                gameObject.transform.Rotate(upwardDir, rotAngle * Time.deltaTime * 15.0f);
                //Debug.Log("turn right " + gameObject.GetComponent<Rigidbody>().velocity);
                //Debug.Log("deltaTime " + Time.deltaTime);
                /*} else {
                    Debug.Log("before turn left " + gameObject.GetComponent<Rigidbody>().velocity);
                    gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, rotAngle * -1 * Time.deltaTime * 50.0f, 0) * gameObject.GetComponent<Rigidbody>().velocity;
                    //gameObject.transform.Rotate(upwardDir, rotAngle * Time.deltaTime * -50.0f);
                    Debug.Log("turn left " + gameObject.GetComponent<Rigidbody>().velocity);
                    Debug.Log("deltaTime " + Time.deltaTime);
                }*/

                //Debug.Log("forwardDir: " + forwardDir);
                //Debug.Log("turnOffset: " + turnOffset);

                //transform.position +=  2.0f * (forwardDir - turnOffset).normalized * Time.deltaTime;
                //transform.Rotate(upwardDir, rotAngle);

                v += speed * forwardDir;

                Debug.Log(speed);

                if (speed > 0.2f) {
                    speed -= 0.01f;
                }

                if (v.x > MAX_VELOCITY) {
                    v.x = MAX_VELOCITY;
                }
                if (v.x < -MAX_VELOCITY) {
                    v.x = -MAX_VELOCITY;
                }
                if (v.y > MAX_VELOCITY) {
                    v.y = MAX_VELOCITY;
                }
                if (v.y < -MAX_VELOCITY) {
                    v.y = -MAX_VELOCITY;
                }
                if (v.z > MAX_VELOCITY) {
                    v.z = MAX_VELOCITY;
                }
                if (v.z < -MAX_VELOCITY) {
                    v.z = -MAX_VELOCITY;
                }

                

                gameObject.GetComponent<Rigidbody>().velocity = v;
                //Debug.Log("velocity " + v);


            } else if ((distanceXZ > MIN_WINGSPAN)) {
                if (distanceXZ > maxWingspan) {
                    maxWingspan = distanceXZ;
                    //Debug.Log("set maxwingspan: " + maxWingspan);
                }
                cyclesToRecordWingspan--;
                Debug.Log(cyclesToRecordWingspan);
                if (cyclesToRecordWingspan == 0) {
                    //Debug.Log("destroy instructions "); 
                    //Destroy(startInstructions.GetComponent<Rigidbody>());
                }
            }
        }
    }
}
