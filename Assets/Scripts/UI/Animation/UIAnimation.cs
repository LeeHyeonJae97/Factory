using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExtension
{
    public abstract class UIAnimation : MonoBehaviour
    {
        public abstract Tween Play();
        public abstract Tween PlayBackwards();

        public abstract void SaveStart();
        public abstract void LoadStart();
        public abstract void SaveEnd();
        public abstract void LoadEnd();

        #region Test

        [Button("Test", upperSpace: 5)]
        public void TestPlay()
        {
            Play();
        }

        [Button("Test Backwards")]
        public void TestPlayBackwards()
        {
            PlayBackwards();
        }

        #endregion
    }
}
