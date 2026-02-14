using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _goals;

    private Color _color;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        
        _color = Random.ColorHSV();
        _color.a = 1;
    }

    public Transform GetGoal()
    {
        int index = Random.Range(0, _goals.Length);

        return _goals[index];
    }

    public Color GetColor()
    {
        return _color;
    }
}
