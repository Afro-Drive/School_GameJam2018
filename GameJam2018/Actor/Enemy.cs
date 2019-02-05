using GameJam2018.Def;
using GameJam2018.Device;
using GameJam2018.Scene;
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
    /// 障害物クラス
    /// </summary>
    class Enemy : Character
    {
        #region フィールド→親クラスに委託
        //private Vector2 position;
        ////private string name;
        //private Random random = new Random();
        //private Rectangle hitArea;//当たり判定エリア
        //private Rectangle rectangle;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Enemy(Vector2 position, float speed, IGameMediator mediator)
            : base("christmas_dance_tonakai mini", position, 64, mediator)
        {
            velocity = new Vector2(0f, speed);//エネミースピード
            #region 抽象コンストラクタに委託
            //position = new Vector2(1000, 500);
            ////positionの座標を基準とする一辺64の矩形（四角形）
            //hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(64));
            #endregion
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
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

        /// <summary>
        /// 更新継承処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //縦方向の移動
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
            UpdateSideMove(Camera_2D.totalMove);

            //座標移動後に当たり判定をそこに合わせて生成（struct型よりそんなにメモリは食わないとのこと）
            hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(64));

            //position.X -= Camera_2D.speed;
            ////スクロールに合わせて移動
            //if(Input.IskeyDown(Keys.Right) || Input.IskeyDown(Keys.Space) || Input.IsButtonDown(Buttons.A))
            //{
            //    position.X -= Camera_2D.Scroll();
            //}
            ////座標移動後に当たり判定をそこに合わせて生成（struct型よりそんなにメモリは食わないとのこと）
            //hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(64));
        }

        /// <summary>
        /// 横方向の更新
        /// </summary>
        /// <param name="totalMove">X軸の自動移動速度</param>
        public void UpdateSideMove(float totalMove)
        {
            if (Input.IskeyDown(Keys.Space) || Input.IsButtonDown(Buttons.A) || Input.IskeyDown(Keys.Right))
            {
                totalMove += Camera_2D.pushScroll;
            }

            position.X -= totalMove;
        }

        ///// <summary>
        ///// 当たり判定を取得
        ///// </summary>
        ///// <returns>自身の当たり判定領域</returns>
        //public Rectangle HitArea()
        //{
        //    return hitArea;
        //}

        /// <summary>
        /// 衝突処理(敵の場合はSE再生のみ)
        /// </summary>
        public override void Hit(Character other)
        {
            gameDevice.GetSound().PlaySE("jam_shougai_set1", 0.2f);//再生音量の指定を追加
        }
    }
}
