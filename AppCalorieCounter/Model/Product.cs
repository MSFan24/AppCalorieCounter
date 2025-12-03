using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Controls;

namespace AppCalorieCounter.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Product_quantity { get; set; } //Количество продукта по массе/объему
        public int Caloric_in_100_units_of_mass { get; set; } //Калорийность продукта на 100 едениц массыы.Еденицы массы либо Мл либо грамм
        public bool Measurement_system { get; set; } = false; //Система измирения продукта по умолчанию-лож значит в граммах истина значит в милилитрах
        [NotMapped] public bool IsSelected { get; set; } = false;//Атрибут указывает EF что не нужно сохранять в БД это свойство у объекта

        public Product(string name,int product_quantity,int caloric_in_100_units_of_mass, bool measurement_system)
        { 
        
            Id += 1;
            Name = name;
            Product_quantity = product_quantity;
            Caloric_in_100_units_of_mass = caloric_in_100_units_of_mass;
            Measurement_system =measurement_system;


        }






    }
}
