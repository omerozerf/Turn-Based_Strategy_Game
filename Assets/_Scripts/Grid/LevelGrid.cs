using System;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Unit;

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


        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit.Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }


        public List<Unit.Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnitList();
        }


        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit.Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }


        public void UnitMovedGridPosition(Unit.Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitAtGridPosition(fromGridPosition, unit);
            
            AddUnitAtGridPosition(toGridPosition, unit);
        }


        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
        
        
        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

        
        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);


        public int GetWidth() => gridSystem.GetWidth();
        
        
        public int GetHeight() => gridSystem.GetHeight();

        
        public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }
        
        
        public Unit.Unit GetUnitAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnit();
        }
    }
}
