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
    enum ResTypes
    {
        [Description("Low Power")]
        Low_Power_Resistor = 0,
        [Description("Power")]
        Power_Resistor = 1
    };

    [Table(Name = "Resistors")]
    class Resistor
    {
        [Column(IsPrimaryKey = true)]
        public int ResId { get; set; }

        [Column(Name = "ResName")]
        public string Name { get; set; }

        [Column(Name = "Resistance")]
        public float Resistance { get; set; }

        [Column(Name = "NomPow")]
        public float NominalPower { get; set; }


        [Column(Name = "ResType")]
        public ResTypes Type { get; set; }

        public override string ToString()
        {
            return "Res:     Id: " + this.ResId + " , Name: " + this.Name + " , Resistance: " + this.Resistance +
                " , Nominal power: " + this.NominalPower + " , Type:" + this.Type.Humanize();
        }


        public void Insert(float price, DataContext db)
        {
            ITable<Element> elems = db.GetTable<Element>();
            elems.Value(el => el.Name, this.Name).Value(el => el.Type, ElTypes.Resistor).Value(el => el.Price, price).Insert();
            Element e = elems.ToList<Element>().Last();
            this.ResId = e.Id;
            db.Insert(this);
        }

        public List<string> ToStringList()
        {
            List<string> res = new List<string> { this.ResId.ToString(), this.Name,this.Resistance.ToString(),
                this.NominalPower.ToString(),this.Type.Humanize() };
            return res;
        }

    }

}
