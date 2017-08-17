using UnityEngine;
using System.Collections;

public class BackgroundTransform : MonoBehaviour {

	//背景循环滚动
	public float moveSpeed = 1f;

	public GameObject camera1;

	public float y;
	
	// Update is called once per frame
	void Update () {
	
		y = (float)camera1.transform.position.y + 20f;
		this.transform.Translate (Vector3.up * moveSpeed * Time.deltaTime);
		Vector3 position = this.transform.position;
		if (position.y >= y) {
			this.transform.position = new Vector3 (position.x, position.y - 18f * 2, position.z);
		}
	}
}
