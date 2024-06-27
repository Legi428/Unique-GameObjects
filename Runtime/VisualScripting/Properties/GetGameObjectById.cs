using GameCreator.Runtime.Common;
using System;
using UnityEngine;

namespace GameCreator.Runtime.UniqueGameObjects
{
    [Title("Unique Game Object by ID")]
    [Category("Game Objects/Unique Game Object by ID")]
    [Description("Reference to a Game Object by its unique ID")]
    [Image(typeof(IconID), ColorTheme.Type.TextNormal)]
    [Serializable]
    public class GetGameObjectByID : PropertyTypeGetGameObject
    {
        [SerializeField]
        PropertyGetString _id = new("my-game-object-id");

        public static PropertyGetGameObject Create => new(new GetGameObjectByID());

        public override string String => $"Game Object ID: {_id}";

        public override GameObject EditorValue => GetObject(Args.EMPTY);

        public override GameObject Get(Args args)
        {
            return GetObject(args);
        }

        GameObject GetObject(Args args)
        {
            var id = _id.Get(args);

            return UniqueGameObjectsManager.Instance.GetByID(id);
        }
    }
}
