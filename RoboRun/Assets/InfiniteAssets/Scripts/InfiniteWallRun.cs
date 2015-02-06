﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// C# translation from http://answers.unity3d.com/questions/155907/basic-movement-walking-on-walls.html
/// Author: UA @aldonaletto 
/// </summary>

// Prequisites: create an empty GameObject, attach to it a Rigidbody w/ UseGravity unchecked
// To empty GO also add BoxCollider and this script. Makes this the parent of the Player
// Size BoxCollider to fit around Player model.

public class InfiniteWallRun : MonoBehaviour
{

    private float moveSpeed = 10; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)
    private float lerpSpeed = 10; // smoothing speed
    private float gravity = 10; // gravity acceleration
    private bool isGrounded;
    private float deltaGround = 0.2f; // character is grounded up to this distance
    private float jumpSpeed = 10; // vertical jump initial speed
    private float jumpRange = 10; // range to detect target wall
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    private float vertSpeed = 0; // vertical jump current speed

    bool autoRun = true;
    float moveSpeedDelta = 0.002f;
    float notGroundedTimer = 0; float resetAfterInAir = 2;
    float speedBoostAmount = 3; float speedPenaltyAmount = 3; float orbDuration = 3;

    private Transform myTransform;
    public CapsuleCollider capsuleCollider; // drag BoxCollider ref in editor

    private void Start() {
        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        rigidbody.freezeRotation = true; // disable physics rotation
        // distance from transform.position to ground
        distGround = 2.5f;

    }

    private void FixedUpdate() {
        // apply constant weight force according to character normal:
        rigidbody.AddForce(-gravity * rigidbody.mass * myNormal);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            autoRun = !autoRun;
        }

        if (isGrounded)
            notGroundedTimer = 0;
        else {
            notGroundedTimer += Time.deltaTime;
            if (notGroundedTimer > resetAfterInAir)
                GameManagerScript.instance.RestartGame();
        }
        

        moveSpeed += moveSpeedDelta;
        GUIScript.instance.speed = moveSpeed;

        // jump code - jump to wall or simple jump
        if (jumping)
            return; // abort Update while jumping to a wall

        Ray ray;
        RaycastHit hit;
        /*
                if (Input.GetButtonDown ("Jump")) { // jump pressed:
                        ray = new Ray (myTransform.position, myTransform.forward);
                        if (Physics.Raycast (ray, out hit, jumpRange)) { // wall ahead?
                                JumpToWall (hit.point, hit.normal); // yes: jump to the wall
                        } else if (isGrounded) { // no: if grounded, jump up
                                rigidbody.velocity += jumpSpeed * myNormal;
                        }
                }
         */

        // movement code - turn left/right with Horizontal axis:
        myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        // update surface normal and isGrounded:
        ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit)) { // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distGround + deltaGround;
            surfaceNormal = hit.normal;
        }
        else {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            surfaceNormal = Vector3.up;
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        // find forward direction with new myNormal:
        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
        // align character to the new myNormal while keeping the forward direction:
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        // move the character forth/back with Vertical axis:
        if (!autoRun)
            myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        else
            myTransform.Translate(0, 0, 1 * moveSpeed * Time.deltaTime);
    }

    private void JumpToWall(Vector3 point, Vector3 normal) {
        // jump to wall
        jumping = true; // signal it's jumping to wall
        rigidbody.isKinematic = true; // disable physics while jumping
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
        //jumptime
    }

    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal) {
        for (float t = 0.0f; t < 1.0f; ) {
            t += Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        rigidbody.isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "speedPortal") {
            StartCoroutine("SpeedBoost");
            Debug.Log("Hit");
        }
        if (col.name == "greenOrb(Clone)") {
            StartCoroutine("GreenOrb");
        }
        if (col.name == "redOrb(Clone)") {
            StartCoroutine("RedOrb");
        }
    }

    IEnumerator SpeedBoost() {
        GUIManager.instance.status = "Speed Boost !";
        moveSpeed = 2 * moveSpeed;
        yield return new WaitForSeconds(2);
        moveSpeed = moveSpeed / 2;
        GUIManager.instance.status = "";
    }

    IEnumerator GreenOrb() {
        moveSpeed += speedBoostAmount;
        GUIScript.instance.specialMessage = "+3 Speed"; GUIScript.instance.showSpecialMessage = true;
        yield return new WaitForSeconds(orbDuration);
        moveSpeed -= speedBoostAmount;
        GUIScript.instance.specialMessage = ""; GUIScript.instance.showSpecialMessage = false;
    }

    IEnumerator RedOrb() {
        moveSpeed -= speedPenaltyAmount;
        GUIScript.instance.specialMessage = "-3 Speed"; GUIScript.instance.showSpecialMessage = true;
        yield return new WaitForSeconds(orbDuration);
        moveSpeed += speedPenaltyAmount;
        GUIScript.instance.specialMessage = ""; GUIScript.instance.showSpecialMessage = false;
    }

}
