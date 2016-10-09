using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using E1Parser;
using System.IO;

namespace MyFirstParser
{
    class DbCreator
    {
        //Строки данных в нашей таблице
        string date = "18.09.2016";
        string vacancy;
        string salary = "33000 руб";
        string description;

        //что делать, если БД уже существует: 1 - перезаписать, 2 - изменить директорию
        public static int rewriteVar = 0;

        public static string filePath = "База.mdb";
        string sql = "SELECT * FROM MyTable";
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Jet OLEDB:Engine Type=5";
        string clearCreation = "CREATE TABLE MyTable(Data text(255),Salary text(255),Vacancy text(255),Description memo);"; //, PRIMARY KEY (Description)
        
        
        public DbCreator()
        {
            //Создаем новую базу в конструкторе
            StartOfDbCreator:
            try
            {
                ADOX.Catalog BD = new ADOX.Catalog();
                BD.Create(connectionString);
                BD = null;
            }
            catch (Exception ex)
            {
                if (ex.Message == "База данных уже существует.")
                {
                    MessageForm form = new MessageForm();
                    form.ShowDialog();
                    if (rewriteVar == 1)
                    {
                        try { File.Delete(filePath); }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error: Failed to rewrite a database." + e.Message);
                            
                        }
                        rewriteVar = 0;
                        goto StartOfDbCreator; 
                    }
                    else if (rewriteVar == 2)
                    {
                        rewriteVar = 0;
                        goto StartOfDbCreator;
                    }
                }
                else
                    MessageBox.Show("Error: Failed to create a database." + ex.Message);
                return;
            }

            //Создаем пустую таблицу и добавляем ее в базу
            DataTable myDataTable = new DataTable();
            dbModification(connectionString, clearCreation, myDataTable, false);
        }

        //Формируем таблицу базы
        public void GetDb(string[] array)
        {
            //Пробегаемся по всем html адресам из списка и заполняем таблицу
            TableHelper th = new TableHelper(true);
            if (Program.myForm.InvokeRequired)
                Program.myForm.Invoke((Action)(() => Program.myForm.label1.Text = "Формирование таблицы базы данных..."));
            for (int i = 0; i < array.Length; i++)
            {
                string choosenVacancy = HtmlDownloadHelper.DownloadHtml(array[i], Encoding.GetEncoding(65001));
                TextSearcher ts = new TextSearcher(choosenVacancy);
                ts.Skip("<title>");
                vacancy = ts.ReadTo(":");
                ts.Skip("description\" content=\"");
                description = ts.ReadTo("\"");
                th.DbAddRow(date, vacancy, salary, description);
            }
            //Записываем полученную таблицу в базу. Все.
            if (Program.myForm.InvokeRequired)
                Program.myForm.Invoke((Action)(() => Program.myForm.label1.Text = "Сохранение базы данных..."));
            dbModification(connectionString, sql, th.table, true);
            if (Program.myForm.InvokeRequired)
                Program.myForm.Invoke((Action)(() => Program.myForm.label1.Text = "Готово"));
        }
        //Метод подключения и модификации базы
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
