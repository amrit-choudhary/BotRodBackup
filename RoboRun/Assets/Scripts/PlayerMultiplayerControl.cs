using UnityEngine;
using System.Collections;

public class PlayerMultiplayerControl : MonoBehaviour
{

    Camera cam;

    // Use this for initialization
    void Start() {
        if (!networkView.isMine) {
            GetComponent<WallRun>().enabled = false;
            transform.FindChild("Camera").gameObject.SetActive(false);
            GetComponent<Teleport>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
        if (stream.isWriting) {
            Vector3 pos = transform.position;
            stream.Serialize(ref pos);

        }
        else {
            Vector3 posRecieve = Vector3.zero;
            stream.Serialize(ref posRecieve);
            transform.position = posRecieve;
        }
    }
}
