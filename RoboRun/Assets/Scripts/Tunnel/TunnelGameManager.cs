using UnityEngine;
using System.Collections;

public class TunnelGameManager : MonoBehaviour {

    public static TunnelGameManager instance;
    public float speed, turnSpeed, fuelConsumptionRate, fuel;
    //[HideInInspector]
    public float turn, distanceTravelled, score;
    public float speedDelta, cutoffDelta, turnSpeedDelta, scoreDelta;
    float savedSpeed;
    public bool paused, dead, gameStarted, showInstruction, showMessage, turnButtonPressed;
    public string message;


    void Awake() {
        instance = this;
    }
    
    // Use this for initialization
	void Start () {
        distanceTravelled = 0;
        savedSpeed = speed;
        fuel = 100;
        score = 0;
        speed = savedSpeed = 0;
        dead = false;
        paused = true;
        gameStarted = false;
        showInstruction = false;
        showMessage = false;
        message = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (!paused) {
            turn = TunnelGUIScript.instance.turnDirection * turnSpeed;
        }
        else
            turn = 0;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.P) && !dead)
            PauseOrUnpause(false);

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && dead)
            Application.LoadLevel(Application.loadedLevel);

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !gameStarted) {
            PauseOrUnpause(false);
            gameStarted = true;
            speed = 10;
            StartCoroutine("ShowInstruction");
        }

	}

    void FixedUpdate() {
        distanceTravelled += speed * Time.deltaTime;
        if (!paused) {
            speed += speedDelta * Time.deltaTime;
            TunnelCreatorScript.instance.pieceCutoff += cutoffDelta * Time.deltaTime;
            turnSpeed += turnSpeedDelta * Time.deltaTime;
            fuel -= fuelConsumptionRate * Time.deltaTime;
            score += scoreDelta * Time.deltaTime;
        }
    }

    void LateUpdate(){
        
    }

    public void PauseOrUnpause(bool death){
        if (death) {
            savedSpeed = speed;
            speed = 0;
            paused = true;
        }
        else {
            if (speed != 0) {
                savedSpeed = speed;
                speed = 0;
                paused = true;
            }
            else {
                speed = savedSpeed;
                paused = false;
            }
        }
    }

    public void GetOil() {
        fuel += 10;
        if (fuel > 100)
            fuel = 100;

        message = "10 Fuel";
        StartCoroutine("ShowMessage");
    }

    public void GetCoins() {
        score += 50;
        message = "50 Bonus Points";
        StartCoroutine("ShowMessage");

    }

    IEnumerator ShowInstruction() {
        showInstruction = true;
        yield return new WaitForSeconds(7);
        showInstruction = false;
    }
    IEnumerator ShowMessage() {
        showMessage = true;
        yield return new WaitForSeconds(1);
        showMessage = false;
    }



}
