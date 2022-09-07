using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Graph2D;
using Media;


namespace Robot
{
    public partial class frmMain : Form
    {
        #region Музыка и звуки
        public GameAudio Audio = null;
        public const int Max_Player_Count_In_Stack = 10;
        public const string AudioCollectionFileName = "GameResources\\sounds\\collection.ini";
        #endregion

        #region Игровой уровень
        public GameArea gameLevel = null;
        public int GameLevelIndex = 0;                       // номер текущего уровня  (для аудио оформления)       
        public int BackgroundMatrixIndex = 0;                // номер текущей матрицы подложки уровня (для визуального оформления)
        #endregion

        #region Коллекция юнитов
        public GameUnitListCollection UnitCollection = null;
        #endregion

        #region Управление выделением юнитов
        GameUnitSelection UnitSelection = new GameUnitSelection();
        #endregion

        #region Коллекция спрайтов
        BitMapMatrixListCollection Sprites = null;
        public const string ImageCollectionFileName = "GameResources\\images\\collection.ini";
        #endregion

        #region Глобальные переменные
        private int GlobalErrDrawCount = 0; // Счетчик глобальных ошибок
        public string ApplicationDirectory = /*@"d:\Projects\Source Codes\__Ветви кода\Робот\Robot\";//*/Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\"; // Пусть к рабочему каталогу
        public const string CursorFileName = "GameResources\\cursor\\cursor.cur";

        public int BackgroundTimerDelay = 100; // задержка смены кадров для фона (почвы)
        public int PlantTimerDelay = 100; // задержка смены кадров для растений
        public int VillyTimerDelay = 50; // задержка смены кадров для юнита "Villy"
        public int TargetTimerDelay = 50; // задержка смены кадров для юнита "Target"
        public const int TotalPlantCount = 6; // количество растений на уровне
        public const int TotalVillyCount = 10;  // количество юнитов "Villy"
        #endregion
           
        #region Поверхность рисования
        private Bitmap buffDrawMain = null;
        private Graphics graphDrawMain = null;
        #endregion

        public frmMain()
        {
            try
            {
                InitializeComponent();

                #region Замена курсора
                Cursor = GameUtils.LoadCustomCursor(ApplicationDirectory + CursorFileName);
                #endregion
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                Environment.Exit(1);
            }
        }
        
        private void CreateTargetPointerList()
        {
            UnitCollection.Add(new GameUnitList("target"));
        }

        private void CreateRocketList()
        {
            UnitCollection.Add(new GameUnitList("rocket"));
        }

        private void CreateTerrainUnitList(bool randomImage)
        {
            Random rnd = new Random();
            if (randomImage) BackgroundMatrixIndex = rnd.Next(0, Sprites[MatrixListIndex.Terrain].Count - 1);

            UnitCollection.Add(new GameUnitList("terrain"));

            int mapWidth = gameLevel.AreaSize.Width / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth;
            int mapHeight = gameLevel.AreaSize.Height / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight;

            for (int j = 0; j < mapHeight; j++)
                for (int i = 0; i < mapWidth; i++)
                {
                    UnitCollection[UnitCollection.Count - 1].Add(
                        Sprites[MatrixListIndex.Terrain],
                        BackgroundMatrixIndex,
                        (int)((i + 0.5) * Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth),
                        (int)((j + 0.5) * Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight),
                        BackgroundTimerDelay,
                        rnd.Next(200, 200 + BackgroundTimerDelay),
                        true,
                        true);
                }
        }

        private void CreatePlantUnitList()
        {
            Random rnd = new Random();
            UnitCollection.Add(new GameUnitList("plant"));

            for (int i = 0; i < TotalPlantCount; i++)
            {
                int matrix_idx = rnd.Next(0, Sprites[MatrixListIndex.Plant].Count);

                int X = rnd.Next(Sprites[MatrixListIndex.Plant][matrix_idx].SizeWidth / 2, gameLevel.AreaSize.Width - Sprites[MatrixListIndex.Plant][matrix_idx].SizeWidth);
                int Y = rnd.Next(Sprites[MatrixListIndex.Plant][matrix_idx].SizeHeight / 2, gameLevel.AreaSize.Height - Sprites[MatrixListIndex.Plant][matrix_idx].SizeHeight);

                UnitCollection[UnitCollection.Count - 1].Add(
                    Sprites[MatrixListIndex.Plant],
                    matrix_idx,
                    X,
                    Y,
                    PlantTimerDelay,
                    rnd.Next(200, 200 + PlantTimerDelay),
                    true,
                    true);
            }
        }

