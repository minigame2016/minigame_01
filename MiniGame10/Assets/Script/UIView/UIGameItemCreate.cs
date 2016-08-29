using UnityEngine;
using System.Collections;

public class UIGameItemCreate : MonoBehaviour
{

    // panel的预设
    public GameObject _item_1001;
    // 添加到谁下
    public GameObject _panelRoot;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    private float timeCount = 0;
	void Update () {
        
	}

    public void OnClickTestCreateBtn()
    {
        Debug.Log("UIGame OnClickTestCreateBtn");

        GameObject panel = NGUITools.AddChild(_panelRoot, _item_1001);
        panel.transform.localPosition = new Vector3(0, 350, 0);
    }

}
