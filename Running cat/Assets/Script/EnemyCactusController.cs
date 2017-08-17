using UnityEngine;
using System.Collections;

public class EnemyCactusController : MonoBehaviour {

    public GameObject target;
    float x = 0.45f;

    public GameObject player;
    float PosY;

    float rate = 10f;

	// Use this for initialization
	void Start () {

        StartCreatBullets();
        if (this.transform.position.x > 0)
        {
            x = 0.45f;
        }
        if(this.transform.position.x<0)
        {
            x = -0.45f;
        }
        this.transform.localScale = new Vector3(x, this.transform.localScale.y, this.transform.localScale.x);

        player = GameObject.FindWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {


        //设置子弹出现时间间隔
        int timeConut = player.GetComponent<PlayerController>().FloorBroken;
        if (0 <= timeConut && timeConut < 100)
        {
            rate= 10f;
        }
        else if (100 <= timeConut && timeConut < 200f)
        {
            rate = 9f;
        }
        else if (200 <= timeConut && timeConut < 300f)
        {
            rate = 8f;
        }
        else if (300 <= timeConut && timeConut < 400f)
        {
            rate = 7f;
        }
        else if (timeConut >= 400f)
        {
            rate = 6f;
        }
	}

    void OnCreatBullets()
    {

        //实例化子弹
        GameObject.Instantiate(target, 
            new Vector3( this.transform.position.x,
                         this.transform.position.y+0.2f,
                         this.transform.position.z),
                         this.transform.rotation);

        //物体超出屏幕范围则销毁
        PosY = this.transform.position.y;
        if (PosY > player.transform.position.y + 7f)
        {
            Destroy(this.gameObject);
        }
    }

    void  StartCreatBullets()
    {
        InvokeRepeating("OnCreatBullets", 0f, rate);
    }
}
