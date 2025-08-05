using StarGrabber.Bodies.Celestial;
using StarGrabber.Core.Interfaces;
using System;
using System.Diagnostics;

namespace StarGrabber.Core
{
    public class Engine : IEngine
    {

        public void Start()
        {
            TimeSpan threadSleep = TimeSpan.FromMilliseconds(5);
            TimeSpan shipTime = TimeSpan.FromMilliseconds(70);
            TimeSpan invaderMoveTime = TimeSpan.FromMilliseconds(100);
            TimeSpan enemySpawnTime;

            List<Invader> invaders = new();
            List<Bullet> bullets = new();
            List<Explosion> explosions = new();

            Stopwatch swatchGame = new();
            Stopwatch swatchInvadersSpawn = new();
            Stopwatch swatchShip = new();
            Stopwatch swatchInvader = new();

            Random random = new();

            int score = 0, blasted = 0;
            bool bulletFrame = default;
            bool shipRender = default;


            //Renders

            string[] bulletRenders = new string[]
            {
    " ",  // 0
	"»-", // 1
	"=-", // 2
    ">►■►", // 3
	"∙:∙" // 4
            };

            string[] shipRenders = new string[]
            {
	// 0
	@"             " + '\n' +
    @"             " + '\n' +
    @"             ",
	// 1
	@"  ∙    >-=■►  " + '\n' +
    @"x ◄■▬■▓░░░▓■▬ " + '\n' +
    @"  ☼    >-=■►  ",
	// 2
	@"  ☼    >-=■►  " + '\n' +
    @"+ ◄■▬■▓░░░▓■▬ " + '\n' +
    @"  ∙    >-=■►  ",
            };

            string[] InvaderRenders = new string[]
            {
	// 0
	@"   ◄■╗  -  " + '\n' +
    @"◄■█1░█■◄ - ",
	// 1
    @"     ◄♦> /█■ " + '\n' +
    @"◄▬■█7░░▓■█  ☼" + '\n' +
    @"     ◄♦> \█■ ",
	// 2
	@"◄♦▓■►<  " + '\n' +
    @" ◄♦3■►═<" + '\n' +
    @"◄♦▓■►<  ",
	// 3
	@"   ◄■\/" + '\n' +
    @"◄■▓3░░▓" + '\n' +
    @"   ◄■/\",
	// 4
	@"  ◄■▓" + '\n' +
    @"(  2◄" + '\n' +
    @"  ◄■▓",
            };

            string[] explosionRenders = new string[]
            {
	// 0
	@" ▓       ▓ " + '\n' +
    @"    ░█░    " + '\n' +
    @"   █▓ ▓█   " + '\n' +
    @"    ░█░    " + '\n' +
    @" ▓       ▓ ",
	// 1
	@"           " + '\n' +
    @"           " + '\n' +
    @"     *     " + '\n' +
    @"           " + '\n' +
    @"           ",
	// 2
	@"           " + '\n' +
    @"     *     " + '\n' +
    @"    *+*    " + '\n' +
    @"     *     " + '\n' +
    @"           ",
	// 3
	@"           " + '\n' +
    @"    *+*    " + '\n' +
    @"   *+*+*   " + '\n' +
    @"    *+*    " + '\n' +
    @"           ",
	// 4
	@"     *     " + '\n' +
    @"   *+*+*   " + '\n' +
    @"  *+* *+*  " + '\n' +
    @"   *+*+*   " + '\n' +
    @"     *     ",
	// 5
	@"    *+*    " + '\n' +
    @"  *+* *+*  " + '\n' +
    @" *+*   *+* " + '\n' +
    @"  *+* *+*  " + '\n' +
    @"    *+*    ",
	// 6
	@"   *   *   " + '\n' +
    @" **     ** " + '\n' +
    @"**       **" + '\n' +
    @" **     ** " + '\n' +
    @"   *   *   ",
	// 7
	@"   .   .   " + '\n' +
    @" .       . " + '\n' +
    @".         ." + '\n' +
    @" .       . " + '\n' +
    @"   .   .   ",
            };

            string[] achievementRenders = new string[]
            {
    // 0
    @"    -+-    " + '\n' +
    @"     |     " + '\n' +
    @"           ",
	// 1
    @"   -   -   " + '\n' +
    @"    /|\    " + '\n' +
    @"           ",
	// 2
    @" * - + - * " + '\n' +
    @"    *|*    " + '\n' +
    @"           ",
	// 3
    @"* --   -- *" + '\n' +
    @"    /|\    " + '\n' +
    @"   * | *   ",
	// 4
    @" -       - " + '\n' +
    @"   /   \   " + '\n' +
    @"     |     ",
	// 5
    @" ∙       ∙ " + '\n' +
    @"           " + '\n' +
    @"  /  ∙  \  ",
            };

            Console.WriteLine(@"    _____ __                ______           __    __             ");
            Console.WriteLine(@"   / ___// /_____ ______   / ____/________ _/ /_  / /_  ___  _____");
            Console.WriteLine(@"   \__ \/ __/ __ `/ ___/  / / __/ ___/ __ `/ __ \/ __ \/ _ \/ ___/");
            Console.WriteLine(@"  ___/ / /_/ /_/ / /     / /_/ / /  / /_/ / /_/ / /_/ /  __/ /    ");
            Console.WriteLine(@" /____/\__/\__,_/_/      \____/_/   \__,_/_.___/_.___/\___/_/     ");

            Console.WriteLine(".....................................: Star Grabber v.0.1 :.");
            Console.WriteLine();

            Console.Write(" What's your name commander : ");
            string name = Console.ReadLine();

            Console.Write(" Choose difficulty level [1 - 10]: ");
            int tmpDiff = int.Parse(Console.ReadLine());
            Rank initialRank = (Rank)tmpDiff;
            Console.SetCursorPosition(36, 8);
            Console.Write($" ► Promoted to ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(initialRank);
            Console.ForegroundColor = ConsoleColor.White;

            enemySpawnTime = TimeSpan.FromSeconds(10.0 / tmpDiff);

            Console.Write(" Enter game field width [80 - 200]: ");
            int fieldW = int.Parse(Console.ReadLine());
            Console.Write(" Enter game field height [24 - 60]: ");
            int fieldH = int.Parse(Console.ReadLine());

            Console.WriteLine();





            Console.Clear();

            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = fieldW;
                Console.WindowHeight = fieldH;
            }

            int height = Console.WindowHeight;
            int width = Console.WindowWidth;
            Player player = new() { Left = 2, Top = height / 2, Rank = initialRank, Name = name };

            Console.CursorVisible = false;

            swatchGame.Restart();
            swatchInvadersSpawn.Restart();
            swatchShip.Restart();
            swatchInvader.Restart();

            int multi = 0;

            while (true)
            {
                //Window Resize Error

                if (height != Console.WindowHeight || width != Console.WindowWidth)
                {
                    Console.Clear();
                    Console.Write("Console window resized. Game closed.");
                    return;
                }





                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("▲▼▲ ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write((player.Rank + " " + player.Name).ToUpper());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" ▲▼▲ ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Score ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(score);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" ▲▼▲ ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Blasted ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(blasted);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" ▲▼▲ ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Missiles ");
                if (player.Missiles > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(player.Missiles);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" ▲▼▲");
                Console.ForegroundColor = ConsoleColor.White;

                //Update Invaders

                if (swatchInvadersSpawn.Elapsed > enemySpawnTime)
                {
                    int h = 0, r = 0;

                    int modifier = random.Next(5);

                    //Invaders health & reward modifier
                    switch (modifier)
                    {
                        case 0:
                            h = 2; r = 300;
                            break;
                        case 1:
                            h = 14; r = 400;
                            break;
                        case 2:
                            h = 6; r = 200;
                            break;
                        case 3:
                            h = 6; r = 200;
                            break;
                        case 4:
                            h = 4; r = 150;
                            break;
                    }

                    invaders.Add(new Invader
                    {
                        Health = h,
                        Frame = modifier,
                        Top = random.Next(height - 3),
                        Left = width,
                        Reward = r
                    });
                    swatchInvadersSpawn.Restart();
                }

                if (swatchInvader.Elapsed > invaderMoveTime)
                {
                    foreach (Invader Invader in invaders)
                    {
                        if (Invader.Left < width)
                        {
                            Console.SetCursorPosition(Invader.Left, Invader.Top);
                            Erase(InvaderRenders[Invader.Frame]);
                        }
                        Invader.Left--;
                        if (Invader.Left <= 0)
                        {
                            Console.Clear();
                            Console.Write("Game Over. Score: " + score + ".");
                            return;
                        }
                    }
                    swatchInvader.Restart();
                }


                //Update Player

                bool playerRenderRequired = false;

                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            Console.SetCursorPosition(player.Left, player.Top);
                            Render(shipRenders[default], true);
                            player.Top = Math.Max(player.Top - 1, 0);
                            playerRenderRequired = true;
                            break;
                        case ConsoleKey.DownArrow:
                            Console.SetCursorPosition(player.Left, player.Top);
                            Render(shipRenders[default], true);
                            player.Top = Math.Min(player.Top + 1, height - 3);
                            playerRenderRequired = true;
                            break;
                        case ConsoleKey.RightArrow:
                            bullets.Add(new Bullet
                            {
                                Left = player.Left + 12,
                                Top = player.Top + 1,
                                Frame = (bulletFrame = !bulletFrame) ? 1 : 2,
                            });
                            break;
                        case ConsoleKey.Spacebar:
                            if (player.Missiles > 0)
                            {
                                bullets.Add(new Bullet
                                {
                                    Left = player.Left + 12,
                                    Top = player.Top + 1,
                                    Frame = 3,
                                    Damage = 2,
                                });
                                player.Missiles--;
                            }
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            Console.Write("Game was closed.");
                            return;
                    }
                }
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }


