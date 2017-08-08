using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Common
{
    public class HashTableExp:Dictionary<string,string>
    {
        public HashTableExp()
        {

        }
        
        public HashTableExp(string key,string value)
        {
            this.Add(key, value);
        }

        public new string this[string key]
        {
            get
            {
                if (base.ContainsKey(key))
                {
                    return base[key];
                }
                return null;
            }
            set
            {
            }
        }

    }
}
