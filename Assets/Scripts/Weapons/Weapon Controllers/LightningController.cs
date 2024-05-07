using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : WeaponController
{
    Camera mainCamera;

    protected override void Start()
    {
        base.Start();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(FireProjectiles());
    }

    protected override IEnumerator FireProjectiles()
    {
        for (int i = 0; i < currentAmount; i++)
        {
            GameObject spawnedLightning = Instantiate(weaponData.Prefab);
            spawnedLightning.transform.position = GetRelativeSpawnLocation();

            yield return new WaitForSeconds(currentInterval);
        }
        attackComplete = true;
    }

    private Vector2 GetRelativeSpawnLocation()
    {
        float xPos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        float yPos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0, 0)).y, mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

        return new Vector2(xPos, yPos);
    }
}
