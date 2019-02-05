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
    /// プレイヤーが視認できるCountUpTimerのUI
    /// </summary>
    class TimerUI
    {
        private Timer timer;

        public TimerUI(Timer timer)
        {
            this.timer = timer;
        }

        public void Draw(Renderer renderer)
        {                       //画像               //座標
            renderer.DrawTexture("timer", new Vector2(400, 10));
            renderer.DrawNumber("number", new Vector2(600, 13), timer.Now());
        }
    }
}
