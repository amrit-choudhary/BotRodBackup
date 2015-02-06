using UnityEngine;
using System.Collections;

public class BotControllerScript : MonoBehaviour
{

    #region Variables

    Vector3 raycastOriginFront, raycastOriginRight, raycastOriginCenter, oldPos, newPos, targetNormal, targetForward, myNormal;
    Vector3 teleportTargetPos, initialPosPos;
    float raycastOriginHeight, raycastSeparation, raycastLegth, groundClearance, shakingRemoval, speed, maxSpeed, turnSpeed, lerpSpeed, steepnessFactor, maximumGroundDifference,currentGroundDifference;
    float teleportTimer, teleportDuration, speedBoostDuration;
    Quaternion initRot, targetRot;
    RaycastHit hit;
    bool underfreeFall, autoRun, isTeleporting, canTeleport;
    Transform myTransform, teleportTarget, initialPos;
    Camera cam;
    Animation anim1;
    GameObject teleporter;


    public GameObject bot1;
    public LayerMask trackLayerMask;

    #endregion

	void Start () {
        raycastOriginHeight = 1;
        raycastSeparation = 1;
        raycastLegth = 2;
        underfreeFall = false;
        groundClearance = 0.2f;
        shakingRemoval = 0.1f;
        oldPos = newPos = transform.position;
        speed = 0;
        maxSpeed = 20;
        turnSpeed = 50;
        lerpSpeed = 10;
        myTransform = transform;
        myNormal = transform.up;
        steepnessFactor = 1;
        maximumGroundDifference = 0.5f;
        autoRun = true;
        currentGroundDifference = 0;
        isTeleporting = false;
        teleportTimer = 0;
        teleportDuration = 1;
        canTeleport = false;
        speedBoostDuration = 2;

        anim1 = bot1.GetComponent<Animation>();
        anim1["Run"].speed = 1.4f;

        if (!networkView.isMine) {
            transform.FindChild("Camera").gameObject.SetActive(false);
            enabled = false;
        }

    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) {
            autoRun = !autoRun;
        }

        GameManager.instance.speed = (int)speed;

        #region Animation

        if (autoRun) {
            if (Input.GetButtonDown("Jump"))
                anim1.Play("Jump");

            if (!anim1.isPlaying)
                anim1.Play("Run");
        }
        else {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && !anim1.isPlaying)
                anim1.Play("Run");

            if (Input.GetButtonDown("Jump"))
                anim1.Play("Jump");
        }

        #endregion
    }
    
    void FixedUpdate() {

        if (canTeleport) {
            canTeleport = false;
            teleportTarget = teleporter.transform.FindChild("Target").transform;
            initialPosPos = transform.position;
            teleportTargetPos = teleportTarget.position;
            initRot = transform.rotation;
            targetRot = teleportTarget.rotation;
            isTeleporting = true;
        }
               
        
        #region Run

        if (!isTeleporting) {
            raycastOriginCenter = transform.position + transform.up * raycastOriginHeight;

            if (Physics.Raycast(raycastOriginCenter, -transform.up, out hit, raycastLegth, trackLayerMask))
                underfreeFall = false;
            else
                underfreeFall = true;


            if (underfreeFall) {
                transform.position += new Vector3(0, -10, 0) * Time.deltaTime;
            }
            else {
                myTransform.Rotate(0, InputManager.instance.turnDirection * turnSpeed * Time.deltaTime, 0);
                myNormal = Vector3.Lerp(myNormal, hit.normal, lerpSpeed * Time.deltaTime);
                Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
                Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
                myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);

                currentGroundDifference = groundClearance - (transform.position - hit.point).magnitude;

                if (autoRun) {
                    speed = maxSpeed * (maximumGroundDifference - Mathf.Abs(currentGroundDifference));
                }
                else
                    speed = Input.GetAxis("Vertical") * maxSpeed * (maximumGroundDifference * steepnessFactor - currentGroundDifference);

                myTransform.position = Vector3.Lerp(myTransform.position + myTransform.forward * speed * Time.deltaTime, myTransform.position + hit.normal * currentGroundDifference, lerpSpeed * Time.deltaTime);

            }
        }
        #endregion

        #region Teleporting
        else {
            teleportTimer += Time.fixedDeltaTime;

            transform.position = Vector3.Lerp(initialPosPos, teleportTargetPos, teleportTimer);
            transform.rotation = Quaternion.Lerp(initRot, targetRot, teleportTimer);

            if (teleportTimer > teleportDuration) {
                isTeleporting = false;
                teleportTimer = 0;
            }
        }
        #endregion
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
        if (stream.isWriting) {
            Vector3 pos = transform.position;
            stream.Serialize(ref pos);

        }
        else {
            Vector3 posRecieve = Vector3.zero;
            stream.Serialize(ref posRecieve);
            transform.position = posRecieve;
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "teleport" && !isTeleporting) {
            ShowTurnStatus(true, col);
            if (CheckCanTeleport(col)) {
                canTeleport = true;
                teleporter = col.gameObject;
                Flip();
            }
        }
        if (col.tag == "speedPortal")
            StartCoroutine("SpeedBoost");
        if (col.tag == "deathFloor") {
            Network.Disconnect(200);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "teleport") {
            ShowTurnStatus(false, col);
            canTeleport = false;
        }
    }


    public void Flip() {
        anim1.Play("Hover");
        StartCoroutine("ResetAfterFlip");
    }

    bool CheckCanTeleport(Collider col) {

        if (col.gameObject.name == "TeleportRight") {
            if (mouseTracker.instance.currentTeleportTurn == TeleportTurn.Right)
                return true;
            else
                return false;
        }

        if (col.gameObject.name == "TeleportLeft") {
            if (mouseTracker.instance.currentTeleportTurn == TeleportTurn.Left)
                return true;
            else
                return false;
        }

        if (col.gameObject.name == "Teleport")
            return true;

        return false;
    }

    void ShowTurnStatus(bool show, Collider col) {
        if (show) {
            if (col.gameObject.name == "TeleportRight") {
                GUIManager.instance.turnStatus = "Swipe Right";
            }
            if (col.gameObject.name == "TeleportLeft") {
                GUIManager.instance.turnStatus = "Swipe Left";
            }
            if (col.gameObject.name == "Teleport") {
                GUIManager.instance.turnStatus = "Swipe Anywhere";
            }
        }
        else
            GUIManager.instance.turnStatus = "";
    }

    IEnumerator ResetAfterFlip() {
        yield return new WaitForSeconds(1);
        anim1.Play("Run");
    }

    IEnumerator SpeedBoost() {
        GUIManager.instance.status = "Speed Boost !";
        anim1["Run"].speed = 2.0f;
        anim1.Play("Hover");
        maxSpeed = 2 * maxSpeed;
        yield return new WaitForSeconds(speedBoostDuration);
        maxSpeed = maxSpeed / 2;
        GUIManager.instance.status = "";
        anim1.Play("Run");
        anim1["Run"].speed = 1.4f;
    }


}
