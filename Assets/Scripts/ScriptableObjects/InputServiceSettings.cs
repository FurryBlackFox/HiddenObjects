using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Input Service Settings")]
    public class InputServiceSettings : ScriptableObject
    {
        [field: SerializeField] 
        public LayerMask SupportedLayers { get; private set; }

        [field: SerializeField] 
        public float RaycastDistance { get; private set; } = 100f;
    }
}