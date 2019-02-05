using Microsoft.Xna.Framework;
using GameJam2018.Device;
using Microsoft.Xna.Framework.Input;
using GameJam2018.Actor;
using GameJam2018.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Scene
{
    class ResultS : IScene
    {
        private bool isEndFlag;
        //IScene backGroundScene;
        private Sound sound;
        //private Timer timer;
        //private TimerUI timerUI;

        public ResultS(IScene scene)
        {
            isEndFlag = false;
            //backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Draw(Renderer renderer)
        {
            //シーンごとにrenderer．begin()～End()を書いているのに注意
            //背景と異なるゲームプレイシーン
            //backGroundScene.Draw(renderer);
            renderer.DrawTexture("rs_s", Vector2.Zero);

            //リザルト表示用　　　Ｓの画像を入れる
            renderer.DrawTexture("rank_s", new Vector2(540, 360));// 9/11追加
            //timerUI.Draw(renderer);
            renderer.DrawTexture("result1_touka", new Vector2(540, 100));
        }

        public void Initialize()
        {
            isEndFlag = false;

            //timerUI = new TimerUI(timer);
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("resultS");


            if (Input.IskeyDown(Keys.Enter))
            {
                isEndFlag = true;

            }
        }
    }
}
