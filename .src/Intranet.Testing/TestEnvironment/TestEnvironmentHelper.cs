using System;
using System.Data.SqlClient;
using Intranet.Dal;

namespace IntranetTestEnvironment
{
    /// <summary>
    ///     Helper for the test environment
    /// </summary>
    public static class TestEnvironmentHelper
    {
        /// <summary>
        ///     Initializes test data
        /// </summary>
        public static void InitializeTestData()
        {
            using ( var context = new IntranetContext() )
            {
                var connection = (SqlConnection) context.Database.Connection;
                var command = new SqlCommand
                {
                    Connection = connection
                };

                connection.Open();

                DeleteAll( command );

                // Insert Test
                SetAutoIncrementOnTable( connection, "Test", true );
                InsertTest( command );
                SetAutoIncrementOnTable( connection, "Test", false );
            }
        }

        private static void DeleteAll( SqlCommand command )
        {
            command.CommandText = "DELETE FROM Test;";
            command.ExecuteNonQuery();

            command.CommandText = "DBCC CHECKIDENT('Test', RESEED, 0)";
            command.ExecuteNonQuery();
        }

        private static void InsertTest( SqlCommand command )
        {
            command.CommandText =
                "INSERT INTO Cars (TestId, TestString)" + Environment.NewLine +
                "   SELECT 1, 'this' UNION" + Environment.NewLine +
                "   SELECT 2, 'that' UNION" + Environment.NewLine +
                "   SELECT 3, 'bla'";

            command.ExecuteNonQuery();
        }

        private static void SetAutoIncrementOnTable( SqlConnection connection, String table, Boolean autoIncrementIsOn )
        {
            using ( var command = new SqlCommand() )
            {
                command.Connection = connection;
                command.CommandText = "SET IDENTITY_INSERT " + table + ( autoIncrementIsOn ? " ON" : " OFF" );
                command.ExecuteNonQuery();
            }
        }
    }
}