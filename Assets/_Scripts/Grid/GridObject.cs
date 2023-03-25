namespace _Scripts.Grid
{
    public class GridObject
    {
        private GridSystem gridSystem;
        private GridPosition gridPosition;
        private Unit.Unit unit;


        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
        }


        public override string ToString()
        {
            return gridPosition.ToString() + "\n" + unit;
        }


        public void SetUnit(Unit.Unit unit)
        {
            this.unit = unit;
        }


        public Unit.Unit GetUnit()
        {
            return unit;
        }
    }
}
