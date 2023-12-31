using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AtomicOrientedDesign.Shooter
{
    internal sealed class AnimatorDispatcher : MonoBehaviour
    {
        internal event Action<string> OnMessageReceived;

        [UsedImplicitly]
        // Called from animator: don't remove!
        public void ReceiveString(string message)
        {
            OnMessageReceived?.Invoke(message);
        }
    }
}