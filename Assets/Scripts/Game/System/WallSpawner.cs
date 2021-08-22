using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wallPrefab;
    [SerializeField] private SpriteRenderer wallBackgroundPrefab;
    [SerializeField] private float edgeWidth = 0.4f;
    [SerializeField] private float edgeDist = 2f;

    private SpriteRenderer[] walls = new SpriteRenderer[8];
    private int wallIdx = 0;

    void Start()
    {
        float xOffset = Camera.main.orthographicSize * Camera.main.aspect - edgeDist / 2f;
        float yOffset = Camera.main.orthographicSize - edgeDist / 2f;

        SpawnWall(wallBackgroundPrefab, yOffset * Vector2.up);
        SpawnWall(wallBackgroundPrefab, yOffset * Vector2.down);
        SpawnWall(wallBackgroundPrefab, xOffset * Vector2.right);
        SpawnWall(wallBackgroundPrefab, xOffset * Vector2.left);

        xOffset -= (edgeWidth + edgeDist) / 2f;
        yOffset -= (edgeWidth + edgeDist) / 2f;

        SpawnWall(wallPrefab, yOffset * Vector2.up);
        SpawnWall(wallPrefab, yOffset * Vector2.down);
        SpawnWall(wallPrefab, xOffset * Vector2.right);
        SpawnWall(wallPrefab, xOffset * Vector2.left);
    }

    void Update()
    {
        float xOffset = Camera.main.orthographicSize * Camera.main.aspect - edgeDist / 2f;
        float yOffset = Camera.main.orthographicSize - edgeDist / 2f;

        UpdateWallBG(walls[0], yOffset * Vector2.up);
        UpdateWallBG(walls[1], yOffset * Vector2.down);
        UpdateWallBG(walls[2], xOffset * Vector2.right);
        UpdateWallBG(walls[3], xOffset * Vector2.left);

        xOffset -= (edgeWidth + edgeDist) / 2f;
        yOffset -= (edgeWidth + edgeDist) / 2f;

        UpdateWall(walls[4], yOffset * Vector2.up);
        UpdateWall(walls[5], yOffset * Vector2.down);
        UpdateWall(walls[6], xOffset * Vector2.right);
        UpdateWall(walls[7], xOffset * Vector2.left);
    }

    private void SpawnWall(SpriteRenderer prefab, Vector2 offset)
    {
        walls[wallIdx] = Instantiate(prefab, offset, Quaternion.identity, transform);
        wallIdx++;
    }

    private void UpdateWall(SpriteRenderer wall, Vector2 offset)
    {
        float width = offset.y != 0 ? (Camera.main.orthographicSize * Camera.main.aspect - edgeDist) * 2 : edgeWidth;
        float height = offset.x != 0 ? (Camera.main.orthographicSize - edgeDist) * 2 : edgeWidth;
        wall.size = new Vector2(width, height);
        wall.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        wall.transform.position = offset;
    }

    private void UpdateWallBG(SpriteRenderer wallBG, Vector2 offset)
    {
        float bgWidth = offset.y != 0 ? (Camera.main.orthographicSize * Camera.main.aspect) * 2 : edgeDist;
        float bgHeight = offset.x != 0 ? (Camera.main.orthographicSize) * 2 : edgeDist;
        wallBG.size = new Vector2(bgWidth, bgHeight);
        wallBG.transform.position = offset;
    }
}
