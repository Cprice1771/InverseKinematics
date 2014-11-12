using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverseKinematics
{
    public class Effector
    {
        Texture2D _texture;
        public Vector2 Position { get; set; }

        public Effector(Texture2D text)
        {
            _texture = text;
            Position = Vector2.Zero;
        }

        public void Update(GameTime gt)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, Position, Color.White);
        }

    }
}
