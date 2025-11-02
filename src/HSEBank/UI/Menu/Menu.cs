using HSEBank.UI.Menu.Items;

namespace HSEBank.UI.Menu;

public class Menu
    {
        private const int InfiniteAttempts = -1;
        private readonly List<IMenuItem> _menuItems;
        private readonly int _cursorTop = Console.CursorTop;
        private readonly int _cursorLeft = Console.CursorLeft;
        private readonly int _maxAttempts;
        private int _selectedIndex;

        /// <summary>
        /// Конструктор меню с параметром maxAttemps.
        /// </summary>
        /// <param name="maxAttempts">Максимальное количество выборов в меню.</param>
        public Menu(int maxAttempts)
        {
            _maxAttempts = maxAttempts;
            _menuItems = new List<IMenuItem>();
            _selectedIndex = 0;
        }


        public Menu(List<IMenuItem> menuItems) : this(InfiniteAttempts)
        {
            _menuItems = menuItems;
        }


        /// <summary>
        /// Конструктро меню, которое создает меню с бесконечным количеством выборов.
        /// </summary>
        public Menu() : this(InfiniteAttempts) { }


        /// <summary>
        /// Метод для добавления нового пункта в меню.
        /// </summary>
        /// <param name="menuItem">Новый пункт в меню.</param>
        public void AddMenuItem(IMenuItem menuItem)
        {
            _menuItems.Add(menuItem);
        }

        /// <summary>
        /// Метод для отображения меню.
        /// </summary>
        public void Show()
        {
            ConsoleKey key;
            int attemptsCount = 0;
            do
            {
                if (_maxAttempts != InfiniteAttempts && attemptsCount >= _maxAttempts)
                {
                    return;
                }

                RenderMenu();
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _selectedIndex = (_selectedIndex - 1 + _menuItems.Count) % _menuItems.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        _selectedIndex = (_selectedIndex + 1) % _menuItems.Count;
                        break;
                    case ConsoleKey.Enter:
                        attemptsCount++;
                        Console.Clear();
                        _menuItems[_selectedIndex].Execute();

                        
                        if (_maxAttempts == InfiniteAttempts)
                        {
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey(true);
                        }

                        break;
                }
            } while (key != ConsoleKey.Escape);
        }


        /// <summary>
        /// Метод для рендера самого меню в консоль.
        /// </summary>
        private void RenderMenu()
        {
              Console.Clear();
            Console.SetCursorPosition(_cursorLeft, _cursorTop);
            for (int i = 0; i < _menuItems.Count; i++)
            {
                if (i == _selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"[{i+1}] {_menuItems[i].Title}");
                Console.ResetColor();
            }

            Console.ResetColor();
        }
    }