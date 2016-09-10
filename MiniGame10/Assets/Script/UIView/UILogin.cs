using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour, IEventListener {

    public UIInput _userName;
    public UIInput _passWord;

    public UILabel _tipsShow;

    void OnEnable()
    {
        AttachEvent();
    }
    void OnDisable()
    {
        DetachEvent();
    }

    void Update()
    {
        if (NetWork.Instance._isLoginFailedCall)
        {
            _tipsShow.text = "登录失败";
            NetWork.Instance._isLoginFailedCall = false;
        }

        if (NetWork.Instance._isRegisterFailedCall)
        {
            _tipsShow.text = "注册失败，重新注册";
            NetWork.Instance._isRegisterFailedCall = false;
        }

        if (NetWork.Instance._isRegisterSuccessCall)
        {
            _tipsShow.text = "注册成功，请登录";
            NetWork.Instance._isRegisterSuccessCall = false;
        }
        
    }

    public void OnClickLoginBtn()//登陆成功后自动拉取排行榜
    {
        string username = _userName.value;
        string password = _passWord.value;

        //临时测试
        LoginSystem.Instance._inputUserName = username;
        LoginSystem.Instance._inputpassWord = password;

        Debug.Log("UILogin OnClickLoginBtn " + username + " " + password);
        string[] loginMsg = new string[2];
        loginMsg[0] = username;
        loginMsg[1] = password;

        LoginSystem.Instance.SendMessage(loginMsg);
    }

    public void OnClickRegisterBtn()
    {
        string username_rig = _userName.value;
        string password_rig = _passWord.value;

        Debug.Log("UILogin OnClickRegisterBtn " + username_rig + " " + password_rig);

        LoginSystem.Instance.RegsiterUser(username_rig, password_rig);
    }

    public bool OnFireEvent(uint key, object param1, object param2)
    {
        if (key == MiniGameEvent.LOGIN_RETURN)
        {
            Debug.Log("UILogin OnFireEvent ");
            LoginSystem.Instance._userName = (string)param1;
            LoginSystem.Instance._passWord = (string)param2;
            Application.LoadLevel("MainScene");

            //拉排行榜数据
            RankSystem.Instance.GetRankList();
        }

        return true;
    }

    public int GetListenerPriority(uint eventKey)
    {
        return 0;
    }

    public void AttachEvent()
    {
        GameEventSystem.rootEventDispatcher.AttachListenerNow(this, MiniGameEvent.LOGIN_RETURN);
    }

    public void DetachEvent()
    {
        GameEventSystem.rootEventDispatcher.DetachListenerNow(this, MiniGameEvent.LOGIN_RETURN);
    }
}
