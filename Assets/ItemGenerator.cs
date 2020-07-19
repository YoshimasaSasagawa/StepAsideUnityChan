using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
	//プレハブを入れる
	public GameObject carPrefab;
	public GameObject coinPrefab;
	public GameObject conePrefab;
	//スタート・ゴール地点
	private int startPos = -160;
	private int goalPos = 120;
	//アイテムを出すX方向の範囲
	private float posRange = 3.4f;

	//Unityちゃんのオブジェクト
	private GameObject unityChan;
	//アイテム生成位置とUnityちゃんのZ軸座標の差
	private int deltaZ = 50;

	// Use this for initialization
	void Start () {
		//Unityちゃんのオブジェクト取得
		this.unityChan = GameObject.Find("unitychan");
	}
	
	// Update is called once per frame
	void Update () {

		//UnityちゃんがZ軸座標 (startPos -50)~(goalPos -50)を１５メール移動するごとにZ軸座標startPosにアイテム生成
		if (startPos <= goalPos) {
			if (this.unityChan.transform.position.z >= (startPos - deltaZ)){
				
				//どのアイテムを出すのかをランダムに設定
				int num = Random.Range (1, 11);
				if (num <= 2) {
					//コーンをｘ軸方向に一直線に生成
					for (float j = -1; j <= 1; j += 0.4f) {
						GameObject cone = Instantiate (conePrefab) as GameObject;
						cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, startPos);
					}
				} else {
					//レーンごとにアイテムを生成
					for (int j = -1; j <= 1; j++) {
						//アイテムの種類を決める
						int item = Random.Range (1, 11);
						//アイテムを置くZ座標のオフセット値をランダムに設定
						int offsetZ = Random.Range (-5, 6);
						//６０％コイン配置：３０％車配置：１０％何もなし
						if (1 <= item && item <= 6) {
							GameObject coin = Instantiate (coinPrefab);
							coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, startPos + offsetZ);
						} else if (7 <= item && item <= 9) {
							GameObject car = Instantiate (carPrefab) as GameObject;
							car.transform.position = new Vector3 (posRange * j, car.transform.position.y, startPos + offsetZ);
						}
					}
				}

				startPos += 15;
			}
		}
	}
}
