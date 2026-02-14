using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private int _currentWaypoint = 0;
    private float _speed = 3f;
    private float _minDistance = 1f;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[_currentWaypoint].position) < _minDistance)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}
