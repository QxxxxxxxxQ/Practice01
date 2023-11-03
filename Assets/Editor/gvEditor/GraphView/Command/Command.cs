namespace Editor.Command
{
    public abstract class Command
    {
        #region Methods
        public abstract void Undo();

        public override string ToString()
        {
            return "test";
        }
        #endregion 
    }
}