using Player;
using UnityEngine;

namespace Scripts
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float lerpValue;
        private Transform target;

        private void Awake()
        {
            target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            Vector3 finalPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, finalPos, lerpValue);
        }
    }
}