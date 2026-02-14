using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void Change(Renderer renderer, Color color)
    {
        renderer.material.color = color;
    }
}
