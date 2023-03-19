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
using System.Windows.Shapes;

namespace PrintManagmentSystem
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
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
        public List<TypeOperation> typeOperationList = new List<TypeOperation>(); // коллекция операций
        public List<Format> formatsList = new List<Format>(); // коллекция форматов
        Journal.DataForListView dataToEdit;
        public Edit(Journal.DataForListView dataToEdit)
        {
            this.dataToEdit = dataToEdit;
            InitializeComponent();
            typeOperation.SelectedItem = dataToEdit.typeOperationText;
            formats.SelectedItem = dataToEdit.formatText;
            if(dataToEdit.side == 1)
            {
                TwoSides.IsChecked = false;
            }
            else
            {
                TwoSides.IsChecked = true;
            }
            if(dataToEdit.colorText == "ЦВ")
            {
                Colors.IsChecked = true;
            }
            else if(dataToEdit.colorText == "ЦВ" + "(> 50%)")
            {
                Colors.IsChecked = true;
                LotOfСolor.IsChecked = true;
            }
            else if(dataToEdit.colorText == "Ч/Б" + "(> 50%)")
            {
                LotOfСolor.IsChecked = true;
            }
            textBoxCount.Text = dataToEdit.count.ToString();
            textBoxPrice.Text = dataToEdit.price.ToString();
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
            string query = String.Format("UPDATE `mydb`.`operations` SET " +
                                "`user` = '{0}', `typeOperation` = '{1}', `format` = '{2}', `side` = '{3}', `color` = '{4}', `count` = '{5}', `price` = '{6}', `date` = '{7}' " +
                                "WHERE id = {8}",
                                dataToEdit.fio, newTOW.typeOperationText, newTOW.formatText,
                                newTOW.side, newTOW.colorText, newTOW.count,
                                newTOW.price, dataToEdit.date, dataToEdit.id);
            DBConnect.Connection(query);
            Close();

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
                CostCalculations(); // Вызываем функцию перерасчёта операции
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0)); // если в строке используется тест, запрещаем ввод
        }
        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CostCalculations(); // производим перерасчёт
        }
        private void ColorsChange(object sender, RoutedEventArgs e)
        {
            // Перерасчёт стоимости операции
            CostCalculations();
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
    }
}
