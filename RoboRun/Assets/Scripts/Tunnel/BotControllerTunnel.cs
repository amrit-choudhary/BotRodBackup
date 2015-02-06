using UnityEngine;
using System.Collections;

public class BotControllerTunnel : MonoBehaviour {

    public GameObject bot;
    public GameObject camera;
    public float fallCutoff, fallDeathCutoff;
    Animation anim;
    string inAir;
    RaycastHit hit;
    float fallTimer;

    // Use this for initialization
	void Start () {
        anim = bot.GetComponent<Animation>();
        anim["Run"].speed = 1.4f;
        fallTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!anim.isPlaying)
            anim.Play("Run");
        if (Input.GetKeyDown(KeyCode.Space))
            anim.Play("Jump");
        if (Input.GetKeyDown(KeyCode.J))
            anim.Play("Hover");
	}

    

    void FixedUpdate() {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10)) {
            inAir = "Ground";
            fallTimer = 0;
            if(anim.IsPlaying("Hover"))
                anim.Stop();
        }
        else {
            inAir = "Falling";
            fallTimer += Time.deltaTime;
        }

        if (fallTimer > fallCutoff) {
            if (!anim.IsPlaying("Hover")) {
                anim.Stop();
                anim.Play("Hover");
            }
        }

        if (fallTimer > fallDeathCutoff) {
            anim.Stop();
            anim.Play("Idle");
            TunnelGameManager.instance.PauseOrUnpause(true);
            TunnelGameManager.instance.dead = true;
        }

        if (TunnelGameManager.instance.paused)
            anim.Stop();

    }

    void OnTriggerEnter(Collider col){
        if (col.name == "gold") {
            TunnelGameManager.instance.GetCoins();
            Destroy(col.gameObject);
        }
        if (col.name == "oil") {
            TunnelGameManager.instance.GetOil();
            Destroy(col.gameObject);
        }
        
    }
     
     
}
