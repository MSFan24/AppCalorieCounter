using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppCalorieCounter.Model
{
   public class Person :INotifyPropertyChanged
    {
        private string name;
        private int weight;
        private int height;
        private bool gender;
        private string leval_Activity;
        private string desired_result;




        public string Name
        {
            get => name; set { name = value; OnPropertyChanged(); }
        }
        public int Weight
        {
            get => weight; set { weight = value; OnPropertyChanged(); }
        }
        public int Height
        {
            get => height; set { height = value; OnPropertyChanged(); }
        }
        public bool Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }
        public string Leval_Activity // Уровень активности
        {
            get => leval_Activity;
            set { leval_Activity = value; OnPropertyChanged(); }

        } 
        public string Desired_result // Желаемый результат
        { get => desired_result;
            set { desired_result = value; OnPropertyChanged(); }
        }

        public Person(string name,int weight,int height,bool gender,string leval_Activity,string desired_result)
        {
            Name = name;
            Weight = weight;
            Height = height;
            Gender = gender;
            Leval_Activity = leval_Activity;
            Desired_result = desired_result;
        }






        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
