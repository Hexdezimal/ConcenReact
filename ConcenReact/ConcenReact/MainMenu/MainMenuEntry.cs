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
        private DebugForm debugForm;

        public MainMenuEntry(DebugForm debugForm, string name)
        {
            this.name = name;
            pressed = false;
            this.DebugForm = debugForm;
        }

        protected MainMenuEntry(string name)
        {
            this.name = name;
        }

        public virtual void Press()
        {
            Pressed = true;
        }

        public string Name { get => name; set => name = value; }
        public bool Pressed { get => pressed; set => pressed = value; }
        public DebugForm DebugForm { get => debugForm; set => debugForm = value; }
    }
}
