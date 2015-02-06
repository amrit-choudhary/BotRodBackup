using UnityEngine;
using System.Collections;

public class TunnelGUIScript : MonoBehaviour {

    public static TunnelGUIScript instance;
    
    public GameObject fuelHand, speedHand;
    public GUISkin skin;
    public Texture2D cover, arrowRight, arrowRightT, arrowLeft, arrowLeftT;

    int maxHandAngle = 240;
    int minHandAngle = 120;
    int currentHandAngle = 205;

    int maxHandAngleSpeedometer = -69;
    int minHandAngleSpeedometer = -272;
    float oldSpeed, newSpeed, currentSpeed;
    int currentHandAngleSpeedometer;
    float lerpSpeed = 10;
    int sw, sh, sw2, sh2;
    GUIStyle buttonStyle1, buttonStyle2;
    public int turnDirection;
    public bool isTurning;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
	void Start () {
        currentHandAngleSpeedometer = -292;
        oldSpeed = newSpeed = currentSpeed = 0;
        sw = Screen.width; sh = Screen.height;
        sw2 = sw / 2; sh2 = sh / 2;
        buttonStyle1 = new GUIStyle();
        buttonStyle2 = new GUIStyle();
        turnDirection = 0;
        isTurning = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!TunnelGameManager.instance.paused) {
            currentHandAngle = 120 + (int)((TunnelGameManager.instance.fuel / 100.0f) * 120);
            fuelHand.transform.rotation = Quaternion.Euler(270, currentHandAngle, 0);

            newSpeed = TunnelGameManager.instance.speed;
            newSpeed += Random.Range(-0.25f, 0.25f);
            Vector3 temp = Vector3.Lerp(new Vector3(newSpeed, 0, 0), new Vector3(oldSpeed, 0, 0), lerpSpeed * Time.deltaTime);
            currentSpeed = temp.x;
            oldSpeed = newSpeed;

            currentHandAngleSpeedometer = minHandAngleSpeedometer + (int)(((currentSpeed - 10) / 10.0f) * (maxHandAngleSpeedometer - minHandAngleSpeedometer));
            speedHand.transform.rotation = Quaternion.Euler(-90, currentHandAngleSpeedometer, 0);
        }
	}

    void OnGUI() {
        GUI.TextField(new Rect(0, 0, 200, 40), "Score : " + (int)TunnelGameManager.instance.score, skin.textField);
        if (TunnelGameManager.instance.dead) {
            GUI.TextField(new Rect(sw/2 - 400, sh/2, 800, 40), "You died, tap the screen to restart", skin.textField);
        }

        if (!TunnelGameManager.instance.gameStarted) {
            GUI.DrawTexture(new Rect(0, 0, sw, sh), cover);
        }

        if (TunnelGameManager.instance.showInstruction) {
            //GUI.TextField(new Rect(sw / 2 - 450, sh / 2 - 200, 900, 120), "Use Arrow Keys to move Right or Left\nSpace to Jump\nCollect Green Powerups for Fuel\nYellow for Bonus Points", skin.textField);
        }

        if (TunnelGameManager.instance.showMessage) {
            GUI.TextField(new Rect(sw / 2 - 400, 50, 800, 40), TunnelGameManager.instance.message, skin.textField);
        }

        buttonStyle1.normal.background = arrowRightT;
        buttonStyle1.active.background = arrowRight;
        buttonStyle2.normal.background = arrowLeftT;
        buttonStyle2.active.background = arrowLeft;

        if (TunnelGameManager.instance.gameStarted) {

            if (GUI.RepeatButton(new Rect(sw - 0.2f * sw2, sh2 - 0.1f * sh2, 0.2f * sw2, 0.2f * sw2), "", buttonStyle1)) {
                turnDirection = 1;
                isTurning = true;
                GUI.RepeatButton(new Rect(0, sh2 - 0.1f * sh2, 0.2f * sw2, 0.2f * sw2), "", buttonStyle2);
            }
            else {
                if (GUI.RepeatButton(new Rect(0, sh2 - 0.1f * sh2, 0.2f * sw2, 0.2f * sw2), "", buttonStyle2)) {
                    turnDirection = -1;
                    isTurning = true;
                }
                else {
                    turnDirection = 0;
                    isTurning = false;
                }
            }
        }
    }
}
