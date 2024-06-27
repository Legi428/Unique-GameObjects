using GameCreator.Runtime.Common;
using System.Collections.Generic;
using UnityEngine;

namespace GameCreator.Runtime.UniqueGameObjects
{
    [AddComponentMenu("")]
    public class UniqueGameObjectsManager : Singleton<UniqueGameObjectsManager>
    {
        readonly static Dictionary<int, GameObject> ReferencedInstanceGuids = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnSubsystemsInit()
        {
            Instance.WakeUp();
        }

        /// <summary>
        ///     Retrieves a GameObject by its IdString identifier.
        /// </summary>
        /// <param name="idString">The IdString identifier of the GameObject.</param>
        /// <returns>The associated GameObject, or null if not found.</returns>
        /// <remarks>
        ///     The GameObject must have an InstanceGuid component to be retrievable.
        /// </remarks>
        public GameObject GetByID(IdString idString)
        {
            return ReferencedInstanceGuids.GetValueOrDefault(idString.Hash);
        }

        /// <summary>
        ///     Retrieves a GameObject by its string identifier.
        /// </summary>
        /// <param name="idString">The string identifier of the GameObject.</param>
        /// <returns>The associated GameObject, or null if not found.</returns>
        /// <remarks>
        ///     The GameObject must have an InstanceGuid component to be retrievable.
        /// </remarks>
        public GameObject GetByID(string idString)
        {
            return ReferencedInstanceGuids.GetValueOrDefault(idString.GetHashCode());
        }

        /// <summary>
        ///     Retrieves a GameObject by its string identifier hash.
        /// </summary>
        /// <param name="hash">The string identifier of the GameObject.</param>
        /// <returns>The associated GameObject, or null if not found.</returns>
        /// <remarks>
        ///     The GameObject must have an InstanceGuid component to be retrievable.
        /// </remarks>
        public GameObject GetByID(int hash)
        {
            return ReferencedInstanceGuids.GetValueOrDefault(hash);
        }

        /// <summary>
        ///     Registers an InstanceGuid and its associated GameObject.
        /// </summary>
        /// <param name="instanceGuid">The InstanceGuid to register.</param>
        /// <remarks>
        ///     This method should be called for GameObjects with an InstanceGuid component
        ///     to make them retrievable via GetByID methods.
        /// </remarks>
        public static void RegisterInstanceGuid(InstanceGuid instanceGuid)
        {
            if (instanceGuid.GuidIdString.String == string.Empty)
            {
                Debug.LogError("Trying to register a GameObject with an empty Guid."
                               + "This is not allowed since an empty Guid is defined as a GameObject without an InstanceGuid component on it.");
                return;
            }
            ReferencedInstanceGuids.TryAdd(instanceGuid.GuidIdString.Hash, instanceGuid.gameObject);
        }

        /// <summary>
        ///     Unregisters an InstanceGuid.
        /// </summary>
        /// <param name="instanceGuid">The InstanceGuid to unregister.</param>
        /// <remarks>
        ///     Call this method when a GameObject with an InstanceGuid component
        ///     should no longer be retrievable via GetByID methods.
        /// </remarks>
        public static void UnregisterInstanceGuid(InstanceGuid instanceGuid)
        {
            if (ApplicationManager.IsExiting) return;
            ReferencedInstanceGuids.Remove(instanceGuid.GuidIdString.Hash);
        }
    }
}
