using EAuction.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;

namespace EAuction.DataMigration
{
    internal static class MigrationHelper
    {
     public static string GetEmbeddedSql(string sqlResourceName)
        {
            string resourcePreFix = "EAuction.DataMigration.Scripts";
            string fullSqlResource = resourcePreFix + sqlResourceName;
            Assembly migrationAssembly = Assembly.GetAssembly(typeof(MigrationHelper));
            using Stream embeddedSqlStream = migrationAssembly.GetManifestResourceStream(fullSqlResource);
            if (embeddedSqlStream!= null)
            {
                using StreamReader streamReadder = new StreamReader(embeddedSqlStream);
                string textOfEmbeddedSql = streamReadder.ReadToEnd();
                return textOfEmbeddedSql;
            }
            throw new UnprocessableException("Error: Embedded SQL resource does not exist");
        }

        public static string GetEmbeddedJsonConfig()
        {
            string resourceName= @"EAuction.DataMigration.migrationsettings.json";
            Assembly migrationAssembly = Assembly.GetAssembly(typeof(MigrationHelper));
            using Stream embeddedSqlStream = migrationAssembly.GetManifestResourceStream(resourceName);
            if (embeddedSqlStream != null)
            {
                using StreamReader streamReadderforJsonConfig = new StreamReader(embeddedSqlStream);
                string textOfEmbeddedJson = streamReadderforJsonConfig.ReadToEnd();
                return textOfEmbeddedJson;
            }
            throw new UnprocessableException("Error: Embedded SQL resource does not exist");
        } 
    }
}
