using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppCalorieCounter.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Product_quantity { get; set; } //Количество продукта по массе/объему
        public int Caloric_in_100_units_of_mass { get; set; } //Калорийность продукта на 100 едениц массыы.Еденицы массы либо Мл либо грамм
        public bool Measurement_system { get; set; } = false; //Система измирения продукта по умолчанию-лож значит в граммах истина значит в милилитрах
        [NotMapped] public bool IsSelected { get; set; }//Атрибут указывает EF что не нужно сохранять в БД это свойство у объекта

    }
}
