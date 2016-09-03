using UnityEngine;
using System.Collections;

public class UIGame : MonoBehaviour {

    public GameObject _PausePanel;
    public GameObject _GameOverPanel;
    
    public UILabel _userName;

    public UILabel _grande;
    public UILabel _resultPanelGrade;

	// Use this for initialization
	void Start () {
        //进行游戏初始化，所有数据进入初始状态
        GameSystem.Instance.ResetGameMessage();
	}
	
	// Update is called once per frame
    private float timeCount = 0;
	void Update () {
        if(!GameSystem.Instance.isPauseState)//非暂停状态下
        {
            //根据总分加快进度
            SetGameSpeed();

            if (!GameSystem.Instance.isGameGoOn)
            {
                //游戏失败，弹出结束界面，上报数据
                GoInGameOver();
                GameSystem.Instance.SendResult(GameSystem.Instance.totalGrade);

            }
            _grande.text = "分数" + GameSystem.Instance.totalGrade.ToString();
        }
	}

    void OnEnable()
    {   
        //测试带数据
        _userName.text = LoginSystem.Instance._userName;
    }

    public void GoInGameOver()
    {
        _GameOverPanel.SetActive(true);
        _resultPanelGrade.text = GameSystem.Instance.totalGrade.ToString();
        Time.timeScale = 0;
    }

    public void OnClickPauseBtn()
    {
        _PausePanel.SetActive(true);

        //暂停游戏等操作
        GameSystem.Instance.isPauseState = true;
    }

    public void OnClickReturnGameBtn()
    {
        _PausePanel.SetActive(false);

        //继续游戏等操作
        Time.timeScale = 1;
        GameSystem.Instance.isPauseState = false;
    }

    public void OnClickReStartBtn()
    {
        //重新开始
        GameSystem.Instance.ResetGameMessage();
        Application.LoadLevel(Application.loadedLevelName);
        Time.timeScale = 1;
    }

    public void OnClickReturnMainBtn()
    {
        //返回主界面
        GameSystem.Instance.ResetGameMessage();
        Application.LoadLevel("MainScene");
        GameSystem.Instance.PlayerClickItemList.Clear();
        Time.timeScale = 1;
    }

    public void SetGameSpeed()
    {
        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_5)
        {
            Time.timeScale = TableNum.UpSpeedScale_5;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_4)
        {
            Time.timeScale = TableNum.UpSpeedScale_4;
            GameSystem.Instance.NowPlayerUpItemNum = TableNum.PlayerUpItemNum_3;
            GameSystem.Instance.NowGradeWeights = TableNum.GradeWeights_3;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_3)
        {
            Time.timeScale = TableNum.UpSpeedScale_3;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_2)
        {
            Time.timeScale = TableNum.UpSpeedScale_2;
            GameSystem.Instance.NowPlayerUpItemNum = TableNum.PlayerUpItemNum_2;
            GameSystem.Instance.NowGradeWeights = TableNum.GradeWeights_2;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_1)
        {
            Time.timeScale = TableNum.UpSpeedScale_1;
            return;
        }
    }
}
