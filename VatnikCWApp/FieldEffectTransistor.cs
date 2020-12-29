using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatnikCWApp
{

    [Table(Name = "FieldEffectTransistors")]
    class FieldEffectTransistor
    {

        [Column(IsPrimaryKey = true)]
        public int FETId { get; set; }

        [Column(Name = "FETName")]
        public string Name { get; set; }

        [Column(Name = "MaxDSV")]
        public float MaxDSVoltage { get; set; }

        [Column(Name = "MaxDSCur")]
        public float MaxDSCurrent { get; set; }

        [Column(Name = "OpChRes")]
        public float OpenChanelResistance { get; set; }

        [Column(Name = "ReMV")]
        public float ReMVoltage { get; set; }

        [Column(Name = "ReMCur")]
        public float ReMCurrent { get; set; }

        public override string ToString()
        {
            return "FET:     Id: " + this.FETId + " , Name: " + this.Name + " , Max DS Voltage: " + this.MaxDSVoltage +
                " , Max DS Current: " + this.MaxDSCurrent + " , Open Chanel Resistance: " + this.OpenChanelResistance +
                " , Reverse Measurement Voltage: " + this.ReMVoltage + " , Reverse Measurement Current: " + this.ReMCurrent;
        }

        public void Insert(float price, DataContext db)
        {
            ITable<Element> elems = db.GetTable<Element>();
            elems.Value(el => el.Name, this.Name).Value(el => el.Type, ElTypes.Field_Effect_Transistor).Value(el => el.Price, price).Insert();
            Element e = elems.ToList<Element>().Last();
            this.FETId = e.Id;
            db.Insert(this);
        }

        public List<string> ToStringList()
        {
            List<string> res = new List<string> { this.FETId.ToString(), this.Name, this.MaxDSVoltage.ToString(),
                this.MaxDSCurrent.ToString(), this.OpenChanelResistance.ToString(), this.ReMVoltage.ToString(),
                this.ReMCurrent.ToString() };
            return res;
        }

    }
}
