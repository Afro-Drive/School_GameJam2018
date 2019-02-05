using GameJam2018.Def;
using GameJam2018.Actor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Device
{

    /// <summary>
    /// スクロール処理用クラス(静的クラスにすべきか？）
    /// 画像がスクロールしないので後ほど修正
    /// 制作者　谷 永吾
    /// </summary>
    static class Camera_2D
    {
        //スクロール用背景画リスト
        //private static List<string> backGrounds
        //    = new List<string> {
        //        "背景　snowground",
        //        "背景　snowground",
        //        "背景　snowground"};
        //private static string backGround1 = "背景　snowground";
        //private static string backGround2 = "背景　snowground";
        //private static string backGround3 = "背景　snowground";
        //private static int count = 0;//押下回数
        //フレームアウトした画像用リスト
        //private static List<string> insertList = new List<string>();
        //private static float totalScroll = 0; //累計スクロール量

        private static string backGround = "背景　snowground";
        public static float autoScroll;   //自動スクロール量
        public static float pushScroll;  //ボタン入力時の移動速度(スクロール速度を変えるならココとautoMove(Update処理内))
        public static float totalMove;         //合計移動量
        public static Vector2 position;　　　  //背景描画座標

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static Camera_2D()
        {   }

        /// <summary>
        /// 背景を4枚連番にならべて描画
        /// （更新処理でスクロール変数を使って進める
        /// </summary>
        /// <param name="renderer"></param>
        public static void Draw(Renderer renderer)
        {
            #region 描画の処理方法を変更したため削除
            //renderer.DrawTexture(backGrounds[0], position);
            //for(int i = 0; i < 3; i++)
            //{
            //    renderer.DrawTexture(backGrounds[i], new Vector2(position.X - 1280 * i, position.Y));
            //}
            //renderer.DrawTexture(backGround1, position1);
            //renderer.DrawTexture(backGround2, position2);
            //renderer.DrawTexture(backGround3, position3);
            #endregion
            for (int i = 0; i < 20; i++)//一気に２０枚背景画像を描画
            {
                Vector2 pos = new Vector2(position.X + i * 1280, -200);
               renderer.DrawTexture(backGround, pos);
            }
            renderer.DrawTexture("背景　snowground GOAL2 ALL", new Vector2(position.X + 20 * 1280, -200));
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime">ゲーム時間（60fps）</param>
        public static void Update(GameTime gameTime)
        {
            #region ヒット判定でのスピード処理はキャラクター管理者に委託
            //自動スクロールスピードを設定
            //if (player.IsHit(enemy))//衝突判定が有効なら
            //{
            //    autoMove = 1f;//遅くする
            //}
            //else
            //{
            //autoMove = 20f;//普段の速度
            //}
            #endregion
            //背景をスクロールさせてプレイヤーを進んでいるように見せる
            //キーボード・ゲームパッド処理
            if (Input.IskeyDown(Keys.Space) || Input.IsButtonDown(Buttons.A) || Input.IskeyDown(Keys.Right))
            {
                //ボタン入力時にスクロール量を加算
                  /*autoScroll = pushScroll;*/
                autoScroll += pushScroll;                
            }
            if(totalMove >= 70f)
            {
                totalMove = 70f;
            }
            autoScroll *= 0.95f;
            totalMove = autoScroll;//毎フレーム減速処理(摩擦表現）

            //foreach (var enemy in enemies)//更新処理がGamePlayと二回呼び出し→更新量が２倍に・・・
            //{
            //    enemy.Update(gameTime);
            //    enemy.UpdateSideMove(totalMove);
            //}

            position.X -= totalMove;//X座標に加算し移動してるように見せる

            #region 画像のフレームアウト時のループ処理（削除）
            //position2.X -= autoMove;
            //position3.X -= autoMove;

            //if (position1.X <= -1920)
            //{
            //    position1.X = -400 + 1920 * 0;
            //    #region フレームアウトした要素の移動（ここの処理がうまくいかず、画像がループしない・・・）
            //    //insertList.Add(backGrounds[0]);//暫定的に専用リストに追加
            //    //backGrounds.Remove(backGrounds[0]);　　　 //元のリストから削除
            //    //backGrounds.Add(insertList[0]);//末尾に暫定リストから追加
            //    //totalScroll = 0;//初期値に戻す
            //    #endregion
            //}
            //if (position2.X <= -1920)
            //{
            //    position2.X = -400 + 1920 * 2;
            //}
            //if (position3.X <= -1920)
            //{
            //    position3.X = -400 + 1920 * 3;
            //}

            //ifスピードアップのアイテムを取得または使用した
            //scroll = 30f;スピードアップ
            #endregion
        }

        #region 不要なメソッド
        ///// <summary>
        ///// 累計スクロール量を返却
        ///// </summary>
        ///// <returns></returns>
        //public static float Scroll()
        //{
        //    return pushScroll;
        //}
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            //背景初期化
            position = new Vector2(-400 + 1920 * 0, -200);
            //スクロール速度の初期化
            InitScroll();
        }

        /// <summary>
        /// スクロール速度の初期化
        /// </summary>
        public static void InitScroll()
        {
            autoScroll = 20f; //ここで自動スクロール速度を設定（この後Playerクラスに操作してもらう）
            totalMove = 0;
            pushScroll = 10f;
        }

        /// <summary>
        /// スクロール速度の変更
        /// </summary>
        /// <param name="autoSpeed">自動スクロール速度</param>
        /// <param name="pushSpeed">操作スクロール速度</param>
        public static void ChangeScroll(float autoSpeed, float pushSpeed)
        {
            autoScroll = autoSpeed;
            pushScroll = pushSpeed;
        }
    }
}
