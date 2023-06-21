using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecttitle : MonoBehaviour
{
    [SerializeField] float speed = 0.001f;
    [SerializeField] EWeaponType weaponType;
    [SerializeField] GameObject weaponModel;

    //[SerializeField] GameObject playerTrailVFX;
    //[SerializeField] GameObject enemyTrailVFX;

    [SerializeField] List<GameObject> modelsList = new List<GameObject>();

    private bool needSpin = true;

    private ProjecttitlePool pool;
    public ProjecttitlePool Pool
    {
        get => pool;
        set => pool = value;
    }

    private Vector3 destination;
    private float progress;
    private Character dealer;

    public void SetupProjectile(Vector3 destination, Character damageDealer, EWeaponType weaponType)
    {
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        destination.y = 1f;
        this.destination = destination;
        //this.destination.y = transform.position.y;

        progress = 0;
        dealer = damageDealer;

        //if (dealer as Player != null)
        //{
        //    playerTrailVFX.gameObject.SetActive(true);
        //    enemyTrailVFX.gameObject.SetActive(false);
        //}
        //else
        //{
        //    playerTrailVFX.gameObject.SetActive(false);
        //    enemyTrailVFX.gameObject.SetActive(true);
        //}

        this.weaponType = weaponType;
        if (weaponType == EWeaponType.Arrow)
        {
            needSpin = false;
        }

        SetupVisualModel();

        //AudioManager.Instance.PlayThrowWeaponSound(transform.position);
    }

    private void SetupVisualModel()
    {
        foreach (var model in modelsList)
        {
            model.SetActive(false);
        }

        modelsList[(int)weaponType].SetActive(true);
    }

    private void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.LerpUnclamped(transform.position, destination, progress);

        if (needSpin)
        {
            weaponModel.transform.Rotate(0f, 5f, 0f);
        }

        if (progress > 1f)
        {
            ReleaseSelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out var target))
        {
            if (target == dealer) return;
            target.TakeDamage(dealer);
            //AudioManager.Instance.PlayHitSound(other.transform.position);
            ReleaseSelf();
        }
    }

    public void ReleaseSelf()
    {
        pool.ReturnToPool(this);
    }
}
