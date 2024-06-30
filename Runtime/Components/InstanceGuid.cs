using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.UniqueGameObjects
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_FIRST)]
    public class InstanceGuid : MonoBehaviour
    {
        [SerializeField]
        UniqueID _guid = new();

        public IdString GuidIdString => _guid.Get;

        void Awake()
        {
            UniqueGameObjectsManager.RegisterInstanceGuid(this);
        }

        void OnDestroy()
        {
            UniqueGameObjectsManager.UnregisterInstanceGuid(this);
        }

        /// <summary>
        ///     Sets a new GUID for this InstanceGuid component.
        /// </summary>
        /// <param name="newIdString">The new IdString to set as the GUID.</param>
        /// <remarks>
        ///     This method unregisters the current GUID, sets the new one, and then
        ///     re-registers the InstanceGuid with the UniqueGameObjectsManager.
        ///     Use this method when you need to change the GUID of an existing
        ///     InstanceGuid component at runtime.
        /// </remarks>
        public void SetGuid(IdString newIdString)
        {
            UniqueGameObjectsManager.UnregisterInstanceGuid(this);
            _guid.Set = newIdString;
            UniqueGameObjectsManager.RegisterInstanceGuid(this);
        }
    }
}
