using Scripts;
using UnityEngine;
using UnityEngine.AI;
using Zone;

namespace Helper
{
    [RequireComponent(typeof(CharacterController))]
    public class HelperMovement : MonoBehaviour
    {
        private Animator animator;
        private NavMeshAgent agent;

        private HelperStack helperStack;

        private CubeDetectHelper[] targets;
        private bool isWaiting = false;

        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            helperStack = GetComponent<HelperStack>();
            targets = FindObjectsOfType<CubeDetectHelper>();
        }

        private void Start()
        {
            StorageArea.OnStorageFilled += Helper_OnStorageFilled;
            StorageArea.OnStorageEmpty += Helper_OnStorageEmpty;

            CheckTargets();
            FindCube();
        }

        private void Helper_OnStorageEmpty(object sender, System.EventArgs e)
        {
            isWaiting = false;
            agent.isStopped = false;
            animator.SetBool("idle", false);
        }

        private void Helper_OnStorageFilled(object sender, System.EventArgs e)
        {
            isWaiting = true;
            agent.isStopped = true;
            animator.SetBool("idle", true);
        }

        private void Update()
        {
            if (!isWaiting && agent.velocity == Vector3.zero)
            {
                animator.SetBool("idle", true);
                CheckTargets();
                FindCube();
            }
        }

        public void CheckTargets()
        {
            targets = FindObjectsOfType<CubeDetectHelper>();
        }

        public void FindCube()
        {
            int rand = Random.Range(0, targets.Length - 1);
            if (targets[rand].TryGetComponent(out Transform transform))
            {
                agent.destination = transform.position;
                animator.SetBool("idle", false);
            }
        }

        public void GoToStorage()
        {
            if (helperStack.hasCube)
            {
                agent.destination = helperStack.storage.position;
                animator.SetBool("idle", false);
            }
            else
            {
                CheckTargets();
                FindCube();
            }
        }
    }
}