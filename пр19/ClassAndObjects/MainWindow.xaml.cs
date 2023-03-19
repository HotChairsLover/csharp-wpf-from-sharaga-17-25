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
using System.Windows.Threading;
using System.IO;

namespace ClassAndObjects
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class PersonInfo
        {
            public string name { get; set; }
            public int health { get; set; }
            public int armor { get; set; }
            public int level { get; set; }
            public int glasses { get; set; }
            public int money { get; set; }
            public float damage { get; set; }

            public string img { get; set; }

            public int counter { get; set; }

            public PersonInfo(string _name, int _health, int _armor, int _level, int _glasses, int _money, float _damage, string _img, int _counter = 0)
            {
                name = _name;
                health = _health;
                armor = _armor;
                level = _level;
                glasses = _glasses;
                money = _money;
                damage = _damage;
                img = _img;
                counter = _counter;
            }
        }

        public PersonInfo player = new PersonInfo("Student", 100, 10, 1, 0, 0, 5, "Dinosaur.jpg");
        public PersonInfo enemy;
        public List<PersonInfo> enemies = new List<PersonInfo>();

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(AttackPlayer);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();

            enemies.Add(new PersonInfo("Берсерк", 100, 20, 1, 15, 5, 20, "Dinosaur.jpg", 10));
            enemies.Add(new PersonInfo("Червь", 20, 5, 1, 5, 2, 5, "dinosaur2.jpg", 1));
            enemies.Add(new PersonInfo("Волк", 50, 3, 1, 10, 10, 15, "dinosaur3.jpg", 5));

            SelectEnemy();
            UpdateInfoPlayer();
        }

        public void SelectEnemy()
        {
            int ID = new Random().Next(0, enemies.Count);
            enemy = new PersonInfo(enemies[ID].name, enemies[ID].health, enemies[ID].armor, enemies[ID].level, enemies[ID].glasses,
                enemies[ID].money, enemies[ID].damage, enemies[ID].img, enemies[ID].counter);
            enemyImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/oponents/" + enemy.img));
        }

        public void AttackPlayer(object sender, EventArgs e)
        {
            player.health -= Convert.ToInt32(enemy.damage * 100f / (100f - player.armor));
            UpdateInfoPlayer();
        }

        public void UpdateInfoPlayer()
        {
            if (player.glasses > 100 * player.level)
            {
                player.level++;
                player.glasses = 0;

                player.health += 100;
                player.damage++;
                player.armor++;
            }

            playerHealth.Content = "Жизненные показатели: " + player.health;
            playerArmor.Content = "Броня: " + player.armor;
            playerLevel.Content = "Уровень: " + player.level;
            playerGlasses.Content = "Опыт: " + player.glasses;
            playerMoney.Content = "Монеты: " + player.money;
        }

        private void AttackEnemy(object sender, RoutedEventArgs e)
        {
            enemy.health -= Convert.ToInt32(player.damage * 100f / (100f - enemy.armor));
            float rand = new Random().Next(0, 100);
            if (enemy.counter > rand)
            {
                player.health -= Convert.ToInt32(enemy.damage * 100f / (100f - player.armor));
                UpdateInfoPlayer();
            }
            UpdateInfoEnemy();
        }

        public void UpdateInfoEnemy()
        {
            if(enemy.health <= 0)
            {
                player.glasses += enemy.glasses;
                player.money += enemy.money;
                UpdateInfoPlayer();
                SelectEnemy();
            }
            else
            {
                enemyHealth.Content = "Жизненные показатели: " + enemy.health;
                enemyArmor.Content = "Броня: " + enemy.armor;
            }
        }
    }
}
