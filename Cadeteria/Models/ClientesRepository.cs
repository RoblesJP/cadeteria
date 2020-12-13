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
            string query = @"SELECT idCliente, nombre, direccion, telefono 
                             FROM Clientes
                             WHERE activo = 1;";
            SQLiteData.OpenConnection();
            SQLiteDataReader data = SQLiteData.ExecuteSQLiteQuery(query);
            while (data.Read())
            {
                int id = Convert.ToInt32(data["idCliente"]);
                string nombre = data["nombre"].ToString();
                string direccion = data["direccion"].ToString();
                string telefono = data["telefono"].ToString();
                Cliente nuevoCliente = new Cliente(id, nombre, direccion, telefono);
                ListaDeClientes.Add(nuevoCliente);
            }
            SQLiteData.CloseConnection();
            return ListaDeClientes;
        }

        public void Insert(Cliente cliente)
        {
            string query =  @"INSERT INTO Clientes (nombre, direccion, telefono)" +
                            @"VALUES (@Nombre, @Direccion, @Telefono)";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public void Update(Cliente cliente)
        {
            string query = @"UPDATE Clientes SET nombre = @Nombre, direccion = @Direccion, telefono = @Telefono
                             WHERE idCliente = @Id;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Id", cliente.Id);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public void Delete(int id)
        {
            string query = @"UPDATE Clientes SET activo = 0 WHERE idCliente = @Id;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Id", id);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public Cliente GetCliente(int id)
        {
            Cliente cliente = new Cliente();
            string query = @"SELECT idCliente, nombre, direccion, telefono
                             FROM Clientes
                             WHERE idCliente = @id AND activo = 1;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@id", id);
            SQLiteDataReader data = SQLiteData.Sql_cmd.ExecuteReader();
            while (data.Read())
            {
                cliente.Id = Convert.ToInt32(data["idCliente"]);
                cliente.Nombre = data["nombre"].ToString();
                cliente.Direccion = data["direccion"].ToString();
                cliente.Telefono = data["telefono"].ToString();
            }
            SQLiteData.CloseConnection();
            return cliente;
        }
    }
}
