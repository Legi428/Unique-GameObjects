using GameCreator.Runtime.Common;
using System;

namespace GameCreator.Editor.UniqueGameObject
{
    [Serializable]
    public class UniqueGameObjectsRepository : TRepository<UniqueGameObjectsRepository>
    {
        public override string RepositoryID => "uniquegameobjects.general";
    }
}
