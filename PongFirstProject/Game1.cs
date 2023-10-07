using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Pong.Models;
using Pong.Sprites;

namespace PongFirstProject;

// The class Game1 inherits the Game attributes.
public class Game1 : Game
{
    // the game has 3 states: not started, game over and game started.
    public enum GameState
    {
        NotStarted,
        GameOver,
        GameStarted
    }

    // these variables help us to choose between 1 player or a 2 player game.
    private AIBat _aiBat;
    private Bat _bat;

    // Defining class members (static)
    // the following attributes are public because the sprites need to access them.
    public static int ScreenWidth;
    public static int ScreenHeight;
    public static Random Random;
    public static GameState CurrentState = GameState.NotStarted;

    // Defining object fields.
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;


    private List<Sprite> _sprites;
    private string _startMessage = "Press SPACE to start the game";

    // class constructor.
    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }


    // Allows the game to perform any initialization it needs to before starting to run.
    protected override void Initialize()
    {
        // assigning width and height of the screen.
        ScreenWidth = graphics.PreferredBackBufferWidth;
        ScreenHeight = graphics.PreferredBackBufferHeight;

        // creating a random variable upon assignment.
        Random = new Random();

        // Calling base.Initialize will enumerate through any components and initialize them as well.
        base.Initialize();
    }


    // LoadContent will be called once per game and is the place to load all of your content.
    protected override void LoadContent()
    {
        var keyboardState = Keyboard.GetState();

        // Defining local variables.
        Texture2D ballTexture;
        Texture2D batTexture;
        Texture2D backgroundTexture;

        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // I previously used the below code to load the texture, but it was not working.
        // var ballTexture = Content.Load<Texture2D>("Ball");
        // So I used the below code to load the texture, and it worked. 
        // I do not know what my problem is but I have had a hard time working with Monogame and visual studio.
        // I must say that I am on a Mac. I haven't tried if this configuration works on Windows because I do not have a Windows machine.
        using (var stream = TitleContainer.OpenStream("Content/Ball.png"))
        {
            ballTexture = Texture2D.FromStream(GraphicsDevice, stream);
        }

        // same problem for Bat texture. 
        // I previously used the below code to load the texture, but it was not working.
        // var ballTexture = Content.Load<Texture2D>("Bat");
        using (var stream = TitleContainer.OpenStream("Content/Bat.png"))
        {
            batTexture = Texture2D.FromStream(GraphicsDevice, stream);
        }

        // and same problem for Background texture as well.
        // I previously used the below code to load the texture, but it was not working.
        // var ballTexture = Content.Load<Texture2D>("Background");
        using (var stream = TitleContainer.OpenStream("Content/Background.png"))
        {
            backgroundTexture = Texture2D.FromStream(GraphicsDevice, stream);
        }

        // For score the below code is not working I tried several ways but it is not working.
        // I even looked through several resources. 
        // I can't define it as a stream like the above code because it is not a texture.
        // So the methods which worked for background, ball and bat textures are not working for score.
        // I just neglected the score and any text for now.
        // However, the game is working fine and has different states and upon reaching 3 points the game ends.
        // but there is no prompt for the user to know that the game has ended or that they are in different states.
        // _score = new Score(Content.Load<SpriteFont>("MyFont"));


        _sprites = new List<Sprite>();

        // Create a background sprite
        var background = new Sprite(backgroundTexture)
        {
            // The background starts from the top left corner of the screen.
            Position = new Vector2(0, 0)
        };
        // it is added to the list of sprites.
        _sprites.Add(background);

        // Create the first bat sprite
        _bat = new Bat(batTexture, 1)
        {
            // the first bat is positioned at the left side of the screen in the middle.
            Position = new Vector2(20, ScreenHeight / 2 - batTexture.Height / 2),
            // the input for the first bat is defined as the W and S keys.
            Input = new Input()
            {
                Up = Keys.W,
                Down = Keys.S
            }
        };
        _sprites.Add(_bat);

        // Create the second bat sprite
        var player2Bat = new Bat(batTexture, 2)
        {
            // the second bat is positioned at the right side of the screen in the middle.
            Position = new Vector2(ScreenWidth - 20 - batTexture.Width, ScreenHeight / 2 - batTexture.Height / 2),
            // the input for the second bat is defined as the Up and Down arrow keys.
            Input = new Input()
            {
                Up = Keys.Up,
                Down = Keys.Down
            }
        };
        _sprites.Add(player2Bat);

        // adding AI bat
        _aiBat = new AIBat(batTexture, 1);
        _sprites.Add(_aiBat);

        // Create the ball sprite
        var ball = new Ball(ballTexture)
        {
            // the ball is positioned at the center of the screen when the game starts.
            Position = new Vector2(ScreenWidth / 2 - ballTexture.Width / 2,
                ScreenHeight / 2 - ballTexture.Height / 2),
        };
        _sprites.Add(ball);
    }

    // Allows the game to run logic such as updating the world and checking for collisions.
    // gameTime Provides a snapshot of timing values.
    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        // if the game has not started yet, the user is prompted to press the space bar to start the game.
        // again I have had problems incorporating the plugin with visual studio in mac.
        // so I have not been able to use the spritefont to display text.

        switch (CurrentState)
        {
            // When the game is not started, the user is prompted to press the space bar OR A to start the game.
            case GameState.NotStarted:
                // if the user presses the space bar, the game starts. The game is 2 player.
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    // If the user presses the space bar, start the game.
                    CurrentState = GameState.GameStarted;
                    // Clear the start message.This is supposed to happen but again there is no spritefont.
                    _startMessage = string.Empty;

                    if (_sprites.Contains(_bat) && _sprites.Contains(_aiBat))
                    {
                        _sprites.Remove(_aiBat);
                    }
                }
                // if the user presses the A key, the game starts. The game is 1 player. The second player is AI.
                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    // If the user presses the space bar, start the game.
                    CurrentState = GameState.GameStarted;
                    // Clear the start message.This is supposed to happen but again there is no spritefont.
                    _startMessage = string.Empty;

                    if (_sprites.Contains(_bat) && _sprites.Contains(_aiBat))
                    {
                        _sprites.Remove(_bat);
                    }
                }
                break;

            case GameState.GameStarted:
                // we update the sprites including the ball and the bats.
                foreach (var sprite in _sprites)
                {
                    sprite.Update(gameTime, _sprites);
                }
                base.Update(gameTime);

                break;

            case GameState.GameOver:
                // We could have displayed a game over message here to differentiate between the states.
                // but again the font is not working.
                // right now because there is no text you cannot tell if the game is over or not.
                // but if you play the game and either one goes to zero lives and beforehand uncomment the below code
                // you will see that the game is over because an exception is thrown.
                // throw new NotImplementedException();
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    // If the user presses the space bar, the game goes to initial mode
                    CurrentState = GameState.NotStarted;
                }
                break;
        }
    }

    // This is called when the game should draw itself.
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        spriteBatch.Begin();

        foreach (var sprite in _sprites)
        {
            sprite.Draw(spriteBatch);
        }

        // again cannot draw the score because of the same problem. (looked at multiple tutorials but wasn't able to fix it).
        //_score.Draw(spriteBatch);

        spriteBatch.End();

        base.Draw(gameTime);
    }
}