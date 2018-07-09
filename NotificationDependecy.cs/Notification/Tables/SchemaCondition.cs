using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationDependecy.Models;
using NotificationFromSytimeSQL;
using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NotificationDependecy.Notification.Tables
{
    public class SchemaCondition
    {
        public List<DynamicQueryParam> GetParamsDynamicTableDataEventQuery(SqlNotificationEventArgs e, String conectionString, String queryExecutePost, SqlConnection connection, String fillterCondition, String tableEvent, String columsNotify, DateTime dateTime)
        {
            ISchemaTables schemaTables = new SchemaTables();
            List<DynamicQueryParam> listKeys = new List<DynamicQueryParam>();
            List<String> idTables = schemaTables.GetColumKeys(conectionString, connection.Database, tableEvent);
            String colums = "";
            int countColum = 0;
            idTables.ForEach(c => {
                if (countColum == 0 && !columsNotify.Contains(c))
                    colums = c;
                else if (!columsNotify.Contains(c))
                    colums += "," + c;
                countColum++;
            });
            if (String.IsNullOrEmpty(colums))
                colums += columsNotify;
            else
                colums += "," + columsNotify;

            String query = "";

            switch (e.Info)
            {
                case SqlNotificationInfo.Insert:
                    query = String.Format("Select Top 1 {0} From  {1} where CreatedAt < @datetime Order by CreatedAt desc", colums, tableEvent);
                    break;
                case SqlNotificationInfo.Update:
                    query = String.Format("Select Top 1 {0} From  {1} where UpdatedAt < @datetime Order by UpdatedAt desc", colums, tableEvent);
                    break;
            }
            using (connection = new SqlConnection(conectionString))
            {
#pragma warning disable S3649 // User-provided values should be sanitized before use in SQL statements
                using (SqlCommand command = new SqlCommand(query, connection))
#pragma warning restore S3649 // User-provided values should be sanitized before use in SQL statements
                {
                    SqlParameter parameter = command.Parameters.Add("@datetime", System.Data.SqlDbType.DateTime);

                    // Set the value.
                    parameter.Value = dateTime;

                    command.Notification = null;
                    connection.Open();
                    // Execute the command.  

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        if (dataTable.Rows.Count > 0)
                        {


                            DataRow row = dataTable.Rows[0];
                            String JsonRow = JsonConvert.SerializeObject(row.Table).Replace("[", "").Replace("]", "");
                            JObject ObjectJson = (JObject)JsonConvert.DeserializeObject(JsonRow);
                            foreach (var prop in ObjectJson)
                            {
                                if (!String.IsNullOrEmpty(fillterCondition) && fillterCondition.Contains("@" + prop.Key))
                                    fillterCondition = fillterCondition.Replace("@" + prop.Key, prop.Value?.ToString());

                                if (!String.IsNullOrEmpty(queryExecutePost) && queryExecutePost.Contains("@" + prop.Key))
                                    queryExecutePost = queryExecutePost.Replace("@" + prop.Key, prop.Value?.ToString());

                                if (idTables.Contains(prop.Key))
                                    listKeys.Add(new DynamicQueryParam() { IdParam = "@" + prop.Key, ValueParam = prop.Value?.ToString() });
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(fillterCondition))
                        using (SqlCommand commandValidate = new SqlCommand(fillterCondition, connection))
                        {
                            using (SqlDataReader reader = commandValidate.ExecuteReader())
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                if (dataTable.Rows.Count <= 0)
                                    listKeys = null;
                            }
                        }


                    if (!String.IsNullOrEmpty(queryExecutePost))
#pragma warning disable S3966 // Objects should not be disposed more than once
                        using (connection)
#pragma warning restore S3966 // Objects should not be disposed more than once
                        {
                            connection.Open();

                            using (SqlCommand commandValidate = new SqlCommand(queryExecutePost, connection))
#pragma warning disable S108 // Nested blocks of code should not be left empty
                            {

                            }
#pragma warning restore S108 // Nested blocks of code should not be left empty
                        }
                }
            }
            return listKeys;
        }


        public List<DynamicQueryParam> GetParamsDynamicTableDataEvenTable<T>(T entity, String conectionString, SqlConnection connection, String fillterCondition,String queryExecutePost, String tableEvent, String columsNotify, DateTime dateTime) where T:class
        {
            ISchemaTables schemaTables = new SchemaTables();
            List<DynamicQueryParam> listKeys = new List<DynamicQueryParam>();
            List<String> idTables = schemaTables.GetColumKeys(conectionString, connection.Database, tableEvent);
            String colums = "";
            int countColum = 0;
            idTables.ForEach(c => {
                if (countColum == 0 && !columsNotify.Contains(c))
                    colums = c;
                else if (!columsNotify.Contains(c))
                    colums += "," + c;
                countColum++;
            });


            if (String.IsNullOrEmpty(colums))
                colums += columsNotify;
            else
                colums += "," + columsNotify;


            JObject ObjectJson = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(entity));
            foreach (var prop in ObjectJson)
            {
                if (!String.IsNullOrEmpty(fillterCondition) && fillterCondition.Contains("@" + prop.Key))
                    fillterCondition = fillterCondition.Replace("@" + prop.Key, prop.Value?.ToString());


                if (!String.IsNullOrEmpty(queryExecutePost) && queryExecutePost.Contains("@" + prop.Key))
                    queryExecutePost = queryExecutePost.Replace("@" + prop.Key, prop.Value?.ToString());


                if (idTables.Contains(prop.Key))
                    listKeys.Add(new DynamicQueryParam() { IdParam = "@" + prop.Key, ValueParam = prop.Value?.ToString() });
            }

            if (!String.IsNullOrEmpty(fillterCondition))
            {
                using (connection)
                {
                    connection.Open();
                  
                    using (SqlCommand commandValidate = new SqlCommand(fillterCondition, connection))
                    {
                        using (SqlDataReader reader = commandValidate.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            if (dataTable.Rows.Count <= 0)
                                listKeys = null;
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(queryExecutePost))
            {
#pragma warning disable S3966 // Objects should not be disposed more than once
                using (connection)
#pragma warning restore S3966 // Objects should not be disposed more than once
                {
                    connection.Open();

                    using (SqlCommand commandValidate = new SqlCommand(queryExecutePost, connection))
#pragma warning disable S108 // Nested blocks of code should not be left empty
                    {

                    }
#pragma warning restore S108 // Nested blocks of code should not be left empty
                }
            }
            return listKeys;
        }
    }
}
