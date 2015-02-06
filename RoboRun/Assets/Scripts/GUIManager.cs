using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public static GUIManager instance;
    public string status = "";
    public string turnStatus = "";
    public int score = 0;
    public int x, y, width, height;
    public GUISkin skin;
    public Texture2D healthOut, healthIn, settings, arrowRight, arrowRightT, arrowLeft, arrowLeftT;
    int sw, sw2, sh2;
    Color healthColor;
    bool showHealthBar;
    float healthBlinkDuration, healthBlinkTimer;
    GUIStyle buttonStyle;
    
    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        sw = Screen.width;
        sw2 = sw / 2;
        sh2 = Screen.height / 2;
        healthColor = Color.green;
        SetColorOfHealthIn(Color.green);
        showHealthBar = true;
        healthBlinkTimer = 0;
        healthBlinkDuration = 0.25f;
        gameObject.SetActive(false);
        buttonStyle = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
        ElementChanged();
        if (GameManager.instance.health < 10) {
            if (healthBlinkTimer > healthBlinkDuration) {
                showHealthBar = !showHealthBar;
                healthBlinkTimer = 0;
            }
            else
                healthBlinkTimer += Time.deltaTime;
        }
	}

    void OnGUI() {
        if (showHealthBar) {
            GUI.DrawTexture(new Rect(sw2 + x, sh2 + y, (int)(width * GameManager.instance.health / 100), height), healthIn);
            GUI.DrawTexture(new Rect(sw2 + x - 1, sh2 + y, width + 5, height), healthOut);
        }
        
        GUI.TextField(new Rect(200, 0, 400, 40), status, skin.textField);
        GUI.TextField(new Rect(200, 50, 400, 40), turnStatus, skin.textField);
        GUI.TextField(new Rect(sw - 400, 0, 300, 40), "Score : " + score , skin.textField);

        buttonStyle.normal.background = settings;
        if (GUI.Button(new Rect(sw - 0.12f * sw2, 0.02f * sh2, 0.1f * sw2, 0.1f * sw2), "", buttonStyle)) {
            StartCoroutine("OpenPaintShop");
        }

        buttonStyle.normal.background = arrowRightT;
        buttonStyle.active.background = arrowRight;
        if (GUI.RepeatButton(new Rect(sw - 0.2f*sw2, sh2 - 0.1f * sh2, 0.2f*sw2, 0.2f*sw2), "", buttonStyle)) {
            InputManager.instance.TurnBot(true);
        }

        buttonStyle.normal.background = arrowLeftT;
        buttonStyle.active.background = arrowLeft;
        if (GUI.RepeatButton(new Rect(0, sh2 - 0.1f * sh2, 0.2f * sw2, 0.2f * sw2), "", buttonStyle)) {
            InputManager.instance.TurnBot(false);
        }




    }

    public void ElementChanged() {
        healthColor = Color.Lerp(Color.red, Color.green, (int)GameManager.instance.health / 100.0f);
        SetColorOfHealthIn(healthColor);
    }

    public void SetColorOfHealthIn(Color col) {
        Color[] fillColorArray =  healthIn.GetPixels();
 
        for(int i = 0; i < fillColorArray.Length; ++i){
            fillColorArray[i] = col;
        }
  
        healthIn.SetPixels( fillColorArray );
   
        healthIn.Apply();
    }

    IEnumerator OpenPaintShop() {
        Network.Disconnect(200);
        Application.LoadLevel("PaintShop");
        yield return new WaitForSeconds(0.1f);
    }


}