        private void CreatePersonUnitList()
        {
            Random rnd = new Random();

            UnitCollection.Add(new GameUnitList("villy"));
            UnitState.Villy.CreateIndex(Sprites[MatrixListIndex.Villy]);

            for (int i = 0; i < TotalVillyCount; i++)
            {
                int X = rnd.Next(Sprites[MatrixListIndex.Villy][UnitState.Villy.Stop].SizeWidth / 2, gameLevel.AreaSize.Width - Sprites[MatrixListIndex.Villy][UnitState.Villy.Stop].SizeWidth);
                int Y = rnd.Next(Sprites[MatrixListIndex.Villy][UnitState.Villy.Stop].SizeHeight / 2, gameLevel.AreaSize.Height - Sprites[MatrixListIndex.Villy][UnitState.Villy.Stop].SizeHeight);

                UnitCollection[UnitCollection.Count - 1].Add(
                    Sprites[MatrixListIndex.Villy],
                    UnitState.Villy.Stop,
                    X,
                    Y,
                    VillyTimerDelay,
                    rnd.Next(200, 200 + VillyTimerDelay),
                            true,
                            true);
            }
        }


        private void AddNewTargetPointer(Point Target)
        {
            Random rnd = new Random();
            UnitCollection[UnitListIndex.Target].Add(
                    Sprites[MatrixListIndex.Target],
                    0,
                    Target.X,
                    Target.Y,
                    TargetTimerDelay,
                    rnd.Next(200, 200 + TargetTimerDelay),
                            true,
                            false);
        }

