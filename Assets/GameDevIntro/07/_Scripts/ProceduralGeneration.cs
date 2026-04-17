using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct NoiseLayer
{
  public Rect Range;
  public float Weight;
  public NoiseLayer(Rect range, float weight)
  {
    Range = range;
    Weight = weight;
  }
}
public class ProceduralGeneration : MonoBehaviour
{
  [SerializeField]
  RectInt _tilesRect = new RectInt(0, 0, 100, 100);
  [SerializeField]
  List<NoiseLayer> _noiseLayers = new List<NoiseLayer>()
  {
    new NoiseLayer(new Rect(0, 0, 5, 5), 0.7f),
    new NoiseLayer(new Rect(0, 0, 10, 10), 0.2f),
    new NoiseLayer(new Rect(0, 0, 20, 20), 0.1f)
  };
  [SerializeField]
  Tilemap _tileMap;
  [SerializeField]
  Tile _tile;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    GenerateTiles();
  }

  // Update is called once per frame
  void Update()
  {

  }
  void GenerateTiles()
  {
    float noiseValue = 0f;
    // Example of procedural tile generation
    for (int x = _tilesRect.xMin; x < _tilesRect.xMax; x++)
    {
      for (int y = _tilesRect.yMin; y < _tilesRect.yMax; y++)
      {
        noiseValue = 0f;
        Vector3Int position = new Vector3Int(x, y);
        foreach (var layer in _noiseLayers)
        {
          // Apply noise layer to the tile position
          noiseValue += layer.Weight * Mathf.PerlinNoise(
            layer.Range.x + (float)x / _tilesRect.width * layer.Range.width,
            layer.Range.y + (float)y / _tilesRect.height * layer.Range.height);
        }

        _tile.color = new Color(noiseValue, noiseValue, noiseValue); // Example color based on Perlin noise
        _tileMap.SetTile(position, _tile); // Assuming _tileMap is defined elsewhere
      }
    }
  }
}
