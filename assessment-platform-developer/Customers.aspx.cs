using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using assessment_platform_developer.Services;
using System.ComponentModel;
using System.Diagnostics;
using Container = SimpleInjector.Container;
using System.Data.Entity.Core.Metadata.Edm;


namespace assessment_platform_developer
{
    public partial class Customers : Page
    {
        private static List<Customer> customers = new List<Customer>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear the customer list on first load
                customers.Clear();
                HeadingLabel.Text = "Add Customer";
                AddButton.Visible = true;
                UpdateButton.Visible = false;
                DeleteButton.Visible = false;
                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = testContainer.GetInstance<ICustomerServiceQry>();
                var allCustomers = customerService.GetAllCustomers();
                ViewState["Customers"] = allCustomers;
                PopulateCustomerListBox();
                PopulateCustomerDropDownLists();
            }
            else
            {
                // Retrieve customers from ViewState on postbacks
                customers = (List<Customer>)ViewState["Customers"];
            }
        }
        protected void PopulateCustomerListBox()
        {
            CustomersDDL.Items.Clear();
            // Add "Add new customer" option if not already present
            if (!CustomersDDL.Items.Cast<ListItem>().Any(li => li.Text == "Add new customer"))
                CustomersDDL.Items.Insert(0, new ListItem("Add new customer"));
            // Populate dropdown with existing customers
            var storedCustomers = customers.Select(c => new ListItem(c.Name)).ToArray();
            if (storedCustomers.Length != 0)
            {
                CustomersDDL.Items.AddRange(storedCustomers);
                return;
            }

            CustomersDDL.SelectedIndex = 0;

        }
        private void PopulateCustomerDropDownLists()
        {
            // Populate the country and province dropdown lists
            StateDropDownList.Items.Clear();
            var countryList = Enum.GetValues(typeof(Countries))
                         .Cast<Enum>()
                         .Select(e =>
                         {
                             var descriptionAttribute = e.GetType()
                                                         .GetField(e.ToString())
                                                         .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                         .FirstOrDefault() as DescriptionAttribute;
                             var text = descriptionAttribute != null ? descriptionAttribute.Description : e.ToString();
                             return new ListItem
                             {
                                 Text = text,
                                 Value = Convert.ToInt32(e).ToString()
                             };
                         })
                         .ToArray();
            CountryDropDownList.Items.AddRange(countryList);
            CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();

            var provinceList = Enum.GetValues(typeof(CanadianProvinces))
                .Cast<CanadianProvinces>()
                .Select(p => new ListItem
                {
                    Text = p.ToString(),
                    Value = ((int)p).ToString()
                })
                .ToArray();

            StateDropDownList.Items.Add(new ListItem(""));
            StateDropDownList.Items.AddRange(provinceList);
        }
        protected void ResetForm()
        {
            // Reset the form to add a new customer
            HeadingLabel.Text = "Add Customer";
            ClearCustomerForm();
            AddButton.Visible = true;
            UpdateButton.Visible = false;
            DeleteButton.Visible = false;
        }
        protected void CustomersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CustomersDDL dropdown selection changed event handler
            ErrMsgLabel.Text = string.Empty;
            ErrMsgLabel.Visible = false;
            int selectedIndex = CustomersDDL.SelectedIndex;

            if (CustomersDDL.SelectedValue == "Add new customer")
            {
                ResetForm();
            }
            else
            {
                HeadingLabel.Text = "Update/Delete Customer";

                AddButton.Visible = false;
                UpdateButton.Visible = true;
                DeleteButton.Visible = true;

                if (selectedIndex >= 0 && selectedIndex < customers.Count + 1)
                {
                    // Populate form with selected customer data
                    Customer selectedCustomer = customers[selectedIndex - 1];
                    PopulateCustomerForm(selectedCustomer);
                }
                else
                {
                    ClearCustomerForm();
                }
            }


        }
        protected void PopulateCustomerForm(Customer customer)
        {
            CustomerName.Text = customer.Name;
            CustomerAddress.Text = customer.Address;
            CustomerEmail.Text = customer.Email;
            CustomerPhone.Text = customer.Phone;
            CustomerCity.Text = customer.City;
            StateDropDownList.SelectedValue = customer.State;
            CustomerZip.Text = customer.Zip;
            CountryDropDownList.SelectedValue = customer.Country;
            CustomerNotes.Text = customer.Notes;

            if (customer.ContactInfo != null)
            {
                ContactName.Text = customer.ContactInfo.Name;
                ContactEmail.Text = customer.ContactInfo.Email;
                ContactPhone.Text = customer.ContactInfo.Phone;
            }
            else
            {
                ContactName.Text = string.Empty;
                ContactEmail.Text = string.Empty;
                ContactPhone.Text = string.Empty;
            }
        }
        protected void ClearCustomerForm()
        {
            CustomerName.Text = string.Empty;
            CustomerAddress.Text = string.Empty;
            CustomerEmail.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            CustomerCity.Text = string.Empty;
            StateDropDownList.SelectedIndex = 0;
            CustomerZip.Text = string.Empty;
            CountryDropDownList.SelectedIndex = 0;
            CustomerNotes.Text = string.Empty;
            ContactName.Text = string.Empty;
            ContactEmail.Text = string.Empty;
            ContactPhone.Text = string.Empty;
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            { 
            
            var customer = new Customer
            {
                Name = CustomerName.Text,
                Address = CustomerAddress.Text,
                City = CustomerCity.Text,
                State = StateDropDownList.SelectedValue,
                Zip = CustomerZip.Text,
                Country = CountryDropDownList.SelectedValue,
                Email = CustomerEmail.Text,
                Phone = CustomerPhone.Text,
                Notes = CustomerNotes.Text,

                ContactInfo = new Contact
                {
                    Name = ContactName.Text,
                    Phone = ContactPhone.Text,
                    Email = ContactEmail.Text,
                }
            };

            var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
            var customerService = testContainer.GetInstance<ICustomerServiceCmd>();
            customerService.AddCustomer(customer);
            customers.Add(customer);
            ViewState["Customers"] = customers;
            CustomersDDL.Items.Add(new ListItem(customer.Name));
            ClearCustomerForm();

            ErrMsgLabel.Text = "Success! Customer " + customer.Name + " is added successfully.";
            ErrMsgLabel.ForeColor = System.Drawing.Color.DarkGreen;
            ErrMsgLabel.Visible = true;
            }
            catch (Exception ex)
            {
                ErrMsgLabel.Text = "Failure! Customer could not be added.";
                ErrMsgLabel.ForeColor = System.Drawing.Color.DarkRed;
                ErrMsgLabel.Visible = true;
                //Exception can be logged to capture the exceptions
            }
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = CustomersDDL.SelectedIndex;
                if (selectedIndex == -1 || selectedIndex >= customers.Count + 1)
                {
                    ErrMsgLabel.Text = "Failure! Customer does not exist. Please select a valid customer to delete.";
                    ErrMsgLabel.ForeColor = System.Drawing.Color.DarkRed;
                    ErrMsgLabel.Visible = true;
                    return;
                }

                Customer selectedCustomer = customers[selectedIndex - 1];
                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = testContainer.GetInstance<ICustomerServiceCmd>();
                customerService.DeleteCustomer(selectedCustomer.ID);
                customers.RemoveAt(selectedIndex - 1);
                ViewState["Customers"] = customers;
                ResetForm();
                PopulateCustomerListBox();

                ErrMsgLabel.Text = "Success! Customer " + selectedCustomer.Name + " is deleted successfully.";
                ErrMsgLabel.ForeColor = System.Drawing.Color.DarkGreen;
                ErrMsgLabel.Visible = true;

                //Javascript alert message
                //ClientScript.RegisterStartupScript(this.GetType(), "DeleteSuccess", "alert('Customer deleted successfully.');", true);

            }
            catch (Exception ex)
            {
                ErrMsgLabel.Text = "Failure! Customer could not be deleted.";
                ErrMsgLabel.ForeColor = System.Drawing.Color.DarkRed;
                ErrMsgLabel.Visible = true;
                //Exception can be logged to capture the exceptions
            }
        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var customer = new Customer
                {
                    ID = CustomersDDL.SelectedIndex-1,
                    Name = CustomerName.Text,
                    Address = CustomerAddress.Text,
                    City = CustomerCity.Text,
                    State = StateDropDownList.SelectedValue,
                    Zip = CustomerZip.Text,
                    Country = CountryDropDownList.SelectedValue,
                    Email = CustomerEmail.Text,
                    Phone = CustomerPhone.Text,
                    Notes = CustomerNotes.Text,

                    ContactInfo = new Contact
                    {
                        Name = ContactName.Text,
                        Phone = ContactPhone.Text,
                        Email = ContactEmail.Text,
                    }
                };

                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = testContainer.GetInstance<ICustomerServiceCmd>();
                customerService.UpdateCustomer(customer);
                ClearCustomerForm();
                PopulateCustomerForm(customer);
                customers[CustomersDDL.SelectedIndex - 1] = customer;
                ViewState["Customers"] = customers;
                ErrMsgLabel.Text = "Success! Customer " + customer.Name + " is updated successfully.";
                ErrMsgLabel.ForeColor = System.Drawing.Color.DarkGreen;
                ErrMsgLabel.Visible = true;
            }
            catch
            {
                ErrMsgLabel.Text = "Failure! Customer could not be updated.";
                ErrMsgLabel.ForeColor = System.Drawing.Color.DarkRed;
                ErrMsgLabel.Visible = true;
            }
        }
        protected void PopulateState(string country)
        {
            StateDropDownList.Items.Clear();

            if (Enum.TryParse<Countries>(country, out var selectedCountry))
            {
                switch (selectedCountry)
                {
                    case Countries.Canada:
                        PopulateDropDownWithEnum(typeof(CanadianProvinces));
                        //Validation for Canadian Postal/ZIP code
                        revCustomerZip.ValidationExpression = @"^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z] \d[ABCEGHJ-NPRSTV-Z]\d$";
                        revCustomerZip.ErrorMessage = "Enter a valid Canadian Postal/ZIP code (e.g., K1A 0B1)";
                        break;
                    case Countries.UnitedStates:
                        PopulateDropDownWithEnum(typeof(USStates));
                        //Validation for US Postal/ZIP code
                        revCustomerZip.ValidationExpression = @"^\d{5}(?:-\d{4})?$";
                        revCustomerZip.ErrorMessage = "Enter a valid US Postal/ZIP code (e.g., 12345 or 12345-6789)";
                        break;
                }
            }
        }
        protected void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateState(CountryDropDownList.SelectedItem.Value);
        }
        private void PopulateDropDownWithEnum(Type enumType)
        {
            var enumValues = Enum.GetValues(enumType)
                         .Cast<Enum>()
                         .Select(e =>
                         {
                             var descriptionAttribute = e.GetType()
                                                         .GetField(e.ToString())
                                                         .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                         .FirstOrDefault() as DescriptionAttribute;
                             var text = descriptionAttribute != null ? descriptionAttribute.Description : e.ToString();
                             return new ListItem
                             {
                                 Text = text,
                                 Value = Convert.ToInt32(e).ToString()
                             };
                         })
                         .ToArray();
            StateDropDownList.Items.Add(new ListItem(""));
            StateDropDownList.Items.AddRange(enumValues);
        }
    }
}