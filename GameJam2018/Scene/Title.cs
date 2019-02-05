using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameJam2018.Def;
using GameJam2018.Device;
using GameJam2018.Util;
using GameJam2018.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Scene
{
    class Title : IScene
    {
        //終了フラグ
        private bool isEndFlag;
        private Sound sound;
        private Renderer renderer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Title()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            renderer = gameDevice.GetRenderer();
        }


        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            //renderer.Begin();
            renderer.DrawTexture("rs_ac", Vector2.Zero);
            renderer.DrawTexture("title_touka", new Vector2(120, 40));
            renderer.DrawTexture("press", new Vector2(640, 500));
            // renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;
            //GameDevice.Instance().GetSound().PlayBGM("jam_titlebgm");
        }

        /// <summary>
        /// 終了か？
        /// </summary>
        /// <returns>シーンが終わっていたらtrue</returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーンへ
        /// </summary>
        /// <returns>次のシーン</returns>
        public Scene Next()
        {
            return Scene.Tutorial;
        }


        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
       
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {

            //スペースキーが押されたか？
            if (Input.IskeyDown(Keys.Enter))
            {
                isEndFlag = true;
            }
        }
    }
}
