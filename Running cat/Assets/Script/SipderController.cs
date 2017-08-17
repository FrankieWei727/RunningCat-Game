using UnityEngine;
using System.Collections;

public class SipderController : MonoBehaviour {


   public  float speed = 1f;                     //机器人移动速度
    GameObject collisionGameObject;

    public GameObject player;
    float PosY;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}

    void OnTriggerEnter(Collider info)
    {
        collisionGameObject = info.gameObject;
        if (collisionGameObject.tag.CompareTo("AirY") == 0)
        {
            speed = -speed;
        }
        if (collisionGameObject.tag.CompareTo("GroundX") == 0)
        {
            speed = -speed;
        }
    }

    //退出碰撞
    void OnTriggerExit(Collider info)
    {

    }
	// Update is called once per frame
	void Update () {

        ///蜘蛛移动
        this.transform.position = new Vector3(this.transform.position.x,
                                              this.transform.position.y+ Time.deltaTime * speed,
                                              this.transform.position.z);

        PosY = this.transform.position.y;
        if (PosY > player.transform.position.y + 10f)
        {
            Destroy(this.gameObject);
        }
	}
}
