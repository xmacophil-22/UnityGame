using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Linq;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;
public class connection : MonoBehaviour
{
    public bool ePressed = false;
    public bool rA = false;
    public bool lA = false;
    public bool close = false;
    public float horizontalIn= 0;
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 receivedPos = Vector3.zero;

    bool running;
    string dataReceived;

    private PlayerMovement playerMovement;
    private IDictionary<string, Action> actions;
    private Inventory inventory;

    //////////////////////////////////////////////
    private void OnApplicationQuit(){
        listener.Stop();
    }

    /////////////////////////////////////////////Aktualisiert zu steuernde Elemente
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        
        if(scene.name.Equals("Adventure")){
        init();
        }
    }
    ////////////////////////////////////////////sorgt dafür, dass nur eine Connection und nicht mehrere entsteht
    private void Awake(){
        SceneManager.sceneLoaded += OnSceneLoaded;
        int numberPlayers = GameObject.FindGameObjectsWithTag("Connection").Length;
        if(numberPlayers > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(this.gameObject);
            init();
        }
    }
    private void c(){
        close = true;
        StartCoroutine(executeAfterTime(close));
    }
    private void leftA(){
        lA = true;
        StartCoroutine(executeAfterTime(lA));
    }
    private void rightA(){
        rA = true;
        StartCoroutine(executeAfterTime(rA));
    }

    private void act(){
        ePressed = true;
        StartCoroutine(executeAfterTime(ePressed));
    }

    private IEnumerator executeAfterTime(bool set){
        yield return new WaitForSeconds(0.05f);
        set = false;
    }
    ////////////////////////////////////////////////////initialisiert Steuerung
    public void init(){
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        actions = new Dictionary<string, Action>();
        actions.Add("left", playerMovement.left);
        actions.Add("right", playerMovement.right);
        actions.Add("middle", playerMovement.stop);
        actions.Add("jump", playerMovement.jump);
        actions.Add("hide", playerMovement.ducken);
        actions.Add("hit", act);
        actions.Add("leggs_crossed", playerMovement.home);
        actions.Add("menu_left", leftA);
        actions.Add("menu_right", rightA);
        actions.Add("close", c);
        try
        {
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            actions.Add("streched_left_arm", inventory.setOne);
            actions.Add("bent_left_arm", inventory.setTwo);
            actions.Add("bent_right_arm", inventory.setThree);
            actions.Add("streched_right_arm", inventory.setFour);
        }
        catch (System.Exception){}
    }

    //////////////////////////////////////////////////verarbeitet Daten von Python ScriptS
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            act();
        }
        if(Input.GetKeyDown(KeyCode.H)){
            c();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            leftA();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            rightA();
        }
        try{
            if(dataReceived != null){
                string[] befehle = dataReceived.Split(",");
                foreach(string s in befehle){
                    Debug.Log(s);
                    actions[s]();
                }
                dataReceived = null;
            }
        }
        catch{
            init();
        }
        
    }

    //////////////////////////////////////////////////Startet Connection
    private void Start()
    {
        
        init();
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
    }

    ///////////////////////////////////////////////////überprüft auf neue Daten
    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            process();
        }
        listener.Stop();
    }

    /////////////////////////////////////////////////verarbeitet erhaltene Daten
    public void process(){
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string
    }
    ////////////////////////////////////////////////////findet IP Adresse heraus
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}