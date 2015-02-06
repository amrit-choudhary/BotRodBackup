using UnityEngine;
using System.Collections;

public enum TeleportTurn
{
    Right,
    Left,
    None
};


public class mouseTracker : MonoBehaviour
{
    
    public static mouseTracker instance;

    public float sw2, sh2;
    public Vector2 mousePos;
    int cameraSize = 1;
    public Camera cam;
    bool trackingMouseMovement = false;
    float mousePosAtStart, mouseDelta, mouseDeltaFinal;
    public float deltaLimit, swipeDirectionDuration;
    public GameObject arrowDirection;
    int arrowDeltaAngle = 70;
    public TeleportTurn currentTeleportTurn;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        sw2 = Screen.width / 2;
        sh2 = Screen.height / 2;
        trackingMouseMovement = false;
        mousePosAtStart = mouseDelta = 0;
        arrowDirection.SetActive(false);
        currentTeleportTurn = TeleportTurn.None; 
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            mousePos = new Vector2(ray.origin.x, ray.origin.z);
            transform.position = new Vector3(mousePos.x * cameraSize, transform.position.y,mousePos.y* cameraSize);
        }

        if (Input.GetMouseButtonDown(0)) {
            trackingMouseMovement = true;
            mousePosAtStart = mousePos.x;
            mouseDelta = 0;
        }

        if (Input.GetMouseButtonUp(0)) {
            trackingMouseMovement = false;
            mouseDeltaFinal = mouseDelta;
            if (Mathf.Abs(mouseDeltaFinal) > deltaLimit) {
                StartCoroutine("ChangeSwipeDirection");
            }
        }

        if (trackingMouseMovement) {
            mouseDelta = mousePos.x - mousePosAtStart;
        }

    }

    IEnumerator ChangeSwipeDirection() {
        int tempAngle;
        arrowDirection.SetActive(true);
        if (mouseDeltaFinal > deltaLimit) {
            tempAngle = arrowDeltaAngle;
            currentTeleportTurn = TeleportTurn.Right;
        }
        else {
            tempAngle = -arrowDeltaAngle;
            currentTeleportTurn = TeleportTurn.Left;
        }

        arrowDirection.transform.rotation = Quaternion.Euler(270, 180 + tempAngle, 0);
        yield return new WaitForSeconds(swipeDirectionDuration);
        arrowDirection.SetActive(false);
        mousePosAtStart = mouseDelta = 0;
        currentTeleportTurn = TeleportTurn.None;
    }



}
