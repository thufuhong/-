using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterWindow : MonoBehaviour {

	public GameObject axe_econ;
	public GameObject enterWindow;
	public Text message;
	public Text Button_next_text;
	public Text Button_quit_text;
	private attribute hero_value;
	private int curIdx = 100;

	private string message_1;
	private string message_2;
	private string message_3;
	// Use this for initialization
	void Start () {
		// Safe_area_sever.cs init
		// InitFunction ();
	}
	
	// Update is called once per frame
	void Update () {
		// other curIdx
		if (curIdx > 2) {
			return;
		}
		// stop
		Time.timeScale = 0;
		//
		if (curIdx == 2) {
			Button_next_text.text = "下一步";
			Button_quit_text.text = "退出游戏";
			message.text = message_1;
		}
		if (curIdx == 1) {
			Button_next_text.text = "下一步";
			Button_quit_text.text = "退出游戏";
			message.text = message_2;
		}
		if (curIdx == 0) {
			Button_next_text.text = "开始游戏";
			Button_quit_text.text = "退出游戏";
			message.text = message_3;
		}
		if (curIdx < 0) {
			// continue
			Time.timeScale = 1;
			// wait for next using init function
			curIdx = 100;
			// 
			enterWindow.SetActive (false);
		}
	}

	// click Button_next
	public void Button_next() {
		curIdx--;
	}

	// click Button_quit
	public void Button_quit() {
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}

	// init : if enter a new level
	public void InitFunction() {
		// dynamic binding
		enterWindow = transform.Find ("EnterWindow").gameObject;
		message = enterWindow.transform.Find ("Message").GetComponent<Text> ();
		axe_econ = transform.Find ("/axe_econ").gameObject;
		Button_next_text = enterWindow.transform.Find ("Button_next").transform.Find ("Text").GetComponent<Text> (); 
		Button_quit_text = enterWindow.transform.Find ("Button_quit").transform.Find ("Text").GetComponent<Text> ();
		enterWindow.SetActive (true);
		curIdx = 2;
		message.supportRichText = true;
		hero_value = axe_econ.GetComponent<attribute> ();

		// stop
		Time.timeScale = 0;
		// init main message
		message_1 = "\n<b><size=15>    2017年秋季学期 <color=yellow>软件工程</color> <color=red>喜迎十九大</color> 小组大作业：</size></b>"
			+ "\n\n\n<b><size=25>        没有名字的多风格混合游戏 </size></b>"
			+ "\n\n<b><size=15>    团队成员：付宏、叶豪、牟宏杰、武智源、慕思成</size></b>"
			+ "\n<b><size=15>    源码地址：<color=blue>https://github.com/thufuhong/-</color></size></b>";
		message_2 = "\n<b><size=15>     <color=red>关卡：" + SceneManager.GetActiveScene().name + "</color> </size></b>"
			+ "\n\n<b><size=15>        " + hero_value.NiCheng + "，欢迎您进入了青青草原</size></b>"
			+ "\n<b><size=15>        击败 <color=blue>boss（蓝色水晶）</color>是过关的充要条件</size></b>"
			+ "\n<b><size=15>        击杀 <color=green>怪物（比你更好看的小人）</color>获得经验和金钱</size></b>"
			+ "\n<b><size=15>        <color=red>红色非安全区</color>里会受到持续伤害</size></b>";
		message_3 = "<b><size=15>     <color=red>操作说明</color> </size></b>"
			+ "\n\n<b><size=15>        <color=blue>↑ </color>键前进 </size></b>"
			+ "\n<b><size=15>       <color=blue> ←</color>，<color=blue>→ </color>键控制方向</size></b>"
			+ "\n<b><size=15>        <color=blue>Alt </color>键攻击</size></b>"
			+ "\n<b><size=15>        <color=blue>U </color>，<color=blue>I </color>，<color=blue>O </color>，<color=blue>P </color>释放技能</size></b>"
			+ "\n<b><size=15>        技能和攻击有 CD</size></b>";
	}
}
