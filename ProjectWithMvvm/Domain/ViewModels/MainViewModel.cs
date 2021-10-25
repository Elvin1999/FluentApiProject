using ProjectWithMvvm.Commands;
using ProjectWithMvvm.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectWithMvvm.Domain.ViewModels
{
   public class MainViewModel:BaseViewModel
    {
        public RelayCommand SelectCustomerCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }

        public MainViewModel()
        {
            AllCustomers = App.DB.CustomerRepository.GetAllData();
            AllOrders = App.DB.OrderRepository.GetAllData();
            ResetCommand = new RelayCommand((sender) =>
              {
                  SelectedCustomer = new Customer();
              });
            SelectCustomerCommand = new RelayCommand((sender) =>
              {
                  if (SelectedCustomer != null)
                  {
                  AllOrders = new ObservableCollection<Order>(SelectedCustomer.Orders);
                  }
              });

       

            AddCommand = new RelayCommand((sender) =>
              {
                  var item = App.DB.CustomerRepository.GetData(SelectedCustomer.Id);
                  if (item == null)
                  {
                  App.DB.CustomerRepository.AddData(SelectedCustomer);
                  AllCustomers = App.DB.CustomerRepository.GetAllData();
                  MessageBox.Show("Add Successfully");
                  }
                  else
                  {
                      MessageBox.Show("This customer is already exists");
                  }
                  SelectedCustomer = new Customer();
              });

            UpdateCommand = new RelayCommand((sender) =>
              {
                  App.DB.CustomerRepository.UpdateData(SelectedCustomer);
                  MessageBox.Show("Update Successfully");
                  SelectedCustomer = new Customer();
              });

        }

        private ObservableCollection<Customer> allCustomers;

        public ObservableCollection<Customer> AllCustomers
        {
            get { return allCustomers; }
            set { allCustomers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Order> allOrders;

        public ObservableCollection<Order> AllOrders
        {
            get { return allOrders; }
            set { allOrders = value; OnPropertyChanged(); }
        }

        private Customer selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value; OnPropertyChanged(); }
        }


    }
}
