using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        const int nrow = 6;
        const int ncol = 9;
        const int TILE_SIZE = 100;
        const int CARDS_SIZE = 100;
        List<string> tile_paths = new List<string>()
        {
            @"../../iconpack/mypc.png",
            @"../../iconpack/1c.png",
            @"../../iconpack/amigo.png",
            @"../../iconpack/avast.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/dir.png",
            @"../../iconpack/excel.png",
            @"../../iconpack/garbage.png",
            @"../../iconpack/gimp.png",
            @"../../iconpack/myworld.png",
            @"../../iconpack/paint.png",
            @"../../iconpack/skype.png",
            @"../../iconpack/telegram.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/txt.png",
            @"../../iconpack/winrar.png",
            @"../../iconpack/word.png"
        };
        PictureBox pictureTile;
        // матрица путей к клеткам
        string[,] matrix_paths = new string[nrow, ncol];

        bool clerkTurn; //true - ход клерка, false - ход хакера
        int days;
        int numOfTasks;
        const int inventorySize = 6;

        const int CLERK_SIZE = 60;
        
        int clerk_x;
        int clerk_y;
        PictureBox pictureClerk;
        int clerkMoves;
        int bytes = 0;
        int clerk_WinPoints;
        bool[,] isVisited_byClerk = new bool[nrow, ncol];
        List<string> tasksClerk = new List<string>
        {
            @"Обновить браузер Амиго",
            @"Создать почту Mail.ru",
            @"Занести данные клиента в таблицу Excel",
            @"Проверить электронную почту",
            @"Написать отчет в Word",
            @"Поработать в 1С.Бухгалтерия",
            @"Поздравить коллегу с днем рождения в Одноклассниках",
            @"Проверить Telegram",
            @"Нарисовать открытку маме в Paint",
            @"Обработать фото кошки в Gimp",
            @"Посмотреть анкеты в Telegram-боте для знакомств",
            @"Оплатить лицензию WinRar",
            @"Обновить корзину до новой версии",
            @"Проверить компьютер на вирусы в Avast",
            @"Позвонить маме в Skype",
            @"Очистить корзину"
        };
        List<string> clerkTasks_inThisDay;
        List<string> clerkCards = new List<string>
        {
            @"../../spritepack/iconclerkcards/certific.png",
            @"../../spritepack/iconclerkcards/firewall.png",
            @"../../spritepack/iconclerkcards/strongpass.png",
        };
        List<string> clerkCards_Hand = new List<string>(inventorySize);
        string clerkAct = "";
        int firewallX = -1;
        int firewallY = -1;
        int strongpassX = -1;
        int strongpassY = -1;

        const int HACKER_SIZE = 60;
        int hacker_x;
        int hacker_y;
        PictureBox pictureHacker;
        int hackerMoves;
        int hacker_WinPoints;
        string[,] isVisited_byHacker = new string[nrow, ncol]; //0 - хакера не было на клетке, 1 - хакер был на клетке, абилка - на клетке абилка
        List<string> tasksHacker = new List<string>
        {
            @"Внедрить вредоносные скрипты в сайты",
            @"Распространить вредоносное ПО через спам",
            @"Украсть корпоративные данные из таблиц",
            @"Отправка фишинговых писем через почту",
            @"Закинуть файл с вредоносным макросом в Word",
            @"Украсть личные данные сотрудников из базы данных",
            @"Поставить «единички» коллегам клерка в Одноклассниках",
            @"Взломать аккаунт клерка в Telegram",
            @"Подменить открытку маме клерка в Paint",
            @"Подменить метаданные фото кошки в Gimp",
            @"Украсть компромат на клерка в Telegram",
            @"Запаролить все архивы клерка",
            @"Добавить бэкдор корзины",
            @"Помешать работе антивируса Avast",
            @"Получить доступ к вебке клерка",
            @"Заполучить данные корзины"
        };
        List<string> hackerTasks_inThisDay;
        List<string> hackerCards = new List<string>
        {
            @"../../spritepack/iconhackercards/ddos.png",
            @"../../spritepack/iconhackercards/fishing.png",
            @"../../spritepack/iconhackercards/sqlink.png",
            @"../../spritepack/iconhackercards/supplychain.png",
            @"../../spritepack/iconhackercards/troyan.png",
        };
        List<string> hackerCards_Hand = new List<string>(inventorySize);
        string hackerAct = "";
        bool ddos = false;

        void VisitMatrixInit()
        {
            for (int r = 0; r < nrow; r++)
                for (int c = 0; c < ncol; c++)
                {
                    isVisited_byClerk[r, c] = false;
                    if (string.Equals(matrix_paths[r, c], @"../../iconpack/amigo.png"))
                    {
                        hacker_x = c * TILE_SIZE;
                        hacker_y = r * TILE_SIZE;
                        isVisited_byHacker[r, c] = "1";
                    }
                    else isVisited_byHacker[r, c] = "0";
                }
                   
        }

        void TasksInit()
        {
            clerkTasks_inThisDay = new List<string>();
            hackerTasks_inThisDay = new List<string>();
            var rand = new Random();
            for (int i = 0; i < numOfTasks; i++)
            {
                int n = rand.Next(tasksClerk.Count);
                switch (tasksClerk[n])
                {
                    case @"Обновить браузер Амиго":
                        if (clerkTasks_inThisDay.IndexOf(@"Поздравить коллегу с днем рождения в Одноклассниках") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Создать почту Mail.ru":
                        if (clerkTasks_inThisDay.IndexOf(@"Проверить электронную почту") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Занести данные клиента в таблицу Excel":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Проверить электронную почту":
                        if (clerkTasks_inThisDay.IndexOf(@"Создать почту Mail.ru") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Написать отчет в Word":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Поработать в 1С.Бухгалтерия":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Поздравить коллегу с днем рождения в Одноклассниках":
                        if (clerkTasks_inThisDay.IndexOf(@"Обновить браузер Амиго") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Проверить Telegram":
                        if (clerkTasks_inThisDay.IndexOf(@"Посмотреть анкеты в Telegram-боте для знакомств") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Нарисовать открытку маме в Paint":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Обработать фото кошки в Gimp":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Посмотреть анкеты в Telegram-боте для знакомств":
                        if (clerkTasks_inThisDay.IndexOf(@"Проверить Telegram") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Оплатить лицензию WinRar":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Обновить корзину до новой версии":
                        if (clerkTasks_inThisDay.IndexOf(@"Очистить корзину") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                    case @"Проверить компьютер на вирусы в Avast":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Позвонить маме в Skype":
                        clerkTasks_inThisDay.Add(tasksClerk[n]);
                        tasksClerk.RemoveAt(n);
                        hackerTasks_inThisDay.Add(tasksHacker[n]);
                        tasksHacker.RemoveAt(n);
                        break;
                    case @"Очистить корзину":
                        if (clerkTasks_inThisDay.IndexOf(@"Обновить корзину до новой версии") < 0)
                        {
                            clerkTasks_inThisDay.Add(tasksClerk[n]);
                            tasksClerk.RemoveAt(n);
                            hackerTasks_inThisDay.Add(tasksHacker[n]);
                            tasksHacker.RemoveAt(n);
                        }
                        else i--;
                        break;
                }
            }
        }

        string InitCard()
        {
            var rand = new Random();
            if (clerkTurn) return clerkCards[rand.Next(clerkCards.Count)];
            else return hackerCards[rand.Next(hackerCards.Count)];
        }

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            days = 0;
            numOfTasks = 3;
            clerkTurn = true;
            hackerTasksInfo.Visible = false;

            clerk_x = 0;
            clerk_y = 0;
            pictureClerk = new PictureBox
            {
                Image = Image.FromFile(@"../../spritepack/clerkmarker.png")
            };
            clerkMoves = 2;
            clerk_WinPoints = 0;

            hacker_x = 0;
            hacker_y = 0;
            pictureHacker = new PictureBox
            {
                Image = Image.FromFile(@"../../spritepack/hackermarker.png")
            };
            hackerMoves = 1;
            hacker_WinPoints = 0;
            SQLink.Visible = false;

            pictureTile = new PictureBox();
            matrix_paths[0, 0] = tile_paths[0];
            tile_paths.Remove(tile_paths[0]);
            // переменная  - генератор случайных чисел
            var rand = new Random();
            // загружаем картинку в виде матрицы пикселей
            for (int r = 0; r < nrow; r++)
            {
                for (int c = 0; c < ncol; c++)
                {
                    if (r == 0 && c == 0) continue;
                    int npic = rand.Next(tile_paths.Count); // генерируем случайный номер картинки
                    matrix_paths[r, c] = tile_paths[npic];   // запомнили путь в матрицу
                    // удаляем картинку из списка
                    tile_paths.Remove(tile_paths[npic]);
                }
            }

            TasksInit();
            VisitMatrixInit();
        }

        private void backgroundPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            //рисуем решетку
            Pen myPen = new Pen(Color.Black, 1);
            int xpen = 0;
            int ypen = 0;
            for (int i = 0; i < ncol; i++)
            {
                g.DrawLine(myPen, xpen, 0, xpen, Height);
                xpen += TILE_SIZE;
                g.DrawLine(myPen, 0, ypen, Width, ypen);
                ypen += TILE_SIZE;
            }

            // рисуем игровое поле
            for (int r = 0; r < nrow; r++)
            {
                int y = r * TILE_SIZE;
                for (int c = 0; c < ncol; c++)
                {
                    int x = c * TILE_SIZE;
                    pictureTile.Image = Image.FromFile(matrix_paths[r, c]);

                    g.DrawImage(pictureTile.Image, new Rectangle(x, y, TILE_SIZE, TILE_SIZE));
                }
            }
            isVisited_byClerk[0,0] = true;

            // рисуем клерка и хакера
            if (clerkTurn)
                g.DrawImage(pictureClerk.Image, new Rectangle(clerk_x + 50, clerk_y + 50, CLERK_SIZE, CLERK_SIZE));
            else {
                g.DrawImage(pictureClerk.Image, new Rectangle(clerk_x + 50, clerk_y + 50, CLERK_SIZE, CLERK_SIZE));
                g.DrawImage(pictureHacker.Image, new Rectangle(hacker_x + 60, hacker_y + 50, HACKER_SIZE, HACKER_SIZE));
            }

            //обновляем игровую информацию
            GameInfo.Text = string.Format("День {0}\nХод ", days + 1);
            if (clerkTurn)
            {
                GameInfo.Text += " клерка\n";
                avatar.Image = Image.FromFile(@"../../spritepack/clerk.png");
            }
            else
            {
                GameInfo.Text += " хакера\n";
                avatar.Image = Image.FromFile(@"../../spritepack/hacker.png");
            }
            GameInfo.Text += string.Format("Количество байтов у клерка: {0}\nОчки клерка {1}\nОчки хакера: {2}", bytes, clerk_WinPoints, hacker_WinPoints);

            //обновляем таски
            clerkTasksInfo.Text = "Задачи на день:";
            hackerTasksInfo.Text = "Пакости:";
            for (int i = 0; i < clerkTasks_inThisDay.Count; i++)
                clerkTasksInfo.Text += "\n" + clerkTasks_inThisDay[i];
            for (int i = 0; i < hackerTasks_inThisDay.Count; i++)
                hackerTasksInfo.Text += "\n" + hackerTasks_inThisDay[i];
        }

        private void backgroundPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //  e.X, e.Y - однозначно определить клетку
            // узнаем координаты клетки внутри матрицы
            int c = e.X / TILE_SIZE;
            int r = e.Y / TILE_SIZE;

            //ХОД КЛЕРКА
            if (clerkTurn)
            {
                //применяем карту
                if (!string.Equals(clerkAct, ""))
                {
                    switch(clerkAct)
                    {
                        case "certific":
                            if (string.Equals(isVisited_byHacker[r, c], "0") || string.Equals(isVisited_byHacker[r, c], "1"))
                                MessageBox.Show("Вирус не обнаружен!");
                            else
                            {
                                isVisited_byHacker[r, c] = "1";
                                MessageBox.Show(string.Format("Удален вирус: {0}", isVisited_byHacker[r,c]));
                            }
                            clerkCards_Hand.Remove(@"../../spritepack/iconclerkcards/certific.png");
                            break;
                        case "firewall":
                            firewallX = r;
                            firewallY = c;
                            clerkCards_Hand.Remove(@"../../spritepack/iconclerkcards/firewall.png");
                            break;
                        case "strongpass":
                            strongpassX = r;
                            strongpassY = c;
                            clerkCards_Hand.Remove(@"../../spritepack/iconclerkcards/strongpass.png");
                            break;
                    }
                    clerkAct = "";
                    clerkMoves--;
                }
                // меняем координаты
                else if (r * TILE_SIZE == clerk_y && Math.Abs(c * TILE_SIZE - clerk_x) == TILE_SIZE && Math.Abs(c * TILE_SIZE - clerk_x) > 0 || 
                    (c * TILE_SIZE == clerk_x && Math.Abs(r * TILE_SIZE - clerk_y) == TILE_SIZE && Math.Abs(r * TILE_SIZE - clerk_y) > 0))
                {
                    if (firewallX == r && firewallY == c)
                        MessageBox.Show("Тут стоит ФАЕРВОЛЛ!!!");
                    else
                    {
                        clerk_x = c * TILE_SIZE;
                        clerk_y = r * TILE_SIZE;
                        clerkMoves--;
                    }
                    
                }
                //или взаимодействуем с иконкой, на которой стоим
                else if ((r * TILE_SIZE == clerk_y) && (c * TILE_SIZE == clerk_x))
                {
                    //если наткнулись на вирус
                    if (!(string.Equals(isVisited_byHacker[r, c], "0") || string.Equals(isVisited_byHacker[r, c], "1")))
                    {
                        switch (isVisited_byHacker[r, c])
                        {
                            case "ddos":
                                MessageBox.Show("ДУДОС! Клерк пропускает следующий ход");
                                ddos = true;
                                isVisited_byHacker[r, c] = "1";
                                break;
                            case "fishing":
                                MessageBox.Show("ФИШИНГ! Клерк теряет все байты");
                                bytes = 0;
                                isVisited_byHacker[r, c] = "1";
                                break;
                            case "supplychain":
                                MessageBox.Show("Атака на цепочку поставок данных. Клерк теряет одну карту");
                                var rand = new Random();
                                clerkCards_Hand.RemoveAt(rand.Next(clerkCards_Hand.Count));
                                isVisited_byHacker[r, c] = "1";
                                break;
                            case "sqlink":
                                MessageBox.Show("SQL-инъекция. Клерк теперь играет в открытую");
                                SQLink.Visible = true;
                                break;
                        }
                        isVisited_byHacker[r, c] = "1";
                    }
                    switch (matrix_paths[r, c])
                    {
                        case "../../iconpack/dir.png":
                            if (!isVisited_byClerk[r, c])
                            {
                                bytes++;
                                isVisited_byClerk[r, c] = true; clerkMoves--;
                            } break;
                        case "../../iconpack/txt.png":
                            if (clerkCards_Hand.Count < inventorySize && !isVisited_byClerk[r, c])
                            {
                                clerkCards_Hand.Add(InitCard());
                                isVisited_byClerk[r, c] = true; clerkMoves--;
                            }
                            break;
                        case @"../../iconpack/1c.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Поработать в 1С.Бухгалтерия") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Поработать в 1С.Бухгалтерия");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/amigo.png":
                            if ((clerkTasks_inThisDay.IndexOf(@"Обновить браузер Амиго") >= 0 ||
                                clerkTasks_inThisDay.IndexOf(@"Поздравить коллегу с днем рождения в Одноклассниках") >= 0) && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Обновить браузер Амиго");
                                clerkTasks_inThisDay.Remove(@"Поздравить коллегу с днем рождения в Одноклассниках");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/avast.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Проверить компьютер на вирусы в Avast") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Проверить компьютер на вирусы в Avast");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/excel.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Занести данные клиента в таблицу Excel") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Занести данные клиента в таблицу Excel");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/garbage.png":
                            if ((clerkTasks_inThisDay.IndexOf(@"Обновить корзину до новой версии") >= 0 ||
                                clerkTasks_inThisDay.IndexOf(@"Очистить корзину") >= 0) && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Обновить корзину до новой версии");
                                clerkTasks_inThisDay.Remove(@"Очистить корзину");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/gimp.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Обработать фото кошки в Gimp") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Обработать фото кошки в Gimp");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/myworld.png":
                            if ((clerkTasks_inThisDay.IndexOf(@"Создать почту Mail.ru") >= 0 ||
                                clerkTasks_inThisDay.IndexOf(@"Проверить электронную почту") >= 0) && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Создать почту Mail.ru");
                                clerkTasks_inThisDay.Remove(@"Проверить электронную почту");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/paint.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Нарисовать открытку маме в Paint") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Нарисовать открытку маме в Paint");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/skype.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Позвонить маме в Skype") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Позвонить маме в Skype");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/telegram.png":
                            if ((clerkTasks_inThisDay.IndexOf(@"Проверить Telegram") >= 0 ||
                                clerkTasks_inThisDay.IndexOf(@"Посмотреть анкеты в Telegram-боте для знакомств") >= 0) && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Проверить Telegram");
                                clerkTasks_inThisDay.Remove(@"Посмотреть анкеты в Telegram-боте для знакомств");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/winrar.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Оплатить лицензию WinRar") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Оплатить лицензию WinRar");
                                clerkMoves--; bytes--;
                            }
                            break;
                        case @"../../iconpack/word.png":
                            if (clerkTasks_inThisDay.IndexOf(@"Написать отчет в Word") >= 0 && bytes > 0)
                            {
                                clerkTasks_inThisDay.Remove(@"Написать отчет в Word");
                                clerkMoves--; bytes--;
                            }
                            break;
                    }
                }
                if (clerkMoves == 0)
                {
                    WindowState = FormWindowState.Minimized;
                    clerkTurn = false;
                    hackerTasksInfo.Visible = true;
                    clerkMoves = 2;
                }
            }

            //ХОД ХАКЕРА
            else
            {
                //применяем карту
                if (!string.Equals(hackerAct, "") && string.Equals(isVisited_byHacker[r,c], "1"))
                {
                    isVisited_byHacker[r, c] = hackerAct;
                    switch(hackerAct)
                    {
                        case "ddos":
                            hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/ddos.png");
                            break;
                        case "fishing":
                            hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/fishing.png");
                            break;
                        case "supplychain":
                            hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/supplychain.png");
                            break;
                        case "sqlink":
                            hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/sqlink.png");
                            break;
                    }
                    hackerAct = "";
                    hackerMoves--;
                }
                //ходим
                else if (r * TILE_SIZE == hacker_y && Math.Abs(c * TILE_SIZE - hacker_x) == TILE_SIZE && Math.Abs(c * TILE_SIZE - hacker_x) > 0 || 
                    (c * TILE_SIZE == hacker_x && Math.Abs(r * TILE_SIZE - hacker_y) == TILE_SIZE && Math.Abs(r * TILE_SIZE - hacker_y) > 0))
                {
                    if (firewallX == r && firewallY == c)
                        MessageBox.Show("Тут стоит ФАЕРВОЛЛ!!!");
                    else
                    {
                        hacker_x = c * TILE_SIZE;
                        hacker_y = r * TILE_SIZE;
                        hackerMoves--;
                        isVisited_byHacker[r, c] = "1";
                    }  
                }
                //взаимодействуем с иконкой, на которой стоим
                else if (r * TILE_SIZE == hacker_y && c * TILE_SIZE == hacker_x)
                {
                    switch (matrix_paths[r, c])
                    {
                        case "../../iconpack/dir.png":
                            hackerCards_Hand.Add(@"../../spritepack/iconhackercards/troyan.png");
                            hackerMoves--; break;
                        case "../../iconpack/txt.png":
                            if (hackerCards_Hand.Count < inventorySize) hackerCards_Hand.Add(InitCard());
                            hackerMoves--; break;
                        case @"../../iconpack/1c.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Украсть личные данные сотрудников из базы данных") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Украсть личные данные сотрудников из базы данных");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/amigo.png":
                            if ((hackerTasks_inThisDay.IndexOf(@"Поставить «единички» коллегам клерка в Одноклассниках") >= 0 ||
                                hackerTasks_inThisDay.IndexOf(@"Внедрить вредоносные скрипты в сайты") >= 0) &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Поставить «единички» коллегам клерка в Одноклассниках");
                                hackerTasks_inThisDay.Remove(@"Внедрить вредоносные скрипты в сайты");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/avast.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Помешать работе антивируса Avast") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Помешать работе антивируса Avast");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/excel.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Украсть корпоративные данные из таблиц") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Украсть корпоративные данные из таблиц");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/garbage.png":
                            if ((hackerTasks_inThisDay.IndexOf(@"Добавить бэкдор корзины") >= 0 ||
                                hackerTasks_inThisDay.IndexOf(@"Заполучить данные корзины") >= 0) &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Добавить бэкдор корзины");
                                hackerTasks_inThisDay.Remove(@"Заполучить данные корзины");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/gimp.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Подменить метаданные фото кошки в Gimp") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Подменить метаданные фото кошки в Gimp");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/myworld.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Распространить вредоносного ПО через спам") >= 0 ||
                                hackerTasks_inThisDay.IndexOf(@"Отправка фишинговых писем через почту") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Распространить вредоносного ПО через спам");
                                hackerTasks_inThisDay.Remove(@"Отправка фишинговых писем через почту");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/paint.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Подменить открытку маме клерка в Paint") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Подменить открытку маме клерка в Paint");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/skype.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Получить доступ к вебке клерка") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Получить доступ к вебке клерка");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/telegram.png":
                            if ((hackerTasks_inThisDay.IndexOf(@"Взломать аккаунт клерка в Telegram") >= 0 ||
                                hackerTasks_inThisDay.IndexOf(@"Украсть компромат на клерка в Telegram") >= 0) &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Взломать аккаунт клерка в Telegram");
                                hackerTasks_inThisDay.Remove(@"Украсть компромат на клерка в Telegram");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/winrar.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Запаролить все архивы клерка") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Запаролить все архивы клерка");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                        case @"../../iconpack/word.png":
                            if (hackerTasks_inThisDay.IndexOf(@"Закинуть файл с вредоносным макросом в Word") >= 0 &&
                                hackerCards_Hand.IndexOf(@"../../spritepack/iconhackercards/troyan.png") >= 0)
                            {
                                hackerTasks_inThisDay.Remove(@"Закинуть файл с вредоносным макросом в Word");
                                hackerCards_Hand.Remove(@"../../spritepack/iconhackercards/troyan.png");
                                hackerMoves--;
                            }
                            break;
                    }
                    isVisited_byHacker[r, c] = "1";
                }

                //закончились ходы у хакера
                if (hackerMoves == 0)
                {
                    WindowState = FormWindowState.Minimized;
                    if (!ddos)
                    {
                        clerkTurn = true;
                        hackerTasksInfo.Visible = false;
                    } 
                    else ddos = false;

                    hackerMoves = 1;
                }
            }
            
            if (clerkTasks_inThisDay.Count == 0 || hackerTasks_inThisDay.Count == 0)
            {
                if (clerkTasks_inThisDay.Count == 0)
                {
                    clerk_WinPoints++;
                    clerkTurn = true;
                }
                else if (hackerTasks_inThisDay.Count == 0)
                {
                    hacker_WinPoints++;
                    clerkTurn = false;
                }

                days++;
                if (days == 3 || clerk_WinPoints > 1 || hacker_WinPoints > 1)
                    if (clerk_WinPoints > hacker_WinPoints)
                    {
                        MessageBox.Show("Победил клерк!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Победил хакер!");
                        Close();
                    }

                firewallX = -1;
                firewallY = -1;
                SQLink.Visible = false;
                numOfTasks++;
                TasksInit();
                VisitMatrixInit();
                
                clerk_x = 0;
                clerk_y = 0;
            }

            inventory.Refresh();
            Refresh();
        }

        private void inventory_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //решетка
            Pen myPen = new Pen(Color.Black, 1);
            int xpen = 0;
            for (int i = 0; i < inventorySize; i++)
            {
                g.DrawLine(myPen, xpen, 0, xpen, Height);
                xpen += CARDS_SIZE;
            }
            
            if (clerkTurn)
                for (int i = 0; i < clerkCards_Hand.Count; i++)
                {
                    int x = i * CARDS_SIZE;
                    pictureTile.Image = Image.FromFile(clerkCards_Hand[i]);
                    g.DrawImage(pictureTile.Image, new Rectangle(x, 0, CARDS_SIZE, CARDS_SIZE));
                }
            else
                for (int i = 0; i < hackerCards_Hand.Count; i++)
                {
                    int x = i * CARDS_SIZE;
                    pictureTile.Image = Image.FromFile(hackerCards_Hand[i]);
                    g.DrawImage(pictureTile.Image, new Rectangle(x, 0, CARDS_SIZE, CARDS_SIZE));
                }
        }

        private void inventory_MouseClick(object sender, MouseEventArgs e)
        {
            int i = e.X / CARDS_SIZE;
            if (clerkTurn)
            {
                switch (clerkCards_Hand[i])
                {
                    case @"../../spritepack/iconclerkcards/certific.png":
                        clerkAct = "certific";
                        break;
                    case @"../../spritepack/iconclerkcards/firewall.png":
                        clerkAct = "firewall";
                        break;
                    case @"../../spritepack/iconclerkcards/strongpass.png":
                        clerkAct = "strongpass";
                        break;
                }
            }
            else
            {
                switch (hackerCards_Hand[i])
                {
                    case @"../../spritepack/iconhackercards/ddos.png":
                        hackerAct = "ddos";
                        break;
                    case @"../../spritepack/iconhackercards/fishing.png":
                        hackerAct = "fishing";
                        break;
                    case @"../../spritepack/iconhackercards/supplychain.png":
                        hackerAct = "supplychain";
                        break;
                    case @"../../spritepack/iconhackercards/sqlink.png":
                        hackerAct = "sqlink";
                        break;
                }
            }
        }

        private void SQLink_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < clerkCards_Hand.Count; i++)
            {
                int x = i * CARDS_SIZE;
                pictureTile.Image = Image.FromFile(clerkCards_Hand[i]);
                g.DrawImage(pictureTile.Image, new Rectangle(x, 0, CARDS_SIZE, CARDS_SIZE));
            }
        }

        private void inventory_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            int i = me.X / CARDS_SIZE;

            if (clerkTurn)
                switch (clerkCards_Hand[i])
                {
                    case @"../../spritepack/iconclerkcards/certific.png":
                        MessageBox.Show("Цифровая подпись – проверяет программу на наличие вируса и удаляет его");
                        break;
                    case @"../../spritepack/iconclerkcards/firewall.png":
                        MessageBox.Show("Фаерволл - блокирует проход через клетку");
                        break;
                    case @"../../spritepack/iconclerkcards/strongpass.png":
                        MessageBox.Show("Стойкий пароль - хакер потратит два хода на взлом");
                        break;
                }
            else
                switch (hackerCards_Hand[i])
                {
                    case @"../../spritepack/iconhackercards/ddos.png":
                        MessageBox.Show("ДУДОС - клерк пропускает ход");
                        break;
                    case @"../../spritepack/iconhackercards/fishing.png":
                        MessageBox.Show("Фишинг - клерк теряет все байты");
                        break;
                    case @"../../spritepack/iconhackercards/supplychain.png":
                        MessageBox.Show("Атака на цепочку поставок данных - клерк сбрасывает случайную карту");
                        break;
                    case @"../../spritepack/iconhackercards/sqlink.png":
                        MessageBox.Show("SQL-инъекция - хакер видит руку клерка");
                        break;
                    case @"../../spritepack/iconhackercards/troyan.png":
                        MessageBox.Show("Троян - нужен хакеру для выполнения заданий");
                        break;
                }
        }
    }
}