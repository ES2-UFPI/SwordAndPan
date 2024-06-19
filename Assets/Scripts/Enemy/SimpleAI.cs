using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;
    public float stoppingDistance = 3f;

    private void Start()
    {

    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > stoppingDistance)
            {
                Vector3 direction = player.position - transform.position;
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
