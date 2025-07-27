using UnityEngine;

namespace Game.Scripts.Entity.Components
{
    public interface ITransformableComponent
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Forward { get; }
    }
}