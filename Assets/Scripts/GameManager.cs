using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
        cameraController.SetTargets(currentPlanet.transform, nextPlanet.transform, currentPlanet.transform.localScale.x / 2, nextPlanet.transform.localScale.x / 2);
    }
    private void Update()
    {
        if (IsOffscreen())
        {
            GameOver();
        }
    }
    bool IsOffscreen()
    {
        Vector3 viewportPos = cameraController.cam.WorldToViewportPoint(rocket.position);
        bool isOffScreen = viewportPos.x < 0 || viewportPos.x > 1 ||
                           viewportPos.y < 0 || viewportPos.y > 1;
        return isOffScreen;
    }

    public void ReplacePlanet()
    {
        Destroy(currentPlanet);
        currentPlanet = nextPlanet;
        nextPlanet = SpawnNewPlanet();

        cameraController.SetTargets(currentPlanet.transform, nextPlanet.transform, currentPlanet.transform.localScale.x / 2, nextPlanet.transform.localScale.x / 2);

    }

    private GameObject SpawnNewPlanet()
    {
        float randomXPos = Random.Range(minXPos, maxXPos);
        float randomYPos = Random.Range(minYPos, maxYPos);

        Vector2 spawnPos = new Vector2(randomXPos, currentPlanet.transform.position.y + randomYPos);
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
