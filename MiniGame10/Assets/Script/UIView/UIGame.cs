using UnityEngine;
using System.Collections;

public class UIGame : MonoBehaviour {

    public AudioClip _begin;
    public AudioClip _anniu;
    public AudioClip _gameover;
    private AudioSource _audioSource;

    public GameObject _pausePanel;
    public GameObject _gameOverPanel;
    
    public UILabel _userName;

    public UILabel _grande;
    public UILabel _resultPanelGrade;

    void Awake()
    {
        //进行游戏初始化，所有数据进入初始状态
        GameSystem.Instance.ResetGameMessage();
    }
	void Start () {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_begin);
        InitGameHeader();
	}
	
	void Update () {
        if(!GameSystem.Instance.isPauseState)//非暂停状态下
        {
            //根据总分加快进度
            SetGameSpeed();

            if ((!GameSystem.Instance.isGameGoOn))
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
        _audioSource.PlayOneShot(_gameover);

        GameSystem.Instance.isPauseState = true;
        GameSystem.Instance.isGameGoOn = true;
        _gameOverPanel.SetActive(true);
        _resultPanelGrade.text = GameSystem.Instance.totalGrade.ToString();
        Time.timeScale = 0;
    }

    public void OnClickPauseBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        _pausePanel.SetActive(true);

        //暂停游戏等操作
        GameSystem.Instance.isPauseState = true;
    }

    public void OnClickReturnGameBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        _pausePanel.SetActive(false);

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

    public void InitGameHeader()
    {
        //初始化上方列表，随机三个填上，1-5随机，6-9随机，10-15随机
        int first = Random.Range(1, 5);
        int second = Random.Range(6, 9);
        int third = Random.Range(10, 15);
        string firstHeader = "100" + first.ToString() + "(Clone)";
        string secondHeader = "100" + second.ToString() + "(Clone)";
        string thirdHeader = "10" + third.ToString() + "(Clone)";
        GameSystem.Instance.PlayerClickItemList.Add(firstHeader);
        GameSystem.Instance.PlayerClickItemList.Add(secondHeader);
        GameSystem.Instance.PlayerClickItemList.Add(thirdHeader);
        GameEventSystem.rootEventDispatcher.FireSynchorEvent(MiniGameEvent.UPDATE_HEADER, null, null);
    }

    public void SetGameSpeed()
    {   
        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_9)
        {
            Time.timeScale = TableNum.UpSpeedScale_6;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_8)
        {
            Time.timeScale = TableNum.UpSpeedScale_3;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_7)
        {
            Time.timeScale = TableNum.UpSpeedScale_2;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_6)
        {
            Time.timeScale = TableNum.UpSpeedScale_1;
            return;
        }

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
            //GameSystem.Instance.ramCount = TableNum.RamdomCounter_3;
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
            GameSystem.Instance.ramCount = TableNum.RamdomCounter_2;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_1)
        {
            Time.timeScale = TableNum.UpSpeedScale_1;
            return;
        }
    }
}
