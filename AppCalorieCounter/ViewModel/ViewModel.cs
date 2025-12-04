using AppCalorieCounter;
using AppCalorieCounter.Data;
using AppCalorieCounter.Model;
using AppCalorieCounter.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;


namespace AppCalorieCounter.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        #region Команды
        public ICommand AddNewProductInDBCommand { get; }

        public  ICommand СhangerProducInDBCommand {  get; }


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

        private Product selectedProduct;
        public Product SelectedProduct
        { get => selectedProduct; 
          set { selectedProduct = value; OnPropertyChanged(); OnselectedProductChanged(value); }
        }

       public event Action<Product> EventHandlerPropertyChanged;

        protected virtual void OnselectedProductChanged(Product product)
        {
            EventHandlerPropertyChanged?.Invoke(product);
        }

        //public void EmptyTextBoxs()//Очистка текбоксов после нажатия кнопки Добавить
        //{
        //    ProductName = null;
        //    ProductCallIn100 = 0;
        //    ProductQuantity = 0;
        //    MeasurementSystemIsChecked = false;

        //}

        public void Refreh(Product SelectedProduct) //Метод вызывается после того как генерируется событие EventHandlerPropertyChanged которое говорит что в свойство попал новый объект 
        {
            ProductName = SelectedProduct.Name;
            ProductCallIn100 = SelectedProduct.Caloric_in_100_units_of_mass;
            ProductQuantity = SelectedProduct.Product_quantity;
            MeasurementSystemIsChecked = SelectedProduct.Measurement_system; 
        }



        public void ChangerMethod(Product SelectedProduct)//Метод изменяет тексбоксы после нажатия на кнопку изменить
        {
            SelectedProduct.Name = ProductName;
            SelectedProduct.Caloric_in_100_units_of_mass = ProductCallIn100;
            SelectedProduct.Product_quantity = ProductQuantity;
            SelectedProduct.Measurement_system = MeasurementSystemIsChecked;
        }

        private Product newProduct;
        public Product NewProduct { get => newProduct; set { newProduct = value; OnPropertyChanged(); } }





        public ViewModel()
        {
            AppDbContext.EnsureDatabaseCreated();  //Создаем БД
            
            OBList = new ObservableCollection<Product>();
            EventHandlerPropertyChanged += Refreh;


            AddNewProductInDBCommand = new RelayCommand(
                execute: () =>
                {
                    var newProduct = new Product(ProductName, ProductQuantity, ProductCallIn100, MeasurementSystemIsChecked);
                    CRUD_DB.CreateNewProduct(newProduct, OBList);
                    //OBList.Add(newProduct);


                    //EmptyTextBoxs();
                },
                canExecute: () => true
                );
            СhangerProducInDBCommand = new RelayCommand(
                execute: () => ChangerMethod(SelectedProduct),
                canExecute: () => true );









        }



    }
}






