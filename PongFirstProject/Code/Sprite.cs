using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Models;
using PongFirstProject;

namespace Pong.Sprites
{
    // This class is the base class for all sprites including the ball and the bat.
    public class Sprite
    {
        // Every sprite has a texture, position, velocity, speed and input.
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        protected float Speed;
        public Input Input;

        // each sprite has a bounding box.
        public Rectangle Bounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        // each sprite has a texture.
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        // this method is used to update the sprite; for each sprite this method is different and is overridden.
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }

        // this method is used to draw the sprite; for each sprite this method is different and is overridden.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        // The following methods are used to check if the sprite is touching another sprite. (Collision detection).
        public bool IsCollidingRight(Sprite other)
        {
            return Bounds().Left + Velocity.X < other.Bounds().Right &&
                   Bounds().Right > other.Bounds().Right &&
                   Bounds().Bottom > other.Bounds().Top &&
                   Bounds().Top < other.Bounds().Bottom;
        }

        public bool IsCollidingLeft(Sprite other)
        {
            return Bounds().Right + Velocity.X > other.Bounds().Left &&
                   Bounds().Left < other.Bounds().Left &&
                   Bounds().Bottom > other.Bounds().Top &&
                   Bounds().Top < other.Bounds().Bottom;
        }

        public bool IsCollidingTop(Sprite other)
        {
            return Bounds().Bottom + Velocity.Y > other.Bounds().Top &&
                   Bounds().Top < other.Bounds().Top &&
                   Bounds().Right > other.Bounds().Left &&
                   Bounds().Left < other.Bounds().Right;
        }

        public bool IsCollidingBottom(Sprite other)
        {
            return Bounds().Top + Velocity.Y < other.Bounds().Bottom &&
                   Bounds().Bottom > other.Bounds().Bottom &&
                   Bounds().Right > other.Bounds().Left &&
                   Bounds().Left < other.Bounds().Right;
        }
    }
}
