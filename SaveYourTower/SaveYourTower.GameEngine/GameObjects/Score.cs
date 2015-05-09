namespace SaveYourTower.GameEngine.GameObjects
{
    public class Score
    {
        #region Properties

        public int Value { get; private set; } 

        #endregion

        #region Methods

        public void AddPoint(int value)
        {
            Value += value;
        }

        public bool SpendPoints(int value)
        {
            if (value <= Value)
            {
                Value -= value;
                return true;
            }

            return false;
        }

        public void Clean()
        {
            Value = 0;
        } 

        #endregion
    }
}
