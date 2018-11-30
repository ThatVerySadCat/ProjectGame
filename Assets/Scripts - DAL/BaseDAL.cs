using UnityEngine;

using System;
using System.Collections.Generic;

public class BaseDAL 
{
    // Make it a MySQL database

    protected bool MakeDatabaseConnection(string databaseIP, string userID, string password, string databaseName)
    {
        throw new NotImplementedException();
    }

    protected bool SuspendDatabaseConnection()
    {
        throw new NotImplementedException();
    }
}
