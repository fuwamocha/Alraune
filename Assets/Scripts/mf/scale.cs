using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scale : MonoBehaviour
{
    [SerializeField] private Vector3 maxScale;

    private float power = 0.70588235f;

    private void Update()
    {
        transform.localScale = (Mathf.Sin((2 * Mathf.PI * Time.time * power) + 1) * 0.5f * maxScale);
    }
}
