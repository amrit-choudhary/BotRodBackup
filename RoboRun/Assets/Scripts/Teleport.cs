using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{

    public bool canTeleport = false, isTeleporting = false;
    GameObject teleporter;
    public float teleportTimer, teleportDuration;
    Transform teleportTarget, initialPos;
    Vector3 teleportTargetPos, initialPosPos;
    Quaternion initRot, targetRot;

    public GameObject cube;

    // Use this for initialization
    void Start() {
        teleportTimer = 0;
        teleportDuration = 1;
        canTeleport = false;
        isTeleporting = false;
    }

    // Update is called once per frame
    void Update() {


    }

    void FixedUpdate() {

        if (canTeleport) {
            Debug.Log("Changing init");
            canTeleport = false;
            teleportTarget = teleporter.transform.FindChild("Target").transform;
            initialPosPos = transform.position;
            teleportTargetPos = teleportTarget.position;
            initRot = transform.rotation;
            targetRot = teleportTarget.rotation; 
            isTeleporting = true;

            cube.transform.position = teleportTargetPos;

        }

        if (isTeleporting) {

            gameObject.GetComponent<WallRun>().enabled = false;

            teleportTimer += Time.fixedDeltaTime;

            transform.position = Vector3.Lerp(initialPosPos, teleportTargetPos, teleportTimer);
            transform.rotation = Quaternion.Lerp(initRot, targetRot, teleportTimer);

            cube.transform.position = teleportTargetPos;

            if (teleportTimer > teleportDuration) {
                isTeleporting = false;
                teleportTimer = 0;
                gameObject.GetComponent<WallRun>().enabled = true;

                cube.transform.position = transform.position;

            }

        }
    }

    




}
