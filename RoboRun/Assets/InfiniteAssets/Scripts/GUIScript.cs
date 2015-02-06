using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

    public static GUIScript instance;
    public float speed = 0;
    public int score = 0;
    public GUISkin skin;
    int sw = 0; int sh = 0; float fScore = 0;
    public bool showSpecialMessage = false;
    public string specialMessage = "";

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        sw = Screen.width;
        sh = Screen.height;
    }

    // Update is called once per frame
    void Update() {
        fScore += speed/10; 
    }

    void OnGUI() {
        GUI.TextField(new Rect(0, 20, 400, 40), "Speed : " + speed.ToString("F1"), skin.textField);
        GUI.TextField(new Rect(sw - 400, 20, 300, 40), "Score : " + fScore.ToString("F0"), skin.textField);

        if(showSpecialMessage)
            GUI.TextField(new Rect(sw/2 - 150, sh - 60, 300, 40), specialMessage, skin.textField);
    }
}
