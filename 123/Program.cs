﻿using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(800, 600, "Hello");
Raylib.SetTargetFPS(60);


Vector2 movement = new Vector2(0.1f, 0.1f);
Color hotPink = new Color(255, 105, 180, 255);


Texture2D characterImage = Raylib.LoadTexture("1234.png");
Rectangle characterRect = new Rectangle(10, 430, 32, 32);
characterRect.Width = characterImage.Width;
characterRect.Height = characterImage.Height;

List<Rectangle> walls = new();
walls.Add(new Rectangle(40, 40, 740, 30));
walls.Add(new Rectangle(40, 540, 740, 30));
walls.Add(new Rectangle(700, 540, 40, 30));
walls.Add(new Rectangle(700, 40, 40, 30));
walls.Add(new Rectangle(40, 40, 30, 360));
walls.Add(new Rectangle(750, 40, 30, 520));


Rectangle doorRect = new Rectangle(690, 490, 25, 25);

float speed = 5;

string scene = "start";

int points = 0;

while (!Raylib.WindowShouldClose())
{
  // --------------------------------------------------------------------------
  // GAME LOGIC
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

    // kod här: läsa in knapptryck, ändra på movement
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

    // bool isInAWall = CheckIfWall(characterRect, walls, scene);

    // if (isInAWall == true)
    // {
    //   characterRect.X -= movement.X;
    // }

    characterRect.Y += movement.Y;

    // isInAWall = CheckIfWall(characterRect, walls, scene);

    // if (isInAWall == true)
    // {
    //   characterRect.Y -= movement.Y;
    // }

    if (characterRect.X < 0 || characterRect.X > 800 - 104)
    {
      characterRect.X -= movement.X;
    }
    if (characterRect.Y < 0 || characterRect.Y > 600 - 84)
    {
      characterRect.Y -= movement.Y;
    }

    // bool isInAWall = CheckIfWall(characterRect, walls, scene);
    // if (isInAWall)
    // {

    // }    

    foreach (Rectangle wall in walls)
    {
      if (Raylib.CheckCollisionRecs(characterRect, wall))
      {
        scene = "start";
      }
    }


    if (Raylib.CheckCollisionRecs(characterRect, doorRect))
    {
      points++;
    }



  }


  // --------------------------------------------------------------------------
  // RENDERING
  // --------------------------------------------------------------------------

  Raylib.BeginDrawing();
  if (scene == "start")
  {
    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawText("Press SPACE to start", 10, 10, 32, Color.BLACK);
  }
  else if (scene == "game")
  {
    Raylib.ClearBackground(Color.GOLD);




    Raylib.DrawTexture(characterImage, (int)characterRect.X, (int)characterRect.Y, Color.WHITE);

    Raylib.DrawRectangleRec(doorRect, Color.BROWN);

    Raylib.DrawRectangleRec(walls[0], Color.BLACK);
    Raylib.DrawRectangleRec(walls[1], Color.BLACK);
    Raylib.DrawRectangleRec(walls[2], Color.BLACK);
    Raylib.DrawRectangleRec(walls[3], Color.BLACK);
    Raylib.DrawRectangleRec(walls[4], Color.BLACK);
    Raylib.DrawRectangleRec(walls[5], Color.BLACK);

    Raylib.DrawText($"Points: {points}", 10, 10, 32, Color.WHITE);

  }

  Raylib.EndDrawing();
}

static bool CheckIfWall(Rectangle characterRect, List<Rectangle> walls, string scene)
{
  foreach (Rectangle wall in walls)
  {
    if (Raylib.CheckCollisionRecs(characterRect, wall))
    {
      // scene = "start";
      // characterRect.X = 10;
      // characterRect.Y = 430;

      return true;
    }

  }
  return false;
}