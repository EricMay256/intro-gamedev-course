using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Unity.Mathematics;

public class GeneratedSimplexNoiseAdapter : MonoBehaviour
{
  GeneratedSimplexNoise _GeneratedSimplexNoise;
  
  [SerializeField]
  RectInt _tilesRect = new RectInt(0, 0, 100, 100);
  [SerializeField]
  Tilemap _tileMap;
  [SerializeField]
  Tile _tile;
  [SerializeField]
  float _scale = 5f, _offset = 0.5f, _range = 1f; //Distance between noise points
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    _GeneratedSimplexNoise = new GeneratedSimplexNoise(45);var noise = new GeneratedSimplexNoise(seed: 42);
    for (int y = 0; y < 5; y++)
    {
      for (int x = 0; x < 5; x++)
      {
        double val = noise.Generate(x * 0.1, y * 0.1);
      }
    GenerateTiles();
    }

  }

  void GenerateTiles()
  {
    float noiseValue = 0f;
    // Example of procedural tile generation
    float min = 1f, max = 0f;
    for (int x = _tilesRect.xMin; x < _tilesRect.xMax; x++)
    {
      for (int y = _tilesRect.yMin; y < _tilesRect.yMax; y++)
      {
        noiseValue = 0f;
        Vector3Int position = new Vector3Int(x, y);

        double d = (_offset + System.Math.Tanh(_GeneratedSimplexNoise.Generate(
          (double)x / _tilesRect.width * _scale,
          (double)y / _tilesRect.height * _scale))) / _range;
        noiseValue = (float)d;

        min = Mathf.Min(min, noiseValue);
        max = Mathf.Max(max, noiseValue);
        _tile.color = new Color(noiseValue, noiseValue, noiseValue); // Example color based on Perlin noise
        _tileMap.SetTile(position, _tile); // Assuming _tileMap is defined elsewhere
      }
    }
        Debug.Log($"Min: {min}, Max: {max}");
  }

  // Update is called once per frame
  void Update()
  {

  }
}
