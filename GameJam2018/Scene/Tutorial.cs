using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameJam2018.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Scene
{
    class Tutorial : IScene
    {
        //終了フラグ
        private bool isEndFlag;
        private Sound sound;
        private Renderer renderer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Tutorial()
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
            renderer.DrawTexture("setumei", Vector2.Zero);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;
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
            return Scene.GamePlay;
        }


        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            sound.PlayBGM("jam_titlebgm");

            //スペースキーが押されたか？
            if (Input.IskeyDown(Keys.Enter))
            {
                isEndFlag = true;
            }
        }
    }
}