        public int cmpUnitPos(int idx1, int idx2)
        {            
            if (UnitCollection[UnitListIndex.Villy][idx1].Position.Y > UnitCollection[UnitListIndex.Villy][idx2].Position.Y) return -1;
            if (UnitCollection[UnitListIndex.Villy][idx1].Position.Y < UnitCollection[UnitListIndex.Villy][idx2].Position.Y) return 1;
            return 0;
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            timerDraw.Enabled = false;

            try
            {
                if (pictMain.ClientRectangle.Width == 0) return;

                #region Инициализируем поверхности рисования, уточняем размеры поля 
                if (buffDrawMain == null) buffDrawMain = new Bitmap(pictMain.ClientRectangle.Width, pictMain.ClientRectangle.Height);
                if (graphDrawMain == null)
                {
                    graphDrawMain = Graphics.FromImage(buffDrawMain);
                    graphDrawMain.Clear(Color.Black);

                    #region Ограничение окна вывода
                    graphDrawMain.Clip = new Region(new Rectangle(gameLevel.ScreenOffset, gameLevel.WorkWindowSize));
                    #endregion
                }
                graphDrawMain.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion


                #region Фоновые события
                {
                    #region События движения мыши
                    OnMouseMove();
                    #endregion

                    #region Проверка фоновой музыки
                    Audio.CheckPlay();
                    #endregion                    
                }
                #endregion


                #region Действия 
                {
                    #region Движение юнита
                    int dist = 0;
                    int goCount = 0;
                    for (int k = 0; k < UnitCollection[UnitListIndex.Villy].Count; k++)
                    {
                        if ((UnitCollection[UnitListIndex.Villy][k].UnitStateIndex) == UnitState.Villy.Go)
                        {
                            dist = (int)GameUtils.Distance(UnitCollection[UnitListIndex.Villy][k].Position, UnitCollection[UnitListIndex.Villy][k].TargetPoint);
                            if (dist <= UnitCollection[UnitListIndex.Villy][k].Speed)
                            {
                                Unit_Villy.VillyStop(UnitCollection[UnitListIndex.Villy][k]);
                            }
                            else
                            {
                                UnitCollection[UnitListIndex.Villy][k].ActionDo_Move(false);
                                goCount++;
                            }
                        }
                        else
                        if ((UnitCollection[UnitListIndex.Villy][k].UnitStateIndex) == UnitState.Villy.AutoGun)
                        {
                            if (UnitCollection[UnitListIndex.Villy][k].phase.completed)
                                Unit_Villy.VillyStop(UnitCollection[UnitListIndex.Villy][k]);
                        }
                        
                    }
                    #endregion
                }
                #endregion
                

                #region Отрисовка кадра
                {
                    Point global_offset = gameLevel.GlobalOffset();
                    #region Отрисовка почвы
                    {
                        int x1 = gameLevel.WorkWindowOffset.X / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth; // пропуск спрайтов слева
                        int y1 = gameLevel.WorkWindowOffset.Y / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight; // пропуск спрайтов сверху
                        int x2 = gameLevel.WorkWindowSize.Width / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth + x1; // правый спрайт
                        int y2 = gameLevel.WorkWindowSize.Height / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight + y1; // нижний спрайт
                        int xCount = gameLevel.AreaSize.Width / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth; // количество спрайтов в строке

                        if (gameLevel.WorkWindowOffset.X % Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth != 0) x2++;
                        if (gameLevel.WorkWindowSize.Width % Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth != 0) x2++;
                        if (x2 > xCount) x2--;

                        if (gameLevel.WorkWindowOffset.Y % Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight != 0) y2++;
                        if (gameLevel.WorkWindowSize.Height % Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight != 0) y2++;
                        if (y2 > (gameLevel.AreaSize.Height / Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight)) y2--;


                        int yIndex = y1 * xCount;
                        int X_Left = gameLevel.GlobalOffset_X(x1 * Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth);
                        Point pos = new Point(X_Left, gameLevel.GlobalOffset_Y(y1 * Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight));

                        for (int y = y1; y < y2; y++, yIndex += xCount, pos.X = X_Left, pos.Y += Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeHeight)
                            for (int x = x1; x < x2; x++, pos.X += Sprites[MatrixListIndex.Terrain][BackgroundMatrixIndex].SizeWidth)
                            {
                                graphDrawMain.DrawImage(UnitCollection[UnitListIndex.Terrain][yIndex + x].GetCurrentSprite(), pos.X, pos.Y);
                                UnitCollection[UnitListIndex.Terrain][yIndex + x].Next();// анимация
                            }
                    }
                    #endregion

                    #region Отрисовка юнитов
                    {
                        Size size = default(Size);
                        Point leftTop = default(Point);
                        Pen selectionPen = new Pen(Color.LightGreen, 2);
                        Rectangle unitRect = new Rectangle();
                        Rectangle gameRect = gameLevel.WorkWindow;

                        int[] unitIndex = new int[UnitCollection[UnitListIndex.Villy].Count];
                        for (int i = 0; i < unitIndex.Length; i++) unitIndex[i] = i;
                        GameUtils.CompareFunction cmp = cmpUnitPos;
                        GameUtils.Sort(ref unitIndex, ref cmp, GameUtils.SortDirection.Asc);

                        for (int k = 0; k < unitIndex.Length; k++)
                        {
                            size = UnitCollection[UnitListIndex.Villy][unitIndex[k]].GetCurrentSprite().Size;
                            leftTop.X = (int)(UnitCollection[UnitListIndex.Villy][unitIndex[k]].Position.X - size.Width / 2);
                            leftTop.Y = (int)(UnitCollection[UnitListIndex.Villy][unitIndex[k]].Position.Y - size.Height / 2);

                            unitRect.Size = size;
                            unitRect.Location = leftTop;

                            if (unitRect.IntersectsWith(gameRect))
                            {
                                #region Юнит
                                graphDrawMain.DrawImage(UnitCollection[UnitListIndex.Villy][unitIndex[k]].GetCurrentSprite(), gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y));
                                UnitCollection[UnitListIndex.Villy][unitIndex[k]].Next();// анимация
                                #endregion

                                #region Маркер выбора
                                if (UnitCollection[UnitListIndex.Villy][unitIndex[k]].Selected)
                                {
                                    int mSize = 10;
                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y), gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y) + mSize);
                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y), gameLevel.GlobalOffset_X(leftTop.X) + mSize, gameLevel.GlobalOffset_Y(leftTop.Y));

                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X) + size.Width, gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height, gameLevel.GlobalOffset_X(leftTop.X) + size.Width, gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height - mSize);
                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X) + size.Width, gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height, gameLevel.GlobalOffset_X(leftTop.X) + size.Width - mSize, gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height);

                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height, gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height - mSize);
                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height, gameLevel.GlobalOffset_X(leftTop.X) + mSize, gameLevel.GlobalOffset_Y(leftTop.Y) + size.Height);

                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X) + size.Width, gameLevel.GlobalOffset_Y(leftTop.Y), gameLevel.GlobalOffset_X(leftTop.X) + size.Width, gameLevel.GlobalOffset_Y(leftTop.Y) + mSize);
                                    graphDrawMain.DrawLine(selectionPen, gameLevel.GlobalOffset_X(leftTop.X) + size.Width, gameLevel.GlobalOffset_Y(leftTop.Y), gameLevel.GlobalOffset_X(leftTop.X) + size.Width - mSize, gameLevel.GlobalOffset_Y(leftTop.Y));
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion

                    #region Отрисовка растений
                    {
                        Point leftTop = default(Point);
                        Size size = default(Size);

                        for (int k = 0; k < UnitCollection[UnitListIndex.Plant].Count; k++)
                        {
                            size = UnitCollection[UnitListIndex.Plant][k].GetCurrentSprite().Size;
                            leftTop.X = (int)(UnitCollection[UnitListIndex.Plant][k].Position.X - size.Width / 2);
                            leftTop.Y = (int)(UnitCollection[UnitListIndex.Plant][k].Position.Y - size.Height / 2);

                            graphDrawMain.DrawImage(UnitCollection[UnitListIndex.Plant][k].GetCurrentSprite(), gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y));
                            UnitCollection[UnitListIndex.Plant][k].Next();
                        }
                    }
                    #endregion

                    #region Отрисовка выделения
                    if (UnitSelection.Active)
                        graphDrawMain.FillRectangle(
                            new SolidBrush(Color.FromArgb(100, 100, 100, 255)),
                            gameLevel.GlobalOffset_X(UnitSelection.CurrentSelection().Left),
                            gameLevel.GlobalOffset_Y(UnitSelection.CurrentSelection().Top),
                            UnitSelection.CurrentSelection().Size.Width,
                            UnitSelection.CurrentSelection().Size.Height);
                    #endregion

                    #region Отрисовка указателей целей
                    {
                        Point leftTop = default(Point);
                        Size size = default(Size);
                        for (int k = 0; k < UnitCollection[UnitListIndex.Target].Count; k++)
                        {
                            size = UnitCollection[UnitListIndex.Target][k].GetCurrentSprite().Size;
                            leftTop.X = (int)(UnitCollection[UnitListIndex.Target][k].Position.X - size.Width / 2);
                            leftTop.Y = (int)(UnitCollection[UnitListIndex.Target][k].Position.Y - size.Height / 2);

                            graphDrawMain.DrawImage(UnitCollection[UnitListIndex.Target][k].GetCurrentSprite(), gameLevel.GlobalOffset_X(leftTop.X), gameLevel.GlobalOffset_Y(leftTop.Y));
                            UnitCollection[UnitListIndex.Target][k].Next();
                            if (UnitCollection[UnitListIndex.Target][k].phase.completed) UnitCollection[UnitListIndex.Target].RemoveAt(k--);
                        }
                    }
                    #endregion

                    #region Переключаем кадр               
                    pictMain.Image = buffDrawMain;
                    #endregion
                }
                #endregion    

                timerDraw.Enabled = true;
            }
            catch (Exception er)
            {
                if ((GlobalErrDrawCount++) < 2) MessageBox.Show("Ошибка в процедуре отрисовки. \n" + er.Message);
                timerDraw.Enabled = GlobalErrDrawCount < 2;
            }
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            graphDrawMain = null;
            buffDrawMain = null;
            pictMain.Left = 0;
            pictMain.Top = toolStripMain.Top + toolStripMain.Height;
            pictMain.Height = this.ClientSize.Height - pictMain.Top;
            pictMain.Width = this.ClientSize.Width;
        }






        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                LabelCaption.Text = "Загрузка...";
                LabelCaption.Visible = true;
                this.WindowState = FormWindowState.Maximized;
                Application.DoEvents();
                frmMain_ResizeEnd(null, null);
                toolStripMain.Enabled = false;

                #region Игровой уровень по умолчанию
                gameLevel = new GameArea(1024, 768);
                #endregion

                #region Спрайты и тайлы
                Sprites = new BitMapMatrixListCollection(ApplicationDirectory, ImageCollectionFileName);
                MatrixListIndex.CreateIndex(Sprites);
                #endregion

                #region Юниты (почва, деревья, фауна, боевые юниты)
                UnitCollection = new GameUnitListCollection();
                #endregion

                #region Музыка и звуки
                Audio = new GameAudio(Max_Player_Count_In_Stack, ApplicationDirectory + AudioCollectionFileName);
                #endregion

                #region Инициализация всех юнитов
                LabelCaption.Text = "Загрузка: инициализация...";
                Application.DoEvents();
                ResetLevel(true);
                #endregion

                LabelCaption.Visible = false;
                toolStripMain.Enabled = true;
                pictMain.Visible = true;
                timerDraw.Enabled = true;
            }
            catch (Exception er)
            {
                MessageBox.Show("Ошибка при загрузке главного окна. \n" + er.Message);
                Close();
            }
        }

        private void ResetLevel(bool randomImage = false)
        {
            SetWorkWindow();
            UnitCollection.Clear();

            CreateTerrainUnitList(randomImage);
            CreateTargetPointerList();
            CreatePlantUnitList();
            CreatePersonUnitList();
            CreateRocketList();
            UnitListIndex.CreateIndex(UnitCollection);
            Audio.Reset(GameLevelIndex);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();

            frm.trbVolumeMusic.Value = Audio.PlayerStackVolume[GameAudio.SFXIndex_PlayerStackType.Music];
            frm.trbVolumeBackSound.Value = Audio.PlayerStackVolume[GameAudio.SFXIndex_PlayerStackType.UnitSoundSpeakNotify];
            frm.trbVolumeCommand.Value = Audio.PlayerStackVolume[GameAudio.SFXIndex_PlayerStackType.UserCommand];

            frm.ShowDialog();
            if (frm.ShowResult == DialogResult.OK)
            {
                Audio.PlayerStackVolume[GameAudio.SFXIndex_PlayerStackType.Music] = frm.trbVolumeMusic.Value;
                Audio.PlayerStackVolume[GameAudio.SFXIndex_PlayerStackType.UnitSoundSpeakNotify] = frm.trbVolumeBackSound.Value;
                Audio.PlayerStackVolume[GameAudio.SFXIndex_PlayerStackType.UserCommand] = frm.trbVolumeCommand.Value;
                Audio.UpdatePlayerVolume();
            }
        }

        private void SetWorkWindow()
        {
            gameLevel.Resize(gameLevel.AreaSize.Width, gameLevel.AreaSize.Height);
            Rectangle screen = new Rectangle(0, 0, pictMain.ClientRectangle.Size.Width, pictMain.ClientRectangle.Size.Height);
            Rectangle wWindow = gameLevel.WorkWindow;
            screen.Offset(-screen.Size.Width / 2, -screen.Size.Height / 2);
            wWindow.Offset(-wWindow.Size.Width / 2, -wWindow.Size.Height / 2);
            wWindow.Intersect(screen);
            wWindow.Offset(gameLevel.WorkWindowSize.Width / 2, gameLevel.WorkWindowSize.Height / 2);
            gameLevel.ResizeWorkWindow(wWindow.Width, wWindow.Height);
            gameLevel.MoveWorkWindow(wWindow.Left, wWindow.Top);
            gameLevel.ScreenOffset = new Point((pictMain.ClientRectangle.Size.Width - wWindow.Size.Width) / 2, (pictMain.ClientRectangle.Size.Height - wWindow.Size.Height) / 2);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Завершить работу с программой?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа 'Робот Вилли'\nРазработал: Егоров Денис Алексеевич\nг.Нижнекамск 2019 год.");
        }

        
        public void GunStart(int posX, int posY)
        {
            PointF Target = new PointF(posX, posY);
            RectangleF targetArea = new RectangleF();
            bool selectedUnits = false;

            for (int i = 0; i < UnitCollection[UnitListIndex.Villy].Count; i++)
            {
                if (!UnitCollection[UnitListIndex.Villy][i].Selected) continue;
                selectedUnits = true;

                if (!UnitCollection[UnitListIndex.Villy][i].phase.completed)
                {
                    if (UnitCollection[UnitListIndex.Villy][i].UnitStateIndex == UnitState.Villy.AutoGun) continue;
                }

                Target.X = posX;
                Target.Y = posY;

                #region Проверяем выход за пределы рабочей зоны и корректируем координаты цели при необходимости
                {
                    targetArea = new RectangleF(UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Width / 2,
                                                UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Height / 2,
                                                gameLevel.AreaSize.Width - UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Width,
                                                gameLevel.AreaSize.Height - UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Height);

                    if ((Target.X < targetArea.Left) | (Target.X > targetArea.Right) | (Target.Y < targetArea.Top) | (Target.Y > targetArea.Bottom))
                        CorrectTarget(ref Target, UnitCollection[UnitListIndex.Villy][i].Position, targetArea);
                }
                #endregion

                PointF Vector0 = new PointF(0, -1);
                PointF VectorCursor;
                if (GameUtils.Distance(Target, UnitCollection[UnitListIndex.Villy][i].Position) > 0)
                    VectorCursor = new PointF(Target.X - UnitCollection[UnitListIndex.Villy][i].Position.X, Target.Y - UnitCollection[UnitListIndex.Villy][i].Position.Y);
                else
                    VectorCursor = new PointF(posX - UnitCollection[UnitListIndex.Villy][i].Position.X, posY - UnitCollection[UnitListIndex.Villy][i].Position.Y);
                GameUtils.NormalizeVector(ref VectorCursor);
                float angle = GameUtils.GetAngleVector(Vector0, VectorCursor);
                UnitCollection[UnitListIndex.Villy][i].RotateVariant(angle);

                Unit_Villy.VillyAutoGun(UnitCollection[UnitListIndex.Villy][i]);
            }

            if (selectedUnits)
            {
                AddNewTargetPointer(new Point(posX, posY));
                #region Добавление звука выстрела
                Audio.AddSound(
                        GameAudio.SFXIndex_PlayerStackType.UnitSoundSpeakNotify,
                        GameAudio.SFXIndex_PlayListProfile.Villy,
                        GameAudio.SFXIndex_Villy.AutoGun);
                #endregion                
            }
        }

        public void GoToTarget(int posX, int posY)
        {
            PointF Target = new PointF(posX, posY);
            RectangleF targetArea = new RectangleF();
            bool selectedUnits = false;

            for (int i = 0; i < UnitCollection[UnitListIndex.Villy].Count; i++)
            {
                if (!UnitCollection[UnitListIndex.Villy][i].Selected) continue;
                selectedUnits = true;
                Target.X = posX;
                Target.Y = posY;

                if (!UnitCollection[UnitListIndex.Villy][i].phase.completed)
                {
                    if (UnitCollection[UnitListIndex.Villy][i].UnitStateIndex == UnitState.Villy.AutoGun) continue;
                }

                #region Проверяем выход за пределы рабочей зоны и корректируем координаты цели при необходимости
                {
                    targetArea = new RectangleF(UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Width / 2,
                                                UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Height / 2,
                                                gameLevel.AreaSize.Width - UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Width,
                                                gameLevel.AreaSize.Height - UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size.Height);

                    if (Target.X < targetArea.Left) Target.X = targetArea.Left; else if (Target.X > targetArea.Right) Target.X = targetArea.Right;
                    if (Target.Y < targetArea.Top) Target.Y = targetArea.Top; else if (Target.Y > targetArea.Bottom) Target.Y = targetArea.Bottom;
                }
                #endregion

                PointF Vector0 = new PointF(0, -1);
                PointF VectorCursor;
                if (GameUtils.Distance(Target, UnitCollection[UnitListIndex.Villy][i].Position) > 0)
                    VectorCursor = new PointF(Target.X - UnitCollection[UnitListIndex.Villy][i].Position.X, Target.Y - UnitCollection[UnitListIndex.Villy][i].Position.Y);
                else
                    VectorCursor = new PointF(posX - UnitCollection[UnitListIndex.Villy][i].Position.X, posY - UnitCollection[UnitListIndex.Villy][i].Position.Y);
                GameUtils.NormalizeVector(ref VectorCursor);
                float angle = GameUtils.GetAngleVector(Vector0, VectorCursor);
                UnitCollection[UnitListIndex.Villy][i].RotateVariant(angle);

                int dist = (int)GameUtils.Distance(UnitCollection[UnitListIndex.Villy][i].Position, Target);                

                if (dist > UnitCollection[UnitListIndex.Villy][i].Speed)
                {
                    Unit_Villy.VillyGo(UnitCollection[UnitListIndex.Villy][i], Target,   MatrixListIndex.Villy);
                }
                else                
                {
                    Unit_Villy.VillyStop(UnitCollection[UnitListIndex.Villy][i]);
                }
            }

            if (selectedUnits)
            {
                AddNewTargetPointer(new Point(posX, posY));
                #region Добавление речи команды
                {
                    Audio.AddSound(
                        GameAudio.SFXIndex_PlayerStackType.UserCommand,
                        GameAudio.SFXIndex_PlayListProfile.Villy,
                        GameAudio.SFXIndex_Villy.CommandGo);
                }
                #endregion

            }
        }


        /// <summary>
        /// Коррекция координат цели в область экрана
        /// </summary>
        /// <param name="target">Цель (коррекция)</param>
        /// <param name="position">Исходная позиция</param>
        /// <param name="area">Ограниченная область экрана</param>
        private void CorrectTarget(ref PointF target, PointF position, RectangleF area)
        {
            PointF[] points = null;
            if (!GameUtils.LineIntersectRectangle(position, target, area, ref points)) return;

            if ((target.X < area.Left) & (GameUtils.Between(points[0].Y, area.Top, area.Bottom)))
            {
                target.X = area.Left;
                target.Y = points[0].Y;
            }
            if ((target.X > area.Right) & (GameUtils.Between(points[1].Y, area.Top, area.Bottom)))
            {
                target.X = area.Right;
                target.Y = points[1].Y;
            }

            if ((target.Y < area.Top) & (GameUtils.Between(points[2].X, area.Left, area.Right)))
            {
                target.Y = area.Top;
                target.X = points[2].X;
            }
            if ((target.Y > area.Bottom) & (GameUtils.Between(points[3].X, area.Left, area.Right)))
            {
                target.Y = area.Bottom;
                target.X = points[3].X;
            }
        }




        private void pictMain_MouseUp(object sender, MouseEventArgs e)
        {
            Point globalPos = gameLevel.ConvertMousePosToGlobal(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left: // Завершение построения области выбора юнитов
                    if (UnitSelection.DoubleClickSelect) break;
                    EndSelection(globalPos.X, globalPos.Y);
                    break;
                case MouseButtons.Right: // указание новой цели выбраным юнитам
                    GoToTarget(globalPos.X, globalPos.Y);
                    break;
                case MouseButtons.Middle:
                    GunStart(globalPos.X, globalPos.Y);
                    break;
            }

        }

        private void pictMain_MouseDown(object sender, MouseEventArgs e)
        {
            Point globalPos = gameLevel.ConvertMousePosToGlobal(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left: // начало построения области выбора юнитов
                    BeginSelection(globalPos.X, globalPos.Y);
                    break;
            }
        }

        private void BeginSelection(int X, int Y)
        {
            UnitSelection.EndPosition.X = UnitSelection.BeginPosition.X = X;
            UnitSelection.EndPosition.Y = UnitSelection.BeginPosition.Y = Y;
            UnitSelection.Active = true;
        }

        private void EndSelection(int X, int Y, bool select_once = false)
        {
            ResizeSelection(X, Y);
            UnitSelection.Active = false;
            Rectangle rect = UnitSelection.CurrentSelection();
            Rectangle unitRect = new Rectangle();
            if (!select_once)
            {
                for (int i = 0; i < UnitCollection[UnitListIndex.Villy].Count; i++)
                {
                    Size unitsize = UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size;
                    unitRect.X = (int)(UnitCollection[UnitListIndex.Villy][i].Position.X - unitsize.Width / 2);
                    unitRect.Y = (int)(UnitCollection[UnitListIndex.Villy][i].Position.Y - unitsize.Height / 2);
                    unitRect.Width = unitsize.Width;
                    unitRect.Height = unitsize.Height;
                    UnitCollection[UnitListIndex.Villy][i].Selected = GameUtils.RectIntersect(unitRect, rect);
                }
            }
            else
            {
                for (int i = 0; i < UnitCollection[UnitListIndex.Villy].Count; i++) UnitCollection[UnitListIndex.Villy][i].Selected = false;
                for (int i = 0; i < UnitCollection[UnitListIndex.Villy].Count; i++)
                {
                    Size unitsize = UnitCollection[UnitListIndex.Villy][i].GetCurrentSprite().Size;
                    unitRect.X = (int)(UnitCollection[UnitListIndex.Villy][i].Position.X - unitsize.Width / 2);
                    unitRect.Y = (int)(UnitCollection[UnitListIndex.Villy][i].Position.Y - unitsize.Height / 2);
                    unitRect.Width = unitsize.Width;
                    unitRect.Height = unitsize.Height;

                    if (GameUtils.RectIntersect(unitRect, rect))
                    {
                        UnitCollection[UnitListIndex.Villy][i].Selected = true;
                        break;
                    }
                }
            }
        }

        private void ResizeSelection(int X, int Y)
        {
            UnitSelection.EndPosition.X = X;
            UnitSelection.EndPosition.Y = Y;
        }

        private int ScreenMoveStep(int delta, int total)
        {
            return (int)Math.Round((float)delta / 3 /* total * 1000*/);
        }

        private void OnMouseMove()
        {
            Point e = MousePosition;
            e.X -= pictMain.Left;
            e.Y -= pictMain.Top;

            // определяем рамки, при выходе за которые будем двигать экран
            Rectangle MoveWindowBorder = new Rectangle(gameLevel.ScreenOffset.X + 100,
                                                        gameLevel.ScreenOffset.Y + 100,
                                                        gameLevel.WorkWindowSize.Width - 200,
                                                        gameLevel.WorkWindowSize.Height - 200);

            // двигаем экран
            gameLevel.MoveWorkWindow((e.X < MoveWindowBorder.Left) ? ScreenMoveStep(e.X - MoveWindowBorder.Left, gameLevel.WorkWindowSize.Width) :
                                     (e.X > MoveWindowBorder.Right) ? ScreenMoveStep(e.X - MoveWindowBorder.Right, gameLevel.WorkWindowSize.Width) : 0,
                                     (e.Y < MoveWindowBorder.Top) ? ScreenMoveStep(e.Y - MoveWindowBorder.Top, gameLevel.WorkWindowSize.Height) :
                                     (e.Y > MoveWindowBorder.Bottom) ? ScreenMoveStep(e.Y - MoveWindowBorder.Bottom, gameLevel.WorkWindowSize.Height) : 0);

            Point globalPos = gameLevel.ConvertMousePosToGlobal(e.X, e.Y);

            // изменение размера области выделения
            if (UnitSelection.Active)
                ResizeSelection(globalPos.X, globalPos.Y);
        }

        private void pictMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point globalPos = gameLevel.ConvertMousePosToGlobal(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left: // единичный выбор юнита
                    EndSelection(globalPos.X, globalPos.Y, true);
                    UnitSelection.DoubleClickSelect = true;
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timerDraw.Enabled = false;
            if ((++BackgroundMatrixIndex) >= Sprites[MatrixListIndex.Terrain].Count) BackgroundMatrixIndex = 0;

            #region Пользовательское окно настроек уровня
            {
                frmNewArea frm = new frmNewArea();
                if (gameLevel != null)
                {
                    frm.nudLevelFieldSizeX.Value = gameLevel.AreaSize.Width;
                    frm.nudLevelFieldSizeY.Value = gameLevel.AreaSize.Height;
                    frm.nudLevelNumber.Value = GameLevelIndex + 1;
                }

                frm.ShowDialog();
                if (frm.ShowResult == DialogResult.OK)
                {
                    GameLevelIndex = (int)frm.nudLevelNumber.Value - 1;
                    gameLevel = new GameArea((int)frm.nudLevelFieldSizeX.Value, (int)frm.nudLevelFieldSizeY.Value);
                    ResetLevel();
                    frmMain_ResizeEnd(null, null);
                }
            }
            #endregion



            ResetLevel();
            frmMain_ResizeEnd(null, null);
            timerDraw.Enabled = true;
        }

        private void pictMain_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Default;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }


    public static class Unit_Villy
    {
        /// <summary>
        /// скорость движения юнита Villy
        /// </summary>
        public const int VillySpeed = 5; 

        /// <summary>
        /// Старт действия "ходьба"
        /// </summary>
        /// <param name="speed">скорость</param>
        /// <param name="target">цель</param>
        /// <returns>Расстояние до цели</returns>
        public static int VillyGo(GameUnit villy, PointF target, float speed = VillySpeed)
        {
            villy.SetAction(UnitState.Villy.Go, true);
            villy.Speed = speed;
            villy.TargetPoint.X = (int)Math.Round(target.X);
            villy.TargetPoint.Y = (int)Math.Round(target.Y);
            return (int)GameUtils.Distance(villy.Position, villy.TargetPoint);
        }

        public static void VillyStop(GameUnit villy)
        {
            villy.SetAction(UnitState.Villy.Stop, true);
        }

        public static void VillyAutoGun(GameUnit villy)
        {
            villy.SetAction(UnitState.Villy.AutoGun, false);
        }
    }






    #region Состояние юнитов
    public static class UnitState
    {
        public static class Villy
        {
            public static int Stop;
            public static int Go;
            public static int AutoGun;
            public static int Bom;

            private static class Name
            {
                public const string Stop = "Villy_Stopping";
                public const string Go = "Villy_Move";
                public const string AutoGun = "Villy_AutoGun";
                public const string Bom = "Villy_Bom";
            }

            public static void CreateIndex(BitMapMatrixList list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].MatrixName.ToUpper().Equals(Name.AutoGun.ToUpper()))
                    {
                        AutoGun = i;
                        continue;
                    }
                    if (list[i].MatrixName.ToUpper().Equals(Name.Bom.ToUpper()))
                    {
                        Bom = i;
                        continue;
                    }
                    if (list[i].MatrixName.ToUpper().Equals(Name.Go.ToUpper()))
                    {
                        Go = i;
                        continue;
                    }
                    if (list[i].MatrixName.ToUpper().Equals(Name.Stop.ToUpper()))
                    {
                        Stop = i;
                        continue;
                    }
                }
            }
        }
    }
    #endregion


    public static class MatrixListIndex
    {
        public static int Terrain;
        public static int Plant;
        public static int Target;
        public static int Villy;
        public static int Rocket;

        private static class MatrixListName
        {
            public static string Terrain = "Terrain";
            public static string Plant = "Plant";
            public static string Target = "Target";
            public static string Villy = "Villy";
            public static string Rocket = "Rocket";
        }

        /// <summary>
        /// Индексация матриц
        /// </summary>
        /// <param name="collection">Коллекция списков матриц</param>
        public static void CreateIndex(BitMapMatrixListCollection collection)
        {
            Plant = collection.SearchByMatrixListName(MatrixListName.Plant);
            Rocket = collection.SearchByMatrixListName(MatrixListName.Rocket);
            Target = collection.SearchByMatrixListName(MatrixListName.Target);
            Terrain = collection.SearchByMatrixListName(MatrixListName.Terrain);
            Villy = collection.SearchByMatrixListName(MatrixListName.Villy);
        }
    }

    public static class UnitListIndex
    {
        
            public static int Terrain;
            public static int Plant;
            public static int Target;
            public static int Villy;
            public static int Rocket;
        

        private static class idxUnit_ListName
        {
            public static string Terrain = "Terrain";
            public static string Plant = "Plant";
            public static string Target = "Target";
            public static string Villy = "Villy";
            public static string Rocket = "Rocket";
        }

        /// <summary>
        /// Индексация списков
        /// </summary>
        /// <param name="throw_if_not_found">Вызвать исключение, если не найден ходя бы один элемент</param>
        /// <returns>Количество найденных элементов</returns>
        public static int CreateIndex(GameUnitListCollection collection, bool throw_if_not_found = true)
        {
            int cnt = 0;
            Villy = Terrain = Target = Plant = Rocket = - 1;
            if ((Plant = collection.GetIndexByName(idxUnit_ListName.Plant, throw_if_not_found)) >= 0) cnt++;
            if ((Rocket = collection.GetIndexByName(idxUnit_ListName.Rocket, throw_if_not_found)) >= 0) cnt++;
            if ((Target = collection.GetIndexByName(idxUnit_ListName.Target, throw_if_not_found)) >= 0) cnt++;
            if ((Terrain = collection.GetIndexByName(idxUnit_ListName.Terrain, throw_if_not_found)) >= 0) cnt++;
            if ((Villy = collection.GetIndexByName(idxUnit_ListName.Villy, throw_if_not_found)) >= 0) cnt++;
            return cnt;
        }
    }
}
