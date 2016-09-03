using UnityEngine;
using System.Collections;

public class Item_1006 : MonoBehaviour
{
    public GameObject _panel_prefab;

    private Transform _transform;

	// Use this for initialization
	void Start () {
        _transform = this.transform;
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

    private void ArriveDeadLineOrNot()
    {
        if (this.transform.position.y < -1)//飘到屏幕下方了
        {
            NGUITools.Destroy(_panel_prefab);
            Debug.Log("Item_1006 ArriveDeadLineOrNot Dead");
            GameSystem.Instance.isGameGoOn = false;
        }
    }

    private void UpdateMove()
    {
        // 左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime * (float)GameSystem.Instance.Item_1006_H_Speed;

        // 向下运动
        _transform.Translate(new Vector3(rx, (-(float)GameSystem.Instance.Item_1006_V_Speed * Time.deltaTime)), 0);
    }

    public void OnClickItem()
    {
        string itemName = _panel_prefab.name;
        GameSystem.Instance.AddPlayerClickItemList(itemName);

        NGUITools.Destroy(_panel_prefab);
    }
}
