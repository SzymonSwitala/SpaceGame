using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Planets")]
    [SerializeField] private float yOffset;
    [SerializeField] float minXPos;
    [SerializeField] float maxXPos;
    [SerializeField] float minRotationSpeed;
    [SerializeField] float maxRotationSpeed;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    [Header("Map")]
    [SerializeField] private float mapHorizontalLimit;
    [SerializeField] private float mapBottomLimit;
    [SerializeField] private float mapTopLimit;

    [Header("References")]
    [SerializeField] private Transform rocket;
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject initialPlanet;
    [SerializeField] private CameraController cameraController;

    private GameObject currentPlanet;
    private GameObject nextPlanet;

    private void Start()
    {
        currentPlanet = initialPlanet;
        initialPlanet.GetComponent<PlanetController>().SetRotationSpeed(100);
        nextPlanet = SpawnNewPlanet();
        cameraController.SetTargets(currentPlanet.transform, nextPlanet.transform);
    }
    private void Update()
    {
        MapBorders();
    }

    private void MapBorders()
    {
        if (Mathf.Abs(rocket.position.x) > mapHorizontalLimit)
        {
            GameOver();
        }
        else if (rocket.position.y <= mapBottomLimit)
        {
            GameOver();
        }
        else if (rocket.position.y >= nextPlanet.transform.position.y + mapTopLimit)
        {
            GameOver();
        }
    }

    public void ReplacePlanet()
    {
        Destroy(currentPlanet);
        currentPlanet = nextPlanet;
        nextPlanet = SpawnNewPlanet();

        cameraController.SetTargets(currentPlanet.transform,nextPlanet.transform);

    }

    private GameObject SpawnNewPlanet()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        Vector2 spawnPos = new Vector2(randomXPos, currentPlanet.transform.position.y + yOffset);
        GameObject newPlanetGO = Instantiate(planetPrefab, spawnPos, Quaternion.identity);

        PlanetController newPlanet = newPlanetGO.GetComponent<PlanetController>();

        bool randomDirection = Random.value > 0.5f;
        float randomSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        float randomSize = Random.Range(minSize, maxSize);

        newPlanet.SetDirection(randomDirection);
        newPlanet.SetRotationSpeed(randomSpeed);
        newPlanet.SetPlanetSize(randomSize);

        return newPlanetGO;
    }

    private void GameOver()
    {
        ResetLevel();
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
