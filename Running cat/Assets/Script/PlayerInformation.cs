using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour {


    public UILabel TimeLabel;
    public UILabel FloorLabel;
    public UILabel ScoreLanel;
    public UILabel BestScoreLabel;

    private int ShowFloors = 0;
    private int ShowScore = 0;
    private float ShowTime = 0;
    private int ShowCoin = 0;
    private int ShowHighScore = 0;

    PlayerController playerInfor;
    GameMenu gameMenu;  
    
    void show()
    {

        FloorLabel.text = ShowFloors.ToString();
        TimeLabel.text = ShowTime.ToString() + "分";
        ScoreLanel.text =ShowScore.ToString();
        BestScoreLabel.text = ShowHighScore.ToString();
       
    }

	// Use this for initialization
	void Start () {

      playerInfor = new PlayerController();
      gameMenu = new GameMenu();

      ShowFloors =  PlayerPrefs.GetInt("TotelFloors", 0);
      ShowCoin =  PlayerPrefs.GetInt("TolCoins", 0);
      ShowTime =  PlayerPrefs.GetFloat("TolTIme", 0);
      ShowScore =  PlayerPrefs.GetInt("TolScore", 0);
      ShowHighScore = PlayerPrefs.GetInt("historyHighScore", 0);

      show();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

 
}
