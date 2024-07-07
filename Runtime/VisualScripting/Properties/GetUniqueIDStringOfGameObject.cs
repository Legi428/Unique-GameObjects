using GameCreator.Runtime.Common;
using System;
using UnityEngine;

namespace GameCreator.Runtime.UniqueGameObjects
{
    [Title("Unique Game Object ID")]
    [Category("Game Objects/Unique Game Object ID")]
    [Image(typeof(IconID), ColorTheme.Type.TextNormal)]
    [Description("Returns the ID of the Unique Game Object reference")]
    [Serializable]
    public class GetStringUniqueIDGameObject : PropertyTypeGetString
    {
        [SerializeField]
        protected PropertyGetGameObject _uniqueGameObject = GetUniqueGameObject.Create;

        public static PropertyGetString Create => new(new GetStringUniqueIDGameObject());

        public override string String => $"{_uniqueGameObject} by ID";

        public override string Get(Args args)
        {
            return _uniqueGameObject.Get(args).GetUniqueID();
        }
    }
}
