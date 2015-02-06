using UnityEngine;
using System.Collections;

public class PaintShopGUIScript : MonoBehaviour {

    public Texture2D rotateLeft, rotateRight, colorHexagon, save;
    public Camera guiCamera;
    public Color pickedColor;
    int sw2, sh2;
    public float rotateX, rotateY, rotateW, rotateD, rotateDV;
    GUIStyle rotateStyle;
    
    // Use this for initialization
	void Start () {
        sw2 = Screen.width / 2; sh2 = Screen.height / 2;
        rotateStyle = new GUIStyle();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(guiCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) {
                if (hitInfo.collider.name == "ColorPicker") {
                    Vector2 texCoord = hitInfo.textureCoord;
                    texCoord *= colorHexagon.width;
                    
                    pickedColor = colorHexagon.GetPixel((int)texCoord.x, (int)texCoord.y);
                    PaintShopGameManager.instance.ChangeColor(pickedColor);
                }
            }
        } 
	
	}

    void OnGUI() {
        rotateStyle.normal.background = rotateLeft;
        if(GUI.RepeatButton(new Rect(sw2 + rotateX * sw2, sh2 + rotateY * sh2, sw2*rotateW, sw2 * rotateW), "", rotateStyle)){
            PaintShopGameManager.instance.RotateLeft();
        }
        rotateStyle.normal.background = rotateRight;
        if (GUI.RepeatButton(new Rect(sw2 + rotateX * sw2 + sw2* rotateD, sh2 + rotateY * sh2, sw2 * rotateW, sw2 * rotateW), "", rotateStyle)) {
            PaintShopGameManager.instance.RotateRight();
        }
        rotateStyle.normal.background = save;
        if (GUI.RepeatButton(new Rect(sw2 + rotateX * sw2 + sw2* 0.5f * rotateD, sh2 + rotateY * sh2 + sh2*rotateDV, sw2 * rotateW * 0.75f, sw2 * rotateW * 0.75f), "", rotateStyle)) {
            PlayerPrefs.SetFloat("ColorR", pickedColor.r);
            PlayerPrefs.SetFloat("ColorG", pickedColor.g);
            PlayerPrefs.SetFloat("ColorB", pickedColor.b);
            Application.LoadLevel("Game");
        }

    }


}
