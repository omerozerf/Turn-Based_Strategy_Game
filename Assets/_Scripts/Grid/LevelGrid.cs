using System;
using UnityEngine;

namespace _Scripts.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance { get; private set; }

        [SerializeField] private Transform gridDebugObjectPrefab;
        
        private GridSystem gridSystem;

        
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than LevelGrid!" + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            
            gridSystem = new GridSystem(10, 10, 2f);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }


        public void SetUnitAtGridPosition(GridPosition gridPosition, Unit.Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.SetUnit(unit);
        }


        public Unit.Unit GetUnitAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnit();
        }


        public void ClearUnitAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.SetUnit(null);
        }


        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    }
}
