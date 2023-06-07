using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] Character myCharacter;

    public void SpawnProjectile()
    {
        Character target = myCharacter.FindClosetEnemy();

        if (target != null)
        {
            myCharacter.SpawnProjectile(target.transform.position);
        }
    }
}
