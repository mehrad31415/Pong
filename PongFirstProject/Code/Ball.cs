using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongFirstProject;

namespace Pong.Sprites;

public class Ball : Sprite
{
    // we increment the speed of the ball every 10 seconds.
    private float _timer = 0f;
    public int IncrementTime = 10;

    // we need to store the start position and speed of the ball.
    private Vector2? _startPosition = null;
    private float _startSpeed;

    // when calling the constructor, we need to pass the texture.
    public Ball(Texture2D texture)
        : base(texture)
    {
        // the INITIAL speed of the ball is 3.
        Speed = 3f;
    }


    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        if (_startPosition == null)
        {
            _startPosition = Position;
            _startSpeed = Speed;

            Restart();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Space))
            Game1.CurrentState = Game1.GameState.GameStarted;

        if (Game1.CurrentState != Game1.GameState.GameStarted)
            return;

        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // if the timer is greater than the speed increment span, we increment the speed of the ball.
        if (_timer > IncrementTime)
        {
            Speed++;
            _timer = 0;
        }

        // we check if the ball is touching the bat.
        foreach (var sprite in sprites)
        {
            if (sprite == this)
                continue;

            if (Velocity.X > 0 && IsCollidingLeft(sprite))
                Velocity.X = -Velocity.X;
            if (Velocity.X < 0 && IsCollidingRight(sprite))
                Velocity.X = -Velocity.X;
            if (Velocity.Y > 0 && IsCollidingTop(sprite))
                Velocity.Y = -Velocity.Y;
            if (Velocity.Y < 0 && IsCollidingBottom(sprite))
                Velocity.Y = -Velocity.Y;
        }

        if (Position.Y <= 0 || Position.Y + Texture.Height >= Game1.ScreenHeight)
            Velocity.Y = -Velocity.Y;

        Position += Velocity * Speed;
    }

    // in the restart method we reset the position, speed and timer of the ball.
    // the velocity is done randomly between 8 directions.
    public void Restart()
    {
        var direction = Game1.Random.Next(0, 8);

        switch (direction)
        {
            case 0:
                Velocity = new Vector2(1, 1);
                break;
            case 1:
                Velocity = new Vector2(1, -1);
                break;
            case 2:
                Velocity = new Vector2(-1, -1);
                break;
            case 3:
                Velocity = new Vector2(-1, 1);
                break;
            case 4:
                Velocity = new Vector2(1, 1);
                break;
            case 5:
                Velocity = new Vector2(1, -1);
                break;
            case 6:
                Velocity = new Vector2(-1, -1);
                break;
            case 7:
                Velocity = new Vector2(-1, 1);
                break;
        }

        Position = (Vector2)_startPosition;
        Speed = (float)_startSpeed;
        _timer = 0;
        Game1.CurrentState = Game1.GameState.NotStarted;
    }
}