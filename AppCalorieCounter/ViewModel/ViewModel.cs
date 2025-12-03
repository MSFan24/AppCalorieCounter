using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using AppCalorieCounter.Data;
using AppCalorieCounter.Model;
using AppCalorieCounter.ViewModel.Command;
using AppCalorieCounter;

namespace AppCalorieCounter.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        #region Команды
        public ICommand AddNewProductInDBCommand { get; }
        #endregion

        #region Свойства и поля для передачи через Bindingi в Продукт
        private string productName;
        public string ProductName { get => productName; set { productName = value; OnPropertyChanged(); } } 

        private int productCallIn100;
        public int ProductCallIn100 { get => productCallIn100; set { productCallIn100 = value; OnPropertyChanged(); } }

        private int productQuantity;
        public int ProductQuantity { get => productQuantity; set { productQuantity = value; OnPropertyChanged(); } }
        private bool measurementSystemIsChecke;
        public bool MeasurementSystemIsChecked { get => measurementSystemIsChecke; set { measurementSystemIsChecke = value; OnPropertyChanged(); } }

        private ObservableCollection<Product> oBList;
        public ObservableCollection<Product> OBList { get => oBList; set { oBList = value; OnPropertyChanged(); } }

        #endregion








        public ViewModel()
        {
            AppDbContext.EnsureDatabaseCreated();  //Создаем БД

            OBList = new ObservableCollection<Product>();

            AddNewProductInDBCommand = new RelayCommand(
                execute: () =>
                {
                    CRUD_DB.CreateNewProduct(ProductName, ProductCallIn100, ProductQuantity, MeasurementSystemIsChecked, OBList);
                },
                canExecute: () => true
                );







        }



    }
}






