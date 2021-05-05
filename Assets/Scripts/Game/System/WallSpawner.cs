using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wallPrefab;
    [SerializeField] private float edgeWidth = 0.4f;

    private float xOffset, yOffset;

    void Start()
    {
        xOffset = Camera.main.orthographicSize * Camera.main.aspect - edgeWidth;
        yOffset = Camera.main.orthographicSize - edgeWidth;

        SpawnWall(yOffset * Vector2.up);
        SpawnWall(yOffset * Vector2.down);
        SpawnWall(xOffset * Vector2.right);
        SpawnWall(xOffset * Vector2.left);
    }

    private void SpawnWall(Vector2 offset)
    {
        SpriteRenderer wall = Instantiate(wallPrefab, offset, Quaternion.identity);
        float width = offset.y != 0 ? Camera.main.orthographicSize * Camera.main.aspect * 2 : edgeWidth;
        float height = offset.x != 0 ? Camera.main.orthographicSize * 2 : edgeWidth;
        wall.size = new Vector2(width, height);
        wall.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }
}
