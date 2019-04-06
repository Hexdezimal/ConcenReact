using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    abstract class MainMenuEntry
    {
        private string name;
        private bool pressed;

        public MainMenuEntry(string name)
        {
            this.name = name;
            pressed = false;
        }


        public void Press()
        {
            Pressed = true;
        }

        public string Name { get => name; set => name = value; }
        public bool Pressed { get => pressed; set => pressed = value; }
    }
}
