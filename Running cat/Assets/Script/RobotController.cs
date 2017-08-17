using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

    float speed = 1f;                     //机器人移动速度
    GameObject collisionGameObject;

    public GameObject player;
    float PosY;

	// Use this for initialization
	void Start () {

        if (this.transform.position.x > 0)
        {
            //机器人向左
            speed = -speed;
            this.transform.localScale = new Vector3(-this.transform.localScale.x,
                                      this.transform.localScale.y,
                                      this.transform.localScale.z);
        }
        if (this.transform.position.x < 0)
        {
            //机器人向右
       
        }

        player = GameObject.FindWithTag("Player");
          
	}

    void OnCollisionEnter(Collision info)
    {
        collisionGameObject = info.gameObject;
        if(collisionGameObject.tag.CompareTo("Column")==0)
        {
            speed = -speed;
            this.transform.localScale = new Vector3(-this.transform.localScale.x,
                                                     this.transform.localScale.y,
                                                     this.transform.localScale.z);
        }


    }

    //退出碰撞
    void OnCollisionExit(Collision info)
    {

    }
	// Update is called once per frame
	void Update () {

        //机器人移动
        this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime * speed,
                                              this.transform.position.y,
                                              this.transform.position.z);

        PosY = this.transform.position.y;
        if (PosY > player.transform.position.y + 5f)
        {
            Destroy(this.gameObject);
        }
	}
}
