using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation : MonoBehaviour {

    private static readonly int Moving = Animator.StringToHash("Moving");
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
	
	void FixedUpdate () 
    {
        animator.SetBool(Moving, agent.hasPath);
    }

    void Hit() {
    }

    void FootR() {
    }

    void FootL() {
    }
}
