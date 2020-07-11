namespace HackThePlanet.Email
{
    using Dapper;
    using Microsoft.Data.Sqlite;


    public class Email :
            IDatabaseTable
    {
        public const string Table = "Email";


        #region Properties
        public string Body { get; set; }
        public string From { get; set; }
        public uint Id { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        #endregion


        public static Email GetEmail(int id)
        {
            Email email = null;
            using (SqliteConnection connection = GameDatabase.GetConnection())
            {
                SqlCommand command = new SqlCommand()
                    .SelectAll()
                    .Where(nameof(Id))
                    .From(Table)
                    .IsEqualTo(id.ToString());
                email = connection.QueryFirst<Email>(command);
            }

            return email;
        }
    }
}