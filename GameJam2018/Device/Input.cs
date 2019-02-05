using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Device
{
    /// <summary>
    /// キャラクター操作の静的クラス
    /// </summary>
    static class Input
    {
        //移動量
        private static Vector2 velocity = Vector2.Zero;

        //キーボード
        private static KeyboardState currentKey;//  現在のキーボードの状態
        private static KeyboardState previousKey;//1フレーム前のキーボードの状態
        //コントローラー
        private static GamePadState currentButton;
        private static GamePadState previousButton;

        //スピードアップ
        //private static float AddSP = 0.5f;

        public static void Update()
        {
            //キーボード
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            //コントローラー
            previousButton = currentButton;
            currentButton = GamePad.GetState(PlayerIndex.One);//1Pのコントローラーの状態


           // UpdateVelocity();
        }

        //キーボード関連
        //public static Vector2 Velocity()
        //{
        //    return velocity;
        //}

        //private static void UpdateVelocity()
        //{
        //    //毎ループ初期化
        //    velocity = Vector2.Zero;

        //    //前進(スピードアップ)
        //    if (currentKey.IsKeyDown(Keys.Space))
        //    {
        //        velocity.X += AddSP;
        //    }
        //}

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>現在キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IskeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        /// <summary>
        /// ボタンが押された瞬間か？
        /// （新しく用意しました。）
        /// </summary>
        /// <param name="button">チェックしたいボタン</param>
        /// <returns>現在フレームは押していて、1フレーム前は押されていないか</returns>
        public static bool IsButtonDown(Buttons button)
        {
            return currentButton.IsButtonDown(button) && !previousButton.IsButtonDown(button);
        }


        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>押された瞬間ならtrue</returns>
        //public static bool GetKeyTrigger(Keys key)
        //{
        //    return IskeyDown(key);
        //}

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていたらtrue</returns>
        //public static bool GetKeyState(Keys key)
        //{
        //    return currentKey.IsKeyDown(key);
        //}

    }
}
