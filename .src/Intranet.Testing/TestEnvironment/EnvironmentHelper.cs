#region Usings

using System;
using System.Data;
using System.Data.SqlClient;
using Intranet.Dal;

#endregion

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     Helper for the test environment
    /// </summary>
    public static class EnvironmentHelper
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
                SetAutoIncrementOnTable( connection, "Model", true );
                InsertModule( command );
                SetAutoIncrementOnTable( connection, "Model", false );
            }
        }

        private static void DeleteAll( SqlCommand command )
        {
            command.CommandText = "DELETE FROM Model;";
            command.ExecuteNonQuery();

            command.CommandText = "DBCC CHECKIDENT('Model', RESEED, 0)";
            command.ExecuteNonQuery();
        }

        private static void GetModule()
        {
            var command = new SqlCommand { CommandText = "SELECT * FROM Module" };
            var modules = command.ExecuteNonQuery();
            command.CommandType = CommandType.Text;
        }

        private static void InsertModule( SqlCommand command )
        {
            command.CommandText =
                "INSERT INTO Module (ModelId, Name,Description,ActionName,ControllerName,AreaName,Visible,Type)" + Environment.NewLine +
                "   SELECT 1,  'LaborCreator',  'Labor QS Creator','LaborCreatorHome','Labor','null','1' UNION" + Environment.NewLine +
                "   SELECT 2,  'LaborDashboard',  'Labor QS','LaborHome','Labor','true','0' UNION" + Environment.NewLine +
                "   SELECT 3,  'Labor',  'Labor QS Creator','LaborCreatorHome','AreaName','null','1' UNION" + Environment.NewLine +
                "   SELECT 4,  'Einstellungen',  'Einstellungen für die Shell','Index','Settings','true','2'";

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