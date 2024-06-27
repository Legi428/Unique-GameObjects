using GameCreator.Runtime.Common;
using System;
using UnityEngine;

namespace GameCreator.Runtime.UniqueGameObjects
{
    [Title("Unique Game Object")]
    [Category("Game Objects/Unique Game Object")]
    [Description("Game Object that has an Instance Guid component")]
    [Image(typeof(IconCubeSolid), ColorTheme.Type.Blue)]
    [Serializable]
    public class GetUniqueGameObject : PropertyTypeGetGameObject
    {
        [SerializeField]
        InstanceGuid _uniqueGameObject;

        public static PropertyGetGameObject Create => new(new GetUniqueGameObject());

        public override string String => _uniqueGameObject == null ? "(none)" : _uniqueGameObject.gameObject.name;

        public override GameObject EditorValue => _uniqueGameObject == null ? null : _uniqueGameObject.gameObject;

        public override GameObject Get(Args args)
        {
            return _uniqueGameObject == null ? null : _uniqueGameObject.gameObject;
        }

        public override GameObject Get(GameObject gameObject)
        {
            return _uniqueGameObject == null ? null : _uniqueGameObject.gameObject;
        }

        public override T Get<T>(Args args)
        {
            if (typeof(T) == typeof(InstanceGuid)) return _uniqueGameObject as T;
            return base.Get<T>(args);
        }
    }
}
