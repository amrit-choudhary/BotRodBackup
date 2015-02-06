using UnityEngine;
using System.Collections;

public class SpeedMeterHandController : MonoBehaviour {

    int maxHandAngle = -69;
    int minHandAngle = -292;
    float oldSpeed, newSpeed, currentSpeed;
    public int currentHandAngle;
    float lerpSpeed = 10;

    // Use this for initialization
	void Start () {
        currentHandAngle = -292;
        oldSpeed = newSpeed = currentSpeed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        newSpeed = GameManager.instance.speed;
        newSpeed += Random.Range(-0.25f, 0.25f);
        Vector3 temp = Vector3.Lerp(new Vector3(newSpeed, 0, 0), new Vector3(oldSpeed, 0, 0), lerpSpeed * Time.deltaTime);
        currentSpeed = temp.x;
        oldSpeed = newSpeed;
  
        currentHandAngle = minHandAngle + (int)((currentSpeed / 20.0f) * (maxHandAngle-minHandAngle));
        transform.rotation = Quaternion.Euler(-90, currentHandAngle, 0);
	}



}
