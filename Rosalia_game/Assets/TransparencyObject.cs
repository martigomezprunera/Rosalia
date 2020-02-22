using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyObject : MonoBehaviour
{
    Color color;
    Renderer renderer;

    private void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            color = renderer.material.color;
            color.a = 0.25f;
            //renderer.material.color = Color.Lerp(renderer.material.color, color, fadeSpeed * Time.deltaTime);
            renderer.material.color = color;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            color = renderer.material.color;
            color.a = 1f;
            //renderer.material.color = Color.Lerp(renderer.material.color, color, fadeSpeed * Time.deltaTime);
            renderer.material.color = color;

        }
    }
}