                //Update Bullets

                HashSet<Bullet> bulletRemovals = new();
                foreach (Bullet bullet in bullets)
                {
                    Console.SetCursorPosition(bullet.Left, bullet.Top);
                    Console.Write(bulletRenders[default]);
                    bullet.Left++;
                    if (bullet.Left >= width || bullet.Frame is 4)
                    {
                        bulletRemovals.Add(bullet);
                    }
                    HashSet<Invader> InvaderRemovals = new();
                    foreach (Invader Invader in invaders)
                    {
                        //Dodge by random can be added
                        if (Invader.Left <= bullet.Left &&
                            Invader.Top <= bullet.Top &&
                            CollisionCheck(
                            (bulletRenders[bullet.Frame], bullet.Left, bullet.Top),
                            (InvaderRenders[Invader.Frame], Invader.Left, Invader.Top)))
                        {
                            bullet.Frame = 4;
                            Invader.Health -= bullet.Damage;
                            Console.SetCursorPosition(Invader.Left, Invader.Top);
                            Console.Write(bullet.Damage);

                            if (Invader.Health <= 0)
                            {
                                score += Invader.Reward;
                                blasted++;

                                if (blasted > 0 && blasted % 100 == 0)
                                {
                                    if (player.Rank < (Rank)10)
                                    {
                                        player.Rank++;
                                        enemySpawnTime = TimeSpan.FromSeconds(10.0 / ++tmpDiff);

                                        HLine(0, fieldW / 2 - 1, 0, ' ');

                                    }
                                    else
                                    {
                                        player.Rank = (Rank)10;
                                    }
                                }

                                multi = score / 1000;

                                if (score >= 1000 && score >= 1000 * multi && score <= 1000 * multi + 150)
                                {
                                    player.Missiles += 5;
                                }

                                Console.SetCursorPosition(Invader.Left, Invader.Top);
                                Erase(InvaderRenders[Invader.Frame]);
                                InvaderRemovals.Add(Invader);
                                explosions.Add(new Explosion
                                {
                                    Left = bullet.Left - 5,
                                    Top = Math.Max(bullet.Top - 2, 0),

                                });
                            }
                        }
                    }
                    invaders.RemoveAll(InvaderRemovals.Contains);
                }
                bullets.RemoveAll(bulletRemovals.Contains);


