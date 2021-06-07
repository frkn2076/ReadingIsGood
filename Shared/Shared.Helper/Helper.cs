using Shared.Helper.Config;
using System;
using System.Security.Cryptography;

namespace Shared.Helper
{
    public static class Helper
    {
        private static readonly SHA512 _hash;

        static Helper() => _hash = System.Security.Cryptography.SHA512.Create();

        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            var hashedInputBytes = _hash.ComputeHash(bytes);
            var hashed = BitConverter.ToString(hashedInputBytes).Replace("-", string.Empty);
            return hashed;
        }

        public static string GetPostgreDatabaseConnection(PostgreSettings postgreSettings, string database)
        {
            return
                $"Server={postgreSettings.Host};" +
                $"Port={postgreSettings.Port};" +
                $"Userid={postgreSettings.User};" +
                $"Password={postgreSettings.Password};" +
                $"Timeout={postgreSettings.Timeout};" +
                $"Database={database}";
        }

        //public static string GetMongoDatabaseConnection()
        //{
        //    return
        //        $"mongodb://{PrebuiltVariables.MongoUser}:" +
        //        $"{PrebuiltVariables.MongoPassword}@" +
        //        $"{PrebuiltVariables.MongoHost}:" +
        //        $"{PrebuiltVariables.MongoPort}";
        //}
    }
}
