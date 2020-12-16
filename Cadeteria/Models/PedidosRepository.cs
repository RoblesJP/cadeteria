using Cadeteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class PedidosRepository
    {
        public List<Pedido> GetAll()
        {
            ClientesRepository clientesRepository = new ClientesRepository();
            CadetesRepository cadetesRepository = new CadetesRepository();
            List<Pedido> ListaDePedidos = new List<Pedido>();
            string query = @"SELECT idPedido, idCliente, idCadete, idTipo, idEstado, descripcion, cupon, precio
                             FROM Pedidos
                             INNER JOIN TiposDePedidos USING(idTipo)
                             INNER JOIN EstadosDePedidos USING(idEstado)
                             WHERE activo = 1;";
            SQLiteData.OpenConnection();
            SQLiteDataReader data = SQLiteData.ExecuteSQLiteQuery(query);
            while (data.Read())
            {
                int id = Convert.ToInt32(data["idPedido"]);
                Cliente cliente = clientesRepository.GetCliente(Convert.ToInt32(data["idCliente"]));
                Cadete cadete = cadetesRepository.GetCadete(Convert.ToInt32(data["idCadete"]));
                Tipo tipo = (Tipo)Convert.ToInt32(data["idTipo"]);
                Estado estado = (Estado)Convert.ToInt32(data["idEstado"]);
                string descripcion = data["descripcion"].ToString();
                bool cupon = Convert.ToBoolean(data["cupon"]);
                Pedido nuevoPedido = new Pedido(id, cliente, cadete, tipo, estado, descripcion, cupon);
                ListaDePedidos.Add(nuevoPedido);
            }
            SQLiteData.CloseConnection();
            return ListaDePedidos;
        }

        public void Insert(Pedido pedido)
        {
            string query = @"INSERT INTO Pedidos (idCliente, idCadete, idTipo, descripcion, cupon, precio)
                             VALUES (@IdCliente, @IdCadete, @IdTipo, @Descripcion, @Cupon, @Precio);";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdCliente", pedido.Cliente.Id);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdCadete", pedido.Cadete.Id);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdTipo", pedido.Tipo);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Descripcion", pedido.Descripcion);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Cupon", pedido.TieneCuponDeDescuento);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Precio", pedido.GetPrecio());
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public void Update(Pedido pedido)
        {
            string query = @"UPDATE Pedidos SET idCliente = @IdCliente, idCadete = @IdCadete, idTipo = @IdTipo, idEstado = @IdEstado, descripcion = @Descripcion, cupon = @Cupon, precio = @Precio
                             WHERE idPedido = @IdPedido;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdPedido", pedido.Id);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdCliente", pedido.Cliente.Id);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdCadete", pedido.Cadete.Id);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdTipo", pedido.Tipo);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdEstado", pedido.Estado);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Descripcion", pedido.Descripcion);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Cupon", pedido.TieneCuponDeDescuento);
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@Precio", pedido.GetPrecio());
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public void Delete(int id)
        {
            string query = @"UPDATE Pedidos SET activo = 0 WHERE idPedido = @IdPedido;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@IdPedido", id);
            SQLiteData.Sql_cmd.ExecuteNonQuery();
            SQLiteData.CloseConnection();
        }

        public Pedido GetPedido(int id)
        {
            ClientesRepository clientesRepository = new ClientesRepository();
            CadetesRepository cadetesRepository = new CadetesRepository();
            Pedido pedido = new Pedido();
            string query = @"SELECT idPedido, idCliente, idCadete, idTipo, idEstado, descripcion, cupon
                             FROM Pedidos
                             WHERE idPedido = @id AND activo = 1;";
            SQLiteData.OpenConnection();
            SQLiteData.Sql_cmd.CommandText = query;
            SQLiteData.Sql_cmd.Parameters.AddWithValue("@id", id);
            SQLiteDataReader data = SQLiteData.Sql_cmd.ExecuteReader();
            while (data.Read())
            {
                Cliente cliente = clientesRepository.GetCliente(Convert.ToInt32(data["idCliente"]));
                Cadete cadete = cadetesRepository.GetCadete(Convert.ToInt32(data["idCadete"]));
                pedido.Id = Convert.ToInt32(data["idPedido"]);
                pedido.Cliente = cliente;
                pedido.Cadete = cadete;
                pedido.Tipo = (Tipo)Convert.ToInt32(data["idTipo"]);
                pedido.Estado = (Estado)Convert.ToInt32(data["idEstado"]);
                pedido.Descripcion = data["descripcion"].ToString();
                pedido.TieneCuponDeDescuento = Convert.ToBoolean(data["cupon"]);
            }
            SQLiteData.CloseConnection();
            return pedido;
        }
    }
}
