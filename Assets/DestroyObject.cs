using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {


	//Unityちゃんのオブジェクト
	private GameObject unityChan;

	//消滅する距離
	private int destroyDistance = 15;

	// Use this for initialization
	void Start () {
		//Unityちゃんのオブジェクト取得
		this.unityChan = GameObject.Find("unitychan");
	}
	
	// Update is called once per frame
	void Update () {
		//アタッチされているオブジェクトが -z方向にdeleteDistance以上Unityちゃんから離れたら、削除
		if ((this.unityChan.transform.position.z - this.transform.position.z) > destroyDistance) {
			Destroy (this.gameObject);
		}
	}
}
