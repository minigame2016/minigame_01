using UnityEngine;
using System.Collections;

public class Item_1015 : MonoBehaviour
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
        if (!GameSystem.Instance.isPauseState)
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
            Debug.Log("Item_1015 ArriveDeadLineOrNot Dead");
            GameSystem.Instance.Hp = GameSystem.Instance.Hp - 1;
            if (GameSystem.Instance.Hp == 0)
            {
                GameSystem.Instance.isGameGoOn = false;
            }
        }
    }

    private void UpdateMove()
    {
        // 左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime * (float)GameSystem.Instance.Item_1015_H_Speed;

        // 向下运动
        _transform.Translate(new Vector3(rx, (-(float)GameSystem.Instance.Item_1015_V_Speed * Time.deltaTime)), 0);
    }

    public void OnClickItem()
    {
        _anim.SetBool("isBreak", true);
        PlayClipData(OnClickItemCallback);
    }

    private void OnClickItemCallback()
    {
        string itemName = _panel_prefab.name;
        GameSystem.Instance.AddPlayerClickItemList(itemName);

        NGUITools.Destroy(_panel_prefab);
    }
}
