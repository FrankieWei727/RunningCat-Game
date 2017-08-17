using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    float speed = 0.8f;                 //子弹发射速度
	// Use this for initialization
	void Start () {
	
        if(this.transform.position.x >0)
        {
            //子弹向左
        }
        if(this.transform.position.x<0 )
        {
            //子弹向右
            speed = -speed;
        }
	}
	
	// Update is called once per frame
	void Update () {

        //子弹移动
        this.transform.position = new Vector3(this.transform.position.x-Time.deltaTime *speed,
                                               this.transform.position.y,
                                               this.transform.position.z);

        //如果子弹超出屏幕范围则销毁
        if(this.transform.position.x>=4f || this.transform.position.x<=-4f)
        {
            Destroy(this.gameObject);
        }
	}
}
