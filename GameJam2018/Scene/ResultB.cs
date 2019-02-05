using Microsoft.Xna.Framework;
using GameJam2018.Device;
using Microsoft.Xna.Framework.Input;
using GameJam2018.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Scene
{
    class ResultB : IScene
    {
        private bool isEndFlag;
        //IScene backGroundScene;
        private Sound sound;
        public ResultB(IScene scene)
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

            renderer.DrawTexture("rs_ac", Vector2.Zero);
            renderer.DrawTexture("result1_touka", new Vector2(540, 100));
            //リザルト表示用　　　Bの画像を入れる
            renderer.DrawTexture("rank_b", new Vector2(540, 360));// 9/11追加
        }

        public void Initialize()
        {
            isEndFlag = false;
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
            sound.PlayBGM("resultAC");

            if (Input.IskeyDown(Keys.Enter))
            {
                isEndFlag = true;
            }
        }
    }
}
