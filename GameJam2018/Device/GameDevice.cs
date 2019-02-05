using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Device
{
    /// <summary>
    /// ゲームデバイスクラス
    /// 継承は不可。sealedで明示的にする
    /// </summary>
    sealed class GameDevice //sealed 生成ができないなら継承だ！を防ぐため継承をできなくしている
    {
        //フィールド
        private GameTime gameTime;

        //唯一のインスタンス（privateより、他のクラスで生成不可）
        private static GameDevice instance;

        /// <summary>
        /// フィールド
        /// </summary>
        private ContentManager content;
        private GraphicsDevice graphics;
        //private GameDevice instance;フィールドではなくインスタンスにまわす
        private Renderer renderer;
        private Random random;
        private Sound sound;

        /// <summary>
        /// コンストラクタ
        /// private宣言で外部からのnewでの実体生成はさせないっ
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphics"></param>
        private GameDevice(ContentManager content, GraphicsDevice graphics)
        {
            renderer = new Renderer(content, graphics);
            sound = new Sound(content);
            random = new Random();
            this.content = content;
            this.graphics = graphics;
        }

        /// <summary>
        /// GameDeviceインスタンスの取得
        /// （Game1クラスで使う実体生成用
        /// </summary>
        /// <param name="content">コンテンツ管理者</param>
        /// <param name="graphics">グラフィック機器</param>
        /// <returns>GameDeviceインスタンス</returns>
        public static GameDevice Instance(ContentManager content, GraphicsDevice graphics)
        {
            if (instance == null)
            {
                instance = new GameDevice(content, graphics);
            }
            return instance;
        }

        public static GameDevice Instance()
        {
            System.Diagnostics.Debug.Assert(instance != null,
                "Game1クラスのInitializeメソッド内の引数付きInstanceメソッドを読んでください。");

            return instance;
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            //デバイスで絶対に1回のみ更新が必要なもの
            Input.Update();
            this.gameTime = gameTime;
        }

        /// <summary>
        /// ゲーム時間の取得
        /// </summary>
        /// <returns></returns>
        public GameTime GetGameTime()
        {
            return gameTime;
        }

        public Renderer GetRenderer()
        {
            return renderer;
        }

        public ContentManager GetContentManager()
        {
            return content;
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return graphics;
        }

        public Random GetRandom()
        {
            return random;
        }

        public Sound GetSound()
        {
            return sound;
        }

    }
}
