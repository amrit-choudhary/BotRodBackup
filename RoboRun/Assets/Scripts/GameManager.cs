using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int scorePerSecond;
    public float health;
    public int speed;
    public float fuel;
    public Material botMaterial;
    int score = 0;
    Vector3 startingPos;
    Color pickedColor, originalColor;

    // Use this for initialization

    void Awake() {
        instance = this;
    }

    void Start() {
        Time.timeScale = 0;
        startingPos = transform.position;
        health = 100;
        speed = 0;
        fuel = 100;
        originalColor = new Color(185.0f / 255, 185.0f / 255, 185.0f / 255, 1);

        if (PlayerPrefs.HasKey("ColorR")) {
            pickedColor = new Color(PlayerPrefs.GetFloat("ColorR"), PlayerPrefs.GetFloat("ColorG"), PlayerPrefs.GetFloat("ColorB"), 1);
            botMaterial.color = pickedColor;
        }
        else {
            Debug.Log("No picked color");
            botMaterial.color = originalColor;
        }

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
            Time.timeScale = 1;

        if (Time.timeScale == 1) {
            score += scorePerSecond;
            if(fuel > 0)
                fuel -= 0.05f;

            if (health > 0)
                health -= 0.05f;
        }

        GUIManager.instance.score = score;

        if (Input.GetKeyDown(KeyCode.P)){
            health += 5;
            GUIManager.instance.ElementChanged();
            if (health > 100)
                health = 100;
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            health -= 5;
            GUIManager.instance.ElementChanged();
            if (health < 0)
                health = 0;
        }
    }

    public void StartGame() {
        Time.timeScale = 1;
        fuel = 100;
        score = 0;
        health = 100;
    }


}
