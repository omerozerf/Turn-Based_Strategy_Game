using GridSystem;
using UnityEngine;

namespace PathfindingSystem
{
    public class PathfindingTest : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var mousePos = GameInput.GetMouseWorldPosition();
            var gridPos = LevelGrid.GetGridPosition(mousePos.GetValueOrDefault());
            var path = Pathfinding.GetPath(GridPosition.Zero, gridPos);
            
            for (int i = 0; i < path.Count - 1; i++)
            {
                UnityEngine.Debug.DrawLine(path[i], path[i + 1], Color.white, 1f);
            }
        }
    }
}