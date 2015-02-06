using UnityEngine;
using System.Collections;

public class Multiplayer : MonoBehaviour {

    string connectToIP = "127.0.0.1";
    int connectPort = 25001;
    public GameObject player;
    Texture2D cover;
    
    // Use this for initialization
	void Start () {
        cover = (Texture2D)Resources.Load("cover", typeof(Texture2D));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        if (Network.peerType == NetworkPeerType.Disconnected) {

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), cover);

            GUILayout.Label("Connection status: Disconnected");

            connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
            connectPort = int.Parse((GUILayout.TextField(connectPort.ToString())));

            GUILayout.BeginVertical();

            GUILayout.Space(20); 
            
            if (GUILayout.Button("Connect as client", GUILayout.Height(50))) {
                //Connect to the "connectToIP" and "connectPort" as entered via the GUI
                //Ignore the NAT for now

                Network.Connect(connectToIP, connectPort);
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Start Server", GUILayout.Height(50))) {
                //Start a server for 32 clients using the "connectPort" given via the GUI
                //Ignore the nat for now	

                Network.InitializeServer(32, connectPort, !Network.HavePublicAddress());
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Exit Game", GUILayout.Height(50))) {
                Application.Quit();
            }

            GUILayout.EndVertical();

        }
        else {
            if (Network.peerType == NetworkPeerType.Connecting) {

                GUILayout.Label("Connection status: Connecting");

            }
            else if (Network.peerType == NetworkPeerType.Client) {

                GUILayout.Label("Connection status: Client!");
                GUILayout.Label("Ping to server: " + Network.GetAveragePing(Network.connections[0]));

            }
            else if (Network.peerType == NetworkPeerType.Server) {

                GUILayout.Label("Connection status: Server!");
                GUILayout.Label("Connections: " + Network.connections.Length);
                if (Network.connections.Length >= 1) {
                    GUILayout.Label("Ping to first player: " + Network.GetAveragePing(Network.connections[0]));
                }
            }

            if (GUILayout.Button("Disconnect", GUILayout.Height(50))) {
                Network.Disconnect(200);
            }
        }
                
    }

    void OnServerInitialized() {
        SpawnPlayer("Server");
    }

    void OnPlayerDisconnected(NetworkPlayer player) {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info) {
        Network.RemoveRPCs(Network.player);
        Network.DestroyPlayerObjects(Network.player);
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnConnectedToServer() {
        SpawnPlayer("Client");
    }

    void SpawnPlayer(string s) {
        Vector3 pos;

        if (s == "Server")
            pos = new Vector3(-73, -2.5f, 74);
        else
            pos = new Vector3(-72.5f, -1.5f, 74);

        Network.Instantiate(player, pos, transform.rotation, 0);
        GameManager.instance.StartGame();
        GUIManager.instance.active = true;
    }


}
