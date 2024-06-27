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
    }
}
