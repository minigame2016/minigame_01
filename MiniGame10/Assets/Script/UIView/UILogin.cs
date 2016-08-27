using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour {

    public UIInput _userName;
    public UIInput _passWord;

    public void OnClickLoginBtn()
    {
        string username = _userName.text;
        string password = _passWord.text;

        Debug.Log("UILogin OnClickLoginBtn " + username + " " + password);
        string[] loginMsg = new string[2];
        loginMsg[0] = username;
        loginMsg[1] = password;

        LoginSystem.Instance.SendMessage(loginMsg);
    }
}
