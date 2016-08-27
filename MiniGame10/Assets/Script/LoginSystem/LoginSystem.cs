using UnityEngine;
using System.Collections;

public class LoginSystem {

	private static LoginSystem instance=null;

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

    public void SendMessage(string[] sendMsg)
    {
        NetWork.Instance.SendMsg(sendMsg);
    }

    public void LoginResultSC()
    {
        Application.LoadLevel("MainScene");
    }
}
