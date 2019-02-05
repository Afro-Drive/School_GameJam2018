// このファイルで必要なライブラリのnamespaceを指定
using GameJam2018.Actor;
using GameJam2018.Def;
using GameJam2018.Device;
using GameJam2018.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace GameJam2018
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        //private SpriteBatch spriteBatch;//画像をスクリーン上に描画するためのオブジェクト
        //private Camera_2D camera;//背景描画クラス
        private SceneManager sceneManager;

        //private CharacterManager characterManager;//それぞれのキャラクターの処理を担当
        //private Player player;
        //private Enemy enemy;
        //private Item item;

        private GameDevice gameDevice;
        //画像をスクリーン上に描画するためのオブジェクト
        private Renderer renderer;//描画オブジェクト

        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            //ゲーム画面の規格を適用
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;

            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";

            //ゲームのタイトルを「GameJam2018」に変更→タイトルが決まり次第変更
            Window.Title = "GameJam2018";
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            gameDevice = GameDevice.Instance(Content, GraphicsDevice);
            //camera = new Camera_2D();
            //characterManager = new CharacterManager();

            sceneManager = new SceneManager();
            sceneManager.Add(Scene.Scene.Title, new Title());
            IScene addScene = new GamePlay();
            sceneManager.Add(Scene.Scene.GamePlay, addScene);
            sceneManager.Add(Scene.Scene.Tutorial, new Tutorial());
            sceneManager.Add(Scene.Scene.ResultD, (new ResultD(addScene)));
            sceneManager.Add(Scene.Scene.ResultC, (new ResultC(addScene)));
            sceneManager.Add(Scene.Scene.ResultB, (new ResultB(addScene)));
            sceneManager.Add(Scene.Scene.ResultA, (new ResultA(addScene)));
            sceneManager.Add(Scene.Scene.ResultS, (new ResultS(addScene)));
            sceneManager.Change(Scene.Scene.Title);
          
            //characterManager.Add(new Enemy());
            //characterManager.Add(new Player());

            //player = new Player();
            //enemy = new Enemy();

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            renderer = new Renderer(Content, GraphicsDevice);

            // この下にロジックを記述
            //GameJam用素材
            renderer.LoadContent("kusai");
            renderer.LoadContent("kusai mini");
            renderer.LoadContent("kusai mini2");//新規追加
            renderer.LoadContent("don't stop skunk");
            renderer.LoadContent("背景　snowground");
            renderer.LoadContent("christmas_santa_sori");
            renderer.LoadContent("christmas_dance_tonakai");
            renderer.LoadContent("christmas_dance_tonakai mini");
            renderer.LoadContent("otanjoubi_birthday_present_balloon");
            renderer.LoadContent("otanjoubi_birthday_present_balloon mini");
            renderer.LoadContent("haikei1");
            renderer.LoadContent("haikei2");
            renderer.LoadContent("haikei3");
            renderer.LoadContent("haikei4");
            renderer.LoadContent("haikei5");
            renderer.LoadContent("haikei6");
            renderer.LoadContent("haikei7");
            renderer.LoadContent("haikei8");
            renderer.LoadContent("goal1");
            renderer.LoadContent("rank_a");
            renderer.LoadContent("rank_b");
            renderer.LoadContent("rank_c");
            renderer.LoadContent("rank_d");
            renderer.LoadContent("rank_s");
            renderer.LoadContent("result1_touka");
            renderer.LoadContent("title_touka");
            renderer.LoadContent("setumei");
            renderer.LoadContent("rs_s");
            renderer.LoadContent("rs_ac");
            renderer.LoadContent("rs_d");
            renderer.LoadContent("count_number");
            renderer.LoadContent("背景　snowground GOAL2 ALL");
            renderer.LoadContent("press");
            //renderer.LoadContent("");
            //renderer.LoadContent("Long back2");



            #region//追いかけ用素材
            //renderer.LoadContent("white");
            //renderer.LoadContent("black");
            //renderer.LoadContent("ending");
            renderer.LoadContent("number");
            //renderer.LoadContent("score");
            //renderer.LoadContent("stage");
            renderer.LoadContent("timer");
            renderer.LoadContent("title");
            //renderer.LoadContent("white");
            //renderer.LoadContent("goodEnding");
            //renderer.LoadContent("pipo-btleffect");
            //renderer.LoadContent("oikake_enemy_4anime");
            //renderer.LoadContent("oikake_player_4anime");
            //renderer.LoadContent("puddle");
            //renderer.LoadContent("particle");
            //renderer.LoadContent("particleBlue");
            #endregion

            Sound sound = gameDevice.GetSound();
            string filepath = "./Sound/";//所定のデータへのフォルダの道筋（path)の記述に要注意！

            //GameJam素材
            //BGM(mp3拡張子)
            sound.LoadBGM("jam_playbgm", filepath);
            sound.LoadBGM("jam_titlebgm", filepath);
            //ここからフォルダ階層がさらに追加→追加分のpathを記述
            //pathの記述方法
            //　./（フォルダ名）/（フォルダ名）/（フォルダ名）/
            //これはContentフォルダをスタート地点とした「相対path」の記述方法
            //（.がスタート地点＝Contentを示す）
            sound.LoadBGM("resultD_DEDEDON", filepath);
            sound.LoadBGM("resultAC", filepath);
            sound.LoadBGM("resultS", filepath);

            //SE(wav拡張子)
            sound.LoadSE("jam_clickse", filepath);
            sound.LoadSE("jam_goalse", filepath);
            sound.LoadSE("jam_itemgetse", filepath);
            sound.LoadSE("jam_onarase", filepath);
            //sound.LoadSE("jam_walking_snow", filepath);
            sound.LoadSE("jam_shougai_set1", filepath);
            sound.LoadSE("jam_shougai_set2", filepath);
            sound.LoadSE("jam_walk", filepath);

            #region 追いかけ素材(Sound関連)
            ////BGM
            //sound.LoadBGM("titlebgm", filepath);
            //sound.LoadBGM("gameplaybgm", filepath);
            //sound.LoadBGM("endingbgm", filepath);
            ////SE
            //sound.LoadSE("titlese", filepath);
            //sound.LoadSE("gameplayse", filepath);
            //sound.LoadSE("endingse", filepath);
            #endregion

            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }

            // この下に更新ロジックを記述
            Input.Update();
            //Camera_2D.Update(gameTime, characterManager.enemies);//静的カメラクラスの更新処理を追加
            //gameDevice.GetSound().PlayBGM("jam_playbgm");
            sceneManager.Update(gameTime);

            //characterManager.Update(gameTime);
            //player.Update(gameTime);
            //enemy.Update(gameTime);


            #region 衝突判定（キャラクター管理者に委託予定）
            //if (player.HitArea().Intersects(enemy.HitArea()))
            //{
            //    player.Stop();
            //}
            #endregion

            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述
            renderer.Begin();

            //renderer.DrawTexture("背景　snowground", Vector2.Zero);camera_2Dに委託
            Camera_2D.Draw(renderer);//静的カメラクラスの描画処理を追加

            //characterManager.Draw(renderer);
            sceneManager.Draw(renderer);
            //player.Draw(renderer);
            //enemy.Draw(renderer);


            renderer.End();

            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
