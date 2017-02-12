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

public class SignUp : MonoBehaviour
{
    private SmartFox sfs;

    public Text Log;
    public Text UserIn;
    public Text PasswordIn;


    void Start()
    {
        
    }

    public void OnSignUpButtonClick()
    {
        //initialize SmartFox and EventListeners
        sfs = new SmartFox();
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        //connect, then login as guest, then send signup request
        ConfigData cfg = new ConfigData();
        cfg.Host = "127.0.0.1";
        cfg.Port = 9933;
        cfg.Zone = "SignUpZone";
        sfs.Connect(cfg);
    }

    //Event Handlers
    private void OnConnection(BaseEvent e)
    {
        sfs.Send(new LoginRequest("", "", "SignUpZone"));
    }

    private void OnConnectionLost(BaseEvent e)
    {
        Log.text = "connection lost";
        sfs.RemoveAllEventListeners();
    }

    private void OnLogin(BaseEvent e)
    {
        SFSObject sfso = new SFSObject();
        sfso.PutUtfString("password", PasswordIn.text);
        sfso.PutUtfString("username", UserIn.text); ;
        sfs.Send(new ExtensionRequest("$SignUp.Submit", sfso));
        Log.text = "sent signup request";
    }

    private void OnLoginError(BaseEvent e)
    {
        sfs.RemoveAllEventListeners();
        Log.text = "login error";
    }

    private void OnExtensionResponse(BaseEvent e)
    {
        string cmd = (string)e.Params["cmd"];
        ISFSObject ResponseParams = (ISFSObject)e.Params["params"];
        Log.text = "recieved response from sign up request";

        if (cmd == "$SignUp.Submit")
        {
            if (ResponseParams.GetBool("success"))
            {
                Log.text = "Successfully created account";
                //sfs.Send(new LoginRequest(UserIn.text, PasswordIn.text, "HelloWorld"));
            }
            else
            {
                Log.text = "Error in creating account:" + ResponseParams.GetUtfString("errorMessage");
            }
        }
        sfs.RemoveAllEventListeners();
    }

    void OnDestroy()
    {
    }

    void Update()
    {
        if (sfs != null)
            sfs.ProcessEvents();
    }
}