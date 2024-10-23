using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class Item
    {
        public string ItemName { get; set; }

        public Item(string itemName)
        {
            ItemName = itemName;
        }
    }
}