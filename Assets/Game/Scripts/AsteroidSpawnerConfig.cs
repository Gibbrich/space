using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/AsteroidSpawnerConfig")]
public class AsteroidSpawnerConfig: ScriptableObject
{
    public List<GameObject> AsteroidPrefabs;
    public float AsteroidSpeedMin = 1;
    public float AsteroidSpeedMax = 3;
    public float AsteroidSizeMin = 1;
    public float AsteroidSizeMax = 3;
    public float Frequency = 1.5f;
    public float SpawnAtATime = 4;
}