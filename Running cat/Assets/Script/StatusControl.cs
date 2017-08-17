using UnityEngine;
using System.Collections;

public class StatusControl : MonoBehaviour {

	public bool CanBreakBricks{ get; set; } //判断是否能破砖

    public bool isGround{get; set;}        //定义判断角色是否在地面变量
    public bool isJump{get;set;}           //判断能否跳起
    public bool isMove{get;set;}           //判断能否移动
    public bool isLeaveTube{get;set;}      //判断是否离开开始场景
    public bool isLastOnGround{get;set;}   //判断碰撞前的状态是否在地面

	// Use this for initialization
	void Start () {
		CanBreakBricks = false;
        isGround = true; 
        isJump = true;             
        isMove = true;        
        isLeaveTube = false;                   
        isLastOnGround = true;                 
	}
	
	// Update is called once per frame
	void Update () {

	}
}
