using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2018.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameJam2018.Def;
using GameJam2018.Scene;

namespace GameJam2018.Actor
{
    class Item : Character
    {
        #region 不要なフィールド
        //private bool isGetFlag;
        //public float speed;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">初期描画位置</param>
        /// <param name="speed">上下移動速度</param>
        public Item(Vector2 position, float speed, IGameMediator mediator)
             : base("otanjoubi_birthday_present_balloon mini", position, 64, mediator)
        {
            velocity = new Vector2(0f, speed);
        }

        /// <summary>
        /// 衝突判定後の処理
        /// </summary>
        /// <param name="other">衝突したキャラクターオブジェクト</param>
        public override void Hit(Character other)
        {
             gameDevice.GetSound().PlaySE("jam_itemgetse", 0.2f);//再生音量の指定を追加
        }

        /// <summary>
        /// 更新継承処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float autoMove = Camera_2D.totalMove;
            position.X -= autoMove;
            if (Input.IskeyDown(Keys.Space) || Input.IsButtonDown(Buttons.A) || Input.IskeyDown(Keys.Right))
            {
                autoMove += Camera_2D.pushScroll;
            }

            //上で反射
            if (position.Y < 300)
            {
                //移動量を反転
                velocity = -velocity;
            }
            //下反射
            else if (position.Y > Screen.Height - 70)
            {
                velocity = -velocity;
            }
            //移動処理
            position += velocity;

            //座標移動後に当たり判定をそこに合わせて生成（struct型よりそんなにメモリは食わないとのこと）
            hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(64));
            #region 処理に使う変数を変更に伴い削除
            ////スクロールに合わせて移動
            //if (Input.IskeyDown(Keys.Right) || Input.IskeyDown(Keys.Space) || Input.IsButtonDown(Buttons.A))
            //{
            //    position.X -= Camera_2D.Scroll();
            //}


            //if (position.Y < 300)
            //{
            //    velocity = -velocity;
            //}
            //else if (position.Y < Screen.Height - 70)
            //{
            //    velocity = -velocity;
            //}
            //position += velocity;


            //position.X -= Camera_2D.Scroll();
            #endregion
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            if(!isHitFlag)
            renderer.DrawTexture(name, position, new Rectangle(0, 0, 150, 150));
            #region 継承によりコメントアウト
            //renderer.DrawTexture("black", position);
            ////追加で出現がうまくいかない・・・
            //if (Camera_2D.Scroll() == 500)//画面スクロール量が500を超えたら
            //{
            //    int randPos = random.Next(500, Screen.Width);
            //    position = new Vector2(randPos, 500);//描画座標を決定

            //    renderer.DrawTexture("black", position);//エネミーを描画
            //}
            #endregion
        }
    }
}
