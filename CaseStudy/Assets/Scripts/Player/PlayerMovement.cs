using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private float playerSpeed = 3.0f;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private Animator playerAnimator;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            playerVelocity.y = 0f;
            Vector2 input = new Vector2(floatingJoystick.Horizontal, floatingJoystick.Vertical);
            Vector3 move = new Vector3(input.x, 0f, input.y);

            controller.Move(playerSpeed * Time.deltaTime * move);

            if (move != Vector3.zero)
            {
                playerAnimator.SetBool("idle", false);
                gameObject.transform.forward = move;
            }
            else
            {
                playerAnimator.SetBool("idle", true);
            }

            controller.Move(playerVelocity * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}