using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject player;

    float rateFloor = 1f;            //实例化楼层的时间间隔
    float rateBoom = 20f;                  //实例化炸弹的时间间隔
	public GameObject Floorter;
    public GameObject floor ;

    public GameObject pass;
    public GameObject Pass;

    public int FloorCount = 0;         //剩余层数
    private int i = 0;
    public int Count = 1;              //总层数 
    public float highFloor;
    public float highPassFloor;

    bool isCreatFloor = true;

    public GameObject Boom;
    GameObject boom;

	// Use this for initialization
	void Start () {

        Time.timeScale = 1;  
		StartCreatFloors ();
        StartCreatBoom();
        player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {

        //设置炸弹出现时间间隔
        int timeConut = player.GetComponent<PlayerController>().FloorBroken;
        if(0 <= timeConut && timeConut < 100)
        {
            rateBoom = 20f;
        }
        else if(100 <= timeConut  && timeConut < 200f)
        {
            rateBoom = 15f;
        }
        else if (200 <= timeConut && timeConut < 300f)
        {
            rateBoom = 10f;
        }
        else if (300 <= timeConut && timeConut < 400f)
        {
            rateBoom = 8f;
        }
        else if(timeConut >=400f)
        {
            rateBoom = 5f;
        }

        if(Application.loadedLevelName.ToString() == "002-Play" && Count == 9)
        {
            isCreatFloor = false;
            CreatPassFloor(isCreatFloor);
        }

	}
	
    void CreatPassFloor(bool isCreat)
    {
        if (!isCreat)
        {
            //实例化过渡
            GameObject p = Instantiate(pass,
                                    new Vector3(-0.02f, Floorter.transform.position.y - highPassFloor * i, 2.27f),
                                    Quaternion.identity) as GameObject;
            i = i + 3;  
            Count++;
            isCreatFloor = true;
  
        }
    }
	void CreatFloor()
	{    
        Count++;
        if (FloorCount <= 15 && isCreatFloor==true)
        {         
       
            //实例化楼层
            GameObject f = Instantiate(floor,
                                    new Vector3(Floorter.transform.position.x, Floorter.transform.position.y - highFloor * i, Floorter.transform.position.z),
                                    Floorter.transform.rotation)  as GameObject;       
            i++;
            FloorCount++;
        }
	}
	void StartCreatFloors()
	{
		InvokeRepeating ("CreatFloor",0f, rateFloor);
	}


    void StartCreatBoom()
    {
        InvokeRepeating("Onfire", 0f, rateBoom);
    }
    void Onfire()
    {
        if (player.GetComponent<PlayerController>().GetComponent<StatusControl>().isLeaveTube == true)
        {
            float a = Random.Range(-3f, -2.7f);
            float b = Random.Range(2.7f, 3f);
            float[] sort = new float[2];
            float RandomX = 0;
            float RandomY = Random.Range(-3.5f, 3.5f);
            int m = Random.Range(0, 1);
            sort[0] = a;
            sort[1] = b;
            RandomX = sort[m];
            GameObject.Instantiate(Boom,
                                   new Vector3(RandomX,
                                               player.transform.position.y + RandomY,
                                               player.transform.position.z),
                                   Quaternion.identity);
        }
    }

 
}
