using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Scene
{
    /// <summary>
    /// ゲーム仲介者
    /// 作成者:谷 永吾
    /// </summary>
    interface IGameMediator
    {
        void ChangeScroll(float autoSpeed, float pushSpeed); //スクロール速度の調整
        void InitScroll();
        void AddTime(); //時間の加算
        void AddTime(float second); //指定された時間の加算
    }
}
