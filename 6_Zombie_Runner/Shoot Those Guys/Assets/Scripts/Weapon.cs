using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [Tooltip("Delay in seconds between shots")]
    [SerializeField] float  timeBetweenShots = 0.067f;
    [Tooltip("Muzzle flash particle system nested instance (not Prefab)")]
    [SerializeField] ParticleSystem muzzleFlash;
    [Tooltip("Hit effect particle system Prefab (not instance)")]
    [SerializeField] GameObject hitEffect;
    [SerializeField] AmmoType ammoType;
    //[SerializeField] Camera FPCamera; // replaced by GetComponentInParent in Start()
    //[SerializeField] Ammo ammoSlot;

    Camera FPCamera;
    Ammo ammoSlot;

    bool canShoot = true;

    void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void Start()
    {
        FPCamera = GetComponentInParent<Camera>();
        ammoSlot = GetComponentInParent<Ammo>();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}