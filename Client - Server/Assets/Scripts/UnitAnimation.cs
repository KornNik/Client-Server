using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation : MonoBehaviour {

    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Resuraction = Animator.StringToHash("Resuraction");
    private static readonly int Attack = Animator.StringToHash("Attack");
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
