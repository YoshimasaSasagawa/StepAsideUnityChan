using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	//コンポーネントを入れる
	private Animator myAnimator;

	//前進のためのコンポーネント追加
	private Rigidbody myRigidbody;
	//前進するための力
	private float forwardForce = 800.0f;
	//左右に移動するための力
	private float turnForce = 500.0f;
	//左右の移動できる範囲
	private float movableRange = 3.4f;
	//ジャンプのための力
	private float upForce = 500.0f;
	//動きを減速させる係数
	private float coefficient = 0.95f;

	//ゲーム終了の判定
	private bool isEnd = false;
	//終了時に表示するテキスト
	private GameObject stareText;

	//スコアを表示するテキスト
	private GameObject scoreText;
	//得点
	private int score = 0;

	//ボタンの判定
	private bool isLButtonDown = false;
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {

		//コンポーネントを取得
		this.myAnimator = GetComponent<Animator> ();

		//走るアニメーションを開始
		this.myAnimator.SetFloat ("Speed", 1);

		//RigidBodyコンポーネントの取得
		this.myRigidbody = GetComponent<Rigidbody>();

		//シーン中のstateTextを取得
		this.stareText = GameObject.Find("GameResultText");

		//ScoreTextの取得
		this.scoreText = GameObject.Find("ScoreText");

	}
	// Update is called once per frame
	void Update () {

		//ゲーム終了ならUnityちゃんの動きを減衰する
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		//unityちゃんに前方の力を加える
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		//Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.movableRange > this.transform.position.x) {
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		//ジャンプステートの時はJumpをfalseにセットする
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		//ジャンプしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}
	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other) {
		
		//障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			//GAME OVERを表示
			this.stareText.GetComponent<Text>().text = "GAME OVER";
		}

		//ゴールに到達した場合
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			//GAME CLEARを表示
			this.GetComponent<Text>().text = "GAME CLEAR!!";
		}

		//コインに衝突した場合
		if (other.gameObject.tag == "CoinTag") {

			//スコアを加算
			this.score += 10;

			//ScoreTextに獲得した点数を表示
			this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

			//パーティクル再生
			GetComponent<ParticleSystem>().Play();
			//接触したコインを破壊
			Destroy (other.gameObject);
		}
	}

	//ジャンプボタンが押された場合の処理
	public void GetMyJumpButtonDown(){
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown(){
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合
	public void GetMyLeftButtonUp(){
		this.isLButtonDown = false;
	}

	//右ボタン
	public void GetMyRightButtonDown(){
		this.isRButtonDown = true;
	}
	public void GetMyRightButtonUp(){
		this.isRButtonDown = false;
	}
}