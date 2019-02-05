using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameJam2018.Device;


namespace GameJam2018.Scene
{
    /// <summary>
    /// シーンインターフェース
    /// </summary>
    interface IScene
    {
        void Initialize();//初期化
        void Update(GameTime gameTime);//更新
        void Draw(Renderer renderer);//描画
        void Shutdown();//終了

        //シーン管理用
        bool IsEnd();//終了チェックへ
        Scene Next();//次のシーンへ
    }
}
