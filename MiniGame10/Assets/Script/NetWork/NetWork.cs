using UnityEngine;
using System.Collections;

public class NetWork {
    //TODO NetWork

    private static NetWork instance=null;

    #region 单例模式抽象出来（优化）
    private NetWork()
    {

    }

    public static NetWork Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new NetWork();
            }
            return instance;
        }
    }
    #endregion

    public void SendLoginMsg(string[] sendMsg)
    {
        Debug.Log("NetWork Send " + sendMsg[0] + " " + sendMsg[1]);
        this.LoginResultSC();
    }

    public void LoginResultSC()
    {
        LoginSystem.Instance.LoginResultSC();
    }

    public void SendResultMsg(string totalGrade)
    {
        Debug.Log("NetWork SendResultMsg TotalGrade:" + totalGrade);
    }
}
