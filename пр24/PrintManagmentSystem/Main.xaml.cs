using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public class DataUser
        {
            public int id { get; set; } // ID пользователя
            public string name { get; set; } // имя пользователя
            public string img { get; set; } // изображение пользователя
        }
        public class TypeOperation
        {
            public int id { get; set; } // ID операции
            public string name { get; set; } // Имя операции
            public string description { get; set; } // Описание

            public TypeOperation(int _id, string _name, string _description)
            {
                this.id = _id;
                this.name = _name;
                this.description = _description;
            }
        }
        public class Format
        {
            public int id { get; set; } // ID формата
            public string format { get; set; } // формат
            public string description { get; set; } // Описание

            public Format(int _id, string _format, string _description)
            {
                this.id = _id;
                this.format = _format;
                this.description = _description;
            }
        }
        public class TypeOperationsWindow
        {
            public string typeOperationText { get; set; } // Текстовый тип операции
            public string formatText { get; set; } // формат операции
            public string colorText { get; set; } // цвет печати
            public int typeOperation { get; set; } // тип операции
            public int format { get; set; } // формат
            public int side { get; set; } // сторону
            public bool color { get; set; } // цвет
            public bool occupancy { get; set; } // прозрачность
            public int count { get; set; } // кол-во
            public float price { get; set; } // цена
        }
        public class Records
        {
            public string firstName { get; set; } // фамилия 
            public string signature { get; set; } // подпись
            public string date { get; set; } // дата
                                             // выполненные операции
            public List<TypeOperationsWindow> typeOperationsWindows = new List<TypeOperationsWindow>();
        }
        public List<TypeOperation> typeOperationList = new List<TypeOperation>(); // коллекция операций
        public List<Format> formatsList = new List<Format>(); // коллекция форматов
        public List<DataUser> dataUser = new List<DataUser>(); // колекция пользователей
        public List<Records> records = new List<Records>(); // коллекция записей
        Records record; // Изменяемая запись
        public Main()
        {
            InitializeComponent();

            typeOperationList.Add(new TypeOperation(1, "Печать", "")); // Добавляем операцию
            typeOperationList.Add(new TypeOperation(2, "Копия", "")); // Добавляем операцию
            typeOperationList.Add(new TypeOperation(3, "Сканирование", "")); // Добавляем операцию
            typeOperationList.Add(new TypeOperation(4, "Ризограф", "")); // Добавляем операцию

            formatsList.Add(new Format(1, "А4", "")); // Добавляем формат
            formatsList.Add(new Format(2, "А3", "")); // Добавляем формат
            formatsList.Add(new Format(3, "А2", "")); // Добавляем формат
            formatsList.Add(new Format(4, "А1", "")); // Добавляем формат

            LoadData();
        }
        void LoadData()
        {
            for (int i = 0; i < typeOperationList.Count; i++) // Перебираем типы операций
            {
                typeOperation.Items.Add(typeOperationList[i].name); // добавляем на экран
            }

            for (int i = 0; i < formatsList.Count; i++) // перебираем форматы
            {
                formats.Items.Add(formatsList[i].format); // выводим на экран
            }
        }
        private void DeleteOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1) // если выбрана операция
            {
                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]); // удаляем её
                CalculationsAllPrice(); // вызываем переращёт всей стоимости
            }
            else MessageBox.Show("Пожалуйста, выбирете операцию для удаления."); // выводим сообщение
        }
        public void CalculationsAllPrice()
        {
            float allPrice = 0; // Вся стоимость = 0

            for (int i = 0; i < Operations.Items.Count; i++) // перебираем все операции
            {
                // создаём новый элемент как элемент класса
                TypeOperationsWindow newTOW = Operations.Items[i] as TypeOperationsWindow;
                allPrice += newTOW.price; // считаем общую стоимость
            }

            labelAllPrice.Content = "Общая сумма: " + allPrice; // выводи общую стоимость
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0)); // если в строке используется тест, запрещаем ввод
        }
        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CostCalculations(); // производим перерасчёт
        }
        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            // автозаполнение на 1
            if (formats.SelectedItem as String == "А4") // Если выбран формат А4
            {
                TwoSides.IsEnabled = true; // Включаем двойную сторону
                Colors.IsEnabled = true; // Включаем цвет
                LotOfСolor.IsEnabled = false; // отключаем насыщенность
            }
            else if (formats.SelectedItem as String == "А3") // Если выбран формат А3
            {
                TwoSides.IsEnabled = true; // Включаем двойную сторону
                Colors.IsEnabled = false; // Отключаем цвет
                LotOfСolor.IsEnabled = false; // Отключаем насыщенность
            }
            else
            {
                TwoSides.IsEnabled = false; // Отключаем двойную сторону
                Colors.IsEnabled = true; // Включаем цветную печать
                LotOfСolor.IsEnabled = true; // Включаем насыщенность
            }

            if (textBoxCount.Text.Length == 0) // Если ничего не введено
                textBoxCount.Text = "1"; // Вводим 1

            CostCalculations(); // Вызываем функцию перерасчёта операции
        }
        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование")
                {
                    formats.SelectedIndex = -1;
                    TwoSides.IsChecked = false;
                    Colors.IsChecked = false;
                    LotOfСolor.IsChecked = false;

                    formats.IsEnabled = false;
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfСolor.IsEnabled = false;


                }
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    formats.IsEnabled = true;
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;

                    if (formats.SelectedItem as String == "А4")
                    {
                        TwoSides.IsEnabled = true;
                        Colors.IsEnabled = true;
                        LotOfСolor.IsEnabled = false;
                    }
                    else if (formats.SelectedItem as String == "А3")
                    {
                        TwoSides.IsEnabled = true;
                        Colors.IsEnabled = false;
                        LotOfСolor.IsEnabled = false;
                    }
                    else if (formats.SelectedItem as String == "А2" || formats.SelectedItem as String == "А1")
                    {
                        TwoSides.IsEnabled = false;
                        Colors.IsEnabled = true;
                        LotOfСolor.IsEnabled = true;
                    }
                }
                else if (typeOperation.SelectedItem as String == "Ризограф")
                {
                    formats.SelectedIndex = 0;
                    formats.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfСolor.IsEnabled = false;
                }


                // автозаполнение на 1
                if (textBoxCount.Text.Length == 0)
                    textBoxCount.Text = "1";

                CostCalculations();
            }
        }
        public void CostCalculations()
        {
            float price = 0;

            // проверка в зависимости от вида работы
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование") price = 10;
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    if (formats.SelectedItem as String == "А4")
                    {
                        // одна сторона
                        if (TwoSides.IsChecked == false)
                        {
                            // ч/б
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else // цвет
                                price = 20;
                        }
                        else
                        {
                            // две стороны
                            // ч/б
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 6;
                                else price = 4;
                            }
                            else // цвет
                                price = 35;
                        }
                    }
                    else if (formats.SelectedItem as String == "А3")
                    {
                        // одна сторона
                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 8;
                            else price = 6;
                        }
                        else
                        {
                            // две стороны
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 12;
                            else price = 10;
                        }
                    }
                    else if (formats.SelectedItem as String == "А2")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfСolor.IsChecked == false)

                                price = 35;

                            else
                                price = 50;
                        }
                        else
                        { // цвет
                            if (LotOfСolor.IsChecked == false)
                                price = 120;
                            else
                                price = 170;

                        }
                    }
                    else if (formats.SelectedItem as String == "А1")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfСolor.IsChecked == false)
                                price = 75;
                            else
                                price = 120;
                        }
                        else
                        { // цвет
                            if (LotOfСolor.IsChecked == false)
                                price = 170;
                            else
                                price = 250;
                        }
                    }
                }
                else if (typeOperation.SelectedItem as String == "Ризограф")
                {
                    // одна сторона
                    if (TwoSides.IsChecked == false)
                    {
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.40f;
                        else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 200 && textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) >= 100)
                            price = 1.10f;
                        else price = 1;
                    }
                    else
                    {
                        // ч/б
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.80f;
                        else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 200 && textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) >= 100)
                            price = 1.40f;
                        else price = 1.10f;
                    }
                }

            }

            // если колличество != 1
            if (textBoxCount.Text.Length > 0)
            {
                textBoxPrice.Text = (float.Parse(textBoxCount.Text) * price).ToString();
            }
        }
        private void AddOperation(object sender, RoutedEventArgs e)
        {
            TypeOperationsWindow newTOW = new TypeOperationsWindow(); // Создаём новую операцию
            newTOW.typeOperationText = typeOperation.SelectedItem as String; // присваиваем текст выбранной операции
            newTOW.typeOperation = typeOperationList.Find(x => x.name == newTOW.typeOperationText).id; // получаем ID операции
            if (formats.SelectedIndex != -1) // если выбран формат
            {
                newTOW.formatText = formats.SelectedItem as String; // присваиваем текст формата
                newTOW.format = formatsList.Find(x => x.format == newTOW.formatText).id; // получаем ID формата
            }
            if (TwoSides.IsEnabled == true) // если включена двойная сторона
            {
                if (TwoSides.IsChecked == false) // если не выбрана
                    newTOW.side = 1; // запоминаем состояние
                else
                    newTOW.side = 2; // запоминаем состояние
            }
            if (Colors.IsChecked == false) // если выключена цветная печать
            {
                newTOW.colorText = "Ч/Б"; // запоминаем
                newTOW.color = false; // запоминаем
                if (LotOfСolor.IsChecked == true) // если выбрана насыщенность
                {
                    newTOW.colorText += "(> 50%)"; // запоминаем
                    newTOW.occupancy = true; // запоминаем
                }
            }
            else
            {
                newTOW.colorText = "ЦВ";  // запоминаем
                newTOW.color = true; // запоминаем

                if (LotOfСolor.IsChecked == true) // если выбрана насыщенность
                {
                    newTOW.colorText += "(> 50%)"; // запоминаем
                    newTOW.occupancy = true; // запоминаем
                }
            }
            newTOW.count = int.Parse(textBoxCount.Text); // запоминаем кол-во
            newTOW.price = float.Parse(textBoxPrice.Text); // запоминаем стоимость
            addOperationButton.Content = "Добавить"; // изменяем название клавиши
            Operations.Items.Add(newTOW); // добавляем операцию в список
            CalculationsAllPrice(); // перерасчитываем полную стоимость операций
        }
        private void EditOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1) // Если выбрана операция
            {
                if (addOperationButton.Content != "Изменить") 
                {
                    TypeOperationsWindow newTOW = Operations.Items[Operations.SelectedIndex] as TypeOperationsWindow; // создаём операцию как класс
                    typeOperation.SelectedItem = typeOperationList.Find(x => x.id == newTOW.typeOperation).name; // Находим тип операции
                    formats.SelectedItem = formatsList.Find(x => x.id == newTOW.format)?.format;// Находим формат операции              
                    if (newTOW.side == 1) TwoSides.IsChecked = false; // если сторона 1, выключаем двойную сторону
                    else if (newTOW.side == 2) TwoSides.IsChecked = true; // если сторона 2, включаем двойную сторону
                    Colors.IsChecked = newTOW.color; // В зависимости от операции, включаем или выключаем цветную печать
                    string[] resultColor = newTOW.colorText.Split('(');
                    if (resultColor.Length == 1) LotOfСolor.IsChecked = false; // в зависимости от печати выключаем насыщенность
                    else if (resultColor.Length == 2) LotOfСolor.IsChecked = true; // в зависимости от печати включаем насыщенность
                    textBoxCount.Text = newTOW.count.ToString(); // присваиваем кол-во страниц
                    textBoxPrice.Text = newTOW.price.ToString(); // присваиваем цену
                    addOperationButton.Content = "Изменить"; // изменяем кнопку
                    Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]); // удаляем операцию из списка
                }
                else
                {
                    MessageBox.Show("Завершите предыдущее изменение");
                }
            }
            else MessageBox.Show("Пожалуйста, выбирете операцию для редактирования."); // выводим надпись
        }
        private void ColorsChange(object sender, RoutedEventArgs e)
        {
            // Перерасчёт стоимости операции
            CostCalculations();
        }
        private void AddRecordsPrints(object sender, RoutedEventArgs e)
        {
            
            if (users.SelectedIndex != -1 || usersName.Text.Length > 0) // Если выбран пользователь или введено его имя
            {
                if (Operations.Items.Count != 0) // Если кол-во операций не равно нулю
                {
                    for (int i = 0; i < Operations.Items.Count; i++)
                    {
                        Records newRocord = new Records(); // создаём новую запись
                        DataUser user = dataUser.Find(x => x.name == usersName.Text); // находим пользователя в списке
                        if (user != null) // если пользователь найден
                        {
                            newRocord.firstName = user.name; // указываем фамилию
                            newRocord.signature = user.img; // укахываем подпись
                        }
                        else
                        {
                            newRocord.firstName = usersName.Text; // укахываем фамилию
                            newRocord.signature = @"/img/nosignature.jpg"; // указываем подпись
                        }
                        // добавляем операции в класс
                        newRocord.typeOperationsWindows.Add(Operations.Items[i] as TypeOperationsWindow);
                        if (record != null) // если запись существует
                        {
                            newRocord.date = record.date; // приравниваем дату новой записи как дату записи

                            int ID = records.FindIndex(x => x.date == record.date); // находим ID операции среди сех
                            records[ID] = newRocord; // обновляем операцию
                            MessageBox.Show("Запись изменена!"); // сообщаем о том, что операция обновлена
                        }
                        else
                        {
                            // получаем датц
                            newRocord.date = DateTime.Now.ToString();
                            records.Add(newRocord); // добавляем новую операцию
                            string query = String.Format("INSERT INTO `mydb`.`operations` " +
                                "(`user`, `typeOperation`, `format`, `side`, `color`, `count`, `price`, `date`) " +
                                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", 
                                newRocord.firstName, newRocord.typeOperationsWindows[0].typeOperationText, newRocord.typeOperationsWindows[0].formatText,
                                newRocord.typeOperationsWindows[0].side, newRocord.typeOperationsWindows[0].colorText, newRocord.typeOperationsWindows[0].count,
                                newRocord.typeOperationsWindows[0].price, newRocord.date);
                            DBConnect.Connection(query);
                        }
                    }
                    Operations.Items.Clear();
                }
                else MessageBox.Show("Пожалуйста, добавьте операции.");
            }
            else MessageBox.Show("Пожалуйста, укажите Фамилию и Инициалы.");
        }

        private void OpenJournal(object sender, RoutedEventArgs e)
        {
            MainWindow.frame.Navigate(new Journal());
        }
    }
}
