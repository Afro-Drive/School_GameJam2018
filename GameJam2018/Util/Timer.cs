using GameJam2018.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Util
{
    /// <summary>
    /// タイマー抽象クラス
    /// </summary>
    abstract class Timer
    {
        //子クラスでも利用できるようprotected
        protected float currentTime;//現在の時間

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second"></param>
        public Timer(float second)
        {
            currentTime = 60 * second;//60fps×秒
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Timer()
            : this(1)//1秒
        { }

        //抽象メソッド
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// 現在時間の取得
        /// </summary>
        /// <returns>秒</returns>
        public float Now()
        {
            return currentTime / 60f;//60fps想定なので60で割る
        }

        /// <summary>
        /// 現在時間にさらに時間を追加
        /// </summary>
        public void AddTime()
        {
            currentTime += 1 * 60f;
        }

        /// <summary>
        /// 現在時間に指定時間を追加
        /// </summary>
        /// <param name="second">指定時間[秒]</param>
        public void AddTime(float second)
        {
            currentTime += second * 60f;
        }

        internal void Draw(Renderer renderer)
        {

        }
    }
}
