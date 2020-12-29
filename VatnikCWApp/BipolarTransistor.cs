using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatnikCWApp
{

    [Table(Name = "BipolarTransistors")]
    class BipolarTransistor
    {

        [Column(IsPrimaryKey = true)]
        public int BTId { get; set; }

        [Column(Name = "BTName")]
        public string Name { get; set; }

        [Column(Name = "MaxCEV")]
        public float MaxCEVoltage { get; set; }

        [Column(Name = "MaxCCur")]
        public float MaxCCurrent { get; set; }

        [Column(Name = "CutoffFreq")]
        public float CutoffFrequency { get; set; }

        [Column(Name = "MaxPowLoss")]
        public float MaxPowerLoss { get; set; }

        public override string ToString()
        {
            return "BiT:     Id: " + this.BTId + " , Name: " + this.Name + " , Max CE Voltage: " + this.MaxCEVoltage +
                " , Max C Current: " + this.MaxCCurrent + " , Cutoff Frequency: " + this.CutoffFrequency +
                " , Max Power Loss: " + this.MaxPowerLoss;
        }

        public void Insert(float price, DataContext db)
        {
            ITable<Element> elems = db.GetTable<Element>();
            elems.Value(el => el.Name, this.Name).Value(el => el.Type, ElTypes.Bipolar_Transistor).Value(el => el.Price, price).Insert();
            Element e = elems.ToList<Element>().Last();
            this.BTId = e.Id;
            db.Insert(this);
        }

        public List<string> ToStringList()
        {
            List<string> res = new List<string> { this.BTId.ToString(), this.Name, this.MaxCEVoltage.ToString(), 
                this.MaxCCurrent.ToString(), this.CutoffFrequency.ToString(), this.MaxPowerLoss.ToString() };
            return res;
        }


    }
}
