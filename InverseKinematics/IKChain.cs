using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverseKinematics
{
    public class IKChain : List<Bone>
    {
        public IKChain()
        {
        }


        public void Update()
        {
            for (int i = 0; i < this.Count; i++)
                this[i].Update();
        }

        public void UpdatePosition()
        {
            bool done = false;

            //for (int j = this.Count - 1; j >= 0; j--)
            //{
            //    this[j].UpdatePosition(this[this.Count - 1].Position);

            //    if (Vector2.Distance(this[3].Effector.Position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 10)
            //    {
            //        done = true;
            //        break;
            //    }
            //}

            for (int i = 0; i < 20; i++)
            {
                if (done)
                    break;

                //giant hack to get it working, its extremly ineffecient, but it works and is similar to the method described in the assignment
                for (int j = this.Count - 1; j >= 0; j--)
                {
                    Update();
                    float origDist = Math.Abs(Vector2.Distance(this[3].Effector.Position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)));

                    this[j]._rotation += 0.1f;
                    Update();
                    float dista = Math.Abs(Vector2.Distance(this[3].Effector.Position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)));

                    this[j]._rotation -= 0.2f;
                    Update();
                    float distb = Math.Abs(Vector2.Distance(this[3].Effector.Position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)));

                    if (distb < dista && distb < origDist);
                        //do nothing
                    else if (dista < origDist && dista < distb)
                        this[j]._rotation += 0.2f; // go back to adding to rot
                    else
                        this[j]._rotation += 0.1f; // go back to the origional

                    if (Math.Abs(Vector2.Distance(this[3].Effector.Position, new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y))) < 5)
                    {
                        done = true;
                        break;
                    }
                }
                
            }
        }

        internal void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = this.Count - 1; i >= 0; i--)
                this[i].Draw(spriteBatch);
        }

        
    }
}
