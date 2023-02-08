using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    Texture2D targetSprite;
    Texture2D crosshairsSprite;
    Texture2D backgroundSprite;
    SpriteFont gameFont;
    Vector2 targetPosition = new Vector2(300, 300);
    Vector2 crosshairsPosition;
    const int targetRadius = 45;
    MouseState mState;
    int score = 0;
    bool mReleased = true;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        targetSprite = Content.Load<Texture2D>("target");
        crosshairsSprite = Content.Load<Texture2D>("crosshairs");
        backgroundSprite = Content.Load<Texture2D>("sky");
        gameFont = Content.Load<SpriteFont>("galleryFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        mState = Mouse.GetState();
        crosshairsPosition = new Vector2(mState.X - crosshairsSprite.Width / 2, mState.Y - crosshairsSprite.Height / 2);
        if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
        {
            float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
            if (mouseTargetDist < targetRadius)
            {
                score++;
                Random rand = new Random();
                targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
            }
            mReleased = false;
        }
        if (mState.LeftButton == ButtonState.Released)
        {
            mReleased = true;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
        _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
        _spriteBatch.DrawString(gameFont, "Score : " + score.ToString(), new Vector2((_graphics.PreferredBackBufferWidth- gameFont.MeasureString("Score : " + score.ToString()).X) / 2, 0), Color.White);
        _spriteBatch.Draw(crosshairsSprite, crosshairsPosition, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
