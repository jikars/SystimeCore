using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using SystimeTrigger.TestingConection;

#pragma warning disable S3903 // Types should be defined in named namespaces
public static class ContractSql
#pragma warning restore S3903 // Types should be defined in named namespaces
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static Boolean TestinTcp(String url, int port)
    {
        return new TcpUtils(url, port).ConectionAvailable();
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void  FillTerSystime(String table, String action, String jsonkeys,String url, int port)
    {

        //Task.Factory.StartNew(()=> {
        //     });
        //new Thread(new ThreadStart( () => {

            if (new TcpUtils(url, port).ConectionAvailable())
            {
            
                using (SqlConnection connection = new SqlConnection("Data Source = ASBTAPDC02;Initial Catalog = Autostok;User Id = esfera;Password=esfera2015;Connection Timeout=30;"/*context connection = true*/))
                {
                try
                {

                    SqlCommand command = new SqlCommand("exec [SYSTIME].[Systimedb].[dbo].[FillterDll] @table,@action,@jsonkeys", connection)
                    {
                        CommandTimeout = 200,                  
                    };
                    command.Parameters.Add("@table", SqlDbType.NVarChar);
                    command.Parameters["@table"].Value = table;

                    command.Parameters.Add("@action", SqlDbType.NVarChar);
                    command.Parameters["@action"].Value = action;

                    command.Parameters.Add("@jsonkeys", SqlDbType.NVarChar);
                    command.Parameters["@jsonkeys"].Value = jsonkeys;
                    connection.Open();
                    command.ExecuteNonQuery();


                    }
                    catch (Exception)
                    {
                        connection?.Close();
                    }
                    finally
                    {
                        connection?.Close();
                    }
                }
            }
        //})).Start();
    }
}


   