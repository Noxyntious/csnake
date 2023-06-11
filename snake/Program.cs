using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
SettingsSet:
int speed = 20;
int deaths = 0;
(int width, int height) dimensions = (10, 10);
bool noclip = false;
int ptsneeded = 5;
int ptsNeededUntilNextScissor = 0;
List<int> scores = new();
int scorecount = 1;
string snakechar = "██";
string wallchar = "▒▒";
string applechar = "()";
string scissorchar = "//";
string name = "csnake";
TitleScreenRender:
List<(int, int)> snake = new()
{
    (1, dimensions.height / 2),
    (2, dimensions.height / 2),
    (3, dimensions.height / 2),
    (4, dimensions.height / 2)  
};
bool alive = true;
int loops = 0;
string direction = "r";
(int, int) head = snake.Last();
int score = 0;
(int, int) apple = CreateApple(dimensions, snake);
(int, int) scissor = CreateScissors(dimensions, snake);
//(int, int)[] obstacles = 
Console.WriteLine("                         __      \r\n  ______________  ____ _/ /_____ \r\n / ___/ ___/ __ \\/ __ `/ //_/ _ \\\r\n/ /__(__  ) / / / /_/ / ,< /  __/\r\n\\___/____/_/ /_/\\__,_/_/|_|\\___/");
Console.WriteLine("C# remake by Noxyntious");
Console.Write(Environment.NewLine);
Console.WriteLine("Press Enter to play");
Console.WriteLine("Press O for options");
Console.WriteLine("Press H for help");
Console.WriteLine("Press S for scores");
Console.WriteLine("Press E to exit");
Console.Write(Environment.NewLine);
Console.Write(">> ");
string choice = Console.ReadLine().ToLower();
if (choice == "h")
{
    Console.Clear();
    Console.WriteLine("                         __      \r\n  ______________  ____ _/ /_____ \r\n / ___/ ___/ __ \\/ __ `/ //_/ _ \\\r\n/ /__(__  ) / / / /_/ / ,< /  __/\r\n\\___/____/_/ /_/\\__,_/_/|_|\\___/");
    Console.WriteLine("C# remake by Noxyntious");
    Console.Write(Environment.NewLine);
    Console.WriteLine("Use WASD or Arrow keys to move");
    Console.WriteLine("Collect apples to get points");
    Console.WriteLine("If you run into the wall or yourself, you die");
    Console.Write(Environment.NewLine);
    Console.WriteLine("Press any key to go back");
    Console.ReadKey();
    Console.Clear();
    goto TitleScreenRender;
}
if (choice == "s")
{
    Console.Clear();
    Console.WriteLine("                         __      \r\n  ______________  ____ _/ /_____ \r\n / ___/ ___/ __ \\/ __ `/ //_/ _ \\\r\n/ /__(__  ) / / / /_/ / ,< /  __/\r\n\\___/____/_/ /_/\\__,_/_/|_|\\___/");
    Console.WriteLine("C# remake by Noxyntious");
    Console.Write(Environment.NewLine);
    Console.WriteLine("Scores");
    Console.Write(Environment.NewLine);
    scorecount = 1;
    if (scores.Count == 0)
    {
        Console.WriteLine("No scores set.");
    }
    foreach (int snakescore in scores)
    {
        Console.WriteLine(scorecount + ". " + snakescore + " set by " + name);
        scorecount++;
    }
    Console.Write(Environment.NewLine);
    Console.WriteLine("Press any key to go back");
    Console.ReadKey();
    Console.Clear();
    goto TitleScreenRender;
}
if (choice == "e")
{
    Environment.Exit(0);
}
if (choice == "wwssadadba")
{
    goto OldAhhSnakeEasterEgg;
}
if (choice == "o")
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("                         __      \r\n  ______________  ____ _/ /_____ \r\n / ___/ ___/ __ \\/ __ `/ //_/ _ \\\r\n/ /__(__  ) / / / /_/ / ,< /  __/\r\n\\___/____/_/ /_/\\__,_/_/|_|\\___/");
        Console.WriteLine("C# remake by Noxyntious");
        Console.Write(Environment.NewLine);
        Console.WriteLine("OPTIONS MENU");
        Console.Write(Environment.NewLine);
        Console.WriteLine("-----");
        Console.WriteLine("GAMEPLAY OPTIONS");
        Console.Write(Environment.NewLine);
        Console.WriteLine("1. Change width and height of play field (current: " + dimensions.width + "x" + dimensions.height + ")");
        Console.WriteLine("2. Change speed (lower is faster) (current: " + speed + ")");
        Console.WriteLine("3. Change when to start spawning scissors (current score needed: " + ptsneeded + ")");
        Console.Write(Environment.NewLine);
        Console.WriteLine("-----");
        Console.WriteLine("EXPERIMENTAL OPTIONS");
        Console.WriteLine("This might break something! Use at your own risk.");
        Console.Write(Environment.NewLine);
        Console.WriteLine("4. Toggle Noclip (current: " + noclip + ")");
        Console.WriteLine("5. Change snake character (current: '" + snakechar + "' )");
        Console.WriteLine("6. Change wall character (current: '" + wallchar + "' )");
        Console.WriteLine("7. Change apple character (current: '" + applechar + "' )");
        Console.WriteLine("8. Change scissor character (current: '" + scissorchar + "' )");
        Console.Write(Environment.NewLine);
        Console.WriteLine("-----");
        Console.WriteLine("SCORE SUBMISSION OPTIONS");
        Console.Write(Environment.NewLine);
        Console.WriteLine("9. Change name used for score submission (current: '" + name + "' )"); ;
        Console.Write(Environment.NewLine);
        Console.WriteLine("-----");
        Console.WriteLine("10. Exit");
        Console.WriteLine("99. Reset all settings to default");
        string optionsChoice = Console.ReadLine();
        if (optionsChoice == "1")
        {
            Console.WriteLine("Enter new width:");
            dimensions.width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new height:");
            dimensions.height = Convert.ToInt32(Console.ReadLine());
        }
        else if (optionsChoice == "2")
        {
            Console.WriteLine("Enter new speed:");
            speed = Convert.ToInt32(Console.ReadLine());
            if (speed < 1)
            {
                Console.WriteLine("You cannot set a speed lower than 1. Resetting to default.");
                speed = 20;
                Thread.Sleep(500);
            }
            Console.Clear();
        }
        else if (optionsChoice == "4")
        {
            if (noclip == false)
            {
                noclip = true;
                Console.WriteLine("Noclip enabled");
            }
            else if (noclip == true)
            {
                noclip = false;
                Console.WriteLine("Noclip disabled");
            }
        }
        else if (optionsChoice == "5")
        {
            Console.WriteLine("Enter new character (Max 2):");
            snakechar = Console.ReadLine();
            if (snakechar.Length > 2)
            {
                Console.WriteLine("Too many characters, resetting to default.");
                snakechar = "██";
                Thread.Sleep(500);
            }
            else if (snakechar.Length == 1)
            {
                snakechar = snakechar + snakechar;
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else if (snakechar.Length == 2)
            {
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Invalid character, resetting to default.");
                snakechar = "██";
                Thread.Sleep(500);
            }
        }
        else if (optionsChoice == "6")
        {
            Console.WriteLine("Enter new character (Max 2):");
            wallchar = Console.ReadLine();
            if (wallchar.Length > 2)
            {
                Console.WriteLine("Too many characters, resetting to default.");
                wallchar = "▒▒";
                Thread.Sleep(500);
            }
            else if (wallchar.Length == 1)
            {
                wallchar = wallchar + wallchar;
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else if (wallchar.Length == 2)
            {
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Invalid character, resetting to default.");
                wallchar = "▒▒";
                Thread.Sleep(500);
            }
        }
        else if (optionsChoice == "7")
        {
            Console.WriteLine("Enter new character (Max 2):");
            applechar = Console.ReadLine();
            if (applechar.Length > 2)
            {
                Console.WriteLine("Too many characters, resetting to default.");
                applechar = "()";
                Thread.Sleep(500);
            }
            else if (applechar.Length == 1)
            {
                applechar = applechar + applechar;
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else if (applechar.Length == 2)
            {
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Invalid character, resetting to default.");
                applechar = "()";
                Thread.Sleep(500);
            }
        }
        else if (optionsChoice == "8")
        {
            Console.WriteLine("Enter new character (Max 2):");
            scissorchar = Console.ReadLine();
            if (scissorchar.Length > 2)
            {
                Console.WriteLine("Too many characters, resetting to default.");
                scissorchar = "()";
                Thread.Sleep(500);
            }
            else if (scissorchar.Length == 1)
            {
                scissorchar =  scissorchar + scissorchar;
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else if (scissorchar.Length == 2)
            {
                Console.WriteLine("Character saved.");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Invalid character, resetting to default.");
                scissorchar = "()";
                Thread.Sleep(500);
            }
        }
        else if (optionsChoice == "9")
        {
            Console.WriteLine("Enter new name: (Max 16 characters)");
            name = Console.ReadLine();
            if (name.Length == 0)
            {
                Console.WriteLine("No name entered, resetting to default.");
                name = "csnake";
                Thread.Sleep(500);
            }
            if (name.Length > 16)
            {
                Console.WriteLine("Name too long, resetting to default.");
                name = "csnake";
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("Name saved.");
                Thread.Sleep(500);
            }
        }
        else if (optionsChoice == "10")
        {
            Console.Clear();
            goto TitleScreenRender;
        }
        else if (optionsChoice == "99")
        {
            Console.Clear();
            goto SettingsSet;
        }

    }
}


GameStart:
string ReadDirection(string direction)
{
        if (Console.KeyAvailable)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                direction = "u";
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                direction = "l";
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                direction = "d";
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                direction = "r";
                    break;
                case ConsoleKey.Backspace:
                direction = "EXIT";
                break;
            }
        }

    return direction;
}


