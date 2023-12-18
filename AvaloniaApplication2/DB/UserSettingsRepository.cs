using Npgsql;
using System.Threading.Tasks;

namespace AvaloniaApplication2
{
    public class UserSettingsRepository
    {
        private readonly string _connectionString;

        public UserSettingsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserSettings> GetSettingsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new NpgsqlCommand("SELECT Email, NotificationsEnabled, MailingEnabled FROM UserSettings", connection);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UserSettings
                {
                    Email = reader.GetString(0),
                    NotificationsEnabled = reader.GetBoolean(1),
                    MailingEnabled = reader.GetBoolean(2)
                };
            }

            return new UserSettings();
        }

        public async Task SaveSettingsAsync(string email, bool notificationsEnabled, bool mailingEnabled)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new NpgsqlCommand(
                "INSERT INTO UserSettings (Email, NotificationsEnabled, MailingEnabled) " +
                "VALUES (@email, @notificationsEnabled, @mailingEnabled) " +
                "ON CONFLICT (UserId) DO UPDATE SET " +
                "Email = @email, NotificationsEnabled = @notificationsEnabled, MailingEnabled = @mailingEnabled", connection);

            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@notificationsEnabled", notificationsEnabled);
            command.Parameters.AddWithValue("@mailingEnabled", mailingEnabled);

            await command.ExecuteNonQueryAsync();
        }
    }
}
