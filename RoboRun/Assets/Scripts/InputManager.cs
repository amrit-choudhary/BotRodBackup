using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager instance;
    public int turnDirection;
    bool turnButtonPressed;


    void Awake() {
        instance = this;
    }
    
    // Use this for initialization
	void Start () {
        turnDirection = 0;
        turnButtonPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (turnButtonPressed == false && turnDirection != 0) {
            turnDirection = 0;
        }
	}

    public void TurnBot(bool right) {
        if (right)
            turnDirection = 1;
        else
            turnDirection = -1;

        turnButtonPressed = true;
        StartCoroutine("Turn");
    }

    IEnumerator Turn() {
        yield return new WaitForEndOfFrame();
        if (turnButtonPressed)
            turnButtonPressed = false;  
    }



}
