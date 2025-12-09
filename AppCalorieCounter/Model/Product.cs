using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppCalorieCounter.Model
{
    public class Product : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int product_quantity;
        private int caloric_in_100_units_of_mass;
        private bool measurement_system = false;
        private bool isSelected = false;
        private double calories_in_1_gram_of_product;//Кк в одном грамме продукта
        private double calories_in_product_of_specified_weight;
        private double number_calories_in_product_during_period; //Количество калорий в продукте за выбранный период времени 


        public int Id 
        { get => id; set { id = value; OnPropertyChanged(); } }
        public string Name 
        { get => name; set { name = value; OnPropertyChanged(); } }
        public int Product_quantity //Количество продукта по массе/объему
        {
            get => product_quantity;
            set
            {
                product_quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Caloric_in_100_units_of_mass));
                MessageBox.Show($"Свойсво изменилось Product_quantity!{" " + product_quantity}"); 


            }
        }
        public int Caloric_in_100_units_of_mass //Калорийность продукта на 100 едениц массы.Еденицы массы либо Мл либо грамм
        {
            get => caloric_in_100_units_of_mass;
            set
            {
                caloric_in_100_units_of_mass = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Calories_in_1_gram_of_product));
                OnPropertyChanged(nameof(Calories_in_product_of_specified_weight));

            }
        }
        public bool Measurement_system //Система измирения продукта по умолчанию-лож значит в граммах истина значит в милилитрах
        { get => measurement_system; set { measurement_system = value; OnPropertyChanged(); } }
        [NotMapped]
        public bool IsSelected //Атрибут указывает EF что не нужно сохранять в БД это свойство у объекта
        { get => isSelected; set { isSelected = value; OnPropertyChanged(); } }

        public double Calories_in_1_gram_of_product
        {
            get => calories_in_1_gram_of_product;
            set { calories_in_1_gram_of_product=value; OnPropertyChanged(); 
                MessageBox.Show($"Свойсво изменилось Calories_in_1_gram_of_product!{" " + calories_in_1_gram_of_product}");
            }


        }
        public double Calories_in_product_of_specified_weight
        {
            get => calories_in_product_of_specified_weight;
            set { calories_in_product_of_specified_weight = value;
                OnPropertyChanged();
            }
         
        }
       
        public Product(int id, string name, int product_quantity, int caloric_in_100_units_of_mass, bool measurement_system)
        {

            Id = id;
            Name = name;
            Product_quantity = product_quantity;
            Caloric_in_100_units_of_mass = caloric_in_100_units_of_mass;
            Measurement_system = measurement_system;
            Calories_in_1_gram_of_product = (double)caloric_in_100_units_of_mass/100;
            Calories_in_product_of_specified_weight = 0;
            MessageBox.Show($"Объект созданн Id: {Id}, name: {Name} ,Product_quantity: {Product_quantity} ,Caloric_in_100_units_of_mass: {Caloric_in_100_units_of_mass} Calories_in_1_gram_of_product:{Calories_in_1_gram_of_product}");
            // Number_calories_in_product_during_period = 0;
            //  Calories_in_product_of_specified_weight = 0;

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }



    }
}
