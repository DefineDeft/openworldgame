using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject bullet;

    public AudioClip impact;
    public AudioSource audioSource;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public float flyDistance;
    public float bulletDuration;

    public int bulletsLeft, bulletsShot;
    public int bulletDamage;
    public float minDistance = 30f;
    public bool shooting, readyToShoot, reloading;

    public Transform attackPoint;

    public GameObject playerTarget;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
       
    }

    public void Start()
    {
        playerTarget = GameObject.Find("MainChar");
    }

    private void Update()
    {
        MyInput();

        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }

    private void MyInput()
    {

        shooting = true;

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();

        }

    }

    private void Shoot()
    {
        

        


        //Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // RaycastHit hit;

        Vector3 playerDirection = playerTarget.transform.position - attackPoint.position;

       
        if (playerDirection.magnitude < minDistance)
        {
            readyToShoot = false;

            Ray ray = new Ray(attackPoint.position, playerDirection.normalized);

            Vector3 targetPoint;

            /*   if (Physics.Raycast(ray, out hit))
               {
                   targetPoint = hit.point;
               } else
               {
              targetPoint = ray.GetPoint(flyDistance);
               }
            */

            targetPoint = ray.GetPoint(flyDistance);

            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

            currentBullet.TryGetComponent(out BulletScript bulletdata);

            bulletdata.SetDamage(bulletDamage);

            bulletdata.isEnemyBullet = true;

            currentBullet.transform.forward = directionWithSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(this.transform.up * upwardForce, ForceMode.Impulse);

            if (muzzleFlash != null)
            {
                Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            }

            StartCoroutine("DeleteBullet", currentBullet);

            //audioSource.PlayOneShot(impact, 0.5f);

            audioSource.Play();

            bulletsLeft--;
            bulletsShot++;

            if (allowInvoke)
            {
                Invoke("ResetShot", timeBetweenShooting);
                allowInvoke = false;

            }

            if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);

            }
        }

    }





    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        reloading = false;
        bulletsLeft = magazineSize;
    }

    private IEnumerator DeleteBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletDuration);

        Destroy(bullet);

        yield return null;

    }



}
