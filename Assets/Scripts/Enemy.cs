using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;
    private bool canMoveRight = false;
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private LayerMask shootLayer;
    [SerializeField] private Transform aimTransform;

    private Attack attack;

    private void Start()
    {
        attack = GetComponent<Attack>();
    }

    void Update()
    {
        EnemyAttack();
        CheckCanMoveRight();
        MoveTowards();
        Aim();
    }

    private void EnemyAttack()
    {
        if (attack.GetCurrentFireRate <= 0 && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
        }
    }
    private void MoveTowards()
    {
        //mermi varsa ve player menzildeyse hareket etme
        if(Aim() && attack.GetAmmo > 0)
        {
            return;
        }
        if(!canMoveRight) // false ise çalıştır. Ünleme dikkat
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x,transform.position.y, movePoints[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[0].position);
        }
    }
    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, movePoints[0].position) <= 0.1f)
        {
         canMoveRight = true;
         //print("Move Right");
        }

        else if (Vector3.Distance(transform.position, movePoints[1].position) <= 0.1f)
        {
         canMoveRight = false;
         //print("Move Left");
        }
    }
    //düşmanın player'a bakması için kaç derece dönmesi gerektiğini hesaplar
    private void LookAtTheTarget(Vector3 newTarget)
    {
        Vector3 newLookPosition = new(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newLookPosition-transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,speed * Time.deltaTime);
    }
    private bool Aim()
    {
        //silahın ucundaki konumdan ileri doğru bir ışın çıkart. shootLayer'da bir objeye çarparsan true döndür
        bool hit = Physics.Raycast(aimTransform.position, -transform.forward, shootRange, shootLayer);
        //lazerin çıktığı pozisyondan ileri doğru shootRange kadar sarı bir ışın çiz
        Debug.DrawRay(aimTransform.position,-transform.forward * shootRange, Color.blue);
        return hit;
    }
}