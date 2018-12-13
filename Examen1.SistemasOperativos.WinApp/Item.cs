using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.WinApp
{
    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Item(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
