using Cadeteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class CadetesRepository
    {
        public List<Cadete> GetAll()
        {
            List<Cadete> ListaDeCadetes = new List<Cadete>();
            string query = @"SELECT idCadete, idVehiculo, nombre, direccion, telefono
                             FROM Cadetes
                             INNER JOIN Vehiculos USING(idVehiculo)
                             WHERE activo = 1;";
            SQLiteData.OpenConnection();
            SQLiteDataReader data = SQLiteData.ExecuteSQLiteQuery(query);
            while (data.Read())
            {
                int id = Convert.ToInt32(data["idCadete"]);
                string nombre = data["nombre"].ToString();
                string direccion = data["direccion"].ToString();
                string telefono = data["telefono"].ToString();
                Vehiculo vehiculo = (Vehiculo)Convert.ToInt32(data["idVehiculo"]);
                Cadete nuevoCadete = new Cadete(id, nombre, direccion, telefono, vehiculo);
                ListaDeCadetes.Add(nuevoCadete);
            }
            SQLiteData.CloseConnection();
            return ListaDeCadetes;
        }

        public void Insert(Cadete cadete)
        {
            string query = @"INSERT INTO Cadetes (idVehiculo, nombre, direccion, telefono)" +
                            @"VALUES (@IdVehiculo, @Nombre, @Direccion, @Telefono)";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Nombre", cadete.Nombre);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Direccion", cadete.Direccion);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Telefono", cadete.Telefono);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdVehiculo", cadete.Vehiculo);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public void Update(Cadete cadete)
        {
            string query = @"UPDATE Cadetes SET nombre = @Nombre, direccion = @Direccion, telefono = @Telefono, idVehiculo = @IdVehiculo
                             WHERE idCadete = @Id;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Nombre", cadete.Nombre);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Direccion", cadete.Direccion);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Telefono", cadete.Telefono);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdVehiculo", cadete.Vehiculo);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Id", cadete.Id);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public void Delete(int id)
        {
            string query = @"UPDATE Cadetes SET activo = 0 WHERE idCadete = @Id;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Id", id);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public Cadete GetCadete(int id)
        {
            Cadete cadete = new Cadete();
            string query = @"SELECT idCadete, idVehiculo, nombre, direccion, telefono
                             FROM Cadetes
                             WHERE idCadete = @id AND activo = 1;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@id", id);
            SQLiteDataReader data = SQLiteData.Sql_cmd.ExecuteReader();
            while (data.Read())
            {
                cadete.Id = Convert.ToInt32(data["idCadete"]);
                cadete.Nombre = data["nombre"].ToString();
                cadete.Direccion = data["direccion"].ToString();
                cadete.Telefono = data["telefono"].ToString();
                cadete.Vehiculo = (Vehiculo)Convert.ToInt32(data["idVehiculo"]);
            }
            SQLiteData.CloseConnection();
            return cadete;
        }
    }
}
