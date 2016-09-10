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

    #region  Variable

    public bool isGameGoOn = true;
    public int totalGrade = 0;
    public bool isPauseState = false;

    public ArrayList PlayerClickItemList = new ArrayList();

    public int NowPlayerUpItemNum = TableNum.PlayerUpItemNum_1;
    public int NowGradeWeights = TableNum.GradeWeights_1;
    

    #endregion

    public void AddPlayerClickItemList(string itemName)
    {
        for (int i = 0; i < PlayerClickItemList.Count; i++ )
        {
            if (PlayerClickItemList[i].Equals(itemName))
            {
                Debug.LogWarning("GameSystem Dead!!!!!!  " + itemName);
                isGameGoOn = false;
            }
        }

        if (isGameGoOn)
        {
            totalGrade = totalGrade + NowGradeWeights;
        }
        
        if (PlayerClickItemList.Count == NowPlayerUpItemNum)
        {
            PlayerClickItemList.RemoveAt(0);
        }

        
        PlayerClickItemList.Add(itemName);

        //通知更新Header
        GameEventSystem.rootEventDispatcher.FireSynchorEvent(MiniGameEvent.UPDATE_HEADER, null, null);

        Debug.Log("GameSystem AddPlayerClickItemList " + itemName);
    }

    public void ResetGameMessage()
    {
        GameSystem.Instance.isGameGoOn = true;
        GameSystem.Instance.totalGrade = 0;
        GameSystem.Instance.PlayerClickItemList.Clear();
        Time.timeScale = 1;
        GameSystem.Instance.isPauseState = false;

        GameSystem.Instance.NowPlayerUpItemNum = TableNum.PlayerUpItemNum_1;
        GameSystem.Instance.NowGradeWeights = TableNum.GradeWeights_1;
    }

    public void SendResult(int totalGrade)
    {
        string username = LoginSystem.Instance._inputUserName;
        NetWork.Instance.SendResultMsg(username, totalGrade.ToString());
    }

    #region Item V H

    public double Item_1001_V_Speed;//上下
    public double Item_1001_H_Speed;//左右

    public double Item_1002_V_Speed;
    public double Item_1002_H_Speed;

    public double Item_1003_V_Speed;
    public double Item_1003_H_Speed;

    public double Item_1004_V_Speed;
    public double Item_1004_H_Speed;

    public double Item_1005_V_Speed;
    public double Item_1005_H_Speed;

    public double Item_1006_V_Speed;
    public double Item_1006_H_Speed;

    public double Item_1007_V_Speed;
    public double Item_1007_H_Speed;

    public double Item_1008_V_Speed;
    public double Item_1008_H_Speed;

    public double Item_1009_V_Speed;
    public double Item_1009_H_Speed;

    public double Item_1010_V_Speed;
    public double Item_1010_H_Speed;

    public double Item_1011_V_Speed;
    public double Item_1011_H_Speed;

    public double Item_1012_V_Speed;
    public double Item_1012_H_Speed;

    public double Item_1013_V_Speed;
    public double Item_1013_H_Speed;

    public double Item_1014_V_Speed;
    public double Item_1014_H_Speed;

    public double Item_1015_V_Speed;
    public double Item_1015_H_Speed;

    #endregion
}
