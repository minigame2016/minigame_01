using UnityEngine;
using System.Collections;

public class GameSystem{

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

    ArrayList PlayerClickItemList = new ArrayList();

    public void AddPlayerClickItemList(string itemName)
    {
        PlayerClickItemList.Add(itemName);
        Debug.Log("GameSystem AddPlayerClickItemList " + itemName);
    }

    public double Item_1001_V_Speed = 0.1;
    public double Item_1001_H_Speed = 0.1;
}
