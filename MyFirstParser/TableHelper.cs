using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace MyFirstParser
{
    class TableHelper
    {

        public DataTable table = new DataTable("MyTable");
        DataRow row;
        DataColumn column;

        public TableHelper()
        {
            MakeTable();
        }

        public TableHelper(bool a)
        {
            MakeTable(a);
        }

        public DataRowView GetView(object o)
        {
            return o as DataRowView;
        }
               
        public void MakeTable()
        {
            // Первый столбец
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Id";
            column.AutoIncrement = true;
            column.Caption = "Id";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            // Второй столбец
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Item";
            column.AutoIncrement = false;
            column.Caption = "Item";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);
        }

        public void MakeTable(bool a)
        {
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Data";
            column.AutoIncrement = false;
            column.Caption = "Data";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Vacancy";
            column.AutoIncrement = false;
            column.Caption = "Vacancy";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Salary";
            column.AutoIncrement = false;
            column.Caption = "Salary";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Description";
            column.AutoIncrement = false;
            column.Caption = "Description";
            column.ReadOnly = false;
            column.Unique = false; //true
            table.Columns.Add(column);
            
            //Убрал ключ, но он может понадобиться. Обратить на это внимание в процессе доработки программы
            //table.PrimaryKey = new DataColumn[] { table.Columns["Description"] };
        }

        public void AddRow(string id, string item)
        {
            row = table.NewRow();
            row["Id"] = id;
            row["Item"] = item;
            table.Rows.Add(row);
        }

        public void DbAddRow(string date, string vacancy, string salary, string description)
        {
            row = table.NewRow();
            row["Data"] = date;
            row["Vacancy"] = vacancy;
            row["Salary"] = salary;
            row["Description"] = description;
            table.Rows.Add(row);
        }

        public static string TakeId()
        {
            
            DataRowView myDataRowView;
            myDataRowView = Program.myForm.Invoke((Func<object>)(() => Program.myForm.comboBox1.SelectedItem)) as DataRowView;
            string sValue = "";
            if (myDataRowView != null)
            {
                sValue = Convert.ToString(myDataRowView.Row["Id"]);
            }
            return sValue;
        }
    }
}
