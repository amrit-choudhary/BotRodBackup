using UnityEngine;
using System.Collections;

public class testBot2 : MonoBehaviour {

    public AnimationClip flip;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.F))
            animation.Play("flip");
	}
}
