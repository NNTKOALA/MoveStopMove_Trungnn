using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Collider[] targetInRange;

    public enum AnimationType
    {
        Idle, Run, Attack, Dead, Win
    }
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected Animator CharacterAnimation;
    [SerializeField] SkinnedMeshRenderer CharacterMaterial;
    [SerializeField] SkinnedMeshRenderer PaintMaterial;

    public LayerMask characterMask;
    public float attackRange = 5;
    public List<Material> listClothesMaterials;
    public AnimationType currentAnimType = AnimationType.Idle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetClothes(Random.Range(0, 9));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
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
            nearestEnemy = null;
            float minimumDistance = Mathf.Infinity;

            foreach (Collider enemy in targetInRange)
            {
                if (enemy == this.GetComponent<Collider>())
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

    public void ThrowWeapon()
    {
        Debug.Log("ThrowWeapon");
    }

    public void EndAttack()
    {

    }
}
