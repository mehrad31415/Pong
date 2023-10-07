using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using PongFirstProject;

namespace Pong.Sprites
{
    public class AIBat : Bat
    {
        public AIBat(Texture2D texture, int tag)
            : base(texture, tag)
        {
            // the speed of the AI bat.
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // the ball is the last element in the sprites list.
            Ball ball = sprites[sprites.Count - 1] as Ball;

            if (ball != null)
            {
                // This is used to adjust the bat's position according to the ball.
                float margin = 10f;

                // We determine the target position of the bat based on the position of the ball.
                float targetY = ball.Position.Y + ball.Texture.Height / 2 - Texture.Height / 2;

                // position must stay within the screen bounds.
                targetY = MathHelper.Clamp(targetY, 0, Game1.ScreenHeight - Texture.Height);

                // The bat moves towards the target position.
                if (Math.Abs(Position.Y - targetY) < margin)
                {
                    // when the bat has reached the target position we stop moving.
                    Velocity.Y = 0;
                }
                else if (Position.Y < targetY)
                {
                    // if the target position is lower than the current position we move down.
                    Velocity.Y = Speed;
                }
                else
                {
                    // otherwise it must go up.
                    Velocity.Y = -Speed;
                }

                // Update the position of the bat.
                Position += Velocity;
            }

            UpdateScore(ball);
            Velocity = Vector2.Zero;
        }
    }
}
