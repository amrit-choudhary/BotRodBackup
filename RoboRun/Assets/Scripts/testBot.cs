using UnityEngine;
using System.Collections;

public class testBot : MonoBehaviour {

    public AnimationClip Run;
    public AnimationClip Jump;
    public AnimationClip Idle;
    public AnimationClip flip;
    public Animation animParent;
    float dotProduct;
    bool autoRun = true;

    // Use this for initialization
	void Start () {
        animParent = transform.parent.animation;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.C)) {
            autoRun = !autoRun;
        }


        if (autoRun) {
            if (Input.GetButtonDown("Jump"))
                animation.Play("Jump");

            if (!animation.isPlaying)
                animation.Play("Run");
        }
        else {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && !animation.isPlaying)
                animation.Play("Run");

            if (Input.GetButtonDown("Jump"))
                animation.Play("Jump");
        }
	}

}
