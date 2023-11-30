namespace data_BAse_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayer = "1";
            const string CommandOutPlayer = "2";
            const string CommandBanPlayer = "3";
            const string CommandUnBanPlayer = "4";
            const string CommandRemuvePlayer = "5";
            const string CommandExit = "6";

            string userInput;
            bool isWork = true;

            Database database = new Database();

            while (isWork)
            {
                Console.WriteLine($"\n {CommandAddPlayer} - Добавить игрока.\n {CommandOutPlayer} - Вывести всех игроков.\n {CommandBanPlayer} - Забанить игрока.\n" +
                    $" {CommandUnBanPlayer} - Разбанить игрока.\n {CommandRemuvePlayer} - Удалить игрока.\n {CommandExit} - Выход из программы.\n");
                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandAddPlayer:
                        database.AddNewPlayer();
                        break;

                    case CommandOutPlayer:
                        database.ShowAllPlayers();
                        break;

                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;

                    case CommandUnBanPlayer:
                        database.UnbanPlayer();
                        break;

                    case CommandRemuvePlayer:
                        database.RemovePlayer();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Не верно введена команда, попробуйте еще раз");
                        break;
                }
            }
        }
    }

    class Player
    {
        public Player(string name, int level, bool isBanned, int id)
        {
            Id = id;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public bool IsBanned { get; private set; }

        public int Level { get; private set; }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }

    class Database
    {
        private List<Player> _players;
        private int _counterId = 1;

        public Database()
        {
            _players = new List<Player>();
            _players.Add(new Player("Сергей", 12, true, _counterId++));
            _players.Add(new Player("Макс", 15, false, _counterId++));
            _players.Add(new Player("Павел", 31, true, _counterId++));
        }

        public void ShowAllPlayers()
        {
            foreach (Player player in _players)
            {
                Console.WriteLine($"Имя Игрока: {player.Name}, левел игрока: {player.Level}, статус игрока {player.IsBanned} " +
                    $", айди игрока: {player.Id}.");
            }
        }

        public void AddNewPlayer()
        {
            Player player = CreatePlayer();
            _players.Add(player);
        }

        public void RemovePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
                Console.WriteLine("Игрок успешно удален");
            }
            else
            {
                Console.WriteLine("Такого игрока нет!");
            }
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
                Console.WriteLine("Игрок успешно заблокирован!");
            }
            else
            {
                Console.WriteLine("Такого игрока не найдено!");
            }

        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
                Console.WriteLine("Игрок разбанен");
            }
            else
            {
                Console.WriteLine("Такого игрока не найдено!");
            }
        }

        public Player CreatePlayer()
        {
            Player player = new Player(AddNamePlayer(), AddLevelPlayer(), AddStatus(), _counterId++);

            return player;
        }

        public static string AddNamePlayer()
        {
            Console.WriteLine("Введите имя игрока");
            string playerName = Console.ReadLine();

            return playerName;
        }

        public static int AddLevelPlayer()
        {
            int minValue = 1;
            int maxValue = 100;
            bool isWork = true;
            int playerLevel = 0;

            while (isWork)
            {
                Console.WriteLine($"Введите левел игрока, от {minValue} до {maxValue}");

                if (int.TryParse(Console.ReadLine(), out playerLevel))
                {
                    if (playerLevel >= minValue && playerLevel <= maxValue)
                    {
                        Console.WriteLine($" Игроку присвоен левел: {playerLevel} ");
                        isWork = false;
                    }
                    else
                    {
                        Console.WriteLine("Попробуй снова, вы вышли за границы!!!");
                    }
                }
            }

            return playerLevel;
        }

        public static bool AddStatus()
        {
            string playerStatus;
            string affirmativeAnswer = "да";
            string negativeAnswer = "нет";
            bool isWork = true;
            bool status = true;

            while (isWork)
            {
                Console.WriteLine("Введите статус игрока , забанен - да , забанен - нет");
                playerStatus = Console.ReadLine();

                if (playerStatus == affirmativeAnswer)
                {
                    status = true;
                    isWork = false;
                }
                else if (playerStatus == negativeAnswer)
                {
                    status = false;
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Введите, да или нет!");
                }
            }

            return status;
        }

        public bool TryGetPlayer(out Player foundPlayer)
        {
            Console.WriteLine("Введите айди игрока.");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                foreach (var player in _players)
                {
                    if (id == player.Id)
                    {
                        foundPlayer = player;
                        return true;
                    }
                }
            }

            foundPlayer = null;
            return false;
        }
    }
}