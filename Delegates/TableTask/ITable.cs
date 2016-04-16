namespace TableTask
{
    public interface ITable 
    {
        ITable Put(int row, int column, int value);
        ITable InsertRow(int rowIndex);
        ITable InsertColumn(int columnIndex);
        int Get(int row, int column);
    }
}