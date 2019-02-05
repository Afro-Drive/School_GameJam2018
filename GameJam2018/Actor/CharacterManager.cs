using GameJam2018.Device;
using GameJam2018.Scene;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Actor
{
    /// <summary>
    /// キャラクター管理者
    /// 作成者:谷 永吾
    /// </summary>
    class CharacterManager
    {
        public List<Enemy> enemies;//エネミー管理用リスト
        private List<Item> items;　//アイテム管理用リスト
        //private Player player;　　 //プレイヤーオブジェクト
        private List<Player> players; //プレイヤー管理用リスト（オブジェクトだと仲介者の引数がよくわからない・・・）

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CharacterManager()
        {
            enemies = new List<Enemy>();
            //player = new Player();
            players = new List<Player>();
            items = new List<Item>();
        }

        ///// <summary>
        ///// 初期化メソッド
        ///// 敵の無限出現のために仕方なく作成）
        ///// </summary>
        //public void Initialize()
        //{
        //    SetEnemy(enemies[0], 3, 10);//敵の出現の設定を変える場合はココ
        //}

        /// <summary>
        /// エネミーの出現間隔の設定
        /// </summary>
        /// <param name="minScond">最速出現間隔[second]</param>
        /// <param name="maxSecond">最遅出現間隔</param>
        /// <returns></returns>
        public void SetEnemy(Enemy enemy, int minSecond, int maxSecond)
        {
        }

        /// <summary>
        /// 各種リストへの追加
        /// </summary>
        /// <param name="addCharacter"></param>
        public void Add(Character addCharacter)
        {
            if (addCharacter is Enemy)//もしエネミー型のオブジェクトなら
            {
                enemies.Add((Enemy)addCharacter);
            }
            else if (addCharacter is Item)//もしアイテム型のオブジェクトなら
            {
                items.Add((Item)addCharacter);
            }
            else//それ以外なら（プレイヤー型のオブジェクト）
            {
                players.Add((Player)addCharacter);
            }
        }

        /// <summary>
        /// 更新処理（所有するリストを片っ端から更新しまくる）
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }
            foreach (var item in items)
            {
                item.Update(gameTime);
            }
            foreach(var player in players)
            {
                player.Update(gameTime);
            }
            //player.Update(gameTime);

            OccurHit();//絶えず衝突判定を確かめ続ける
        }

        /// <summary>
        /// 当たったかの判定
        /// </summary>
        public void OccurHit()
        {
            //エネミーとプレイヤー
            foreach (var enemy in enemies)
            {
                //if (player.IsHit(enemy))
                foreach(var player in players)
                {
                    if (player.IsHit(enemy))
                    {
                        enemy.Hit(player);
                        player.Hit(enemy);
                    }
                }
            }
            //アイテムとプレイヤー
            foreach (var item in items)
            {
                //if (player.IsHit(item))
                foreach(var player in players)
                {
                    if (player.IsHit(item))
                    {
                        item.Hit(player);
                        player.Hit(item);
                    }
                }
            }
        }

        /// <summary>
        /// 描画処理（所有するリストを一斉に描画しまくる）
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(renderer);
            }
            foreach (var item in items)
            {
                item.Draw(renderer);
            }
            foreach(var player in players)
            {
                player.Draw(renderer);
            }
            //player.Draw(renderer);
        }
    }
}
