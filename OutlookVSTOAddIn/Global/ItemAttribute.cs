using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookVSTOAddIn.Global
{
    public partial class ItemAttribute
    {
        private string name;
        private string desc;
        private string type;
        private string qual;
        private string flag;
        private string repr;
        private string max;
        private string min;
        private string size;

        public ItemAttribute() { }

        public ItemAttribute(string name, string desc, string type, string qual, string flag, string repr, string max, string min, string size)
        {
            this.name = name;
            this.desc = desc;
            this.type = type;
            this.qual = qual;
            this.flag = flag;
            this.repr = repr;
            this.max = max;
            this.min = min;
            this.size = size;
        }

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Qual
        {
            get { return qual; }
            set { qual = value; }
        }

        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public string Repr
        {
            get { return repr; }
            set { repr = value; }
        }

        public string Max
        {
            get { return max; }
            set { max = value; }
        }

        public string Min
        {
            get { return min; }
            set { min = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }
        #endregion
    }
}
