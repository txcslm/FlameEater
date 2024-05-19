using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.CharacterLogic.Colliding
{
    [RequireComponent(typeof(SphereCollider))][RequireComponent(typeof(Rigidbody))]
    public class TriggerChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable trigger))
                trigger.Interact();
        }
    }
}
