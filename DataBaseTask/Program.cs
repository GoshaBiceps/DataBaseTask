namespace DataBase
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

            DataBase dataBase = new DataBase();

            while (isWork)
            {
                Console.WriteLine($"\n {CommandAddPlayer} - Добавить игрока.\n {CommandOutPlayer} - Вывести всех игроков.\n {CommandBanPlayer} - Забанить игрока.\n" +
                    $" {CommandUnBanPlayer} - Разбанить игрока.\n {CommandRemuvePlayer} - Удалить игрока.\n {CommandExit} - Выход из программы.\n");
                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandAddPlayer:
                        dataBase.AddNewPlayer();
                        break;

                    case CommandOutPlayer:
                        dataBase.ShowAllPlayer();
                        break;

                    case CommandBanPlayer:
                        dataBase.BanPlayer();
                        break;

                    case CommandUnBanPlayer:
                        dataBase.UnBanPlayer();
                        break;

                    case CommandRemuvePlayer:
                        dataBase.RemovePlayer();
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

        class Player
        {
            public Player(string name, int level, bool isBanned, int id)
            {
                Name = name;
                Level = level;
                IsBanned = isBanned;
                Id = id;
            }

            public string Name { get; private set; }
            public int Level { get; private set; }
            public bool IsBanned { get; private set; }
            public int Id { get; private set; }

            public void Ban()
            {
                IsBanned = true;
            }

            public void UnBan()
            {
                IsBanned = false;
            }
        }

        class DataBase
        {
            private List<Player> _players;
            private int _countId = 1;

            public DataBase()
            {
                _players = new List<Player>();
                _players.Add(new Player("Сергей", 12, true, _countId++));
                _players.Add(new Player("Макс", 15, false, _countId++));
                _players.Add(new Player("Павел", 31, true, _countId++));
            }

            public void ShowAllPlayer()
            {
                foreach (Player player in _players)
                {
                    Console.WriteLine($"Имя игрока: {player.Name}, левел игрока: {player.Level}, статус игрока {player.IsBanned}" +
                        $", айди игрока: {player.Id}.");
                }
            }

            public void AddNewPlayer()
            {
                Player player = CreatePlayer();
                _players.Add(player);
            }

            public Player CreatePlayer()
            {
                Player player = new Player(AddNamePlayer(), AddLevel(), AddStatus(), _countId++);

                return player;
            }

            public string AddNamePlayer()
            {
                Console.WriteLine("Введите имя Игрока");
                string playerName = Console.ReadLine();

                return playerName;
            }

            public void RemovePlayer()
            {
                if (TryGetPlayer(out Player player))
                {
                    _players.Remove(player);
                    Console.WriteLine("Игрок успешно удален!");
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
                    Console.WriteLine("Игрок успешно разбанен!");
                }
                else
                {
                    Console.WriteLine("Таког игрока не существует!");
                }
            }

            public void UnBanPlayer()
            {
                if (TryGetPlayer(out Player player))
                {
                    player.UnBan();
                    Console.WriteLine("Игрок успешно заблокирован!");
                }
                else
                {
                    Console.WriteLine("Таког игрока не существует!");
                }
            }

            public int AddLevel()
            {
                int minValue = 1;
                int maxValue = 100;
                int playerLevel = 0;
                bool isWork = true;

                while (isWork)
                {
                    Console.WriteLine($"Введите левлел игрока, от {minValue} до {maxValue}.");

                    if (int.TryParse(Console.ReadLine(), out playerLevel))
                    {
                        if (playerLevel >= minValue && playerLevel <= maxValue)
                        {
                            Console.WriteLine($"Игроку присвоен левел: {playerLevel}");
                            isWork = false;
                        }
                        else
                        {
                            Console.WriteLine("Попробй снова, вы вышли за границы!");
                        }
                    }
                }

                return playerLevel;
            }

            public bool AddStatus()
            {
                string playerStatus;
                string affirmativeAnswer = "да";
                string negativeAnswer = "нет";
                bool isWork = true;
                bool status = true;

                while (isWork)
                {
                    Console.WriteLine("Введите статус игрока, забанен - да , не забанен - нет");
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
}
