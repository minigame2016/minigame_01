using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour, IEventListener {

    public AudioClip _anniu;
    private AudioSource _audioSource;

    public UIInput _userName;
    public UIInput _passWord;

    public UILabel _tipsShow;

    public UILabel _netLabel;

    public delegate void AudioCallBack();

    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        AttachEvent();
    }
    void OnDisable()
    {
        DetachEvent();
    }

    void Update()
    {
        NetWorkListerer();
        if(MainSystem.Instance.isOpenNetWork)
        {
            _netLabel.text = "网络关闭";
        }
        else
        {
            _netLabel.text = "网络打开";
        }
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

    public void OnClickNetBtn()
    {
        Debug.Log("UILogin OnClickNetBtn ");
        if (MainSystem.Instance.isOpenNetWork)
        {
            MainSystem.Instance.isOpenNetWork = false;
        }
        else
        {
            MainSystem.Instance.isOpenNetWork = true;
        }
    }

    public void OnClickLoginBtn()
    {
        PlayClipData(OnClickLoginBtnCallback);
    }

    private void OnClickLoginBtnCallback()
    {
        string userName = null;
        string passWord = null;
        if (_userName.value != "")
        {   
            
            userName = _userName.value;
        }
        if (_passWord.value != "")
        {
            passWord = _passWord.value;
        }

        LoginSystem.Instance._userName = userName;
        LoginSystem.Instance._passWord = passWord;

        Debug.Log("UILogin OnClickLoginBtn " + userName + " " + passWord);

        LoginSystem.Instance.SendLoginMsg(userName, passWord);
    }

    public void OnClickRegisterBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        string userName = null;
        string passWord = null;
        if (_userName.value != "")
        {

            userName = _userName.value;
        }
        if (_passWord.value != "")
        {
            passWord = _passWord.value;
        }

        Debug.Log("UILogin OnClickRegisterBtn " + userName + " " + passWord);

        LoginSystem.Instance.SendRegsiterMsg(userName, passWord);
    }

    public void OnClickQuitBtn()
    {
        Debug.Log("UILogin OnClickQuitBtn ");
        UnityEngine.Application.Quit();
    }

    private void NetWorkListerer()
    {
        if (NetWork.Instance._isLoginSuccessCall)
        {
            NetWork.Instance.LoginResultSC();
            NetWork.Instance._isLoginSuccessCall = false;
        }

        if (NetWork.Instance._isLoginFailedCall)
        {
            _tipsShow.text = "Tips:LoginFiled!";
            NetWork.Instance._isLoginFailedCall = false;
        }

        if (NetWork.Instance._isRegisterFailedCall)
        {
            _tipsShow.text = "Tips:RegistFailed! Redo!";
            NetWork.Instance._isRegisterFailedCall = false;
        }

        if (NetWork.Instance._isRegisterSuccessCall)
        {
            _tipsShow.text = "Tips:RegistSuccess! GoOn!";
            NetWork.Instance._isRegisterSuccessCall = false;
        }
    }

    public bool OnFireEvent(uint key, object param1, object param2)
    {
        if (key == MiniGameEvent.LOGIN_RETURN)
        {
            Debug.Log("UILogin OnFireEvent");
            Application.LoadLevel("MainScene");
        }

        return true;
    }

    public int GetListenerPriority(uint eventKey)
    {
        return 0;
    }

    public void AttachEvent()
    {
        GameEventSystem.rootEventDispatcher.AttachListenerNow(this, MiniGameEvent.LOGIN_RETURN);
    }

    public void DetachEvent()
    {
        GameEventSystem.rootEventDispatcher.DetachListenerNow(this, MiniGameEvent.LOGIN_RETURN);
    }
}
