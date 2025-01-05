using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;    // Prefab de las plataformas
    public GameObject enemyGroundPrefab; // Prefab de los enemigos terrestres
    public GameObject enemyAirPrefab;    // Prefab de los enemigos aéreos
    public GameObject stonePrefab;       // Prefab de las piedras u objetos arrojadizos
    public GameObject flagPrefab;        // Prefab de la bandera
    public GameObject playerPrefab;      // Prefab del jugador

    public int platformCount = 50;        // Número de plataformas
    public float minHorizontalSpacing = 6f; // Espaciado mínimo horizontal entre plataformas
    public float maxHorizontalSpacing = 10f; // Espaciado máximo horizontal entre plataformas
    public float minY = -3f;              // Altura mínima en el eje Y para las plataformas
    public float maxY = 5f;               // Altura máxima en el eje Y para las plataformas
    public float maxVerticalDifference = 3f; // Diferencia máxima de altura entre plataformas consecutivas

    private Vector3 lastPlatformPosition; // Posición de la última plataforma generada

    void Start()
    {
        StartCoroutine(GenerateLevelCoroutine());
    }

    IEnumerator GenerateLevelCoroutine()
{
    yield return null; // Esperar un frame para asegurar que el Tilemap se actualiza completamente
    GenerateLevel();
}

    void GenerateLevel()
    {
        lastPlatformPosition = new Vector3(-5f, 0f, 0f); // Posición inicial
        GameObject firstPlatform = null;

        for (int i = 0; i < platformCount; i++)
        {
            // Generar una nueva plataforma dentro de los límites establecidos
            Vector3 newPlatformPosition = GeneratePlatformPosition();
            GameObject platform = Instantiate(platformPrefab, newPlatformPosition, Quaternion.identity);

            if (i == 0) firstPlatform = platform; // Guardar la primera plataforma

            // Generar enemigos y piedras en la plataforma
            GenerateEnemyOnTilemap(platform);
            GenerateStoneOnTilemap(platform);

            // Actualizar la posición de la última plataforma
            lastPlatformPosition = newPlatformPosition;
        }

        // Colocar al jugador en la primera plataforma
        if (firstPlatform != null)
        {
            SpawnPlayerOnTilemap(firstPlatform);
        }

        // Generar bandera en la última posición
        SpawnFlag(lastPlatformPosition + new Vector3(2f, 2f, 0));
    }

    Vector3 GeneratePlatformPosition()
    {
        float horizontalOffset = Random.Range(minHorizontalSpacing, maxHorizontalSpacing);

        // Generar un desplazamiento vertical relativo dentro del rango permitido
        float verticalOffset = Random.Range(-maxVerticalDifference, maxVerticalDifference);

        // Calcular la nueva posición vertical
        float nextY = Mathf.Clamp(lastPlatformPosition.y + verticalOffset, minY, maxY);

        // Generar la nueva posición
        Vector3 newPosition = new Vector3(lastPlatformPosition.x + horizontalOffset, nextY, 0);

        return newPosition;
    }

void GenerateEnemyOnTilemap(GameObject platform)
{
    Tilemap tilemap = platform.GetComponent<Tilemap>();
    if (tilemap != null)
    {
        // Forzar la compresión de los bounds del Tilemap
        tilemap.CompressBounds();

        // Obtener el tamaño del Tilemap y la posición en el mundo
        BoundsInt bounds = tilemap.cellBounds;
        Vector3 platformPosition = platform.transform.position;
        float tileHeight = tilemap.cellSize.y;

        // Depuración temporal
        Debug.Log($"Platform Position: {platformPosition}");
        Debug.Log($"Tilemap Bounds: Position: {bounds.position}, Size: {bounds.size}");
        Debug.Log($"Tile Height: {tileHeight}");

        // Calcular el punto más alto del Tilemap
        float topY = platformPosition.y + bounds.max.y * tileHeight;

        // Calcular límites horizontales de la plataforma
        float platformWidth = bounds.size.x * tilemap.cellSize.x;

        // Generar enemigo terrestre
        float randomXGround = Random.Range(-platformWidth / 2f + 1f, platformWidth / 2f - 1f);
        Vector3 groundEnemyPosition = new Vector3(randomXGround + platformPosition.x, topY, 0);
        Debug.Log($"Ground Enemy Position: {groundEnemyPosition}");
        Instantiate(enemyGroundPrefab, groundEnemyPosition, Quaternion.identity);

        // Generar enemigo aéreo
        float randomXAir = Random.Range(-platformWidth / 2f + 1f, platformWidth / 2f - 1f);
        float randomYAir = Random.Range(2f, 4f);
        Vector3 airEnemyPosition = new Vector3(randomXAir + platformPosition.x, topY + randomYAir, 0);
        Instantiate(enemyAirPrefab, airEnemyPosition, Quaternion.identity);
    }
}



void GenerateStoneOnTilemap(GameObject platform)
{
    Tilemap tilemap = platform.GetComponent<Tilemap>();
    if (tilemap != null)
    {
        // Obtener el tamaño del Tilemap y la posición en el mundo
        BoundsInt bounds = tilemap.cellBounds;
        Vector3 platformPosition = platform.transform.position;
        float tileHeight = tilemap.cellSize.y;

        // Calcular el punto más alto del Tilemap
        float topY = platformPosition.y + tileHeight / 2f;

        // Calcular límites horizontales de la plataforma
        float platformWidth = 5f * tilemap.cellSize.x;

        // Generar piedra en una posición aleatoria
        float randomXStone = Random.Range(-platformWidth / 2f + 1f, platformWidth / 2f - 1f); // Ajustar márgenes
        Vector3 stonePosition = new Vector3(randomXStone + platformPosition.x, topY + 0.5f, 0); // Directamente sobre la plataforma
        Instantiate(stonePrefab, stonePosition, Quaternion.identity);
    }
}

    

    void SpawnPlayerOnTilemap(GameObject platform)
    {
        Tilemap tilemap = platform.GetComponent<Tilemap>();
        if (tilemap != null)
        {
            // Obtener la posición central del Tilemap
            Vector3 platformPosition = platform.transform.position;
            Vector3 playerPosition = platformPosition + new Vector3(0, 1f, 0); // Ajuste en Y para colocarlo encima
            Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        }
    }

    void SpawnFlag(Vector3 position)
    {
        Instantiate(flagPrefab, position, Quaternion.identity);
    }
}
