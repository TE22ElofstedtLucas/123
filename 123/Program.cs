﻿using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

Raylib.InitWindow(800, 600, "Hello");
Raylib.SetTargetFPS(60);

Vector2 movement = new Vector2(0.1f, 0.1f);
Color hotPink = new Color(255, 105, 180, 255);

Texture2D characterImage = Raylib.LoadTexture("cube.png");

Rectangle characterRect = new Rectangle(10, 430, 32, 32);
characterRect.Width = characterImage.Width;
characterRect.Height = characterImage.Height;

List<Rectangle> walls = new();
walls.Add(new Rectangle(40, 40, 740, 30));
walls.Add(new Rectangle(40, 560, 740, 30));
walls.Add(new Rectangle(700, 560, 40, 30));
walls.Add(new Rectangle(700, 40, 40, 30));
walls.Add(new Rectangle(40, 40, 30, 360));
walls.Add(new Rectangle(750, 40, 30, 520));
walls.Add(new Rectangle(180, 140, 30, 450));
walls.Add(new Rectangle(180, 140, 150, 30));
walls.Add(new Rectangle(320, 140, 30, 420));
walls.Add(new Rectangle(440, 40, 30, 420));
walls.Add(new Rectangle(550, 140, 30, 450));
walls.Add(new Rectangle(550, 140, 80, 30));
walls.Add(new Rectangle(630, 140, 30, 450));

List<Rectangle> collectibles = new();
collectibles.Add(new Rectangle(100, 200, 20, 20));
collectibles.Add(new Rectangle(500, 300, 20, 20));
collectibles.Add(new Rectangle(400, 400, 20, 20));

Rectangle doorRect = new Rectangle(690, 490, 25, 25);

float speed = 5;

string scene = "start";

while (!Raylib.WindowShouldClose())
{
    // --------------------------------------------------------------------------
    // Keys
    // --------------------------------------------------------------------------

    if (scene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }
    else if (scene == "game")
    {
        movement = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            movement.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            movement.X = 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            movement.Y = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            movement.Y = 1;
        }

        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * speed;
        }

        characterRect.X += movement.X;
        characterRect.Y += movement.Y;

        if (characterRect.X < 0 || characterRect.X > 800 - characterRect.Width)
        {
            characterRect.X -= movement.X;
        }
        if (characterRect.Y < 0 || characterRect.Y > 600 - characterRect.Height)
        {
            characterRect.Y -= movement.Y;
        }

        foreach (Rectangle wall in walls)
        {
            if (Raylib.CheckCollisionRecs(characterRect, wall))
            {
                scene = "start";
                characterRect.X = 10;
                characterRect.Y = 430;
            }
        }

        for (int i = collectibles.Count - 1; i >= 0; i--)
        {
            if (Raylib.CheckCollisionRecs(characterRect, collectibles[i]))
            {
                collectibles.RemoveAt(i);
            }
        }

        if (Raylib.CheckCollisionRecs(characterRect, doorRect))
        {
            scene = "win";
            characterRect.X = 10;
            characterRect.Y = 430;
        }
    }

    // --------------------------------------------------------------------------
    // Väggar och scener
    // --------------------------------------------------------------------------

    Raylib.BeginDrawing();
    if (scene == "start")
    {
        Raylib.ClearBackground(Color.SKYBLUE);
        Raylib.DrawText("Press SPACE to start", 10, 10, 32, Color.BLACK);
    }
    else if (scene == "game")
    {
        Raylib.ClearBackground(Color.PURPLE);

        Raylib.DrawTexture(characterImage, (int)characterRect.X, (int)characterRect.Y, Color.WHITE);

        Raylib.DrawRectangleRec(doorRect, Color.BROWN);

        for (int i = 0; i < walls.Count; i++)
        {
            Raylib.DrawRectangleRec(walls[i], Color.BLACK);
        }

        foreach (Rectangle collectible in collectibles)
        {
            Raylib.DrawRectangleRec(collectible, Color.GOLD);
        }
    }
    if (scene == "win")
    {
        Raylib.ClearBackground(Color.YELLOW);
        Raylib.DrawText("YOU WIN!", 100, 100, 32, Color.BLACK);
    }

    Raylib.EndDrawing();
}
