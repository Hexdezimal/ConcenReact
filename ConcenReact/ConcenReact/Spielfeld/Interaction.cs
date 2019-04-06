using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Interaction
    {
        private string interactionName;
        private string interactionText;
        private bool interacted;
        public Interaction(string interName, string interText)
        {
            InteractionName = interName;
            InteractionText = interText;
            Interacted = false;
        }

        //Properties
        public bool Interacted { get => interacted; set => interacted = value; }
        public string InteractionText { get => interactionText; set => interactionText = value; }
        public string InteractionName { get => interactionName; set => interactionName = value; }
    }
}
