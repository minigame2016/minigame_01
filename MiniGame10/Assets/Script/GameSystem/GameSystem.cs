using UnityEngine;
using System.Collections;
using System;

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

    public int ramCount = TableNum.RamdomCounter_1;

    public int Hp = TableNum.Hp;//血量
    public bool isAdd = true;//是否添加进List

    //开始时间
    public double clickItemTime = 0;
    public int combo = 0;

    //是否点击了反道具
    public bool isClickReverseItem = false;
    //反道具开始生效时间
    public float reverseCreateTime = 0.0f;
    //反道具结束生效时间
    public float reverseEndTime = 0.0f;
    //反道具生效时的TImeScale
    public float reverseCreTimeScale = 1f;
    

    #endregion

    public void AddPlayerClickItemList(string itemName)
    {
        for (int i = 0; i < PlayerClickItemList.Count; i++ )
        {
            if (PlayerClickItemList[i].Equals(itemName))
            {
                Debug.LogWarning("GameSystem Hp-1  " + itemName);
                Hp = Hp - 1;
                isAdd = false;
                if (Hp == 0)
                {
                    isGameGoOn = false;
                }  
            }
        }

        if (isGameGoOn)
        {
            if (isAdd)
            {
                totalGrade = totalGrade + NowGradeWeights;
                if (PlayerClickItemList.Count == NowPlayerUpItemNum)
                {
                    PlayerClickItemList.RemoveAt(0);
                }
                PlayerClickItemList.Add(itemName);

                double clickItemTime_Now = double.Parse((DateTime.Now - DateTime.Parse("1970-1-1")).TotalSeconds.ToString());
                if (clickItemTime_Now - clickItemTime < 1)
                {
                    combo++;
                    totalGrade = totalGrade + NowGradeWeights;//双倍分数
                }
                else
                {
                    combo = 0;
                }
                clickItemTime = clickItemTime_Now;
            }
            else
            {
                combo = 0;
            }
            Debug.Log("combo: " + combo);

            isAdd = true;
        }

        //通知更新Combo
        GameEventSystem.rootEventDispatcher.FireSynchorEvent(MiniGameEvent.UPDATE_COMBO, null, null);
        
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

        GameSystem.Instance.ramCount = TableNum.RamdomCounter_1;

        GameSystem.Instance.Hp = TableNum.Hp;
        GameSystem.Instance.isAdd = true;

        GameSystem.instance.combo = 0;
    }

    public void SendResult(int totalGrade)
    {
        string username = LoginSystem.Instance._userName;
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
