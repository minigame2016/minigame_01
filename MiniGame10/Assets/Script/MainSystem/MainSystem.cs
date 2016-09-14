using UnityEngine;
using System.Collections;

public class MainSystem {

    private static MainSystem instance = null;
    #region 单例模式抽象出来（优化）
    private MainSystem()
    {

    }

    public static MainSystem Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new MainSystem();
            }
            return instance;
        }
    }
    #endregion

    public bool isOpenNetWork = false;

    public void GameStart()
    {
        Application.LoadLevel("GameScene");
    }
}