                //Update & Render Explosions

                HashSet<Explosion> explosionRemovals = new();
                foreach (Explosion explosion in explosions)
                {
                    if (explosion.Frame > 0)
                    {
                        Console.SetCursorPosition(explosion.Left, explosion.Top);
                        Erase(explosionRenders[explosion.Frame - 1]);
                    }
                    if (explosion.Frame < explosionRenders.Length)
                    {
                        Console.SetCursorPosition(explosion.Left, explosion.Top);
                        Render(explosionRenders[explosion.Frame]);
                    }
                    explosion.Frame++;
                    if (explosion.Frame > explosionRenders.Length)
                    {

                        explosionRemovals.Add(explosion);
                    }
                }
                explosions.RemoveAll(explosionRemovals.Contains);


                //Render Player

                if (swatchShip.Elapsed > shipTime)
                {
                    shipRender = !shipRender;
                    swatchShip.Restart();
                    playerRenderRequired = true;
                }
                if (playerRenderRequired)
                {
                    Console.SetCursorPosition(player.Left, player.Top);
                    Render(shipRenders[shipRender ? 1 : 2]);
                }


                //Render Invaders

                foreach (Invader Invader in invaders)
                {
                    if (Invader.Left < width)
                    {
                        Console.SetCursorPosition(Invader.Left, Invader.Top);
                        Render(InvaderRenders[Invader.Frame]);
                    }
                }


