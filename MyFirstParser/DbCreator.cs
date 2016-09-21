using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MyFirstParser
{
    class DbCreator
    {
        //Строки данных в нашей таблице
        string date = "18.09.2016";
        string vacancy;
        string salary = "33000 руб";
        string description;

        string sql = "SELECT * FROM MyTable";
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=База.mdb;Jet OLEDB:Engine Type=5";
        string clearCreation = "CREATE TABLE MyTable(Data text(255),Salary text(255),Vacancy text(255),Description memo, PRIMARY KEY (Description));";
        
        public DbCreator()
        {
            //Создаем новую базу в конструкторе
            try
            {
                ADOX.Catalog BD = new ADOX.Catalog();
                BD.Create(connectionString);
                BD = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to create a database." + ex.Message);
                return;
            }

            //Создаем пустую таблицу и подключаемся к базе
            DataTable myDataTable = new DataTable();
            dbModification(connectionString, clearCreation, myDataTable, false);
            /*
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to create a database connection." + ex.Message);
                return;
            }

            //Заполняем базу пустой таблицей
            try
            {
                OleDbCommand myAccessCommand = new OleDbCommand(clearCreation, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                myAccessConn.Open();
                myDataAdapter.Fill(myDataTable);
            }
            catch (Exception ex)
            {
               MessageBox.Show("Error: Failed to retrieve the required data from the DataBase." + ex.Message);
                return;
            }
            finally
            {
                myAccessConn.Close();
            } */
        }

        //Формируем таблицу базы
        public void GetDb(List<string> list)
        {
            //Пробегаемся по всем html адресам из списка и заполняем таблицу
            TableHelper th = new TableHelper(true);
            for(int i = 0; i < list.Count; i++)
            {
                string choosenVacancy = HtmlDownloadHelper.DownloadHtml(list[i], Encoding.GetEncoding(65001));
                TextSearcher ts = new TextSearcher(choosenVacancy);
                ts.Skip("<title>");
                vacancy = ts.ReadTo(":");
                ts.Skip("description\" content=\"");
                description = ts.ReadTo("\"");
                th.dbAddRow(date, vacancy, salary, description);
            }

            dbModification(connectionString, sql, th.table, true);
            /*
            OleDbConnection connection = new OleDbConnection(connectionString);
            
            connection.Open();
            oleDbCommand = new OleDbCommand(sql, connection);
            dataAdapter1 = new OleDbDataAdapter(sql, connection);
            
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter1);

            //dataAdapter1.Fill(th.table);
            dataAdapter1.Update(th.table);
            connection.Close(); */
        }

        public static void dbModification(string sqlConnection, string sqlCommand, DataTable dataTable, bool isUpdate)
        {
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(sqlConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to create a database connection." + ex.Message);
                return;
            }

            //Заполняем базу таблицей
            try
            {
                OleDbCommand myAccessCommand = new OleDbCommand(sqlCommand, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(sqlCommand, myAccessConn);
                myAccessConn.Open();
                if (isUpdate)
                {
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(myDataAdapter);
                    myDataAdapter.Update(dataTable);
                }
                else 
                myDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve the required data from the DataBase." + ex.Message);
                return;
            }
            finally
            {
                myAccessConn.Close();
            }
        }
    }
}
