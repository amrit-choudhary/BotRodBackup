using UnityEngine;
using System.Collections;

public class lerpTest : MonoBehaviour {

    Vector3 initPos, finalPos;
    bool isTeleporting;
    public float teleportTimer;
    float teleportDuration;

	// Use this for initialization
	void Start () {
        initPos = transform.position;
        finalPos = new Vector3(10, 10, 10);
        isTeleporting = true;
        teleportTimer = 0;
        teleportDuration = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (isTeleporting) {
            teleportTimer += Time.fixedDeltaTime;

            transform.position = Vector3.Lerp(initPos, finalPos, teleportTimer);

            if (teleportTimer > teleportDuration) {
                isTeleporting = false;
                teleportTimer = 0;
            }

        }

	}
}
