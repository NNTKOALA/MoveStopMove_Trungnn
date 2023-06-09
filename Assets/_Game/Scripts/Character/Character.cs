using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons;

    protected int weaponPosition;

    public static event EventHandler<OnAnyCharacterSpawnProjectileArgs> onAnyCharacterSpawnProjectile;
    public class OnAnyCharacterSpawnProjectileArgs : EventArgs
    {
        public Vector3 destination;
        public EWeaponType weaponType;
    }

    [SerializeField] protected Collider[] targetInRange;

    public enum AnimationType
    {
        Idle, Run, Attack, Dead, Win
    }
    [SerializeField] protected float moveSpeed = 15;
    [SerializeField] protected float characterScale = 0.05f;
    [SerializeField] protected int characterLevel = 1;
    [SerializeField] protected Animator CharacterAnimation;
    [SerializeField] protected Weapon currentWeapon;
    [SerializeField] SkinnedMeshRenderer CharacterMaterial;
    [SerializeField] SkinnedMeshRenderer PaintMaterial;
    [SerializeField] Transform weaponTranform;
    [SerializeField] Transform weaponBase;
    [SerializeField] Projecttitle projecttitlePrefab;

    public Transform indigatorTranform;
    public StateMachine stateMachine { get; protected set; }

    private GameObject weapon;
    public LayerMask characterMask;
    public float attackRange = 5;
    public List<Material> listClothesMaterials;
    public AnimationType currentAnimType = AnimationType.Idle;
    public bool isDead { get; set; } = false;

    protected CharacterPool pool;
    public CharacterPool Pool
    {
        get => pool;
        set => pool = value;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetClothes(UnityEngine.Random.Range(0, 9));
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (stateMachine.CurrentState != null)
        {
            stateMachine.CurrentState.Tick();
        }
    }

    public void ChangeAnim(AnimationType _type)
    {
        if (currentAnimType != _type)
        {
            currentAnimType = _type;
            switch (_type)
            {
                case AnimationType.Idle:
                    CharacterAnimation.SetTrigger("idle");
                    break;
                case AnimationType.Run:
                    CharacterAnimation.SetTrigger("run");
                    break;
            }
        }
    }

    public void SetClothes(int colorID)
    {
        if (colorID < listClothesMaterials.Count)
        {
            CharacterMaterial.material = listClothesMaterials[colorID];
            PaintMaterial.material = listClothesMaterials[colorID];
        }
    }

    public Character FindClosetEnemy()
    {
        targetInRange = Physics.OverlapSphere(transform.position, attackRange, characterMask);
        Collider nearestEnemy = null;
        if (targetInRange.Length == 0)  
        {
            return null;
        }
        else 
        {
            float minimumDistance = Mathf.Infinity;

            foreach (Collider enemy in targetInRange)
            {
                if (enemy == this.GetComponent<Collider>())
                {
                    continue;
                }
                if (enemy.GetComponent<Character>().isDead)
                {
                    continue;
                }
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null)
            {
                return nearestEnemy.GetComponent<Character>();
            }
            else
            {
                return null;
            }
        }
    }

    public void LookAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    internal void ModifyStatsByWeapon(float attackRange, int damage)
    {
        
    }

    public virtual void TakeDamage(Character damageDealer)
    {

    }

    internal void SpawnProjectile(Vector3 position)
    {
        LookAtTarget(position);

        onAnyCharacterSpawnProjectile?.Invoke(this, new OnAnyCharacterSpawnProjectileArgs
        {
            destination = position,
            weaponType = currentWeapon.getWeaponData().weaponType
        });
    }

    public virtual void ReleaseSelf()
    {
        pool.ReturnToPool(this);
    }

    public void ChangeScale(Character damageDealer)
    {
        damageDealer.transform.localScale += Vector3.one * characterLevel * characterScale;
        attackRange = 5 + characterScale * characterLevel;
    }

    public virtual void OnNewGame()
    {
        
    }

    public virtual void OnDead()
    {
        isDead = true;
        ReleaseSelf();
    }

    public virtual void SetWeapon(EWeaponType type)
    {
        weapons[weaponPosition].gameObject.SetActive(false);

        weaponPosition = (int)type;
        weapons[weaponPosition].gameObject.SetActive(true);
        currentWeapon = weapons[weaponPosition];
    }
}
