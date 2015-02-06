using UnityEngine;
using System.Collections;

public class BotFlip : MonoBehaviour {

    public GameObject bot1, bot2;
    Animation bot2Anim;
	// Use this for initialization
	void Start () {
        bot2Anim = bot2.GetComponent<Animation>();
        bot2.SetActive(false);
        bot2Anim["Flip"].speed = 0.40f;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.F)) {
            bot1.SetActive(false);
            bot2.SetActive(true);
            bot2Anim.Play("Flip");
            StartCoroutine("ResetAfterPlay");
        }
         
	}

    public void Flip() {
        bot1.SetActive(false);
        bot2.SetActive(true);
        bot2Anim.Play("Flip");
        StartCoroutine("ResetAfterPlay");
    }

    IEnumerator ResetAfterPlay() {
        yield return new WaitForSeconds(1);
        bot1.SetActive(true);
        bot2.SetActive(false);
    }
}
