using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;
    private bool canMoveRight = false;
    void Update()
    {
        CheckCanMoveRight();
        MoveTowards();
    }
    private void MoveTowards()
    {
        if(!canMoveRight) // false ise ünleme dikkat
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[0].position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[1].position, speed * Time.deltaTime);
        }
    }
    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, movePoints[0].position) <= 0.1f)
        {
         canMoveRight = true;
         print("Move Right");
        }

        else if (Vector3.Distance(transform.position, movePoints[1].position) <= 0.1f)
        {
         canMoveRight = false;
         print("Move Left");
        }
    }
}