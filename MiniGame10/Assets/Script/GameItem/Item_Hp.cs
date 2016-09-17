using UnityEngine;
using System.Collections;

public class Item_Hp : MonoBehaviour
{
    public AudioClip _break;
    private AudioSource _audioSource;
    public delegate void AudioCallBack();

    public GameObject _panel_prefab;

    private Transform _transform;

    private Animator _anim;

	// Use this for initialization
	void Start () {
        _transform = this.transform;
        _audioSource = this.GetComponent<AudioSource>();
        _anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    //物体动
        if(!GameSystem.Instance.isPauseState)
        {
            UpdateMove();
        }
        
        ArriveDeadLineOrNot();
	}

    public void PlayClipData(AudioCallBack callback)
    {
        _audioSource.PlayOneShot(_break);
        StartCoroutine(DelayedCallback(TableNum.BreakDelayTime, callback));
    }

    private IEnumerator DelayedCallback(float time, AudioCallBack callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

    private void ArriveDeadLineOrNot()
    {
        if (this.transform.position.y < TableNum.DeadLine)//飘到屏幕下方了
        {
            NGUITools.Destroy(_panel_prefab);
            Debug.Log("Item_Hp ArriveDeadLineOrNot Dead");
        }
    }

    private void UpdateMove()
    {
        // 左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime * (float)0;

        // 向下运动
        _transform.Translate(new Vector3(rx, (-(float)TableNum.HpSpeed * Time.deltaTime)), 0);
    }

    public void OnClickItem()
    {   
        _anim.SetBool("isBreak", true);
        PlayClipData(OnClickItemCallback);
    }

    private void OnClickItemCallback()
    {
        Debug.Log("Item_Hp OnClickItemCallback");
        GameSystem.Instance.Hp = GameSystem.Instance.Hp + 1;
        NGUITools.Destroy(_panel_prefab);
    }
}
