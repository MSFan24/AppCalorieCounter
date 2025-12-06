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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Xml.Linq;


namespace AppCalorieCounter.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        #region Команды
        public ICommand AddNewProductInDBCommand { get; }

        public ICommand СhangerProducInDBCommand { get; }
        public ICommand DeleteProducInDBCommand { get; }
        public ICommand Transferring_selected_products_to_another_table_Command { get; }


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

        private ObservableCollection<Product> obProducts;
        public ObservableCollection<Product> OBProducts { get => obProducts; set { obProducts = value; OnPropertyChanged(); } }

        private ObservableCollection<Product> obSelektProductl;
        public ObservableCollection<Product> OBSelektProduct { get => obSelektProductl; set { obSelektProductl = value; OnPropertyChanged(); } } // Коллекция для отсортированных продуктов с флагом выбран

        #endregion

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set { selectedProduct = value; OnPropertyChanged(); OnselectedProductChanged(value); }
        }

        public event Action<Product> EventHandlerPropertyChanged;

        protected virtual void OnselectedProductChanged(Product product)
        {
            EventHandlerPropertyChanged?.Invoke(product);
        }

        public void EmptyTextBoxs()//Очистка текбоксов после нажатия кнопки Добавить
        {
            ProductName = null;
            ProductCallIn100 = 0;
            ProductQuantity = 0;
            MeasurementSystemIsChecked = false;

        }

        public void Refreh(Product SelectedProduct) //Метод вызывается после того как генерируется событие EventHandlerPropertyChanged которое говорит что в свойство попал новый объект 
        {
            ProductName = SelectedProduct.Name;
            ProductCallIn100 = SelectedProduct.Caloric_in_100_units_of_mass;
            ProductQuantity = SelectedProduct.Product_quantity;
            MeasurementSystemIsChecked = SelectedProduct.Measurement_system;
        }



        public void ChangerMethod(Product SelectedProduct, ObservableCollection<Product> observableCollection)//Метод изменяет тексбоксы после нажатия на кнопку изменить
        {
            int CountUtems = observableCollection.Count;

            SelectedProduct.Name = ProductName;
            SelectedProduct.Caloric_in_100_units_of_mass = ProductCallIn100;
            SelectedProduct.Product_quantity = ProductQuantity;
            SelectedProduct.Measurement_system = MeasurementSystemIsChecked;
        }

        private Product newProduct;
        public Product NewProduct { get => newProduct; set { newProduct = value; OnPropertyChanged(); } }

        public void Method_transferring_selected_products_to_another_table(ObservableCollection<Product> OBProducts,ObservableCollection<Product> OBSelektProduct)
        {
            OBSelektProduct.Clear();
            var SortProductsIsSelected = OBProducts.Where(p => p.IsSelected).ToList();
            foreach( var p in SortProductsIsSelected)
            {
                OBSelektProduct.Add(p);
            }
            
        }



        public ViewModel()
        {
            AppDbContext.EnsureDatabaseCreated();  //Создаем БД

            OBProducts = new ObservableCollection<Product> ();
            OBProducts.Add(new Product(ProductName = "Мясо", ProductQuantity = 200, ProductCallIn100 = 100, measurementSystemIsChecke = true));
            OBProducts.Add(new Product(ProductName = "Рыба", ProductQuantity = 100, ProductCallIn100 = 200, measurementSystemIsChecke = true));
            OBProducts.Add(new Product(ProductName = "Творог", ProductQuantity = 50, ProductCallIn100 = 300, measurementSystemIsChecke = true));
            OBProducts.Add(new Product(ProductName = "Масло", ProductQuantity = 500, ProductCallIn100 = 100, measurementSystemIsChecke = true));
            OBProducts.Add(new Product(ProductName = "Яйцо", ProductQuantity = 600, ProductCallIn100 = 800, measurementSystemIsChecke = true));

            OBSelektProduct = new ObservableCollection<Product>();
            EventHandlerPropertyChanged += Refreh;


            AddNewProductInDBCommand = new RelayCommand(
                execute: () =>
                {
                    var newProduct = new Product(ProductName, ProductQuantity, ProductCallIn100, MeasurementSystemIsChecked);
                    CRUD_DB.CreateNewProduct(newProduct, OBProducts);
                    EmptyTextBoxs();
                },
                canExecute: () => true
                );
            СhangerProducInDBCommand = new RelayCommand(
                execute: () => ChangerMethod(SelectedProduct, OBProducts),
                canExecute: () => true);

            DeleteProducInDBCommand = new RelayCommand(
                execute: () => { CRUD_DB.DeleteProduct(SelectedProduct, OBProducts); },
                canExecute: () => true);

            Transferring_selected_products_to_another_table_Command = new RelayCommand(
                execute: () => { Method_transferring_selected_products_to_another_table(OBProducts, OBSelektProduct); },
                canExecute: () => true  

                );






        }



    }
}






