using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace AppCalorieCounter.Model
{
    public class Product : INotifyPropertyChanged
    {
        private string name;
        private int product_quantity;
        private int caloric_in_100_units_of_mass;
        private bool measurement_system = false;
        private bool isSelected=false;


        public int Id { get; set; }
        public string Name { get=> name; set { name = value; OnPropertyChanged(); } } 
        public int Product_quantity { get=> product_quantity; set { product_quantity = value; OnPropertyChanged(); } } //Количество продукта по массе/объему
        public int Caloric_in_100_units_of_mass { get=> caloric_in_100_units_of_mass; set { caloric_in_100_units_of_mass = value; OnPropertyChanged(); } } //Калорийность продукта на 100 едениц массыы.Еденицы массы либо Мл либо грамм
        public bool Measurement_system { get=> measurement_system; set { measurement_system = value; OnPropertyChanged(); } }  //Система измирения продукта по умолчанию-лож значит в граммах истина значит в милилитрах
        [NotMapped] public bool IsSelected { get=> isSelected; set { isSelected = value; OnPropertyChanged(); } } //Атрибут указывает EF что не нужно сохранять в БД это свойство у объекта

        public Product(string name,int product_quantity,int caloric_in_100_units_of_mass, bool measurement_system)
        { 
        
            Id += 1;
            Name = name;
            Product_quantity = product_quantity;
            Caloric_in_100_units_of_mass = caloric_in_100_units_of_mass;
            Measurement_system =measurement_system;


        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }



    }
}
