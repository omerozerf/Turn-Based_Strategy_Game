using UnityEngine;

namespace _Scripts
{
    public class GridSystem
    {
        private int widht;
        private int height;
        private float cellSize;
        
        
        public GridSystem(int widht, int height, float cellSize)
        {
            this.widht = widht;
            this.height = height;
            this.cellSize = cellSize;


            for (int x = 0; x < widht; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    Debug.DrawLine(GetWordlPosition(x, z), GetWordlPosition(x, z) + Vector3.right * .2f, Color.white, 1000);
                }
            }
        }


        public Vector3 GetWordlPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * cellSize;
        }


        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / cellSize),
                Mathf.RoundToInt(worldPosition.z / cellSize)
                );
        }
    }
}
