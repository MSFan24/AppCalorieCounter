using System;
using System.Collections.Generic;
using System.Text;
using AppCalorieCounter.Data;

namespace AppCalorieCounter.ViewModel
{
    public class ViewModel
    {

        public ViewModel()
        {
            AppDbContext.EnsureDatabaseCreated();
        }

    }
}
