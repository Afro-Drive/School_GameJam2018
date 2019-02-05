using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameJam2018.Util
{
    /// <summary>
    /// ゴールまでに要した時間を計測するクラス
    /// </summary>
    class CountUpTimer:Timer
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public CountUpTimer()
            : base()
        {   
            //自分の初期化メソッドで初期化
            Initialize();
        }

        /// <summary>
        /// コンストラクタ（引数あり）
        /// </summary>
        /// <param name="second">初期化時の指定時間</param>
        public CountUpTimer(float second)
            : base(second)
        {
            Initialize();
        }

        /// <summary>
        /// 初期化継承処理
        /// </summary>
        public override void Initialize()
        {
            currentTime = 0;
        }

        /// <summary>
        /// 更新継承処理
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //現在の時間をプラス
            currentTime = currentTime + 1;
        }
    }
}



