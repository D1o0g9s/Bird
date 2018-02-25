using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveTrigger : MonoBehaviour {

    private AudioSource aud;

    [SerializeField] private GameObject leftCollider;
    [SerializeField] private GameObject rightCollider;
    [SerializeField] private float eulerAngleX;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    public bool leftEntered;
    public bool rightEntered;

    private Vector3 velocity;

    private Vector3 setDirection;
    private bool directionSet;

    private float MAX_VELOCITY = 15.0f;

    float unitScalar;

    private void Start() {
        velocity = player.GetComponent<Rigidbody>().velocity;

        aud = GetComponent<AudioSource>();
        aud.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == leftCollider) {
            leftEntered = true;

        }

        if (other.gameObject == rightCollider) {
            rightEntered = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == leftCollider) {
            leftEntered = false;
        }

        if (other.gameObject == rightCollider) {
            rightEntered = false;
        }
    }

    void FixedUpdate() {
        eulerAngleX = camera.transform.localEulerAngles.x;
        if (leftEntered && rightEntered && eulerAngleX < -30 && eulerAngleX >= -90) {

            if (!directionSet) {
                setDirection = GetComponent<Camera>().transform.forward;
                directionSet = true;

                if (eulerAngleX > -60) {
                    unitScalar = 2;
                } else {
                    unitScalar = 3;
                }
            }

            
            // increment velocity in same direction if player is looking in relatively same direction
            if (Vector3.Dot(setDirection.normalized, velocity.normalized) > 0.6f) {

                // increment velocity until by scalar times the direction if the sum is less than max
                if ((velocity + setDirection.normalized * unitScalar).magnitude < MAX_VELOCITY) {
                    velocity += setDirection.normalized * unitScalar;
                } else {    // else keep velocity at max
                    velocity = velocity.normalized * MAX_VELOCITY;
                }
            } else { // else, must recalculate set direction or break from dive
                directionSet = false;
            }
        }
    }
}
