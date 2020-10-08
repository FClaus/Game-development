using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using Tutorial_Walking.Tiles;
using Microsoft.Xna.Framework.Content;

namespace Tutorial_Walking
{
    public class CharacterEntity
    {
        Animation idle;
        Animation right;
        Animation left;
        Animation down;
        Animation jump;
        Animation slide;
        Animation currentAnimation;

        SpriteEffects flip;

        
        Rectangle sourceRectangle;
        float rotation = 0f;
        public float Speed = 4f;

        ContentManager content;

        public Vector2 Position;
        public Vector2 velocity;

        Rectangle collisionRectangle;

        public bool HasJumped;
        private bool HoldLeft;

        public Input input;

        static Texture2D characterSheetTexture;

    
            
        

        public CharacterEntity(GraphicsDevice graphicsDevice, Vector2 position)
        {
             if (characterSheetTexture == null)
             {
                 using (var stream = TitleContainer.OpenStream("adventurer-v1.5-Sheet.png"))
                 {
                     characterSheetTexture = Texture2D.FromStream(graphicsDevice, stream);
                 }
             }

           

            Position = position;
            HasJumped = true;

            collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 37);

            idle = new Animation();
            idle.AddFrame(new Rectangle(0, 0, 50, 37), TimeSpan.FromSeconds(.25));
            idle.AddFrame(new Rectangle(50, 0, 50, 37), TimeSpan.FromSeconds(.25));
            idle.AddFrame(new Rectangle(100, 0, 50, 37), TimeSpan.FromSeconds(.25));
            idle.AddFrame(new Rectangle(150, 0, 50, 37), TimeSpan.FromSeconds(.25));
             

            right = new Animation();
            right.AddFrame(new Rectangle(50, 37, 50, 37), TimeSpan.FromSeconds(.12));
            right.AddFrame(new Rectangle(100, 37, 50, 37), TimeSpan.FromSeconds(.12));
            right.AddFrame(new Rectangle(150, 37, 50, 37), TimeSpan.FromSeconds(.12));
            right.AddFrame(new Rectangle(200, 37, 50, 37), TimeSpan.FromSeconds(.12));
            right.AddFrame(new Rectangle(250, 37, 50, 37), TimeSpan.FromSeconds(.12));
            right.AddFrame(new Rectangle(300, 37, 50, 37), TimeSpan.FromSeconds(.12));

            jump = new Animation();
            jump.AddFrame(new Rectangle(0, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(50, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(100, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(150, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(200, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(250, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(300, 74, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(0, 111, 50, 37), TimeSpan.FromSeconds(.12));
            jump.AddFrame(new Rectangle(50, 111, 50, 37), TimeSpan.FromSeconds(.09));
            jump.AddFrame(new Rectangle(100, 111, 50, 37), TimeSpan.FromSeconds(.09));

            down = new Animation();
            down.AddFrame(new Rectangle(200, 0, 50, 37), TimeSpan.FromSeconds(.25));
            down.AddFrame(new Rectangle(250, 0, 50, 37), TimeSpan.FromSeconds(.25));
            down.AddFrame(new Rectangle(300, 0, 50, 37), TimeSpan.FromSeconds(.25));
            down.AddFrame(new Rectangle(0, 37, 50, 37), TimeSpan.FromSeconds(.25));

            left = new Animation();
            left.AddFrame(new Rectangle(50, 37, 50, 37), TimeSpan.FromSeconds(.12));
            left.AddFrame(new Rectangle(100, 37, 50, 37), TimeSpan.FromSeconds(.12));
            left.AddFrame(new Rectangle(150, 37, 50, 37), TimeSpan.FromSeconds(.12));
            left.AddFrame(new Rectangle(200, 37, 50, 37), TimeSpan.FromSeconds(.12));
            left.AddFrame(new Rectangle(250, 37, 50, 37), TimeSpan.FromSeconds(.12));
            left.AddFrame(new Rectangle(300, 37, 50, 37), TimeSpan.FromSeconds(.12));

            slide = new Animation();
            slide.AddFrame(new Rectangle(150, 111, 50, 37), TimeSpan.FromSeconds(.12));
            slide.AddFrame(new Rectangle(200, 111, 50, 37), TimeSpan.FromSeconds(.12));
            slide.AddFrame(new Rectangle(250, 111, 50, 37), TimeSpan.FromSeconds(.12));
            slide.AddFrame(new Rectangle(300, 111, 50, 37), TimeSpan.FromSeconds(.12));
            slide.AddFrame(new Rectangle(0, 148, 50, 37), TimeSpan.FromSeconds(.12));

        }

        public void Update(GameTime gameTime)
        {
            // temporary - we'll replace this with logic based off of which way the
            // character is moving when we add movement logic

            Position += velocity;
            collisionRectangle.X = (int)Position.X;
            collisionRectangle.Y = (int)Position.Y;


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && HasJumped == false && Keyboard.GetState().IsKeyDown(Keys.D)
                || Keyboard.GetState().IsKeyDown(Keys.Space) && HasJumped == false
                || Keyboard.GetState().IsKeyDown(Keys.Space) && HasJumped == false && Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                
                Position.Y -= 15f;
                velocity.Y = -5f;
                HasJumped = true;
                
            } 

            else if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.S))    
            {
                flip = SpriteEffects.None;
                currentAnimation = slide;
                velocity.X = Speed;
                
            }

            else if ( Keyboard.GetState().IsKeyDown(Keys.Q) && HasJumped == false && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                currentAnimation = slide;
               
                flip = SpriteEffects.FlipHorizontally;
                velocity.X = -Speed;

                
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.D)) {

                flip = SpriteEffects.None;
                currentAnimation = right;
                velocity.X = Speed;
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {

                currentAnimation = left;
                flip = SpriteEffects.FlipHorizontally;
                velocity.X = -Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                currentAnimation = down;
                velocity.X = 0;
            }
            
            else {
                currentAnimation = idle;
                velocity.X = 0;
            }

            if (HoldLeft == true && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                HoldLeft = false;
            }


            if (HasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
                currentAnimation = jump;

            }

            if (Position.Y >= 700)
            {
                HasJumped = false;

            }

            if (HasJumped == false)
            {  
                velocity.Y += 0.3f;
            }

            
            currentAnimation.Update(gameTime);
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {

            if (collisionRectangle.TouchTopOf(newRectangle))
            {
                collisionRectangle.Y = newRectangle.Y - collisionRectangle.Height;
                velocity.Y = 0f;
                HasJumped = false;
            }


            if (collisionRectangle.TouchLeftOf(newRectangle))
            {
                Position.X = newRectangle.X - collisionRectangle.Width - 2;
            }

            if (collisionRectangle.TouchRightOf(newRectangle))
            {
                Position.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (collisionRectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }


            if (Position.X < 0)
            {
                Position.X = 0;
            }

            if (Position.X > xOffset - collisionRectangle.Width)
            {
                Position.X = xOffset - collisionRectangle.Width;
            }

            if (Position.Y < 0)
            {
                velocity.Y = 1f;
            }
               
            if (Position.Y > yOffset - collisionRectangle.Height)
            {
                Position.Y = yOffset ;
            }
                
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Position = new Vector2(Position.X, Position.Y);
            sourceRectangle = currentAnimation.CurrentRectangle;

            spriteBatch.Draw(
                characterSheetTexture,
                Position,
                sourceRectangle,
                Color.White,
                rotation,
                new Vector2(15, 20),
                2.5f,
                flip,
                0f);
        }
    }
}