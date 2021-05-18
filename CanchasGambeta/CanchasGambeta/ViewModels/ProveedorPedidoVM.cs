using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class TablaPedido
    {
        private int idPedido;
        private string descripcionPedido;
        private int idProveedor;
        private string nombreProveedor;
        private string telefono;
        private string empresa;
        private DateTime fecha;
        private bool estado;
        private List<TablaPedido> pedidosPendientes;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public string DescripcionPedido { get => descripcionPedido; set => descripcionPedido = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public bool Estado { get => estado; set => estado = value; }
        public List<TablaPedido> PedidosPendientes { get => pedidosPendientes; set => pedidosPendientes = value; }
    }

    public class NuevoPedido
    {
        private int idPedido;
        private int idProveedor;
        private string descripcion;
        private DateTime fecha = DateTime.Today;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
    }

    public class VistaMisPedidos
    {
        public List<TablaPedido> TablaPedido { get; set; }
        public NuevoPedido NuevoPedido { get; set; }
    }
}