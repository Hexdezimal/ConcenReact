using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class VisualMenuEntryChangeIntValueOption : VisualMenuEntryOption
    {
        private int value;
        private int maxValue;
        private int minValue;
        private string valueTitle;
        public VisualMenuEntryChangeIntValueOption(AssetHandler asset, Size windowSize,int value,int maxValue,int minValue,string valTitle) : base(asset, windowSize)
        {
            this.Value = value;
            ValueTitle = valTitle;

            this.maxValue = maxValue;
            this.minValue = minValue;

            Refresh();
        }
        public override void Action(int status, int entryX, int entryY, Graphics g)
        {
            if(status==1)
            {
                if (value + 1 <= maxValue)
                    value++;
                
            }
            else if(status==0)
            {
                if (value - 1 >= MinValue)
                    value--;
            }
            Refresh();
        }
        public void Refresh()
        {
            Title = ValueTitle + ": " + value;   
        }
        public int Value { get => value; set => this.value = value; }
        public int MaxValue { get => maxValue; set => maxValue = value; }
        public int MinValue { get => minValue; set => minValue = value; }
        public string ValueTitle { get => valueTitle; set => valueTitle = value; }
    }
}
