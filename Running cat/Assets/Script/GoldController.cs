using UnityEngine;
using System.Collections;

public class GoldController : MonoBehaviour {

    public GameObject player;
    Vector3 PlayerV;
    Vector3 GlodCoinV;
    public float distance;

    bool isCoinMove = false;                   //判断金币是否可以移动
    float PosY;

    PlayerController PlayerCon;
	// Use this for initialization
	void Start () {

        PlayerCon = new PlayerController();
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        PlayerV = player.transform.position;
        GlodCoinV = this.transform.position;
        distance = Vector3.Distance(PlayerV, GlodCoinV);    //计算角色和金币之间的距离

        //角色吸收金币
        if(distance<1f)
        {
            isCoinMove = true;  //金币移动
        }

        if(isCoinMove)
        {   
            iTween.MoveTo(this.gameObject, player.transform.position, 0.3f);
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().PlayPickSound();
            if(distance <=0.5f)
            {       
             
                //吸收金币加1分,金币数量加1
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().Score++;
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().CoinCount++;

                Destroy(this.gameObject);          
              

            }
        }


        PosY = this.transform.position.y;
        if (PosY > player.transform.position.y + 15f)
        {
            Destroy(this.gameObject);
        }
        

	}
}
