using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    //merminin bizi oöldürmemesi için owner değişkeni oluşturuyoruz
    public GameObject owner;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Temas ettiği objede Target componenti yoksa aşğıdakini yok ol
        //duvara çarparsa yok ol
        if (other.gameObject.GetComponent<Target>() == false)
        {
            Destroy(gameObject);
        }
    }   
}