﻿using ProjectWithMvvm.Contexts;
using ProjectWithMvvm.Domain.Abstractions;
using ProjectWithMvvm.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWithMvvm.DataAccess.EFrameworkServer
{
    public class EFCustomerRepository : ICustomerRepository
    {
        public void AddData(Customer data)
        {
            using (var context=new MyContext())
            {
                context.Customers.Add(data);
                context.SaveChanges();
            }
        }

        public void DeleteData(Customer data)
        {
            using (var context = new MyContext())
            {
                    if(data.Orders!=null && data.Orders.Count!=0)
                    foreach (var item in data.Orders)
                    {
                        App.DB.OrderRepository.DeleteData(item);
                    }
                context.Entry(data).State = EntityState.Deleted;
                context.SaveChanges();

               
            }
        }

        public ObservableCollection<Customer> GetAllData()
        {
            var customers = new ObservableCollection<Customer>();
            using (var context = new MyContext())
            {

                customers =new ObservableCollection<Customer>(context.Customers);
            }
            return customers;
        }

        public Customer GetData(int id)
        {
            using (var context = new MyContext())
            {
                var data = context.Customers.Include("Orders").FirstOrDefault(c => c.Id == id);
                return data;
            }

        }

        public void UpdateData(Customer data)
        {
            using (var context=new MyContext())
            {
                context.Entry(data).State =EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
