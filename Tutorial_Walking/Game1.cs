using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial_Walking.Characters;
using Tutorial_Walking.Tiles;

namespace Tutorial_Walking
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        CharacterEntity character;
        private Texture2D background;

        private Map map;
        Camera camera;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            map = new Map();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("BG/BG");

            character = new CharacterEntity(this.GraphicsDevice, new Vector2(0,350));

            Tile.Content = Content;

            camera = new Camera(GraphicsDevice.Viewport);

            map.Generate(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,13,14,15,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,2,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0},       
                {2,2,2,2,5,2,2,2,5,5,5,2,2,2,2,0,0,0,0,2,2,2,2,2,2},
            }, 64);


    }

        protected override void Update(GameTime gameTime)
        {

            character.Update(gameTime);
            character.input = new Input
            {
                Up = Keys.Z,
                Down = Keys.S,
                Left = Keys.Q,
                Right = Keys.D
            };

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                character.Collision(tile.Rectangle, map.Width, map.Heigth);
                camera.Update(character.Position,map.Width,map.Heigth);
            }
                

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise,null, camera.Transform);


            
            _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            
            map.Draw(_spriteBatch);
            character.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
