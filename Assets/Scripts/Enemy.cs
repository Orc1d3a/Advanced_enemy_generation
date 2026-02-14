using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> ReachedGoal;
    public Renderer Renderer;

    private Rigidbody _rigidbody;

    private bool _shoudMove;
    private float _speed = 5f;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _shoudMove = true;
    }

    private void OnDisable()
    {
        _shoudMove = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Goal>(out Goal goal))
            ReachedGoal?.Invoke(this);
    }

    public void Go(Transform goal)
    {
        StartCoroutine(Move(goal));
    }

    private IEnumerator Move(Transform goal)
    {
        while (_shoudMove)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, goal.position, _speed * Time.deltaTime);

            yield return null;
        }
    }
}
