using UnityEngine;
using System.Collections;

public class RuleData
{
    public int id = 1;
    public string ruleName = "";
    public string rule = "";
}

public class ReadRuleXML : MonoBehaviour{

    public TextAsset _xmldata;

    public ArrayList _ruleListR = new ArrayList();

    void Start()
    {
        ReadRule();
        XMLManager.Instance._ruleList = _ruleListR;
    }

    // 读取规则XML
    private void ReadRule()
    {
        XMLParser xmlparse = new XMLParser();
        XMLNode node = xmlparse.Parse(_xmldata.text);

        XMLNodeList list = node.GetNodeList("ROOT>0>table");
        for (int i = 0; i < list.Count; i++)
        {
            string id = node.GetValue("ROOT>0>table>" + i + ">@id");
            string ruleName = node.GetValue("ROOT>0>table>" + i + ">@rulename");
            string rule = node.GetValue("ROOT>0>table>" + i + ">@rule");

            RuleData ruleData = new RuleData();
            ruleData.id = int.Parse(id);
            ruleData.ruleName = ruleName;
            ruleData.rule = rule;

            _ruleListR.Add(ruleData);
        }
    }
}
