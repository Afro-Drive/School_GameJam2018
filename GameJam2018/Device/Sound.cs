using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2018.Device
{
    /// <summary>
    /// サウンド、サウンドエフェクトの再生に関するクラス
    /// </summary>
    class Sound
    {
        #region 折りたたみ機能(visual studio独自の機能で、他の開発環境では機能しないこともある）
        //フィールド
        private ContentManager contentManager;
        private Dictionary<string, Song> bgms; //ロードしたBGMの管理リスト
        private Dictionary<string, SoundEffect> soundEffects; //SE管理リスト
        private Dictionary<string, SoundEffectInstance> seInstance;
        private Dictionary<string, SoundEffectInstance> sePlayDict;
        private string currentBGM;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content"></param>
        public Sound(ContentManager content)
        {
            contentManager = content;
            MediaPlayer.IsRepeating = true;

            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstance = new Dictionary<string, SoundEffectInstance>();
            sePlayDict = new Dictionary<string, SoundEffectInstance>();

            currentBGM = null;
        }

        public void Unload()
        {
            bgms.Clear();
            soundEffects.Clear();
            seInstance.Clear();
            sePlayDict.Clear();
        }
        #endregion フィールドとコンストラクタ

        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名（" + name + ")が足りません" + "アセット名を確認、Dictionaryに登録しているか確認してください。";
        }

        #region BGM(MP3:MediaPlayer)関連

        /// <summary>
        /// BGM（MP3）の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filepath">ファイルパス</param>
        /// <returns></returns>

        public void LoadBGM(string name, string filepath = "./Sound/")
        {
            if (bgms.ContainsKey(name))
            {
                return;
            }
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }

        /// <summary>
        /// BGMが停止中か？
        /// </summary>
        /// <returns>再生中ならtrue</returns>
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        /// <summary>
        /// BGMが再生中か？
        /// </summary>
        /// <returns>再生中ならtrue</returns>
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }
        /// <summary>
        /// BGMが一時停止中か？
        /// </summary>
        /// <returns>一時停止中ならtrue</returns>
        public bool IsPauseBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        public void PlayBGM(string name)
        {

            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));
            if (currentBGM == name)
            {
                return;
            }

            if (IsPlayingBGM())
            {
                StopBGM();
            }

            MediaPlayer.Volume = 1f;

            currentBGM = name;

            MediaPlayer.Play(bgms[currentBGM]);
        }


        /// <summary>
        /// BGMの一時停止
        /// </summary>
        public void PauseBGM()
        {
            if (IsPlayingBGM())
            {
                MediaPlayer.Pause();
            }
        }

        /// <summary>
        /// 一時停止からの再生
        /// </summary>
        public void ResumeBGM()
        {
            if (IsPauseBGM())
            {
                MediaPlayer.Resume();
            }
        }

        /// <summary>
        /// BGMループフラグを変更
        /// </summary>
        /// <param name="loopFlag"></param>
        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }
        #endregion　BGM(MP3:MediaPlayer)関連

        #region WAV(SE:SoundEffect)関連

        public void LoadSE(string name, string filepath = "./Sound/")
        {
            if (soundEffects.ContainsKey(name))
            {
                return;
            }
            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void PlaySE(string name)
        {
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));

            soundEffects[name].Play();
        }

        public void PlaySE(string name, float volume)
        {
            soundEffects[name].Play(volume, 0.0f, 0.0f);
        }
        #endregion WAV(SE:SoundEffect)関連

        #region WAVインスタンス関連

        public void CreateSEInstance(string name)
        {
            if (seInstance.ContainsKey(name))
            {
                return;
            }

            Debug.Assert(soundEffects.ContainsKey(name), "先に" + name + "の読み込み処理を行ってください。");

            seInstance.Add(name, soundEffects[name].CreateInstance());
        }

        public void PlaySEInstances(string name, int no, bool loopFlag = false)
        {
            Debug.Assert(seInstance.ContainsKey(name), ErrorMessage(name));

            if (sePlayDict.ContainsKey(name + no))
            {
                return;

            }
            var data = seInstance[name];
            data.IsLooped = loopFlag;
            data.Play();
            sePlayDict.Add(name + no, data);
        }

        public void StoppedSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if (sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Stop();
            }
        }

        public void StoppedSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Playing)
                {
                    se.Value.Stop();
                }
            }
        }

        public void RemoveSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            sePlayDict.Remove(name + no);
        }
        public void RemoveSE()
        {
            sePlayDict.Clear();
        }

        public void PauseSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if (sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Pause();

            }
        }

        public void PauseSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Playing)
                {
                    se.Value.Pause();
                }
            }
        }

        public void ResumeSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;

            }

            if (sePlayDict[name + no].State == SoundState.Paused)
            {
                sePlayDict[name + no].Resume();
            }
        }


        public void ResumeSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Paused)
                {
                    se.Value.Resume();
                }

            }
        }

        public bool IsPlayingSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Playing;
        }

        public bool IsStoppedSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Stopped;
        }

        public bool IsPauseSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Paused;
        }
        #endregion WAVインスタンス関連

    }
}
