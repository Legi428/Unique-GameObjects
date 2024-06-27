using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.UniqueGameObjects
{
    public static class GameObjectExtensions
    {
        /// <summary>
        ///     Retrieves the unique identifier for a GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject to get the unique ID for.</param>
        /// <returns>
        ///     The unique ID string if the GameObject has an InstanceGuid component;
        ///     otherwise, an empty string.
        /// </returns>
        /// <remarks>
        ///     This method requires the GameObject to have an InstanceGuid component
        ///     to return a non-empty string.
        /// </remarks>
        public static string GetUniqueID(this GameObject gameObject)
        {
            if (gameObject.Get<InstanceGuid>() is { } instanceGuid)
            {
                return instanceGuid.GuidIdString.String;
            }
            return string.Empty;
        }
    }
}
