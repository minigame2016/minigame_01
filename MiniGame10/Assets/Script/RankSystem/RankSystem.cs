using UnityEngine;
using System.Collections;

public class RankSystem
{

	private static RankSystem instance=null;

    #region 单例模式抽象出来（优化）
    private RankSystem()
    {

    }

    public static RankSystem Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new RankSystem();
            }
            return instance;
        }
    }
    #endregion

    public int[] RankList = { 80000, 70000, 60000, 50000, 40000 };//没有分数后台赋值为0再传回来
}

