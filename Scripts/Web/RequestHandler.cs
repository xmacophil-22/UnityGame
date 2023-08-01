using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
public class RequestHandler : MonoBehaviour
{

private string key;
private string snippetid;
private static string[] loginSeperators = {"{\"key\":\"", "\",\"SnippetID\":", "}"};
private PlayerStats playerStats;

//////////////////////////////////////////////////////////////////Sendet http-Request und verarbeitet sie
void Awake(){
    int numberPlayers = GameObject.FindGameObjectsWithTag("RequestHandler").Length;
        if(numberPlayers > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(this.gameObject);
        }
}
void Start()
{
    playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    key = "";
    snippetid = "";
}

public void Upload(){
    Debug.Log(Serialize.seri(playerStats));
    StartCoroutine(putRequest("https://unitygamebackend.herokuapp.com/snippets/"+snippetid+"/", "{\"data\": "+ Serialize.seri(playerStats) + "}"));
}

public void putScore(){
    StartCoroutine(putRequest("https://unitygamebackend.herokuapp.com/snippets/"+snippetid+"/", "{\"highscore\": "+playerStats.getHighScore()+"}"));
}
public void getScores(){
    StartCoroutine(getRequest("https://unitygamebackend.herokuapp.com/game/?highscore", (result) =>{
        Debug.Log(result);
        ScoreBoard scoreBoard = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        JsonUtility.FromJsonOverwrite(result, scoreBoard);
        Debug.Log(scoreBoard.Scores[0].username);
        Debug.Log(scoreBoard.Scores[0].highscore);
        scoreBoard.loadScores();
    }));
}

public void getStats(){
    StartCoroutine(getRequest("https://unitygamebackend.herokuapp.com/snippets/"+snippetid+"/", (result) =>{
        string sub1 = "\"data\":";
        string sub2 = ",\"highscore\":";
        int start = result.IndexOf(sub1) + sub1.Length;
        int end = result.IndexOf(sub2) - start;
        string fina = result.Substring(start, end);
        Debug.Log(fina);
        JsonUtility.FromJsonOverwrite(fina, playerStats);
        playerStats.reload();
        SceneManager.LoadScene("Home");
    }));
}

public void login(string username, string password1){
    IDictionary<string,string> dict = new Dictionary<string,string>();
    dict.Add("username", username);
    dict.Add("password", password1);
    StartCoroutine(postRequest("https://unitygamebackend.herokuapp.com/login/", dict, (result) =>{
        if(result != null){
            string[] data = result.Split(loginSeperators, StringSplitOptions.None);
            key = data[1];
            snippetid = data[2];
            Debug.Log(key + ", " + snippetid);
            GameObject.Find("Starter").GetComponent<Starter>().doneLogin();
        }
    }));
}


public void register(string username, string password1){
    IDictionary<string,string> dict = new Dictionary<string,string>();
    dict.Add("username", username);
    dict.Add("password1", password1);
    dict.Add("password2", password1);
    StartCoroutine(postRequest("https://unitygamebackend.herokuapp.com/registration/", dict, (result) =>{
        Debug.Log(result);
        GameObject.Find("Starter").GetComponent<Starter>().doneRegister();
    }));
}

private IEnumerator postRequest(string url, IDictionary<string, string> dict, Action<string> callback)
{
    WWWForm form = new WWWForm();

    List<string> keyList = new List<string>(dict.Keys);

    foreach(string k in keyList){
        form.AddField(k, dict[k]);
    }
    UnityWebRequest uwr = UnityWebRequest.Post(url, form);
    yield return uwr.SendWebRequest();

    if (uwr.result == UnityWebRequest.Result.ConnectionError)
    {
        Debug.Log("Error While Sending: " + uwr.error);
        callback(null);
    }
    else
    {
        Debug.Log("Received: " + uwr.downloadHandler.text);
        callback(uwr.downloadHandler.text);
    }
}
private IEnumerator getRequest(string uri, Action<string> callback)
{
    UnityWebRequest uwr = UnityWebRequest.Get(uri);
    uwr.SetRequestHeader("Authorization", "Token "+key);
    yield return uwr.SendWebRequest();

    if (uwr.result == UnityWebRequest.Result.ConnectionError)
    {
        Debug.Log("Error While Sending: " + uwr.error);
    }
    else
    {   
        callback(uwr.downloadHandler.text);
    }
}
private IEnumerator putRequest(string url, string x)
{
    Debug.Log(x);
    byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes(x);
    UnityWebRequest uwr = UnityWebRequest.Put(url, dataToPut);
    uwr.SetRequestHeader("Authorization", "Token "+key);
    uwr.SetRequestHeader("Content-Type", "application/json");
    yield return uwr.SendWebRequest();

    if (uwr.result == UnityWebRequest.Result.ConnectionError)
    {
        Debug.Log("Error While Sending: " + uwr.error);
    }
    else
    {
        Debug.Log("Received: " + uwr.downloadHandler.text);
    }
}
}