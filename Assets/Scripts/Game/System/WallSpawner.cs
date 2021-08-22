using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wallPrefab;
    [SerializeField] private SpriteRenderer wallBackgroundPrefab;
    [SerializeField] private float edgeWidth = 0.4f;
    [SerializeField] private float edgeDist = 2f;

    void Start()
    {
        float xOffset = Camera.main.orthographicSize * Camera.main.aspect - edgeDist / 2f;
        float yOffset = Camera.main.orthographicSize - edgeDist / 2f;

        SpawnBGWall(yOffset * Vector2.up);
        SpawnBGWall(yOffset * Vector2.down);
        SpawnBGWall(xOffset * Vector2.right);
        SpawnBGWall(xOffset * Vector2.left);

        xOffset -= (edgeWidth + edgeDist) / 2f;
        yOffset -= (edgeWidth + edgeDist) / 2f;

        SpawnWall(yOffset * Vector2.up);
        SpawnWall(yOffset * Vector2.down);
        SpawnWall(xOffset * Vector2.right);
        SpawnWall(xOffset * Vector2.left);
    }

    private void SpawnWall(Vector2 offset)
    {
        SpriteRenderer wall = Instantiate(wallPrefab, offset, Quaternion.identity, transform);
        float width = offset.y != 0 ? (Camera.main.orthographicSize * Camera.main.aspect - edgeDist) * 2 : edgeWidth;
        float height = offset.x != 0 ? (Camera.main.orthographicSize - edgeDist) * 2 : edgeWidth;
        wall.size = new Vector2(width, height);
        wall.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }

    private void SpawnBGWall(Vector2 offset)
    {
        SpriteRenderer wallBG = Instantiate(wallBackgroundPrefab, offset, Quaternion.identity, transform);
        float bgWidth = offset.y != 0 ? (Camera.main.orthographicSize * Camera.main.aspect) * 2 : edgeDist;
        float bgHeight = offset.x != 0 ? (Camera.main.orthographicSize) * 2 : edgeDist;
        wallBG.size = new Vector2(bgWidth, bgHeight);
    }
}
