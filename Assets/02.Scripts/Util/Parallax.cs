using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0.01f;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(GameManager.Instance.Speed * speed  * Time.fixedDeltaTime, 0);
    }
}
