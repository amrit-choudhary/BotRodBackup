using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    int w, h;

    void Start() {
        w = Screen.width; h = Screen.height;
    }

    void Update() {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI() {


        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, h-20, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = (int)fps + " fps";
        GUI.Label(rect, text, style);
    }
}