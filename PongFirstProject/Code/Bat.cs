using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongFirstProject;

namespace Pong.Sprites;

// Bat is a sprite.
public class Bat : Sprite
{
    // each player has 3 lives.
    public int Lives = 3;
    public int Tag;

    // when calling the constructor, we need to pass the texture.
    public Bat(Texture2D texture, int tag)
        : base(texture)
    {
        // the speed of the bat is 5.
        Speed = 5f;
        Tag = tag;
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        // if the user presses up, the bat will move up, if the user presses down, the bat will move down.
        if (Keyboard.GetState().IsKeyDown(Input.Up))
            Velocity.Y = -Speed;
        else if (Keyboard.GetState().IsKeyDown(Input.Down))
            Velocity.Y = Speed;
        // if the user does not press up or down, the bat will not move.
        else
            Velocity.Y = 0;

        // the position of the bat is updated.
        Position += Velocity;
        // the position cannot exceed the screen height.
        Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.ScreenHeight - Texture.Height);
        UpdateScore(sprites[sprites.Count - 1] as Ball);
        Velocity = Vector2.Zero;
    }

    // Add a method to update the score based on the Ball's position.
    public void UpdateScore(Ball ball)
    {
        // if the game is over, the lives are reset.
        if (Game1.CurrentState == Game1.GameState.GameOver)
        {
            Lives = 3;
        }
        // if the ball is passed the left of the screen and the bat is the left bat, the life is decremented.
        if (ball.Position.X <= 0 && Tag == 1)
        {
            Lives--;
            // if the lives are 0, the game is over.
            if (Lives == 0)
            {
                Game1.CurrentState = Game1.GameState.GameOver;

            }
            Restart(ball);
        }
        // if the ball is passed the right of the screen and the bat is the right bat, the life is decremented.
        else if (ball.Position.X + Texture.Width >= Game1.ScreenWidth && Tag == 2)
        {
            Lives--;
            // if the lives are 0, the game is over.
            if (Lives == 0)
            {
                Game1.CurrentState = Game1.GameState.GameOver;

            }
            Restart(ball);
        }
    }

    private void Restart(Ball ball)
    {
        ball.Restart();
    }

}