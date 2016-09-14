using UnityEngine;
using System.Collections;

public class LoginSystem
{
    private static LoginSystem instance = null;

    #region 单例模式抽象出来（优化）
    private LoginSystem()
    {

    }

    public static LoginSystem Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new LoginSystem();
            }
            return instance;
        }
    }
    #endregion

    public string _userName = null;
    public string _passWord = null;

    public void SendLoginMsg(string userName, string passWord)
    {
        NetWork.Instance.SendLoginMsgCS(userName, passWord);
    }

    public void SendRegsiterMsg(string userName, string passWord)
    {
        NetWork.Instance.SendRegsiterMsgCS(userName, passWord);
    }

    public void LoginResult()
    {
        GameEventSystem.rootEventDispatcher.FireSynchorEvent(MiniGameEvent.LOGIN_RETURN, null, null);
    }
}


