using System;
using System.Collections.Generic;
using System.Text;

namespace AppCalorieCounter.Model
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Product_quantity { get; set; }
        public int Caloric_in_100_units_of_mass { get; set; } //Калорийность продукта на 100 едениц массыы.Еденицы массы либо Мл либо грамм
    }
}
