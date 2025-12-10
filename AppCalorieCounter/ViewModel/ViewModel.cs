using AppCalorieCounter;
using AppCalorieCounter.Data;
using AppCalorieCounter.Model;
using AppCalorieCounter.ViewModel.Command;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
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
        public ICommand Method_entering_caloric_content_in_productCommand { get; }
        public ICommand Method_constructing_pie_chart_of_colorCommand { get; }


        #endregion


        #region Свойства и поля для передачи через Bindingi в Продукт

        private int vm_productId;
        private string vm_productName;
        private int vm_productCallIn100;
        private int vm_productQuantity;
        private bool vm_measurementSystemIsChecke;
        public double vm_calories_in_product_of_specified_weight;
        private Product vm_selectedProductinDataGrid_1;
        private Product vm_selectedProductinDataGrid_2;
        private Product newProduct;
        private ObservableCollection<Product> obProducts;
        private ObservableCollection<Product> obSelektProductl;
        private string texBlockProgress;

        public int VM_ProductId
        { get => vm_productId; set { vm_productId = value; OnPropertyChanged(); } }
        public string VM_ProductName
        { get => vm_productName; set { vm_productName = value; OnPropertyChanged(); } }
        public int VM_ProductCallIn100
        { get => vm_productCallIn100; set { vm_productCallIn100 = value; OnPropertyChanged(); } }
        public int VM_ProductQuantity
        {
            get => vm_productQuantity;
            set
            {
                vm_productQuantity = value;
                OnPropertyChanged();

            }
        }
        public bool VM_MeasurementSystemIsChecked
        { get => vm_measurementSystemIsChecke; set { vm_measurementSystemIsChecke = value; OnPropertyChanged(); } }
        public double VM_Calories_in_product_of_specified_weight
        { get => vm_calories_in_product_of_specified_weight; set { vm_calories_in_product_of_specified_weight = value; OnPropertyChanged(); } }
        public ObservableCollection<Product> OBProducts
        { get => obProducts; set { obProducts = value; OnPropertyChanged(); } }
        public ObservableCollection<Product> OBSelektProduct
        { get => obSelektProductl; set { obSelektProductl = value; OnPropertyChanged(); } } // Коллекция для отсортированных продуктов с флагом выбран

        public string TexBlockProgress //Свойсво для отображения релузальтатов добавления продукта удаления продукта изминения продукта переноса выбранных продуктов
        { get => texBlockProgress;
        set { texBlockProgress = value; OnPropertyChanged(); }
        }

        public Product VM_SelectedProductinDataGrid_1
        {
            get => vm_selectedProductinDataGrid_1;
            set { vm_selectedProductinDataGrid_1 = value; OnPropertyChanged(); OnselectedProductChanged(value); }
        }
        public Product VM_SelectedProductinDataGrid_2
        {
            get => vm_selectedProductinDataGrid_2;
            set
            {
                vm_selectedProductinDataGrid_2 = value; OnPropertyChanged();
                //OnselectedProductChanged(value); 
            }
        }
        public Product NewProduct
        { get => newProduct; set { newProduct = value; OnPropertyChanged(); } }

        #endregion

        #region Круговая диаграмма
        private SeriesCollection pieSeries;
        public SeriesCollection PieSeries
        { get => pieSeries; set { pieSeries = value; OnPropertyChanged(); } }
        public ChartValues<double> Values1 { get; set; } = new ChartValues<double> { 20 };

        #endregion

        public event Action<Product> EventHandlerPropertyChanged;
        protected virtual void OnselectedProductChanged(Product product)
        {
            EventHandlerPropertyChanged?.Invoke(product);
        }
        public void MethodCreateNewProduct(Product product, ObservableCollection<Product> oblist, Product VM_SelectedProductinDataGrid_1)
        {
            VM_SelectedProductinDataGrid_1 = null;
            var newProduct = new Product(VM_ProductId, VM_ProductName, VM_ProductQuantity, VM_ProductCallIn100, VM_MeasurementSystemIsChecked);
            //   MessageBox.Show("Все работает кнопка Жим-Жим");
            var CountItems = oblist.Count;

            newProduct.Id = CountItems + 1;
            oblist.Add(newProduct);
            TexBlockProgress = $"Продукт {newProduct.Name} добавлен!";
        }
        public void EmptyTextBoxs()//Очистка текбоксов после нажатия кнопки Добавить
        {
            VM_ProductName = "";
            VM_ProductCallIn100 = 0;
            VM_ProductQuantity = 0;
            VM_MeasurementSystemIsChecked = false;

        }
        public void Refreh(Product SelectedProduct) //Метод вызывается после того как генерируется событие EventHandlerPropertyChanged которое говорит что в свойство попал новый объект 
        {
            VM_ProductName = SelectedProduct.Name;
            VM_ProductCallIn100 = SelectedProduct.Caloric_in_100_units_of_mass;
            VM_ProductQuantity = SelectedProduct.Product_quantity;
            VM_MeasurementSystemIsChecked = SelectedProduct.Measurement_system;
        }
        public void ChangerProductMethod(Product SelectedProductinDataGrid)//Метод изменяет тексбоксы после нажатия на кнопку изменить
        {
            SelectedProductinDataGrid.Name = VM_ProductName;
            SelectedProductinDataGrid.Caloric_in_100_units_of_mass = VM_ProductCallIn100;
            SelectedProductinDataGrid.Product_quantity = VM_ProductQuantity;
            SelectedProductinDataGrid.Measurement_system = VM_MeasurementSystemIsChecked;
            TexBlockProgress = $"Продукт {SelectedProductinDataGrid.Name} отредактирован!";
        }
        public void Method_transferring_selected_products_to_another_table(ObservableCollection<Product> OBProducts, ObservableCollection<Product> OBSelektProduct)
        {
            OBSelektProduct.Clear();
            var SortProductsIsSelected = OBProducts.Where(p => p.IsSelected).ToList();
            foreach (var p in SortProductsIsSelected)
            {
                OBSelektProduct.Add(p);
            }
            TexBlockProgress = "Выбранные продукты перенесены в Выбранные продукты";
        }
        public void Method_entering_caloric_content_in_product(Product SelectedProductforDataGrid)
        {
            if (SelectedProductforDataGrid != null)
            {
                SelectedProductforDataGrid.Product_quantity = VM_ProductQuantity;
                SelectedProductforDataGrid.Calories_in_product_of_specified_weight = SelectedProductforDataGrid.Product_quantity * SelectedProductforDataGrid.Calories_in_1_gram_of_product;
                SelectedProductforDataGrid = null;


            }
        }
        public void Method_constructing_pie_chart_of_color()
        {
            PieSeries = Method_create_new_diagramPieCharts(PieSeries, OBSelektProduct);
        }

        public SeriesCollection Method_create_new_diagramPieCharts(SeriesCollection PieSeries, ObservableCollection<Product> OBSelektProduct)
        {
            
            PieSeries = new SeriesCollection();

            int ir;

            foreach (var item in OBSelektProduct)
            {
                ir = item.Id;
                var t = new PieSeries
                {
                    Title = $"{item.Name}",
                    Values = Values1 = new ChartValues<double> { item.Calories_in_product_of_specified_weight }
                };
                PieSeries.Add(t);
            }



            return PieSeries;
        }


        public ViewModel()
        {
            //AppDbContext.EnsureDatabaseCreated();  //Создаем БД

            OBProducts = new ObservableCollection<Product>();
            OBProducts.Add(new Product(VM_ProductId = OBProducts.Count + 1, VM_ProductName = "Мясо", VM_ProductQuantity = 0, VM_ProductCallIn100 = 100, VM_MeasurementSystemIsChecked = true));
            OBProducts.Add(new Product(VM_ProductId = OBProducts.Count + 1, VM_ProductName = "Рыба", VM_ProductQuantity = 0, VM_ProductCallIn100 = 200, VM_MeasurementSystemIsChecked = true));
            OBProducts.Add(new Product(VM_ProductId = OBProducts.Count + 1, VM_ProductName = "Творог", VM_ProductQuantity = 0, VM_ProductCallIn100 = 300, VM_MeasurementSystemIsChecked = true));
            OBProducts.Add(new Product(VM_ProductId = OBProducts.Count + 1, VM_ProductName = "Масло", VM_ProductQuantity = 0, VM_ProductCallIn100 = 100, VM_MeasurementSystemIsChecked = true));
            OBProducts.Add(new Product(VM_ProductId = OBProducts.Count + 1, VM_ProductName = "Яйцо", VM_ProductQuantity = 0, VM_ProductCallIn100 = 800, VM_MeasurementSystemIsChecked = true));

            OBSelektProduct = new ObservableCollection<Product>();
            EventHandlerPropertyChanged += Refreh;


            #region DataGrid_1
            AddNewProductInDBCommand = new RelayCommand(
                execute: () =>
                {

                    MethodCreateNewProduct(newProduct, OBProducts, VM_SelectedProductinDataGrid_1);
                    //EmptyTextBoxs();
                },
                canExecute: () => true
                );
            СhangerProducInDBCommand = new RelayCommand(
                execute: () => ChangerProductMethod(VM_SelectedProductinDataGrid_1),
                canExecute: () =>
                {
                    if (vm_selectedProductinDataGrid_1 != null)
                    { return true; }
                    else
                    {

                        return false;
                    }
                });
            DeleteProducInDBCommand = new RelayCommand(
                execute: () => { CRUD_DB.DeleteProduct(VM_SelectedProductinDataGrid_1, OBProducts); TexBlockProgress = $"Продукт{VM_SelectedProductinDataGrid_1.Name} удален!"; },
                canExecute: () => true);
           
            Transferring_selected_products_to_another_table_Command = new RelayCommand(
            execute: () =>
            {
                Method_transferring_selected_products_to_another_table(OBProducts, OBSelektProduct);

            },
            canExecute: () => true

            );//Перенос выбранных товаров в другую таблицу 
            #endregion


            Method_entering_caloric_content_in_productCommand = new RelayCommand(
                execute: () => { Method_entering_caloric_content_in_product(VM_SelectedProductinDataGrid_2); },
                canExecute: () => //Проверка чтобы был выбран продукт
                {
                    if (VM_SelectedProductinDataGrid_2 != null)
                    { return true; }
                    else
                    {

                        return false;
                    }
                }
                );

            Method_constructing_pie_chart_of_colorCommand = new RelayCommand(
                execute: () => { Method_constructing_pie_chart_of_color(); },
                canExecute: () => true
                );

        }

       
    }
}






