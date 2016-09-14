using UnityEngine;
using System.Collections;

public class UIMainPage : MonoBehaviour, IEventListener{

    public AudioClip _anniu;
    private AudioSource _audioSource;

    public GameObject _rulePanel;
    public GameObject _rankPanel;

    public UILabel _ruleLabel;

    public UILabel[] rank;

    public UILabel _userName;

    public delegate void AudioCallBack();

    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
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

    void Update()
    {
        NetWorkListerer();
    }

    public void PlayClipData(AudioCallBack callback)
    {
        _audioSource.PlayOneShot(_anniu);
        StartCoroutine(DelayedCallback(1, callback));
    }

    private IEnumerator DelayedCallback(float time, AudioCallBack callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

    public void OnClickGameStartBtn()
    {
        PlayClipData(OnClickGameStartBtnCallback);
    }

    private void OnClickGameStartBtnCallback()
    {
        Debug.Log("UIMainPage OnClickGameStartBtn");
        MainSystem.Instance.GameStart();
    }

    public void OnClickRuleBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        Debug.Log("UIMainPage OnClickRuleBtn");
        _rulePanel.SetActive(true);

        SetRuleLabel();
    }

    public void OnClickCloseRuleBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        _rulePanel.SetActive(false);
    }

    public void OnClickRankBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        Debug.Log("UIMainPage OnClickRankBtn");
        _rankPanel.SetActive(true);

        //请求排行榜
        RankSystem.Instance.SendGetRankListMsg();
    }

    public void OnClickCloseRankBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        _rankPanel.SetActive(false);
    }

    public void OnClickQuitBtn()
    {
        Debug.Log("UILogin OnClickQuitBtn ");
        UnityEngine.Application.Quit();
    }

    private void SetRankItem()
    {
        int [] rankList = RankSystem.Instance.RankList;
        for (int i = 0; i < rankList.Length; i++ )
        {
            rank[i].text = rankList[i].ToString();
        }
    }

    private void SetRuleLabel()
    {
        ArrayList ruleList = XMLManager.Instance._ruleList;
        RuleData ruleData = (RuleData)ruleList[1];
        _ruleLabel.text = ruleData.rule;
    }

    private void NetWorkListerer()
    {
        if (NetWork.Instance._isRankListFinish)
        {
            NetWork.Instance.SaveRankListInfoSC();
            NetWork.Instance._isRankListFinish = false;
        }
    }

    public bool OnFireEvent(uint key, object param1, object param2)
    {
        if (key == MiniGameEvent.GET_RANK)
        {
            //给Label赋值
            SetRankItem();
        }

        return true;
    }

    public int GetListenerPriority(uint eventKey)
    {
        return 0;
    }

    public void AttachEvent()
    {
        GameEventSystem.rootEventDispatcher.AttachListenerNow(this, MiniGameEvent.GET_RANK);
    }

    public void DetachEvent()
    {
        GameEventSystem.rootEventDispatcher.DetachListenerNow(this, MiniGameEvent.GET_RANK);
    }
}
