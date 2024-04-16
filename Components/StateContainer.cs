using CodeSummonary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSummonary.Components
{
    public class StateContainer : ContainableComponent
    {
        public StateContainer(Vector2 size, EventHandler click = null, EventHandler<GameTime> updating = null, 
            EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> draw = null) 
        {
            States = new();
            _width = (int)size.X;
            _height = (int)size.Y;
            Click += click != null ? click : (sender, args) => { };
            Drawing += draw != null ? draw : (sender, SpriteBatch) => { };
            Updating += updating != null ? updating : (sender, SpriteBatch) => { };
        }

        public Dictionary<string, List<Component>> States;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Drawing;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Hovering;

        public event EventHandler<GameTime> Updating;

        public string CurrentState;

        public void SwitchToState(string state)
        {
            Children = States[state];
            CurrentState = state;
        }

        public bool RegisterState(string state, params Component[] components)
        {
            if (States.ContainsKey(state))
                return false;
            else
                States.Add(state, new());
            foreach (var component in components)
                States[state].Add(component);
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            Drawing?.Invoke(this, (spriteBatch, gameTime));
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Updating?.Invoke(this, gameTime);
            base.Update(gameTime);
        }
    }
}
