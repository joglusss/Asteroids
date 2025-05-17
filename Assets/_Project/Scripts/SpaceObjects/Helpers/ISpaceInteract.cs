namespace Asteroids.Objects
{
    public interface ISpaceInteract
    {
        SpaceObjectType SpaceObjectType { get; }

        void Interact(SpaceObjectType collisionSpaceObjectType);
    }
}