using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    [SerializeField] Waypoint waypointPrefab;

    [SerializeField] Dictionary<Character, Waypoint> waypointDict = new Dictionary<Character, Waypoint>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateNewWaypoint(Character character)
    {
        if (waypointDict.ContainsKey(character)) return;

        Waypoint instance = Instantiate(waypointPrefab, transform);
        instance.SetupWaypoint(character.indigatorTranform);
        waypointDict.Add(character, instance);

    }

    public void RemoveIndicator(Character character)
    {
        if (waypointDict.TryGetValue(character, out var indicator))
        {
            Destroy(indicator.gameObject);
            waypointDict.Remove(character);
        }
    }
}