                //Render Bullets

                foreach (Bullet bullet in bullets)
                {
                    Console.SetCursorPosition(bullet.Left, bullet.Top);
                    Render(bulletRenders[bullet.Frame]);
                }

                Thread.Sleep(threadSleep);
            }


            void Render(string unit, bool renderSpace = false)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                foreach (char c in unit)
                    if (c is '\n')
                        Console.SetCursorPosition(x, ++y);
                    else if (Console.CursorLeft < width - 1 && (c is not ' ' || renderSpace))
                        Console.Write(c);
                    else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            }


            void Erase(string unit)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                foreach (char c in unit)
                    if (c is '\n')
                        Console.SetCursorPosition(x, ++y);
                    else if (Console.CursorLeft < width - 1 && c is not ' ')
                        Console.Write(' ');
                    else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            }


            bool CollisionCheck((string String, int Left, int Top) A, (string String, int Left, int Top) B)
            {
                char[,] buffer = new char[width, height];
                int left = A.Left;
                int top = A.Top;
                foreach (char c in A.String)
                {
                    if (c is '\n')
                    {
                        left = A.Left;
                        top++;
                    }
                    else if (left < width && top < height && c != ' ')
                    {
                        buffer[left++, top] = c;
                    }
                }
                left = B.Left;
                top = B.Top;
                foreach (char c in B.String)
                {
                    if (c is '\n')
                    {
                        left = A.Left;
                        top++;
                    }
                    else if (left < width && top < height && c != ' ')
                    {
                        if (buffer[left, top] != default)
                        {
                            return true;
                        }
                        buffer[left++, top] = c;
                    }
                }
                return false;
            }


            void HLine(int x1, int x2, int y, char c)
            {
                Console.SetCursorPosition(x1, y);

                for (int i = 0; i < x2; i++)
                {
                    Console.Write(c);
                }
            }

            void VLine(int x, int y1, int y2, char c)
            {
                Console.SetCursorPosition(x, y1);

                for (int i = 0; i < y2; i++)
                {
                    Console.Write(c);
                }
            }
        }

        class Player
        {
            public string Name;
            public Rank Rank = 0;
            public int Left;
            public int Top;
            public int Missiles = 5;
        }

        class Bullet
        {
            public int Left;
            public int Top;
            public int Frame;
            public int Damage = 1;
        }

        class Explosion
        {
            public int Left;
            public int Top;
            public int Frame;
            public int FrameOffset = 0;
        }

        class Invader
        {
            public int Frame;
            public int Left;
            public int Top;
            public int Health;
            public int Reward;
        }

        enum Rank
        {
            Militia = 1,
            Private,
            Corporal,
            Leutenant,
            Captain,
            Major,
            Colonel,
            General,
            Admiral,
            Emperor
        }
    }
}
