using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace UserRoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=localhost;Port=5432;Database=Practice;User id=postgres; Password=123;";

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Вывести имена всех пользователей и названия их ролей");
                Console.WriteLine("2. Вывести названия ролей и количество пользователей, занимающих эти роли");
                Console.WriteLine("3. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetUsersWithRoles(connectionString);
                        break;
                    case "2":
                        await GetRoleUserCounts(connectionString);
                        break;
                    case "3":
                        Console.WriteLine("Выход из программы.");
                        return; // Выход из программы
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите 1, 2 или 3.");
                        break;
                }

                Console.WriteLine(); // Дополнительный пробел для лучшей читаемости
            }
        }

        private static async Task GetUsersWithRoles(string connectionString)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sql = "SELECT * FROM get_users_with_roles();";
                var result = await db.QueryAsync<UserRole>(sql);

                Console.WriteLine("Имена пользователей и их роли:");
                foreach (var row in result)
                {
                    Console.WriteLine($"Пользователь: {row.User_name}, Роль: {row.Role_name}");
                }
            }
        }

        private static async Task GetRoleUserCounts(string connectionString)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sql = "SELECT * FROM get_role_user_counts();";
                var result = await db.QueryAsync<RoleUserCount>(sql);

                Console.WriteLine("Названия ролей и количество пользователей:");
                foreach (var row in result)
                {
                    Console.WriteLine($"Роль: {row.Role_name}, Количество пользователей: {row.User_count}");
                }
            }
        }

        public class UserRole
        {
            public string User_name { get; set; }
            public string Role_name { get; set; }
        }

        public class RoleUserCount
        {
            public string Role_name { get; set; }
            public int User_count { get; set; }
        }
    }
}