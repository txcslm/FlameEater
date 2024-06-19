using CodeBase.GameLogic.Interfaces;
using UnityEngine;

namespace CodeBase.GameLogic.CharacterLogic.Colliding
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
