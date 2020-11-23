using Cadeteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class ClientesRepository
    {
        public List<Cliente> GetAll()
        {
            List<Cliente> ListaDeClientes = new List<Cliente>();
            string query = "SELECT * FROM Clientes;";
            SQLiteData.OpenConnection();
            SQLiteDataReader data = SQLiteData.ExecuteSQLiteQuery(query);
            while (data.Read())
            {
                int id = data.GetInt32(0);
                string nombre = data.GetString(1);
                string direccion = data.GetString(2);
                long telefono = data.GetInt64(3);
                Cliente nuevoCliente = new Cliente(id, nombre, direccion, Convert.ToString(telefono));
                ListaDeClientes.Add(nuevoCliente);
            }
            SQLiteData.CloseConnection();
            return ListaDeClientes;
        }

        public void Insert(Cliente cliente)
        {
            string query =  $"INSERT INTO Clientes (nombre, direccion, telefono)" +
                            $"VALUES ('{cliente.Nombre}', '{cliente.Direccion}', '{cliente.Telefono}')";
            SQLiteData.OpenConnection();
            SQLiteData.ExecuteSQLiteNonQuery(query);
            SQLiteData.CloseConnection();
        }
    }
}
