using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform fireTransform;
    private float currentFireRate = 0.5f;
    [SerializeField] private int maxAmmoCount = 5;
    [SerializeField] private bool isPlayer = false;
    private int ammoCount = 0;

    

    [SerializeField] private float fireRate = 0.5f;
    public float GetCurrentFireRate
    {
        get
        {
            return currentFireRate;
        }

        set
        {
            currentFireRate = value;
        }
    }

    public int GetAmmo
    {
        get { return ammoCount; }
        set
        {
            ammoCount = value;
            if (ammoCount > maxAmmoCount) ammoCount = maxAmmoCount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = maxAmmoCount;
    }

    // atış mekaniğine cooldown koyduk fireRate saniyede bir ateş edebileceğiz
    void Update()
    {
        if (currentFireRate > 0f)
        {
            currentFireRate -= Time.deltaTime;
        }
        if (isPlayer)
        {
            //0 sol tık, 1 sağ tık, 2 orta tık
            if (Input.GetMouseButtonDown(0))
            {
                if (currentFireRate <= 0 && ammoCount > 0)
                {
                    Fire();
                }
            }
        }
        
        //print(transform.eulerAngles.y);
    }
    public void Fire()
    {
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = -90f;
        if (difference >= 90f)
        {
            targetRotation = 90f;
        }
        else if (difference < 90f)
        {
            targetRotation = -90f;
        }

        //ateş ettiğmizde mermi sayımız 1 azalır 
        ammoCount--;
        currentFireRate = fireRate;
        //bulletClone çağırılan objedir diye çağırma işlemi yaparken bir değişken tanımlayabiliyoruz
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation));
        //Attack scriptinin takılı olduğu obje owner değişkenidir.
        bulletClone.GetComponent<Bullet>().owner = gameObject;
    }
}