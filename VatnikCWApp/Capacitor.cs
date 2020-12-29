using Humanizer;
using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatnikCWApp
{
    enum CapTypes
    {
        [Description("Ceramic")]
        Ceramic = 0,
        [Description("Film")]
        Film = 1,
        [Description("Electrolytic")]
        Electrolytic = 2
    };

    [Table(Name = "Capacitors")]
    class Capacitor
    {
        [Column(IsPrimaryKey = true)]
        public int CapId { get; set; }

        [Column(Name = "CapName")]
        public string Name { get; set; }

        [Column(Name = "Capacity")]
        public float Capacity { get; set; }

        [Column(Name = "CapType")]
        public CapTypes Type { get; set; }


        public override string ToString()
        {
            return "Cap:     Id: " + this.CapId + " , Name: " + this.Name + " , Capacity: " + this.Capacity +
                " , Type: " + this.Type.Humanize();
        }

        public void Insert(float price, DataContext db)
        {
            ITable<Element> elems = db.GetTable<Element>();
            elems.Value(el => el.Name, this.Name).Value(el => el.Type, ElTypes.Capacitor).Value(el => el.Price, price).Insert();
            Element e = elems.ToList<Element>().Last();
            this.CapId = e.Id;
            db.Insert(this);
        }

        public List<string> ToStringList()
        {
            List<string> res = new List<string> { this.CapId.ToString(), this.Name,this.Capacity.ToString(), this.Type.Humanize()};
            return res;
        }

    }
}
