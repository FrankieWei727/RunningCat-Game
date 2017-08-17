using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {


	public GameObject BgMenu ;
	public GameObject BgOption;
	public GameObject BgInformation;
	public GameObject BgBuy;
	public GameObject MusicButton;
	public GameObject SoundButton;

	//判断背景音乐是否播放
	public bool NowIsMusic = true;

	//判断音效是否播放
	public bool isSound = true;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	//改变背景音乐按钮的图片
		if(NowIsMusic)
			MusicButton.GetComponent<UISprite>().spriteName =  "musicOn";
		else
			MusicButton.GetComponent<UISprite>().spriteName =  "musicOff";
	//改变音效按钮的图片
		if (isSound)
            SoundButton.GetComponent<UISprite>().spriteName = "soundOn";
		else
            SoundButton.GetComponent<UISprite>().spriteName = "soundOff";

	}

	//开始游戏
	public void OnGamePlay()
	{
		Application.LoadLevel ("003-Load");
	}

	
	//返回主菜单
	public void OnBack()
	{
		BgMenu.SetActive (true);
		BgOption.SetActive (false);
		BgInformation.SetActive (false);
		BgBuy.SetActive (false);
	}

	//进入人物信息界面
	public void OnInformation()
	{
		BgMenu.SetActive (false);
		BgOption.SetActive (false);
		BgInformation.SetActive (true);
		BgBuy.SetActive (false);
	}

	//进入购买界面
	public void OnBuy()
	{
		BgMenu.SetActive (false);
		BgOption.SetActive (false);
		BgInformation.SetActive (false);
		BgBuy.SetActive (true);
	}
	
	//进入选择菜单界面
	public void OnOption()
	{
		BgMenu.SetActive (false);
		BgOption.SetActive (true);
		BgInformation.SetActive (false);
		BgBuy.SetActive (false);
		
	}

	//背景音乐播放按钮
	public void OnMusicController()
	{
        //int isMusic = PlayerPrefs.GetInt("isMusic", 0);
        //if(isMusic ==0)
        //{
        //    NowIsMusic = true;
        //}
        //else
        //{
        //    NowIsMusic = false;
        //}
		if (NowIsMusic) 
		{
			GetComponent<AudioSource> ().Stop ();
            PlayerPrefs.SetInt("isMusic", 1);
			NowIsMusic = false;
		}
		else 
		{
			GetComponent<AudioSource> ().Play ();
            PlayerPrefs.SetInt("isMusic", 0);
			NowIsMusic = true;
		}
	}

	//音乐播放按钮
	public void OnSoundController()
	{
		if (isSound) {
			isSound = false;
		} 
		else 
		{
			isSound = true;
		}
	}

	//退出游戏
	public void OnQuit()
	{
		Application.Quit ();
	}













}
