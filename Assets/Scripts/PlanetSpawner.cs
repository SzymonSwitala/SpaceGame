using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private float spawnYOffset;

    private void Start()
    {
        Spawm();
    }
    public void Spawm()
    {

        Vector2 spawnPos = new Vector2(transform.position.x,transform.position.y+ spawnYOffset);
        Instantiate(planetPrefab, spawnPos, Quaternion.identity);
    }
}
