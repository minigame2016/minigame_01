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

    public int[] RankList = { 0, 0, 0, 0, 0 };//没有分数显示0

    public void SendGetRankListMsg()
    {
        NetWork.Instance.SendGetRankListMsgCS();
    }

    public void GetRankListResut()
    {
        GameEventSystem.rootEventDispatcher.FireSynchorEvent(MiniGameEvent.GET_RANK, null, null);
    }
}

