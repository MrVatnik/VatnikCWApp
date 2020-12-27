using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatnikCWApp
{

    [Table(Name = "Diodes")]
    class Diode
    {

        [Column(IsPrimaryKey = true)]
        public int DioId { get; set; }

        [Column(Name = "DioName")]
        public string Name { get; set; }

        [Column(Name = "MaxReV")]
        public float MaxReVoltage { get; set; }

        [Column(Name = "MaxForV")]
        public float MaxForVoltage { get; set; }

        [Column(Name = "MaxReCur")]
        public float MaxReCurrent { get; set; }

        [Column(Name = "MaxForCur")]
        public float MaxForCurrent { get; set; }


        public override string ToString()
        {
            return "Dio:     Id: " + this.DioId + " , Name: " + this.Name + " , Max Reverse Voltage: " +
                this.MaxReVoltage + " , Max Forward Voltage: " + this.MaxForVoltage +
                " , Max Reverse Current: " + this.MaxReCurrent + " , Max Forward Current: " + this.MaxForCurrent;
        }

        public void Insert(float price, DataContext db)
        {
            ITable<Element> elems = db.GetTable<Element>();
            elems.Value(el => el.Name, this.Name).Value(el => el.Type, ElTypes.Diode).Value(el => el.Price, price).Insert();
            Element e = elems.ToList<Element>().Last();
            this.DioId = e.Id;
            db.Insert(this);
        }

    }
}