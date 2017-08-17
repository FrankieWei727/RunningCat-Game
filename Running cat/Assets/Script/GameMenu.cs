using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    public GameObject PausePlane;
    public GameObject GameOverPlane;
    public GameObject PauseButton;
    public GameObject PassPlane; 

    public Text FloorsLable;
    public Text ScoreLable;
    public Text GoldLable;
    GameObject player;

    PlayerInformation PlayerInfor;

    int NowFloors ;
    int NowScore ;
    int NowCoin ;
    float NowTime;
   
	// Use this for initialization
	void Start () {

        PlayerInfor = new PlayerInformation();
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (Application.loadedLevelName.ToString() == "002-Play" &&
            player.GetComponent<PlayerController>().FloorBroken == 15)
        { 
            TimeStop();
            PassPlane.SetActive(true);       
        }
	}

    public void OnPassGame()
    {     
        NextPass();
    }

    //进入下一关
    void NextPass()
    {   
        Application.LoadLevel("005-LoadSecond");
    }
    

    //游戏暂停
    public void OnPauseGame()
    {
        TimeStop();
        PausePlane.SetActive(true);
        PauseButton.GetComponent<Button>().interactable = false;
    }

    //返回主菜单
    public void OnReturnMenu()
    {
        SavePlayerInformation();
        PauseButton.GetComponent<Button>().interactable = true;
        Time.timeScale = 1;  
        Application.LoadLevel("001-Menu");
    }

    //返回游戏
    public void OnReturnGame()
    {
        Time.timeScale = 1;
        PauseButton.GetComponent<Button>().interactable = true;
        PausePlane.SetActive(false);
    }

    //重新开始游戏
    public void OnPlayAgain()
    {
        Time.timeScale = 1;

        SavePlayerInformation();
        //重新加载游戏场景
        Application.LoadLevel("002-Play");
        GameOverPlane.SetActive(false);
    }

    //游戏中止，保存玩家信息
    public void SavePlayerInformation()
    {
        int Tolfloors = PlayerPrefs.GetInt("TotelFloors", 0);
        int TolCoins = PlayerPrefs.GetInt("TolCoins", 0);
        int TolScore = PlayerPrefs.GetInt("TolScore", 0);
        float TolTime = PlayerPrefs.GetFloat("TolTime", 0);

        NowFloors = Tolfloors + GameObject.FindWithTag("Player").GetComponent<PlayerController>().FloorBroken;
        NowScore = TolScore + GameObject.FindWithTag("Player").GetComponent<PlayerController>().Score;
        NowCoin = TolCoins + GameObject.FindWithTag("Player").GetComponent<PlayerController>().CoinCount;
        NowTime = TolTime + GameObject.FindWithTag("Player").GetComponent<PlayerController>().time;

        PlayerPrefs.SetInt("TotelFloors", NowFloors);
        PlayerPrefs.SetInt("TolCoins", NowCoin);
        PlayerPrefs.SetFloat("TolTIme", NowTime);
        PlayerPrefs.SetInt("TolScore", NowScore);
    }

    //游戏结束，保存玩家信息
    public void OnGameOver(int nowFloors , int nowScore,int nowCoin,float nowTime)
    {

        int Tolfloors = PlayerPrefs.GetInt("TotelFloors", 0);
        int TolCoins = PlayerPrefs.GetInt("TolCoins", 0);
        int TolScore = PlayerPrefs.GetInt("TolScore", 0);
        int historyHighScore = PlayerPrefs.GetInt("historyHighScore", 0);
        float TolTime = PlayerPrefs.GetFloat("TolTime", 0);   
        
        int Floors = 0;
        int Score = 0;
        int Coins = 0;
        float Times = 0;

        GameOverPlane.SetActive(true);      
        PauseButton.SetActive(false);

        Floors = Tolfloors + nowFloors;
        Score = TolScore + nowScore;
        Coins = TolCoins + nowCoin;
        Times = TolTime + nowTime;

        FloorsLable.text = nowFloors.ToString();
        ScoreLable.text = nowScore.ToString();
        GoldLable.text =  nowCoin.ToString();

        //保存历史最高成绩
        if(nowScore > historyHighScore)
        {
            PlayerPrefs.SetInt("historyHighScore", nowScore);
        }

        PlayerPrefs.SetInt("TotelFloors", Floors);
        PlayerPrefs.SetInt("TolCoins", Coins);
        PlayerPrefs.SetFloat("TolTIme", Times);
        PlayerPrefs.SetInt("TolScore",Score);

        Invoke("TimeStop", 1f);  

    }

    //退出游戏
    public void OnExitGame()
    {
        SavePlayerInformation();
        Application.Quit();
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }
}
