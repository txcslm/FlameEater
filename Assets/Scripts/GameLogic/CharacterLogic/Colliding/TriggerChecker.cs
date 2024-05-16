using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Colliding
{
    [RequireComponent(typeof(SphereCollider))]
    public class TriggerChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable trigger))
                trigger.Interact();
        }
    }
}
