using UnityEngine;
using System.Collections;
using System;

public class UIGame : MonoBehaviour, IEventListener
{

    public AudioClip _begin;
    public AudioClip _anniu;
    public AudioClip _gameover;
    private AudioSource _audioSource;

    public GameObject _pausePanel;
    public GameObject _gameOverPanel;

    public UITexture _level;
    
    public UILabel _userName;

    public UILabel _grande;
    public UILabel _resultPanelGrade;

    private Animator _anim;

    public UITexture _hpBar;

    public UILabel _comboLabel;

    void Awake()
    {
        //进行游戏初始化，所有数据进入初始状态
        GameSystem.Instance.ResetGameMessage();
    }
	void Start () {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_begin);
        _anim = this.GetComponent<Animator>();
        InitGameHeader();
        GameSystem.Instance.clickItemTime = double.Parse((DateTime.Now - DateTime.Parse("1970-1-1")).TotalSeconds.ToString());
        _comboLabel.text = "x0";
	}
	
	void Update () 
    {
        if(!GameSystem.Instance.isPauseState)//非暂停状态下
        {
            //根据总分加快进度
            if (GameSystem.Instance.totalGrade + TableNum.GradeWeights_1 >= TableNum.UpSpeedGradeNode_2)
            {
                _anim.SetBool("isShrink", true);
            }
            if (GameSystem.Instance.totalGrade + TableNum.GradeWeights_2 >= TableNum.UpSpeedGradeNode_4)
            {
                _anim.SetBool("isShrinkAgain", true);
            }

            if(!GameSystem.Instance.isClickReverseItem)
            {
                SetGameSpeed();
            }
            
            if ((!GameSystem.Instance.isGameGoOn))
            {
                //游戏失败，弹出结束界面，上报数据
                _hpBar.fillAmount = 0f;
                GoInGameOver();
                GameSystem.Instance.SendResult(GameSystem.Instance.totalGrade);

            }

            SetHpBarFillAmount();
            _grande.text = GameSystem.Instance.totalGrade.ToString();
        }
	}

    private void SetTotalLevel(int totalGrade)
    {
        if (totalGrade >= TableNum.ScoreNode_4)
        {
            _level.mainTexture = Resources.Load("Image/S") as Texture2D;
        }
        else if (totalGrade >= TableNum.ScoreNode_3 && totalGrade < TableNum.ScoreNode_4)
        {
            _level.mainTexture = Resources.Load("Image/A") as Texture2D;
        }
        else if (totalGrade >= TableNum.ScoreNode_2 && totalGrade < TableNum.ScoreNode_3)
        {
            _level.mainTexture = Resources.Load("Image/B") as Texture2D;
        }
        else if (totalGrade >= TableNum.ScoreNode_1 && totalGrade < TableNum.ScoreNode_2)
        {
            _level.mainTexture = Resources.Load("Image/C") as Texture2D;
        }
        else 
        {
            _level.mainTexture = Resources.Load("Image/C") as Texture2D;
        }
    }

    public void GoInGameOver()
    {
        _audioSource.PlayOneShot(_gameover);

        GameSystem.Instance.isPauseState = true;
        GameSystem.Instance.isGameGoOn = true;
        _gameOverPanel.SetActive(true);

        //显示最终得分
        _resultPanelGrade.text = GameSystem.Instance.totalGrade.ToString();
        Time.timeScale = 0;

        //显示等级 SABC
        SetTotalLevel(GameSystem.Instance.totalGrade);
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

    public void SetHpBarFillAmount()
    {
        if (GameSystem.Instance.Hp == 3)
        {
            _hpBar.fillAmount = 1.0f;
        }
        else if (GameSystem.Instance.Hp == 2)
        {
            _hpBar.fillAmount = 0.65f;
        }
        else if (GameSystem.Instance.Hp == 1)
        {
            _hpBar.fillAmount = 0.35f;
        }
        else if (GameSystem.Instance.Hp == 0)
        {
            _hpBar.fillAmount = 0f;
        }
        else
        {
            _hpBar.fillAmount = 0.75f;
        }
    }

    public void InitGameHeader()
    {
        //初始化上方列表，随机三个填上，1-5随机，6-9随机，10-15随机
        int first = UnityEngine.Random.Range(1, 5);
        int second = UnityEngine.Random.Range(6, 9);
        int third = UnityEngine.Random.Range(10, 15);
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
        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_14)
        {
            Time.timeScale = TableNum.UpSpeedScale_11;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_13)
        {
            Time.timeScale = TableNum.UpSpeedScale_10;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_12)
        {
            Time.timeScale = TableNum.UpSpeedScale_9;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_11)
        {
            Time.timeScale = TableNum.UpSpeedScale_8;
            return;
        }

        if (GameSystem.Instance.totalGrade >= TableNum.UpSpeedGradeNode_10)
        {
            Time.timeScale = TableNum.UpSpeedScale_7;
            return;
        }

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

    public void UpdateCombo()
    {
        int combo = GameSystem.Instance.combo;
        _comboLabel.text = "x" + combo.ToString();
        if (combo > 0)
        {
            _anim.SetTrigger("comboShake");
        }
    }

    void OnEnable()
    {
        AttachEvent();

        //测试带数据
        _userName.text = LoginSystem.Instance._userName;
    }
    void OnDisable()
    {
        DetachEvent();
    }

    public bool OnFireEvent(uint key, object param1, object param2)
    {
        if (key == MiniGameEvent.UPDATE_COMBO)
        {
            UpdateCombo();
        }

        return true;
    }

    public int GetListenerPriority(uint eventKey)
    {
        return 0;
    }

    public void AttachEvent()
    {
        GameEventSystem.rootEventDispatcher.AttachListenerNow(this, MiniGameEvent.UPDATE_COMBO);
    }

    public void DetachEvent()
    {
        GameEventSystem.rootEventDispatcher.DetachListenerNow(this, MiniGameEvent.UPDATE_COMBO);
    }
}
