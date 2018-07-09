using Microsoft.SqlServer.Server;
using NotificationFromSytimeSQL;
using NotificationFromSytimeSQL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystimeCore;
using SystimeCore.Config;
using SystimeCore.Managers;
using SystimeWCF.Contract;
using static SystimeCore.Config.Enums;


#pragma warning disable S3903 // Types should be defined in named namespaces
#pragma warning disable S1118 // Utility classes should not have public constructors
public partial  class RequestSql
#pragma warning restore S1118 // Utility classes should not have public constructors
#pragma warning restore S3903 // Types should be defined in named namespaces
{
   

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void RefreshWorkORder(String tableName, DateTime dateTime)
    {
        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).RefresgFromTime(new Config(false, false), dateTime);
        });
      
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void RefreshWorkORderTest(String tableName, DateTime dateTime)
    {
        if (Enum.TryParse(tableName, out TableName table))
            UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).RefresgFromTime(new Config(false, false), dateTime);

    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void FillterSql(String tableName, String action, String jsonKeys)
    {
        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table) && Enum.TryParse(action, out TableAction tableAction))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(new Config(false, false), jsonKeys, tableAction);
        });
        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table) && Enum.TryParse(action, out TableAction tableAction))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(new Config(true, false), jsonKeys, tableAction);
        });

        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table) && Enum.TryParse(action, out TableAction tableAction))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(new Config(false, true), jsonKeys, tableAction);
        });
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void FillterTestSql(String tableName, String action, String jsonKeys)
    {
        if (Enum.TryParse(tableName, out TableName table) && Enum.TryParse(action, out TableAction tableAction))
        {
            //SqlContext.Pipe?.Send("aquui1111"+ tableName+","+ action+","+ jsonKeys);
            //UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(new Config(false, false), jsonKeys, tableAction);
            UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(new Config(true, false), jsonKeys, tableAction);
            //UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).FillterAccionSql(new Config(false, true), jsonKeys, tableAction);
            //SqlContext.Pipe?.Send("aquui");
        }
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateNotifySystimeWcfSql(String tableName, String action, int isTest)
    {
        Task.Factory.StartNew(() =>
        {
            INotifyManager wcfSystime = DependencyFactory.Resolve<INotifyManager>();
            Config config = new Config(false, false);
            if (config.DealerInfo.NotifyWcfChangeDataBase && Enum.TryParse(action, out TableAction tableAction))
            {
                String conectionString = config.DealerInfo.ConectionStringToSystime;
                if (isTest == 1 && config.DealerInfo.SaveInTest)
                    conectionString = config.DealerInfo.ConectionStringSystimeTest;
                switch (tableAction)
                {
                    case TableAction.I:
                        wcfSystime.CreateNotificationTrigger(tableName, conectionString, isTest);
                        break;
                    case TableAction.D:
                        wcfSystime.DeleteTrigger(tableName, conectionString);
                        break;
                }
            }
        });
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateNotifySystimeWcfSqlTest(String tableName, String action, int isTest)
    {
        INotifyManager wcfSystime = DependencyFactory.Resolve<INotifyManager>();
        Config config = new Config(false, false);
        if (config.DealerInfo.NotifyWcfChangeDataBase && Enum.TryParse(action, out TableAction tableAction))
        {
            String conectionString = config.DealerInfo.ConectionStringToSystime;
            if (isTest == 1 && config.DealerInfo.SaveInTest)
                conectionString = config.DealerInfo.ConectionStringSystimeTest;
            switch (tableAction)
            {
                case TableAction.I:
                    wcfSystime.CreateNotificationTrigger(tableName, conectionString, isTest);
                    break;
                case TableAction.D:
                    wcfSystime.DeleteTrigger(tableName, conectionString);
                    break;
            }
        }
    }





 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void NotifyWcfSql(String tableName, String action, String id, int isTest)
    {
        Task.Factory.StartNew(() =>
        {
            IWcfSystime wcfSystime = DependencyFactory.Resolve<IWcfSystime>();
            Config config = new Config(false, false);
            if (config.DealerInfo.NotifyWcfChangeDataBase)
            {
                String urlNotifyWcf = config.DealerInfo.UrlWcf;
                if (isTest == 1 && config.DealerInfo.SaveInTest)
                    urlNotifyWcf = config.DealerInfo.UrlWcfTest;


                if (!String.IsNullOrEmpty(urlNotifyWcf))
                    wcfSystime.NotifyChangeDatabaseWcf(tableName, action, id, urlNotifyWcf);
            }
        });
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static Boolean NotifyWcfSqlTest(String tableName, String action, String id, int isTest)
    {
        IWcfSystime wcfSystime = DependencyFactory.Resolve<IWcfSystime>();
        Config config = new Config(false, false);
        if (config.DealerInfo.NotifyWcfChangeDataBase)
        {
            String urlNotifyWcf = String.Empty;
            urlNotifyWcf = config.DealerInfo.UrlWcf;
            if (isTest == 1 && config.DealerInfo.SaveInTest)
                urlNotifyWcf = config.DealerInfo.UrlWcfTest;


            if (!String.IsNullOrEmpty(urlNotifyWcf))
                return wcfSystime.NotifyChangeDatabaseWcf(tableName, action, id, urlNotifyWcf);
        }
        return false;
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void RefreshSql(String tableName, DateTime dateTime)
    {
        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).RefresgFromTime(new Config(false, false), dateTime);
        });
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void RefreshSqlTest(String tableName,DateTime dateTime)
    {
        if (Enum.TryParse(tableName, out TableName table))
            UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).RefresgFromTime(new Config(false, false), dateTime);
    }
   
    public static void MigrateErpToSystimeSql(String tableName, String year,Config config)
    {
        //Task.Factory.StartNew(() =>
        //{
        //    if (Enum.TryParse(tableName, out TableName table) && int.TryParse(year, out int yea))
        //        UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).MigrateAll(new Config(false, true), null, yea);
        //});


        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table) && int.TryParse(year, out int yea))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).MigrateAll(config, null, yea);
        });

        //Task.Factory.StartNew(() =>
        //{
        //    if (Enum.TryParse(tableName, out TableName table) && int.TryParse(year, out int yea))
        //         UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).MigrateAll(new Config(false, false), null, yea);
        //});
     }



    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MigrateErpToSystimeSqlTest(String tableName, String year)
    {
        //Task.Factory.StartNew(() =>
        //{
        //    if (Enum.TryParse(tableName, out TableName table) && int.TryParse(year, out int yea))
        //        UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).MigrateAll(new Config(false, true), null, yea);
        //});


        //Task.Factory.StartNew(() =>
        //{
        //    if (Enum.TryParse(tableName, out TableName table) && int.TryParse(year, out int yea))
        //        UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).MigrateAll(new Config(true, false), null, yea);
        //});

        Task.Factory.StartNew(() =>
        {
            if (Enum.TryParse(tableName, out TableName table) && int.TryParse(year, out int yea))
                UtilsSystimeCore.ResolverIntanceFromTable<IManager>(table).MigrateAll(new Config(true, false), null, yea);
        }).Wait();

    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SendNotificationSql(string eventName, string tableName, string acction, string jsonTableData)
    {
        Task.Factory.StartNew(() => {
            INotificationSql notificationSql = new NotificationSql();
            notificationSql.Notification(eventName, tableName, acction, jsonTableData, new Config(false, false).DealerInfo.ConectionStringNotification);
        });
    }


    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SendNotificationSqlTest(string eventName, string tableName, string acction, string jsonTableData)
    {
        INotificationSql  notificationSql= new NotificationSql();
        notificationSql.Notification( eventName, tableName, acction, jsonTableData,new Config(false,false).DealerInfo.ConectionStringNotification);
     }

    
}

