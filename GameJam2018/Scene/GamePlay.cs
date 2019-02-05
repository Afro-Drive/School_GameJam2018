using Microsoft.Xna.Framework;
using GameJam2018.Device;
using Microsoft.Xna.Framework.Input;
using GameJam2018.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2018.Actor;

namespace GameJam2018.Scene
{
    class GamePlay : IScene, IGameMediator
    {
        private Timer timer;
        private TimerUI timerUI;
        private bool isEndFlag;
        private CharacterManager characterManager;
        private Sound sound;
        private Random random;

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        /// <summary>
        /// 仲介者を通して現在時間に加算
        /// </summary>
        public void AddTime()
        {
            timer.AddTime();
        }

        /// <summary>
        /// 仲介者を通して現在時間に指定秒数を加算
        /// </summary>
        /// <param name="second">指定秒数</param>
        public void AddTime(float second)
        {
            timer.AddTime(second);
        }

        /// <summary>
        /// 仲介者を通してスクロール速度を変更
        /// </summary>
        /// <param name="pushSpeed">操作時の追加スクロール速度</param>
        /// <param name="autoSpeed">自動スクロール速度</param>
        public void ChangeScroll(float autoSpeed, float pushSpeed)
        {
            Camera_2D.ChangeScroll(autoSpeed, pushSpeed);
        }

        public void Draw(Renderer renderer)
        {
            //背景を描画
            //renderer.DrawTexture("stage", Vector2.Zero);//9.11消去

            Camera_2D.Draw(renderer);//静的カメラクラスの描画処理を追加
            timerUI.Draw(renderer);
            //9.11追加
            characterManager.Draw(renderer);
        }

        public void Initialize()
        {
            isEndFlag = false;
            random = new Random();
            var player = new Player(this);

            characterManager = new CharacterManager();
            characterManager.Add(player);
            //エネミーの追加
            for(int i = 0; i < 5; i++)
            {
                var position = new Vector2(0, 0);
                position.X = random.Next((int)(player.position.X + 30f), 30500);
                position.Y = random.Next(450, 550);
              characterManager.Add(new Enemy(position, random.Next(1, 10), this));
            }
            //アイテムの追加
            for(int i = 0; i < 7; i++)
            {
                var position = new Vector2(0, 0);
                position.X = random.Next((int)(player.position.X + 30f), 30500);
                position.Y = random.Next(450, 550);
                characterManager.Add(new Item(position, random.Next(5, 13), this));
            }

            timer = new CountUpTimer(0);
            timerUI = new TimerUI(timer);

            Camera_2D.Initialize();
        }

        /// <summary>
        /// スクロール速度の初期化
        /// </summary>
        public void InitScroll()
        {
            Camera_2D.InitScroll();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            //タイムによってリザルトを変更
            if (timer.Now() >= 26)
            {
                return Scene.ResultD;
            }
            else if (timer.Now() >= 23)
            {
                return Scene.ResultC;
            }
            else if (timer.Now() >= 20)
            {
                return Scene.ResultB;
            }
            else if (timer.Now() >= 18)
            {
                return Scene.ResultA;
            }
            return Scene.ResultS;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            sound.PlayBGM("jam_playbgm");
            Camera_2D.Update(gameTime);//静的カメラクラスの更新処理を追加

            //キャラクターマネージャーを更新
            characterManager.Update(gameTime);//9.11追加
            if (Camera_2D.position.X < -26800)
            {
                isEndFlag = true;
                Next();
            }
            ////
            //if (Input.IskeyDown(Keys.Space))
            //{
            //    isEndFlag = true;
            //    sound.PlaySE("titlese");
            //}
        }

        //9.12消去
        //public void AddActor(Character character)
        //{
        //    characterManager.Add(character);
        //}
    }
}