Console.Clear();
void MakeWall() { 
for (int i = 0; i <= dimensions.width; i++)
{
    for (int j = 0; j <= dimensions.height; j++)
    {
        if(i == 0 || j == 0 || i == dimensions.width || j == dimensions.height) 
        {
            Console.SetCursorPosition(i * 2, j);
            Console.Write(wallchar);
        }
    }
}
}
MakeWall();
bool Validate(List<(int, int)> snake, (int width, int height) dimensions, (int, int) head) 
{
    if(head.Item1 == dimensions.width || head.Item1 < 1 || head.Item2 > dimensions.height - 1 || head.Item2 < 1)
    {
        return false;
    }
    if (snake.Contains(head))
        return false;

    return true;
}
(int, int) CreateApple((int width, int height) dimensions, List<(int, int)> snake)
{
    Random rnd = new();
    (int, int) apple = new();
    bool ok = true;
    while(ok)
    {
        ok = false;
        apple = (rnd.Next(1, dimensions.width), rnd.Next(1, dimensions.height));

        foreach ((int, int) bodypart in snake)
        {
            if (bodypart == apple)
            {
                ok = true;
            }
        }
    }
    return apple;
}
(int, int) CreateScissors((int width, int height) dimensions, List<(int, int)> snake)
{
    Random rnd = new();
    (int, int) scissor = new();
    bool ok = true;
    while (ok)
    {
        ok = false;
        apple = (rnd.Next(1, dimensions.width), rnd.Next(1, dimensions.height));
        scissor = (rnd.Next(1, dimensions.width), rnd.Next(1, dimensions.height));

        foreach ((int, int) bodypart in snake)
        {
            if (bodypart == scissor)
            {
                ok = true;
            }
            if (scissor == apple)
            {
                ok = true;
            }
        }
    }
    return scissor;
}
void Render(List<(int, int)> snake, (int, int) apple, (int, int) scissor)
{
    foreach ((int, int) bodypart in snake)
    {
        Console.SetCursorPosition(bodypart.Item1 * 2, bodypart.Item2);
        Console.Write(snakechar);
    }
    Console.SetCursorPosition(scissor.Item1 * 2, scissor.Item2);
    Console.Write(scissorchar);
    Console.SetCursorPosition(apple.Item1 * 2, apple.Item2);
    Console.Write(applechar);
}
Render(snake, apple, scissor);
while (alive) {
    Thread.Sleep(1);
    loops++;
    Console.SetCursorPosition(0, dimensions.height + 1);
    direction = ReadDirection(direction);
    if (loops == speed)
    {
        loops = 0;
        switch (direction)
        {
            case "r":
                head.Item1 += 1;
                break;
            case "u":
                head.Item2 -= 1;
                break;
            case "l":
                head.Item1 -= 1;
                break;
            case "d":
                head.Item2 += 1;
                break;
            case "EXIT":
                goto gameEnd;

        }
        if (head == apple)
        {
            score++;
            ptsNeededUntilNextScissor++;
            apple = CreateApple(dimensions, snake);
            Console.SetCursorPosition(0, dimensions.height + 1);
            Console.Write("Score: " + score);
            if (noclip == true)
            {
                Console.SetCursorPosition(0, dimensions.height + 2);
                Console.Write("Noclip enabled! Press BKSP to exit.");
                Console.SetCursorPosition(0, dimensions.height + 3);
                Console.WriteLine("Deaths: " + deaths);
            }
        }
        else if (head == scissor)
        {
            snake.RemoveRange(0, snake.Count / 3 + 1 );
            scissor = CreateScissors(dimensions, snake);
            if (ptsNeededUntilNextScissor >= ptsneeded)
            {

            }
            Console.Clear(); 
            MakeWall();

        }
            
        else
        {
            Console.SetCursorPosition(snake[0].Item1 * 2, snake[0].Item2);
            Console.Write("  ");
            snake.RemoveAt(0);
        }

        if (noclip == false)
        {
            if (!Validate(snake, dimensions, head))
                break;
        }

        else if (noclip == true)
        {
            if (!Validate(snake, dimensions, head))
                deaths++;
        }

        snake.Add(head);
        Render(snake, apple, scissor);
        Console.SetCursorPosition(0, dimensions.height + 1);
        Console.Write("Score: " + score);
        if (noclip == true)
        {
            Console.SetCursorPosition(0, dimensions.height + 2);
            Console.Write("Noclip enabled! Press BKSP to exit.");
            Console.SetCursorPosition(0, dimensions.height + 3);
            Console.WriteLine("Deaths: " + deaths);
        }
    
    }

}

gameEnd:
Console.Clear();
Console.WriteLine("Your score is " + score);
if (noclip == true)
{
    Console.WriteLine("Noclip enabled! Score not saved.");
}
else if (noclip == false)
{
    if (score > 0)
    {
        Console.WriteLine("Score saved.");
        scores.Add(score);
        scores.Sort();
        scores.Reverse();
    }

}
Console.WriteLine("Return to title or quit game? r/q");
string playagain = Console.ReadLine().ToLower();
if (playagain == "q")
{
    Environment.Exit(0);
}
else 
{ 
    Console.Clear();
    goto TitleScreenRender;
}

OldAhhSnakeEasterEgg:
snakechar = "[]";
wallchar = "XX";
speed = 20;
dimensions.width = 10;
dimensions.height = 10;
noclip = false;
Console.Clear();
goto GameStart;