using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private Vector3 pos;
    
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float uppperLimit;
    
    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<PlayerController>().transform;
        }
    }
    
    private void Update()
    {
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
                Mathf.Clamp(transform.position.y, bottomLimit, uppperLimit),
                transform.position.z
                );
        
        pos = player.position;
        pos.z = -10f;
        pos.y = 0f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }

    // Закоменировать гизмо, чтоб скрыть их
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, uppperLimit), new Vector2(rightLimit, uppperLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, uppperLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, uppperLimit), new Vector2(rightLimit, bottomLimit));
    }
}
