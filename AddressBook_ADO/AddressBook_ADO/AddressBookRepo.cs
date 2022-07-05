using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AddressBook_ADO
{
    class AddressBookRepo
    {
        public static string connectionString = "Data Source=DESKTOP-V5LU9FE\\SQLEXPRESS;Initial Catalog=Address_Book_Service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void AddData(AddressBook_ADO.AddressBook_Model model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("AddContacts", connection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FIRST_NAME", model.FIRST_NAME);
                    command.Parameters.AddWithValue("@LAST_NAME", model.LAST_NAME);
                    command.Parameters.AddWithValue("@ADDRESS", model.ADDRESS);
                    command.Parameters.AddWithValue("@CITY", model.CITY);
                    command.Parameters.AddWithValue("@STATE", model.STATE);
                    command.Parameters.AddWithValue("@ZIP", model.ZIP);
                    command.Parameters.AddWithValue("@PhoneNO", model.PhoneNO);
                    command.Parameters.AddWithValue("@EMAIL", model.EMAIL);
                    command.Parameters.AddWithValue("@Type", model.Type);

                    connection.Open();

                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("Data Added Successfully");
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally
                {
                    connection.Close();

                }
            }

        }

        public void ReadData()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                AddressBook_ADO.AddressBook_Model model = new AddressBook_ADO.AddressBook_Model();
                try
                {
                    string query = "SELECT * FROM ADDRESS_BOOK";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.ID = reader.GetInt32(0);
                            model.FIRST_NAME = reader.GetString(1);
                            model.LAST_NAME = reader.GetString(2);
                            model.ADDRESS = reader.GetString(3);
                            model.CITY = reader.GetString(4);
                            model.STATE = reader.GetString(5);
                            model.ZIP = reader.GetString(6);
                            model.PhoneNO = reader.GetString(7);
                            model.EMAIL = reader.GetString(8);
                            model.Type = reader.GetString(9);


                            Console.WriteLine("ID: " + model.ID + "\nFirst Name: " + model.FIRST_NAME + "\nLast Name: " + model.LAST_NAME +
                                "\nAddress" + model.ADDRESS + "\nCity: " + model.CITY + "\nState:" + model.STATE + "\nZip Code: " + model.ZIP
                                + "\nPhone: " + model.PhoneNO + "\nEmail: " + model.EMAIL + "\nType: " + model.Type + "\n");
                        }

                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void UpdateTable()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            {
                try
                {
                    using (connection)
                    {
                        Console.WriteLine("Enter ID");

                        int ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter an Address to Update");

                        string ADDRESS = Console.ReadLine();
                        SqlCommand command = new SqlCommand("UpdateTable", connection);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ADDRESS", ADDRESS);
                        command.Parameters.AddWithValue("@ID", ID);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        Console.WriteLine("Data Updated Successfully");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();

                }
            }
        }
        public void DeleteData()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    Console.WriteLine("Enter ID");
                    int id = Convert.ToInt32(Console.ReadLine());
                    SqlCommand command = new SqlCommand("DeleteData", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("Data Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
