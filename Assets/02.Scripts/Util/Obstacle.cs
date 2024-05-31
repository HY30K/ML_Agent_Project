using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void FixedUpdate()
    {
        transform.localPosition -= new Vector3(GameManager.Instance.Speed * Time.fixedDeltaTime, 0);

        if (transform.localPosition.x <= -7.5f)
        {
            Destroy(gameObject);
        }
    }
}
