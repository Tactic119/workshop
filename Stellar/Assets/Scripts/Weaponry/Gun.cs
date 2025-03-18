using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField]
    public bool AddBulletSpread;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private float speed = 100;
    [SerializeField]
    private bool BouncingBullets;
    [SerializeField]
    private float BounceDistance = 10f;
    [SerializeField]
    private GameObject rayStart;

    public int maxAmmo;
    public int ammo;
    public GameObject explosion;
    public GameObject explosionEffects;


    private Animator Animator;
    private float LastShootTime;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        ammo = maxAmmo;
    }

    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time && BulletSpawnPoint != null && ammo >= 0 && gameObject.activeSelf)
        {
            //Animator.SetBool("IsShooting", true);

            ShootingSystem.Play();

            Vector3 direction = AngleToTarget(); //BulletSpawnPoint.transform.forward
            TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, BounceDistance, true));
            }
            else
            {
                StartCoroutine(SpawnTrail(trail, transform.position + direction * 100, Vector3.zero, BounceDistance, false));
            }

            LastShootTime = Time.time;
            ammo -= 1;
        }
    }

    public Vector3 AngleToTarget()
    {
        FindTarget(out Vector3 target1, out bool foundTarget1);

        Vector3 positionA = BulletSpawnPoint.transform.position; // where bullet starts
        Vector3 positionB = target1; // where bullet ends
        bool foundTarget = foundTarget1;
        Vector3 direction;

        if(foundTarget)direction = positionB - positionA;
        else direction = BulletSpawnPoint.transform.forward;
        
        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    public void FindTarget(out Vector3 Target, out bool FoundTarget)
    {
        Vector3 target;
        bool foundTarget;

        RaycastHit hit;
        if (Physics.Raycast(rayStart.transform.position, rayStart.transform.forward, out hit))
        {
            //Debug.DrawLine(BulletSpawnPoint.position, hit.point, Color.blue);

            target = hit.point;
            foundTarget = true;
        }
        else
        {
            target = BulletSpawnPoint.transform.forward;
            foundTarget = false;
        }

        Target = target;
        FoundTarget = foundTarget;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BounceDistance, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        Vector3 direction = (HitPoint - Trail.transform.position).normalized;

        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float startingDistance = distance;

        while (distance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * speed;

            yield return null;
        }
        //Animator.SetBool("IsShooting", false);

        Trail.transform.position = HitPoint;

        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));

            if (BouncingBullets && BounceDistance > 0)
            {
                Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);

                if (Physics.Raycast(HitPoint, bounceDirection, out RaycastHit hit, BounceDistance, Mask))
                {
                    yield return StartCoroutine(SpawnTrail(
                        Trail,
                        hit.point,
                        hit.normal,
                        BounceDistance - Vector3.Distance(hit.point, HitPoint),
                        true
                    ));
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(
                        Trail,
                        bounceDirection * BounceDistance,
                        Vector3.zero,
                        0,
                        false
                    ));
                }
            }

            if (gameObject.tag == "Cannon")
            {
                Instantiate(explosion, Trail.gameObject.transform.position, Quaternion.identity);
                Instantiate(explosionEffects, Trail.gameObject.transform.position, Quaternion.identity);
            }

            Destroy(Trail.gameObject, Trail.time);
        }
    }










}



