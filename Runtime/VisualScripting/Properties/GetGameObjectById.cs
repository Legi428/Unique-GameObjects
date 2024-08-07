using GameCreator.Runtime.Common;
using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

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

        public override string String => $"Game Object with ID: {_id}";

        public override GameObject EditorValue
        {
            get
            {
                var id = _id.Get(Args.EMPTY);
                var list = Object.FindObjectsByType<InstanceGuid>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                return list.FirstOrDefault(x => x.GuidIdString.String == id)?.gameObject;
            }
        }

        public override GameObject Get(Args args)
        {
            return GetObject(args);
        }

        GameObject GetObject(Args args)
        {
            var id = _id.Get(args);

            return UniqueGameObjectsManager.GetByID(id);
        }
    }
}
