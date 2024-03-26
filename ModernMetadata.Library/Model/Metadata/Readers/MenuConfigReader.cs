using ModernMetadata.Library.Model.Metadata.MenuData;
using ModernMetadata.Library.Model.Metadata.Users;

namespace ModernMetadata.Library.Model.Metadata.Readers
{
    public class MenuConfigReader : IMenuConfigReader
    {
        private string _fileName;

        /// <summary>
        /// Создает экземпляр класса сериализующего конфигурацию меню.
        /// </summary>

        public MenuConfigReader(string fileName = "menuConfig.txt")
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException(fileName);

            _fileName = fileName;
        }

        /// <summary>
        /// Прочитать данные из заданного в конструктор файла
        /// </summary>
        /// <param name="userMenuData">Структура данных, описывающая статус меню для конкретного пользователя</param>
        /// <returns>Структура с меню</returns>

        public IMenuData ReadMenuData(IUserMenuData userMenuData)
        {
            string? line;
            using StreamReader reader = new(_fileName);

            // Лист для временного меню, если у элемента есть вложенные меню,
            // то он записывает это меню в этот лист под индексом равным числу перед именем раздела в этом файле.
            List<IMenuItemData> temp = [];
            List<IMenuItemData> menu = [];
            int lastNotVisibleId = -1;

            // Читаем по одной линии из файла.
            while ((line = reader.ReadLine()) != null)
            {
                // Делим линию по пробелу.
                string[] words = line.Split(' ');

                if (words.Length < 0)
                    throw new IOException("cannot find information");
                if (!int.TryParse(words[0], out var id))
                    throw new IOException("cannot find information");


                // Записываем имя раздела.
                string name = words[1];
                int zeroIndex = 2;

                for (int i = 2; words[i][0] != '0'; ++i, zeroIndex = i)
                {
                    name += " ";
                    name += words[i];
                }

                // Проверяем есть ли что-то про этот раздел в файле пользователя.
                bool isStatusExist = userMenuData.ItemsConfig.TryGetValue(name, out var status);

                // если раздел есть и имеет статус скрыт, то мы переходим на следующую итерацию.
                if (isStatusExist && status == ItemStatus.NotVisible)
                {
                    lastNotVisibleId = id;
                    continue;
                }

                // Если пункт не видим, и у него есть вложенные меню, мы также их скрываем.
                if (lastNotVisibleId != -1 && lastNotVisibleId < id) continue;
                else lastNotVisibleId = -1;

                if (name == "Ноль")
                    Console.Write("sss");

                // Если метод имеет вложенные меню.
                if (words.Length == zeroIndex + 1)
                {
                    if (id != 0)
                    {
                        // Если id меньше, чем количество элементов, то обрезаем массив до этого числа.
                        if (id <= temp.Count - 1)
                            temp = temp[..id];

                        // Добавляем новый раздел и подписываем его как внутреннее меню к предыдущему разделу.
                        temp.Add(new MenuItemData(name));
                        temp[id - 1].AddInnerMenu(temp[id]);
                    }
                    else
                    {
                        // Если перед этим массив содержал значения, записываем их в структуру.
                        if(temp.Count > 0)
                        {
                            menu.Add(temp[0]);
                            temp.Clear();
                        }

                        // Если число перед разделом равно 0, то мы записываем ее во временный массив.
                        temp.Add(new MenuItemData(name));
                    }
                }
                else
                {
                    // Получаем имя метода.
                    string methodName = words[zeroIndex + 1];
                    
                    // Записываем в структуру данные завися от условия.
                    if (temp.Count == 0)
                        menu.Add(new MenuItemData(name, methodName));
                    else if (id == 0)
                    {
                        menu.Add(temp[0]);
                        temp.Clear();
                        menu.Add(new MenuItemData(name, methodName));
                    }
                    else
                        temp[id - 1].AddInnerMenu(new MenuItemData(name, methodName));
                }
            }

            if (temp.Count > 0)
                menu.Add(temp[0]);

            return new MenuData.MenuData(menu);
        }
    }
}
