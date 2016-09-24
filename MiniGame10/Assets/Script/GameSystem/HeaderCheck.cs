using UnityEngine;
using System.Collections;

public class HeaderCheck : MonoBehaviour, IEventListener
{
    public UITexture _itemTex1;
    public UITexture _itemTex2;
    public UITexture _itemTex3;
    public UITexture _itemTex4;
    public UITexture _itemTex5;

    public void UpdateHeader()
    {
        Debug.Log("HeaderCheck UpdateHeader " + GameSystem.Instance.PlayerClickItemList.Count);
        #region 显示上方图片
        if (GameSystem.Instance.PlayerClickItemList.Count == 1)
        {
            string item1 = GameSystem.Instance.PlayerClickItemList[0].ToString();
            item1 = item1.Substring(0, 4);
            _itemTex1.mainTexture = Resources.Load("Image/Virus/" + item1) as Texture2D;
        }
        if (GameSystem.Instance.PlayerClickItemList.Count == 2)
        {
            string item1 = GameSystem.Instance.PlayerClickItemList[0].ToString();
            item1 = item1.Substring(0, 4);
            _itemTex1.mainTexture = Resources.Load("Image/Virus/" + item1) as Texture2D;

            string item2 = GameSystem.Instance.PlayerClickItemList[1].ToString();
            item2 = item2.Substring(0, 4);
            _itemTex2.mainTexture = Resources.Load("Image/Virus/" + item2) as Texture2D;
        }

        if (GameSystem.Instance.PlayerClickItemList.Count == 3)
        {
            string item1 = GameSystem.Instance.PlayerClickItemList[0].ToString();
            item1 = item1.Substring(0, 4);
            _itemTex1.mainTexture = Resources.Load("Image/Virus/" + item1) as Texture2D;

            string item2 = GameSystem.Instance.PlayerClickItemList[1].ToString();
            item2 = item2.Substring(0, 4);
            _itemTex2.mainTexture = Resources.Load("Image/Virus/" + item2) as Texture2D;

            string item3 = GameSystem.Instance.PlayerClickItemList[2].ToString();
            item3 = item3.Substring(0, 4);
            _itemTex3.mainTexture = Resources.Load("Image/Virus/" + item3) as Texture2D;
        }

        if (GameSystem.Instance.PlayerClickItemList.Count == 4)
        {
            string item1 = GameSystem.Instance.PlayerClickItemList[0].ToString();
            item1 = item1.Substring(0, 4);
            _itemTex1.mainTexture = Resources.Load("Image/Virus/" + item1) as Texture2D;

            string item2 = GameSystem.Instance.PlayerClickItemList[1].ToString();
            item2 = item2.Substring(0, 4);
            _itemTex2.mainTexture = Resources.Load("Image/Virus/" + item2) as Texture2D;

            string item3 = GameSystem.Instance.PlayerClickItemList[2].ToString();
            item3 = item3.Substring(0, 4);
            _itemTex3.mainTexture = Resources.Load("Image/Virus/" + item3) as Texture2D;

            string item4 = GameSystem.Instance.PlayerClickItemList[3].ToString();
            item4 = item4.Substring(0, 4);
            _itemTex4.mainTexture = Resources.Load("Image/Virus/" + item4) as Texture2D;
        }

        if (GameSystem.Instance.PlayerClickItemList.Count == 5)
        {
            string item1 = GameSystem.Instance.PlayerClickItemList[0].ToString();
            item1 = item1.Substring(0, 4);
            _itemTex1.mainTexture = Resources.Load("Image/Virus/" + item1) as Texture2D;

            string item2 = GameSystem.Instance.PlayerClickItemList[1].ToString();
            item2 = item2.Substring(0, 4);
            _itemTex2.mainTexture = Resources.Load("Image/Virus/" + item2) as Texture2D;

            string item3 = GameSystem.Instance.PlayerClickItemList[2].ToString();
            item3 = item3.Substring(0, 4);
            _itemTex3.mainTexture = Resources.Load("Image/Virus/" + item3) as Texture2D;

            string item4 = GameSystem.Instance.PlayerClickItemList[3].ToString();
            item4 = item4.Substring(0, 4);
            _itemTex4.mainTexture = Resources.Load("Image/Virus/" + item4) as Texture2D;

            string item5 = GameSystem.Instance.PlayerClickItemList[4].ToString();
            item5 = item5.Substring(0, 4);
            _itemTex5.mainTexture = Resources.Load("Image/Virus/" + item5) as Texture2D;
        }

        #endregion

    }

    void OnEnable()
    {
        AttachEvent();
    }
    void OnDisable()
    {
        DetachEvent();
    }

    public bool OnFireEvent(uint key, object param1, object param2)
    {
        if (key == MiniGameEvent.UPDATE_HEADER)
        {
            UpdateHeader();
        }

        return true;
    }

    public int GetListenerPriority(uint eventKey)
    {
        return 0;
    }

    public void AttachEvent()
    {
        GameEventSystem.rootEventDispatcher.AttachListenerNow(this, MiniGameEvent.UPDATE_HEADER);
    }

    public void DetachEvent()
    {
        GameEventSystem.rootEventDispatcher.DetachListenerNow(this, MiniGameEvent.UPDATE_HEADER);
    }
}
