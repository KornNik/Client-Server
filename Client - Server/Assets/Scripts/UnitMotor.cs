using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour {

    private NavMeshAgent _agent;
    private Transform _target;


    private void Start () 
    {
        _agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if(_target!=null)
        {
            if (_agent.velocity.magnitude == 0) { FaceTarget(); }
            _agent.SetDestination(_target.position);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void MoveToPoint(Vector3 point) 
    {
        _agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        _agent.stoppingDistance = newTarget.Radius;
        _target = newTarget.InterectionTransform;
    }

    public void StopFollowingTarget()
    {
        _agent.stoppingDistance = 0f;
        _agent.ResetPath();
        _target = null;
    }
}
