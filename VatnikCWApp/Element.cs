using Humanizer;
using LinqToDB;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VatnikCWApp
{

    enum ElTypes
    {
        [Description("Резистор")]
        Resistor = 0,
        [Description("Конденсатор")]
        Capacitor = 1,
        [Description("Диод")]
        Diode = 2,
        [Description("Полевой Транзистор")]
        Field_Effect_Transistor = 3,
        [Description("Биполярный транзистор")]
        Bipolar_Transistor = 4
    };

    [Table(Name = "Elements")]
    class Element
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set;}

        [Column(Name = "ElementName")]
        public string Name { get; set; }

        [Column(Name = "ElementType")]
        public ElTypes Type { get; set; }

        [Column(Name = "Price")]
        public float Price { get; set; }

        public override string ToString()
        {
            return "Element:     Id: " + this.Id + " , Name: " + this.Name + " , Type: " + this.Type.Humanize() +
                " , Price: " + this.Price;
        }
    }
}
