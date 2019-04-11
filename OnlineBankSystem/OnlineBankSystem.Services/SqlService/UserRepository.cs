namespace OnlineBankSystem.Services.SqlService
{
    using Common.SqlServer;
    using Contracts;
    using Data;
    using Models.Users;
    using System;
    using System.Collections.Generic;

    public class UserRepository : DbConnector, IUserRepository
    {
        public UserRepository()
        {
        }

        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public UserServiceModel GetUserByUsername(string username)
        {
            var reader = this.ExecuteReader(QueryConstants.GetUserByUsername, new Dictionary<string, object> { { "@username", username } });

            var counter = 0;
            UserServiceModel user = null;

            while (reader.Read())
            {
                counter++;

                var userId = (int)reader[0];
                var usernameDb = (string)reader[1];
                var firstName = (string)reader[2];
                var lastName = (string)reader[3];
                var passwordHash = (string)reader[4];
                var passwordSalt = (string)reader[5];

                user = new UserServiceModel(userId, username, firstName, lastName, passwordHash, passwordSalt);
            }

            if (counter > 1)
            {
                throw new InvalidOperationException("Username must be unique");
            }

            return user;
        }
    }
}
