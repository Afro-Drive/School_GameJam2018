using GameJam2018.Device;
using GameJam2018.Scene;
using GameJam2018.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Actor
{
    /// <summary>
    /// ステージを走るプレイヤークラス
    /// </summary>
    class Player : Character
    {
        #region (変更)親クラスに委託
        //private Vector2 position;//位置
        //private string name;//使用画像:string
        //private Renderer renderer;//描画オブジェクト
        //音管理用オブジェクト
        //スタミナオブジェクト
        //private bool isStopFlag;//スタミナ切れか？
        //private bool isHitFlag;//ほかのオブジェクトに接触したか？
        //private Rectangle hitArea;//当たり判定用のエリア
        //妨害オブジェクト
        //private Enemy enemy;
        //プレイヤーの状態（列挙型）　normal, dash, stop
        #endregion
        //フィールド
        private CountDownTimer hitTimer;//敵衝突効果の持続時間
        private CountDownTimer speedUpTimer;//アイテム取得効果の持続時間
        private float jumpPower;
        private bool isGetFlag;//アイテムゲット用フラグ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">描画位置</param>
        /// <param name="rectangle">当たり判定の大きさ（矩形）</param>
        public Player(IGameMediator mediator)
            : base("kusai mini2", new Vector2(150, 500), 64, mediator)
        {
            #region 抽象クラスからの継承により省略
            //position = new Vector2(150, 500);
            //positionの座標を基準とする一辺64の矩形（四角形）の当たり判定
            //hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(64));
            #endregion

            #region Camera_2Dクラスのスクロール処理に移動
            //フラグをfalseにするれないため一旦コメントアウト)
            //if (Input.GetKeyState(Keys.Right) || Input.GetKeyState(Keys.Space)) Inputクラスを活用すると反映されないから断念涙
            //{
            //    position.X += 1f;
            //}
            #endregion
            #region 操作処理→Camera_2Dクラスに移動
            //キーボード
            //if(Input.IskeyDown(Keys.Space) || Input.IskeyDown(Keys.Right))
            //{
            //    position.X += 12.0f;//スピードを調節予定
            //}

            ////ゲームパッド(新しく用意したよ）
            //if (Input.IsButtonDown(Buttons.A))
            //{
            //    position.X += 12.0f;
            //}
            #endregion

            #region 敵と自分の当たり判定が交わったら→Game１クラスに委託
            //if (hitArea.Intersects(enemy.HitArea()))
            //{
            //    Hit();//ヒットメソッドを発動
            //}
            #endregion

            //item = new Item();
            //enemy = new Enemy();
            hitTimer = new CountDownTimer(2.5f);
            hitTimer.Initialize();
            speedUpTimer = new CountDownTimer(1.5f);
            speedUpTimer.Initialize();
            isGetFlag = false;
            jumpPower = 15f;
        }

        /// <summary>
        /// 描画継承処理
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture("kusai mini2", position, new Rectangle(650, 0, 150, 100));
        }

        /// <summary>
        /// 更新継承処理
        /// (当たり判定を生成し続ける）
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(64));
            //ボタンが押されるたびに小さく上下する
            if (Input.IskeyDown(Keys.Space) || Input.IskeyDown(Keys.Right) || Input.IsButtonDown(Buttons.A))
            {
                //もし衝突フラグが立っていたら操作不能(操作が止まってくれない)
                //if (isHitFlag) position.X += 0;
                if (position.Y < 500)
                {
                    position.Y += jumpPower;
                }
                else if (position.Y >= 500)
                {
                    position.Y -= jumpPower;
                }

                //移動中（ボタン押下中）は移動SEを再生させる
                soundDevice.PlaySE("jam_walk");
            }

            if (isHitFlag)//敵と衝突したら
            {
                //hitTimer.Initialize();//タイマーをセット
                hitTimer.Update(gameTime);//フラグが立った瞬間にタイマーを起動
                if (hitTimer.IsTime())//時間切れになったら
                {
                    //フラグを戻し、挙動を元通りに
                    isHitFlag = false;//
                    hitTimer.Initialize();//タイマーを元通りに
                    soundDevice.PauseSE();//SEとまらねぇ・・・
                    jumpPower = 15f;
                    //移動速度を元に戻す
                    mediator.InitScroll();//ゲーム仲介者に委託
                                          //↓のように定数をその都度示す手間をなくす
                                          //Camera_2D.pushScroll = 30f;//ボタンを押すことで進む量も元に戻す
                                          //Camera_2D.totalMove = 15f;
                }
            }


            if (isGetFlag)//アイテムをゲットした場合
            {
                //speedUpTimer.Initialize();
                speedUpTimer.Update(gameTime);
                if (speedUpTimer.IsTime())//時間切れになったら
                {
                    isGetFlag = false;
                    speedUpTimer.Initialize();//タイマーをもとに戻す
                    mediator.InitScroll();//速度を元通りに
                }
            }

        }

        /// <summary>
        /// 衝突継承処理
        /// （当たったと判定された場合の処理）
        /// キャラクター管理者が実行させる
        /// </summary>
        /// <param name="enemy">敵オブジェクト</param>
        public override void Hit(Character other)
        {
            if(other is Enemy)//エネミーに衝突
            {
                isHitFlag = true;
                jumpPower = 80f;//ノックアップ処理
                //移動速度を遅く見せる
                mediator.ChangeScroll(5f, 3f);//速度変更は仲介者に委託
                  //Camera_2D.pushScroll = 1f;
                  //Camera_2D.totalMove = 1f;
                soundDevice.PlaySE("jam_shougai_set2", 0.2f);//再生音量の指定を追加
            }
            else if(other is Item)//アイテムを取得
            {
                isGetFlag = true;
                mediator.ChangeScroll(30f, 15f);//仲介者に委託
                  //Camera_2D.pushScroll = 60f;
                soundDevice.PlaySE("jam_onarase", 0.2f);
            }
            #region 削除
            //for(int i = 0; i < 4; i++)
            //{
            //    if(position.Y > 500)
            //    {
            //        position.Y -= 80;
            //    }
            //    else if(position.Y <= 500)
            //    {
            //       position.Y += 80;
            //    }
            //}
            //return IsHit(other);
            #endregion
        }

        #region 必要のないメソッド
        ///// <summary>
        ///// 当たり判定を取得
        ///// 
        ///// </summary>
        //public Rectangle HitArea(/*妨害オブジェクト*/)
        //{
        //    return hitArea;
        //}
        #endregion

        public void ChangeMotion()
        {
            //if(isStopFlag)列挙型をStopにして動きを止める

            //if(Input.IsKeyDown(Keys.Space)) 列挙型をDashにして移動量をアップ
        }
    }
}
