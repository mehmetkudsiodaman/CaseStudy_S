using Scripts;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zone;

namespace Helper
{
    [RequireComponent(typeof(CharacterController))]
    public class HelperMovement : MonoBehaviour
    {
        private Transform storage;
        private NavMeshAgent agent;
        private Animator animator;

        private HelperStack stackCube;
        private HelperDetectStorage detectZones;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            stackCube = FindObjectOfType<HelperStack>();
            detectZones = FindObjectOfType<HelperDetectStorage>();
        }

        private void OnEnable()
        {
            stackCube.OnCubeStacked += Helper_OnCubeStacked;
            stackCube.OnStorageFilled += Helper_OnStorageFilled;
            detectZones.OnStorageDetected += Stack_OnStorageDetected;
            storage = FindObjectOfType<StorageArea>().GetComponent<Transform>();
        }

        private void Stack_OnStorageDetected(object sender, HelperDetectStorage.OnStorageDetectedEventArgs e)
        {
            FindCube();
        }

        private void Helper_OnCubeStacked(object sender, EventArgs e)
        {
            agent.destination = storage.position;
            animator.SetBool("idle", false);
        }

        private void Helper_OnStorageFilled(object sender, EventArgs e)
        {
            animator.SetBool("idle", true);
            agent.isStopped = true;
        }

        private void Start()
        {
            FindCube();
        }

        private void FindCube()
        {
            if (FindObjectOfType<CubeDetectHelper>() == null)
            {
                return;
            }

            if (FindObjectOfType<CubeDetectHelper>().TryGetComponent(out Transform transform))
            {
                agent.destination = transform.position;
                animator.SetBool("idle", false);
            }
        }
    }
}