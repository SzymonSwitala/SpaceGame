using NUnit.Framework.Internal;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [Header("Planets")]
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    [Header("References")]
    [SerializeField] private GameObject planetPrefab;

    private GameObject previousPlanet;
    private GameObject currentPlanet;
    private GameObject nextPlanet;

    void Start()
    {
        nextPlanet = SpawnPlanet(transform.position, RandomPlanetSize(), RandomPlanetSpeed(), RandomPlanetDirection());
    }
    private GameObject SpawnPlanet(Vector3 position, float size, float rotationSpeed, bool rotationDirection)
    {
        GameObject newPlanet = Instantiate(planetPrefab, position, Quaternion.identity);

        PlanetController planetController = newPlanet.GetComponent<PlanetController>();
        planetController.SetPlanetSize(size);
        planetController.SetRotationSpeed(rotationSpeed);
        planetController.SetDirection(rotationDirection);

        return newPlanet;
    }
    public void ReplacePlanet()
    {
        previousPlanet = currentPlanet;
        if (previousPlanet != null) Destroy(previousPlanet);

        currentPlanet = nextPlanet;

        float randomXPos = Random.Range(minXPos, maxXPos);
        float randomYPos = Random.Range(minYPos, maxYPos);
        Vector2 spawnPos = new Vector2(randomXPos, currentPlanet.transform.position.y + randomYPos);

        nextPlanet = SpawnPlanet(spawnPos, RandomPlanetSize(), RandomPlanetSpeed(), RandomPlanetDirection());

        CameraManager.Instance.twoTargetCamera.SetTarget1(currentPlanet.transform);
        CameraManager.Instance.twoTargetCamera.SetTarget2(nextPlanet.transform);
    }
    private float RandomPlanetSize()
    {
        return Random.Range(minSize, maxSize);
    }

    private float RandomPlanetSpeed()
    {
        return Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private static bool RandomPlanetDirection()
    {
        return Random.value > 0.5f;
    }
}
