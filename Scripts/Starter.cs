using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Starter : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] TMP_InputField username;
    [SerializeField] TMP_InputField password;
    private bool regist = true;

    ///////////////////////////////////////startet Einloggfenster
    public void hostLogin(){
        start.SetActive(true);
    }

    ////////////////////////////////////////////wechselt von Registrieren zu Einloggen

    public void change(){
        start.transform.GetChild(8).gameObject.SetActive(!regist);
        start.transform.GetChild(4).gameObject.SetActive(!regist);
        if(regist){
            start.transform.GetChild(9).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Register";
            start.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Login";
            regist = false;
        }
        else{
            start.transform.GetChild(9).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Login";
            start.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Register";
            regist = true;
        }
    }

    /////////////////////////////////////////////führt http Request aus
    public void submit(){
        RequestHandler requestHandler = GameObject.Find("RequestHandler").GetComponent<RequestHandler>();
        if(regist){
            Debug.Log(username.text);
            requestHandler.register(username.text, password.text);
        }
        else{
            requestHandler.login(username.text, password.text);
        }
    }

    public void doneRegister(){
        change();
    }

    ////////////////////////////////////////////schließt wenn erfolgreicher Login

    public void doneLogin(){
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        RequestHandler requestHandler = GameObject.Find("RequestHandler").GetComponent<RequestHandler>();
        requestHandler.getStats();
        start.SetActive(false);
    }
}
