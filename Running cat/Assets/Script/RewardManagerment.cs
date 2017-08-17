using UnityEngine;
using System.Collections;

public class RewardManagerment : MonoBehaviour {

    public GameObject target;

    GameObject collisionGameObject;
    GameObject player;
    float PosY;

	// Use this for initialization
	void Start () {

        player = GameObject.FindWithTag("Player");
	}

    //进入碰撞
    void OnCollisionEnter(Collision info)
    {
        collisionGameObject = info.gameObject;
        if(collisionGameObject.tag.CompareTo("Player")==0&&
             player.GetComponent<PlayerController>().GetComponent<StatusControl>().isGround == false)
        {

            //撞破道具加分
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().Score++;
            Destroy(this.gameObject);

            //实例化金币
            GameObject.Instantiate(target,
                new Vector3(this.transform.position.x,
                             this.transform.position.y + 1.3f,
                             this.transform.position.z),
                             this.transform.rotation);
        }
    }

    //退出碰撞
    void OnCollisionExit(Collision info)
    {

    }
	// Update is called once per frame
	void Update () {


        PosY = this.transform.position.y;
        if (PosY > player.transform.position.y + 10)
        {
            Destroy(this.gameObject);
        }
	}
}
