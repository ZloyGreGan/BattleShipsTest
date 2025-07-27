namespace Game.Scripts.Entity.Components
{
    public interface IEntityComponent
    {
        void Initialize();
        void SetOwner(AEntity entity);
    }
}