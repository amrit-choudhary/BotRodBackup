using UnityEngine;
using System.Collections;

public class AIControl : MonoBehaviour {

    public GameObject startingNode;
    public GameObject bot;
    public float heightOfBotFromGround;
    public LayerMask rayMask;

    Animation botAnim;
    Vector3 nextNode, velocity, rayOrigin;
    public float speed;
    float distToGround, currentSpeed;
    Ray ray;
    RaycastHit rayHit;

    // Use this for initialization
	void Start () {
        botAnim = bot.GetComponent<Animation>();
	    nextNode = startingNode.transform.FindChild("NextNode").transform.position;
        velocity = (nextNode - transform.position).normalized * speed;
        rigidbody.velocity = velocity;
        currentSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {

        currentSpeed = speed * Random.Range(0.9f, 1.1f);

        rayOrigin = transform.position + new Vector3(0, 50, 0);
        if (Physics.Raycast(rayOrigin, Vector3.down, out rayHit, 100, rayMask)) {
            bot.transform.position = new Vector3(bot.transform.position.x, rayHit.point.y + heightOfBotFromGround, bot.transform.position.z);        
        }
        if (!botAnim.isPlaying)
            botAnim.Play("Run");
	}

    void OnTriggerEnter(Collider col) {
        if (col.tag == "aiTravelNode") {
            Debug.Log("Next");
            nextNode = col.gameObject.transform.FindChild("NextNode").transform.position;
            velocity = (nextNode - transform.position).normalized * currentSpeed;
            rigidbody.velocity = velocity;  
        }
    }


}
