using GameJam2018.Device;
using GameJam2018.Scene;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Actor
{
    /// <summary>
    /// キャラクター抽象クラス
    /// 作成者:谷 永吾
    /// </summary>
    abstract class Character
    {
        //フィールド
        public Vector2 position;　　//描画位置
        protected string name;　　　//描画に用いるファイル名
        protected Rectangle hitArea;//当たり判定の範囲（矩形）
        protected bool isHitFlag;   //当たったか？
        protected Vector2 velocity; //移動量
        protected GameDevice gameDevice;//デバイス関連
        protected Sound soundDevice; //サウンドデバイス
        protected IGameMediator mediator;//ゲーム仲介者

        /// <summary>
        /// コンストラクタ（引数が分かりにくいのに注意）
        /// </summary>
        /// <param name="name">描画に使うアセット名</param>
        /// <param name="position">描画位置</param>
        /// <param name="length">当たり判定の一辺の長さ</param>
        /// <param name="mediator">ゲーム仲介者</param>
        public Character(string name, Vector2 position, int length, IGameMediator mediator)
        {
            this.name = name;
            this.position = position;
            //positionの座標を基準（左上の点）とした１辺がvalueに代入した整数値の矩形（四角）を形成
            //これが当たり判定となる
            hitArea = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(length));
            this.mediator = mediator;

            gameDevice = GameDevice.Instance();
            soundDevice = gameDevice.GetSound();
            isHitFlag = false;
        }

        /// <summary>
        /// （新規追加）衝突したか？
        /// </summary>
        /// <param name="other">ぶつかる相手</param>
        /// <returns>衝突したかどうか</returns>
        public bool IsHit(Character other)
        {
            return hitArea.Intersects(other.hitArea);//お互いの当たり判定領域が交わったら
        }

        /// <summary>
        /// 描画処理（仮想）
        /// </summary>
        /// <param name="renderer"></param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        //抽象メソッド群
        public abstract void Update(GameTime gameTime);//更新処理
        public abstract void Hit(Character other);//ヒット判定後の処理
    }
}
