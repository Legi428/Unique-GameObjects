using GameCreator.Runtime.Common;

namespace GameCreator.Editor.UniqueGameObject
{
    public class UniqueGameObjectsSettings : AssetRepository<UniqueGameObjectsRepository>
    {
        public override IIcon Icon => new IconID(ColorTheme.Type.TextLight);
        public override string Name => "Unique Game Objects";
    }
}
