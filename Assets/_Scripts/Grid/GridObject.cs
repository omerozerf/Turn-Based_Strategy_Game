using System.Collections.Generic;

namespace _Scripts.Grid
{
    public class GridObject
    {
        private GridSystem gridSystem;
        private GridPosition gridPosition;
        private List<Unit.Unit> unitList;


        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
            unitList = new List<Unit.Unit>();
        }


        public override string ToString()
        {
            string unitString = "";
            foreach (Unit.Unit unit in unitList)
            {
                unitString += unit + "\n";
            }
            
            return gridPosition.ToString() + "\n" + unitString;
        }


        public void AddUnit(Unit.Unit unit)
        {
            unitList.Add(unit); 
        }


        public void RemoveUnit(Unit.Unit unit)
        {
            unitList.Remove(unit);
        }


        public List<Unit.Unit> GetUnitList()
        {
            return unitList;
        }


        public bool HasAnyUnit()
        {
            return unitList.Count > 0;
        }


        public Unit.Unit GetUnit()
        {
            if (HasAnyUnit()) return unitList[0];
            
            else return null;
        }
    }
}
