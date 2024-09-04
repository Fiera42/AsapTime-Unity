using System;
using UnityEngine;

namespace AsapTime_Namespace
{
    /// <summary>
    /// # SUMMARY
    /// AsapTime is an update method just like Update() and FixedUpdate(),
    /// it is called after every Update() or FixedUpdate().
    /// This class allow you to update your code as often as possible 
    /// whithout compromising anything thanks to it's own DeltaTime
    /// 
    /// # HOW TO ?
    /// Suscribe to the OnAsapUpdate event (AsapTime.OnAsapUpdate += methodName; // See https://discussions.unity.com/t/how-to-use-an-action/588825/8 for more)
    /// -> Dont forget to un-suscribe too (AsapTime.OnAsapUpdate -= methodName; )
    /// </summary>
    public static class AsapTime
    {
        public static event Action OnAsapUpdate;
        public static float Time { get; private set; } = -UnityEngine.Time.fixedDeltaTime;
        public static float DeltaTime { get; private set; } = UnityEngine.Time.fixedDeltaTime;
        //public static bool IsInFixedUpdate { get; private set; } = false;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            PlayerLoopInterface.InsertSystemAfter(typeof(AsapTime), UpdateAsap, typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate));
            //PlayerLoopInterface.InsertSystemAfter(typeof(AsapUpdateSystem), NotInFixedUpdate_Method, typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate));
            PlayerLoopInterface.InsertSystemAfter(typeof(AsapTime), UpdateAsap, typeof(UnityEngine.PlayerLoop.FixedUpdate.ScriptRunBehaviourFixedUpdate));
            //PlayerLoopInterface.InsertSystemAfter(typeof(AsapUpdateSystem), InFixedUpdate_Method, typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate));
        }

        /*
        private static void InFixedUpdate_Method()
        {
            IsInFixedUpdate = true;
        }
        private static void NotInFixedUpdate_Method()
        {
            IsInFixedUpdate = false;
        } */

        private static void UpdateAsap()
        {
            if (UnityEngine.Time.time <= Time) return;

            DeltaTime = UnityEngine.Time.time - Time;
            Time = UnityEngine.Time.time;

            OnAsapUpdate();
        }
    }
}