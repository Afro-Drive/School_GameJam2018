using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Util
{
    class CountDownTimer : Timer
    {
        private float limitTime;

        public CountDownTimer()
            : base()
        {
            //自分の初期化メソッドで初期化
            Initialize();
        }

        public CountDownTimer(float second)
            : base(second)
        {
            limitTime = currentTime;
            Initialize();
        }

        public override void Initialize()
        {
            currentTime = limitTime;
        }

        public bool IsTime()
        {
            //0以下になっていたら設定した時間を超えたのでtrueを返す
            return currentTime <= 0.0f;
        }

        public float Rate()
        {
            return 1.0f - currentTime / limitTime;//制限時間における現在時間の割合を示す（残り時間が0になったらRateが1になるようにするために　最初に1.0fを設置）
        }

        public override void Update(GameTime gameTime)
        {
            //currentTime -= 1;
            currentTime = Math.Max(currentTime - 1f, 0.0f);//引数内で大きい方を返す
        }
    }
}

