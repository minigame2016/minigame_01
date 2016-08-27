using UnityEngine;
using System.Collections;

public class GameSystem {

	private static GameSystem instance = null;

    #region 单例模式抽象出来（优化）
    private GameSystem()
    {

    }

    public static GameSystem Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new GameSystem();
            }
            return instance;
        }
    }
    #endregion


}
