using UnityEngine;
using System.Collections;

public class PaintShopGameManager : MonoBehaviour {

    public static PaintShopGameManager instance;
    public GameObject bot;
    public Material botMaterial;
    GameObject DisplayBot;
    Color botColor, originalColor;



    void Awake() {
        instance = this;
    }

    // Use this for initialization
	void Start () {
        StartCoroutine("SpawnPlayer");

        originalColor = new Color(185.0f/255, 185.0f/255, 185.0f/255, 1);
        ChangeColor(originalColor);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
	}

    public void RotateLeft() {
        DisplayBot.transform.Rotate(Vector3.up, 5);
    }

    public void RotateRight() {
        DisplayBot.transform.Rotate(Vector3.up, -5);
    }

    public void ChangeColor(Color inputColor) {
        botMaterial.color = inputColor;
    }

    IEnumerator SpawnPlayer() {
        yield return new WaitForSeconds(0.1f);
        DisplayBot = (GameObject)Instantiate(bot);
    }


}
