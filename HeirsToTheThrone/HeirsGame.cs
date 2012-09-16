using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using HeirsToTheThrone.graphics;

namespace HeirsToTheThrone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class HeirsGame : Microsoft.Xna.Framework.Game
    {
        public static HeirsGame instance;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameScreen gameScreenNode;
        MapNode mapNode;
        Keys[] previousPressedKeys = new Keys[0];

        public HeirsGame()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            model.GameConfiguration conf = new HeirsToTheThrone.model.GameConfiguration();
            conf.MapWidth = 30;
            conf.MapHeight = 22;
            conf.NumProvinces = 16;
            conf.waterPercentage = 0.3;
            /*
            conf.MapWidth = 30;
            conf.MapHeight = 22;
            conf.NumProvinces = 30;
            conf.waterPercentage = 0.3;
             */
            model.Map map = model.Map.CreateMap(conf);

            List<model.Player> players = new List<HeirsToTheThrone.model.Player>();
            model.Player redPlayer = new model.Player();
            model.Player bluePlayer = new model.Player();
            model.Player greenPlayer = new model.Player();
            model.Player yellowPlayer = new model.Player();
            model.Player royalistPlayer = new model.Player();
            redPlayer.color = Color.Red;
            redPlayer.darkColor = Color.DarkRed;
            bluePlayer.color = Color.Blue;
            bluePlayer.darkColor = Color.DarkBlue;
            greenPlayer.color = Color.Green;
            greenPlayer.darkColor = Color.DarkGreen;
            yellowPlayer.color = Color.Yellow;
            yellowPlayer.darkColor = Color.Gold;
            royalistPlayer.color = Color.Gray;
            royalistPlayer.darkColor = Color.DarkGray;
            players.Add(redPlayer);
            players.Add(bluePlayer);
            players.Add(greenPlayer);
            players.Add(yellowPlayer);
            players.Add(royalistPlayer);

            Random random = new Random();
            foreach (model.Province province in map.provinces)
            {
                province.owner = players.ElementAt(random.Next(players.Count));
                province.owner.provinces.Add(province);
            }

            gameScreenNode = new GameScreen(map);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            List<Keys> justPressedKeys = pressedKeys.ToList();
            foreach(Keys k in previousPressedKeys){
                justPressedKeys.Remove(k);
            }
            previousPressedKeys = pressedKeys;

            if ( justPressedKeys.Contains(Keys.Escape) ){
                this.Exit();
            }

            foreach (Keys k in justPressedKeys){
                KeyEvent ke = new KeyEvent();
                ke.key = k;
                ke.pressed = true;
                ke.released = false;
                ke.remainsPressed = false;
                this.gameScreenNode.keyEvent(ke);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            
            // TODO: Add your drawing code here
            
            gameScreenNode.draw(spriteBatch, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

            base.Draw(gameTime);
        }
    }
}
