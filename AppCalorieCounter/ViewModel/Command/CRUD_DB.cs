using AppCalorieCounter.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AppCalorieCounter.ViewModel;
using System.Collections.ObjectModel;

namespace AppCalorieCounter.ViewModel.Command
{
    public static class CRUD_DB
    {
        public static void CreateNewProduct(string name, int product_quantity, int caloric_in_100_units_of_mass, bool measurement_system, ObservableCollection<Product> oblist)
        {
            MessageBox.Show("Все работает кнопка Жим-Жим");

            var NewProduct = new Product(name, product_quantity, caloric_in_100_units_of_mass, measurement_system);

            oblist.Add(NewProduct);



        }
    }
}
