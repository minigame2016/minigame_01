using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour, IEventListener {

    public AudioClip _anniu;
    private AudioSource _audioSource;

    public UIInput _userName;
    public UIInput _passWord;

    public UILabel _tipsShow;

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

    public void OnClickLoginBtn()
    {
        PlayClipData(OnClickLoginBtnCallback);
    }

    private void OnClickLoginBtnCallback()
    {
        string userName = _userName.value;
        string passWord = _passWord.value;

        LoginSystem.Instance._userName = userName;
        LoginSystem.Instance._passWord = passWord;

        Debug.Log("UILogin OnClickLoginBtn " + userName + " " + passWord);

        LoginSystem.Instance.SendLoginMsg(userName, passWord);
    }

    public void OnClickRegisterBtn()
    {
        _audioSource.PlayOneShot(_anniu);

        string userName = _userName.value;
        string passWord = _passWord.value;

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
            _tipsShow.text = "登录失败";
            NetWork.Instance._isLoginFailedCall = false;
        }

        if (NetWork.Instance._isRegisterFailedCall)
        {
            _tipsShow.text = "注册失败，重新注册";
            NetWork.Instance._isRegisterFailedCall = false;
        }

        if (NetWork.Instance._isRegisterSuccessCall)
        {
            _tipsShow.text = "注册成功，请登录";
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
