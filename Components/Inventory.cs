using CodeSummonary.Components.Summaries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public class Inventory : ContainableComponent
    {
        public List<Summary> Summaries;

        public Inventory() 
        { 
            Summaries = new List<Summary>();
        }

        public void AddSummary(Summary summary)
        {
            Summaries.Add(summary);
            summary.Parent = this;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Summary summary in Summaries)
                summary.Update(gameTime);
        }
    }
}
