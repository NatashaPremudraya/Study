using System;
using System.Collections.Generic;
using System.Data;

namespace TableTask
{
    public class Table : ITable, IObservable<ITable>
    {
        private List<IObserver<ITable>> observers;
        DataTable table = new DataTable("Table");

        public IDisposable Subscribe(IObserver<ITable> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(this);
            }
            return new Unsubscriber<ITable>(observers, observer);
        }

        private void NotifyObservers()
        {
            foreach (var observer in observers)
                observer.OnNext(this);
        }

        public ITable Put(int row, int column, int value)
        {
            table.Rows[row][column] = value;
            NotifyObservers();
            return this;
        }

        public ITable InsertRow(int rowIndex)
        {
            var row = table.NewRow();
            table.Rows.InsertAt(row, rowIndex);
            NotifyObservers();
            return this;
        }

        public ITable InsertColumn(int columnIndex)
        {
            var column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = table.Columns.Count.ToString();

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);
            /*
            for (int i = table.Columns.Count; i > columnIndex; i--)
            {
                var name = i.ToString();
                var last = (i - 1).ToString();
                table.Columns[name] = table.Columns[last];
            }
            */

            NotifyObservers();
            return this;
        }

        public int Get(int row, int column)
        {      
            NotifyObservers();
            return (int) table.Rows[row].ItemArray[column];
        }
    }
}