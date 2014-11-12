using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverseKinematics
{
    public class Bone
    {
        Texture2D _texture;
        Rectangle _rect;
        public Vector2 Position;
        public float _rotation;
        Bone _parent;
        public Effector Effector;

        public Bone(Texture2D text, Vector2 pos, Effector effect, Bone parent)
        {
            _texture = text;
            Position = pos;
            _rect = new Rectangle(0, 0, text.Width, text.Height);
            _rotation = 0.0f;
            Effector = effect;
            Effector.Position = new Vector2(Position.X + ((float)Math.Cos(_rotation) * _texture.Width), Position.Y + ((float)Math.Sin(_rotation) * _texture.Height));
            _parent = parent;
            _rotation = (float)Math.Atan2(Mouse.GetState().Position.Y - Position.Y, Mouse.GetState().Position.X - Position.X);
        }

        public void Update()
        {
            
            if (_parent != null)
            {
                Position = _parent.Effector.Position;
            }
            Effector.Position = new Vector2(Position.X + ((float)Math.Cos(_rotation) * (_texture.Width - 34)), Position.Y + ((float)Math.Sin(_rotation) * (_texture.Width - 34)));
            
        }

        //This is what the assignment said to do but it doesn't actually make any of the bones adjust for when the mouse is inside the bones
        public void UpdatePosition(Vector2 EndPosition)
        {
            //Effector.Position = new Vector2(Position.X + ((float)Math.Cos(_rotation) * (_texture.Width - 34)), Position.Y + ((float)Math.Sin(_rotation) * (_texture.Width - 35)));
            _rotation += (float)(Math.Atan2(Mouse.GetState().Position.Y - (Position.Y - (34 * Math.Sin(_rotation))), Mouse.GetState().Position.X - (Position.X - (34 * Math.Cos(_rotation)))) 
                - Math.Atan2(Effector.Position.Y - (Position.Y - (34 * Math.Sin(_rotation))), Effector.Position.X - (Position.X - (34 * Math.Cos(_rotation)))));

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, Position, _rect, Color.White, _rotation, new Vector2(34, 35), 1.0f, SpriteEffects.None, 1.0f);
            Effector.Draw(sb);
        }
    }
}
