using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] CharacterPool characterPool;
    [SerializeField] ProjecttitlePool projecttitlePool;

    private void Start()
    {
        Character.onAnyCharacterSpawnProjectile += Character_onAnyCharacterSpawnProjectile;
        GameManager.onEnemySpawn += GameManager_onEnemySpawn;
    }

    private Bot GameManager_onEnemySpawn()
    {
        Bot bot = characterPool.GetPooledObject() as Bot;

        if (bot != null)
        {

            Vector3 pos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
            bot.transform.SetParent(null);
            bot.transform.position = pos;
            WaypointManager.Instance.CreateNewWaypoint(bot);
            //bot.OnNewGame();
        }

        return bot;
    }

    private void Character_onAnyCharacterSpawnProjectile(object sender, Character.OnAnyCharacterSpawnProjectileArgs e)
    {
        SpawnProjectile(sender as Character, e.destination, e.weaponType);
    }

    private void SpawnProjectile(Character instigator, Vector3 destination, EWeaponType weaponType)
    {
        Projecttitle project = projecttitlePool.GetPooledObject();
        project.transform.SetParent(null);
        project.transform.position = instigator.transform.position;
        project.SetupProjectile(destination, instigator, weaponType);
    }
}
