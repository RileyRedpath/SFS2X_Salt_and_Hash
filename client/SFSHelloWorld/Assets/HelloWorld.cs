using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sfs2X;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using Sfs2X.Entities;

public class HelloWorld : MonoBehaviour {

    private string zone = "HelloWorld";
    private SmartFox sfs;
    private bool loggedIn = false;

    public Canvas LoginCanvas;
    public Canvas LobbyCanvas;

    public Text Log;
    public Text LoginButtonText;

    public Text UserIn;
    public Text PasswordIn;
    public Text MessageIn;


    // Use this for initialization
    void Start ()
    {
        //initialize SmartFox and EventListeners
        LobbyCanvas.GetComponent<CanvasGroup>().alpha = 0;
        LobbyCanvas.GetComponent<CanvasGroup>().interactable = false;
        LobbyCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (sfs != null)
            sfs.ProcessEvents();
    }

    public void OnLoginButtonClick()
    {
        if (!loggedIn)
        {
            sfs = new SmartFox();
            sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            sfs.AddEventListener(SFSEvent.LOGOUT, OnLogout);
            sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
            ConfigData cfg = new ConfigData();
            cfg.Host = "127.0.0.1";
            cfg.Port = 9933;
            cfg.Zone = zone;
            sfs.Connect(cfg);
        }
        else
        {
            sfs.Send(new LogoutRequest());
        }
    }

    public void OnMessageButtonClick()
    {
        if (loggedIn && sfs != null)
        {
            ISFSObject paramsOut = new SFSObject();
            paramsOut.PutUtfString("message",MessageIn.text);
            sfs.Send(new ExtensionRequest("MessageSent",paramsOut));
        }
    }

    //Event Listeners
    private void OnConnection(BaseEvent e)
    {
        Log.text = "Connected";
        ISFSObject loginParams = new SFSObject();
        loginParams.PutUtfString("rawPassword", PasswordIn.text);
        sfs.Send(new LoginRequest(UserIn.text, "", zone, loginParams));
    }

    private void OnConnectionLost(BaseEvent e)
    {
        loggedIn = false;
        LoginButtonText.text = "Login";
        LobbyCanvas.GetComponent<CanvasGroup>().alpha = 0;
        LobbyCanvas.GetComponent<CanvasGroup>().interactable = false;
        LobbyCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        LoginCanvas.GetComponent<CanvasGroup>().alpha = 1;
        LoginCanvas.GetComponent<CanvasGroup>().interactable = true;
        LoginCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Log.text = "Connection lost";
    }

    private void OnExtensionResponse(BaseEvent e)
    {
        string cmd = (string)e.Params["cmd"];
        if(cmd == "MessageSent" || cmd == "MessageRequest")
        {
            ISFSObject ResponseParams = (ISFSObject)e.Params["params"];
            if (ResponseParams.GetBool("success"))
            {
                Log.text = "User Message:" + ResponseParams.GetUtfString("message");
            }
            else
            {
                Log.text = "SQL query failure";
            }
        }
    }

    private void OnLogin(BaseEvent Ext)
    {
        User user = (User) Ext.Params["user"];
        Log.text = "Login success: " + user.Name;
        loggedIn = true;
        LoginButtonText.text = "Logout";
        LobbyCanvas.GetComponent<CanvasGroup>().alpha = 1;
        LobbyCanvas.GetComponent<CanvasGroup>().interactable = true;
        LobbyCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        LoginCanvas.GetComponent<CanvasGroup>().alpha = 0;
        LoginCanvas.GetComponent<CanvasGroup>().interactable = false;
        LoginCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        sfs.Send(new ExtensionRequest("MessageRequest", new SFSObject()));
    }

    private void OnLoginError(BaseEvent e)
    {
    }

    private void OnLogout(BaseEvent e)
    {
        Log.text = "Logged out";
        loggedIn = false;
        LoginButtonText.text = "Login";
        LobbyCanvas.GetComponent<CanvasGroup>().alpha = 0;
        LobbyCanvas.GetComponent<CanvasGroup>().interactable = false;
        LobbyCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        LoginCanvas.GetComponent<CanvasGroup>().alpha = 1;
        LoginCanvas.GetComponent<CanvasGroup>().interactable = true;
        LoginCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
