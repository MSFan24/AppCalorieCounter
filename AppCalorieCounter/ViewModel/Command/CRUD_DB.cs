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
        public static void CreateNewProduct(Product product, ObservableCollection<Product> oblist)
        {
            MessageBox.Show("Все работает кнопка Жим-Жим");
            oblist.Add(product);
        }
    }
 }

