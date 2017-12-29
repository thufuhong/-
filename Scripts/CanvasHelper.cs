using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CanvasHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public attribute _attr;
    public ThirdPersonUserControl c;
    public Text text1;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string name = eventData.pointerEnter.transform.parent.name;
        Debug.Log(name);
        if(name == "Skill_1")
            text1.text = "<b><size=25>狂战士之吼 <color=blue>[U]</color></size></b>\n\n<size=15>斧王通过怒吼激发出可怕的力量，短暂的提升攻击力和移动速度。\n\n"+
                "冷却时间：<color=blue>" + c.Skill_1_CoolDownTime_Max.ToString() + "</color>\n" +
                "等级：<color=blue>" +  _attr.Skill_Level[0].ToString() + "</color>\n" +
                "攻击力提升：<color=red>" + ((c.Skill_1_Value[0] + c.Skill_1_Value[3] * _attr.Skill_Level[0] - 1)*100).ToString() +"%</color>\n"+
                //"防御力提升：<color=red>" + ((c.Skill_1_Value[1] + c.Skill_1_Value[4] * _attr.Skill_Level[0] - 1) * 100).ToString() + "%</color>\n"+
                "移动速度提升：<color=red>" + ((c.Skill_1_Value[2] + c.Skill_1_Value[5] * _attr.Skill_Level[0] - 1) * 100).ToString() + "%</color>\n"+
                "<color=gray>下次升级：\n"+
                "攻击力提升：" + ((c.Skill_1_Value[0] + c.Skill_1_Value[3] * (_attr.Skill_Level[0]+1) - 1) * 100).ToString() + "%\n" +
                //"防御力提升：" + ((c.Skill_1_Value[1] + c.Skill_1_Value[4] * (_attr.Skill_Level[0] + 1) - 1) * 100).ToString() + "%\n" +
                "移动速度提升：" + ((c.Skill_1_Value[2] + c.Skill_1_Value[5] * (_attr.Skill_Level[0] + 1) - 1) * 100).ToString() + "%\n" +
                "</color></size>";
        if (name == "Skill_2")
            text1.text = "<b><size=25>自然之助 <color=blue>[I]</color></size></b>\n\n<size=15>斧王获得自然的祝福，瞬间恢复生命值，并且获得持续的治疗效果\n\n" +
                "冷却时间：<color=blue>" + c.Skill_2_CoolDownTime_Max.ToString() + "</color>\n" +
                "等级：<color=blue>" + _attr.Skill_Level[1].ToString() + "</color>\n" +
                "生命回复：<color=red>" + ((c.Skill_2_Value[0] + c.Skill_2_Value[2] * _attr.Skill_Level[1])).ToString() + "</color>\n" +
                "每秒治疗：<color=red>" + ((c.Skill_2_Value[1] + c.Skill_2_Value[3] * _attr.Skill_Level[1])).ToString() + "</color>\n" +
                "<color=gray>下次升级：\n" +
                "生命回复：" + ((c.Skill_2_Value[0] + c.Skill_2_Value[2] * (_attr.Skill_Level[1]+1))).ToString() + "\n" +
                "每秒治疗：" + ((c.Skill_2_Value[1] + c.Skill_2_Value[3] * (_attr.Skill_Level[1] + 1))).ToString() + "\n" +
                "</color></size>";
        if (name == "Skill_3")
            text1.text = "<b><size=25>昆恩法印 <color=blue>[O]</color></size></b>\n\n<size=15>斧王开启昆恩法印，短暂提升防御力，并免疫危险区域的伤害\n\n" +
                "冷却时间：<color=blue>" + c.Skill_3_CoolDownTime_Max.ToString() + "</color>\n" +
                "等级：<color=blue>" + _attr.Skill_Level[2].ToString() + "</color>\n" +
                "持续时间：<color=red>" + ((c.Skill_3_Value[0] + c.Skill_3_Value[1] * _attr.Skill_Level[2])).ToString() + "</color>\n" +
                "防御力提升：<color=red>" + ((c.Skill_1_Value[1] + c.Skill_1_Value[4] * _attr.Skill_Level[2] - 1) * 100).ToString() + "%</color>\n" +
                "<color=gray>下次升级：\n" +
                "持续时间：" + ((c.Skill_3_Value[0] + c.Skill_3_Value[1] * (_attr.Skill_Level[2] + 1))).ToString() + "\n" +
                "防御力提升：" + ((c.Skill_1_Value[1] + c.Skill_1_Value[4] * (_attr.Skill_Level[2] + 1) - 1) * 100).ToString() + "%\n" +
                "</color></size>";
        if (name == "Skill_4")
            text1.text = "<b><size=25>反击螺旋 <color=blue>[P]</color></size></b>\n\n<size=15>斧王召唤能量粒子攻击附近的单位，不受攻击距离的限制。\n\n" +
                "冷却时间：<color=blue>" + c.Skill_4_CoolDownTime_Max.ToString() + "</color>\n" +
                "等级：<color=blue>" + _attr.Skill_Level[3].ToString() + "</color>\n" +
                "能量粒子攻击力系数：<color=red>" + ((c.Skill_4_Value[0] + c.Skill_4_Value[3] * _attr.Skill_Level[3])).ToString() + "</color>\n" +
                "每波能量粒子数目：<color=red>" + ((c.Skill_4_Value[1] + c.Skill_4_Value[4] * _attr.Skill_Level[3])).ToString() + "</color>\n" +
                "能量粒子波数：<color=red>" + ((c.Skill_4_Value[2] + c.Skill_4_Value[5] * _attr.Skill_Level[3])).ToString() + "</color>\n" +
                "<color=gray>下次升级：\n" +
                "能量粒子攻击力系数：" + ((c.Skill_4_Value[0] + c.Skill_4_Value[3] * (_attr.Skill_Level[3] + 1))).ToString() + "\n" +
                "每波能量粒子数目：" + ((c.Skill_4_Value[1] + c.Skill_4_Value[4] * (_attr.Skill_Level[3] + 1))).ToString() + "\n" +
                "能量粒子波数：" + ((c.Skill_4_Value[2] + c.Skill_4_Value[5] * (_attr.Skill_Level[3] + 1))).ToString() + "\n" +
                "</color></size>";

        text1.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text1.gameObject.transform.parent.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
