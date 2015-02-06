using UnityEngine;
using System.Collections;

public class BotControlPaintMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Application.loadedLevelName == "PaintShop")
            GetComponent<BotControllerScript>().enabled = false;
        else
            enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
