using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed = 1.5f;

    private void FixedUpdate()
    {
        transform.localPosition -= new Vector3(GameManager.Instance.Speed * speed * Time.fixedDeltaTime, 0);

        if (transform.localPosition.x <= -7.5f)
        {
            Destroy(gameObject);
        }
    }
}
