using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoojerController : WeaponController
{
    [Header("Attack Feedback")]
    public Color attackColour = new Color(1, 0, 1, 0.3f);
    public float attackFlashDuration = 0.2f;
    Color originalColour;
    SpriteRenderer sr;
    GameObject spawnedWave;
    WoojerBehaviour woojerBehaviour;

    protected override void Start()
    {
        base.Start();
        attackComplete = true;
        spawnedWave = Instantiate(weaponData.Prefab);
        spawnedWave.transform.position = transform.position;
        spawnedWave.transform.parent = transform;
        woojerBehaviour = spawnedWave.GetComponent<WoojerBehaviour>();
    }

    protected override void Update()
    {
        base.Update();
        SetCurrentArea();
        if (currentArea != woojerBehaviour.GetCurrentArea())
        {
            woojerBehaviour.SetCurrentArea(currentArea);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        woojerBehaviour.ClearMarkedEnemies();
        sr = spawnedWave.GetComponent<SpriteRenderer>();
        originalColour = sr.color;
        StartCoroutine(FlashAndReset());
        attackComplete = true;
    }


    IEnumerator FlashAndReset()
    {
        sr.color = attackColour;
        // disable the trigger
        spawnedWave.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(attackFlashDuration);
        // clear the list of marked enemies
        woojerBehaviour.ClearMarkedEnemies();
        // re-enable the trigger so enemies can get damaged again
        spawnedWave.GetComponent<Collider2D>().enabled = true;
        sr.color = originalColour;
    }

}
