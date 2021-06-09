using CanchasGambeta.Models;
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
        private DateTime fecha = DateTime.Today;
        private string descripcion;
        private List<InsumosAPedir> insumosPedido;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public List<InsumosAPedir> InsumosPedido { get => insumosPedido; set => insumosPedido = value; }
    }

    public class NuevoProveedor
    {
        private int idProveedor;
        private string nombreCompleto;
        private string telefono;
        private string email;
        private string empresa;

        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Email { get => email; set => email = value; }
        public string Empresa { get => empresa; set => empresa = value; }
    }

    public class TablaProveedores
    {
        private int idProveedor;
        private string nombreCompleto;
        private string telefono;
        private string email;
        private string empresa;

        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Email { get => email; set => email = value; }
        public string Empresa { get => empresa; set => empresa = value; }
    }

    public class BuscarInsumos
    {
        private int idInsumo;
        private string insumo;
        private int cantidad;
        private int stock;

        public int IdInsumo { get => idInsumo; set => idInsumo = value; }
        public string Insumo { get => insumo; set => insumo = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int Stock { get => stock; set => stock = value; }
    }

    public class InsumosAPedir
    {
        private int idInsumo;
        private string insumo;
        private int cantidad;
        private int stock;

        public int IdInsumo { get => idInsumo; set => idInsumo = value; }
        public string Insumo { get => insumo; set => insumo = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int Stock { get => stock; set => stock = value; }

        public InsumosAPedir(int idInsumo, string insumo, int cantidad)
        {
            this.IdInsumo = idInsumo;
            this.Insumo = insumo;
            this.Cantidad = cantidad;
        }

        public InsumosAPedir()
        {

        }
    }

    public class ConcretarPedido
    {
        private int idPedido;
        private int idProveedor;
        private string nombreProveedor;
        private DateTime fechaRealizado;
        private DateTime fechaRecibido;
        private List<InsumosAPedir> listaInsumosPedidos;
        private List<InsumosAPedir> listaCantidadesRecibidas;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public DateTime FechaRealizado { get => fechaRealizado; set => fechaRealizado = value; }
        public DateTime FechaRecibido { get => fechaRecibido; set => fechaRecibido = value; }
        public List<InsumosAPedir> ListaInsumosPedidos { get => listaInsumosPedidos; set => listaInsumosPedidos = value; }
        public List<InsumosAPedir> ListaCantidadesRecibidas { get => listaCantidadesRecibidas; set => listaCantidadesRecibidas = value; }
    }

    public class InsumosPedidosRecibidos
    {
        private int idInsumo;
        private string insumo;
        private int cantidadPedida;
        private int cantidadRecibida;

        public int IdInsumo { get => idInsumo; set => idInsumo = value; }
        public string Insumo { get => insumo; set => insumo = value; }
        public int CantidadPedida { get => cantidadPedida; set => cantidadPedida = value; }
        public int CantidadRecibida { get => cantidadRecibida; set => cantidadRecibida = value; }
    }

    public class DetallePedidoConcretado
    {
        private int idPedido;
        private string nombreProveedor;
        private DateTime fechaRealizado;
        private DateTime fechaRecibido;
        private List<InsumosPedidosRecibidos> listaInsumosPedidosrecibidos;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public DateTime FechaRealizado { get => fechaRealizado; set => fechaRealizado = value; }
        public DateTime FechaRecibido { get => fechaRecibido; set => fechaRecibido = value; }
        public List<InsumosPedidosRecibidos> ListaInsumosPedidosrecibidos { get => listaInsumosPedidosrecibidos; set => listaInsumosPedidosrecibidos = value; }
    }

    public class VistaMisPedidos
    {
        public List<TablaPedido> TablaPedido { get; set; }
        public NuevoPedido NuevoPedido { get; set; }
        public NuevoProveedor NuevoProveedor { get; set; }
        public List<TablaProveedores> TablaProveedores { get; set; }
        public List<InsumosAPedir> InsumosAPedir { get; set; }
    }

    public class VistaPedirInsumos
    {
        public List<BuscarInsumos> BuscarInsumos { get; set; }
        public List<InsumosAPedir> InsumosAPedir { get; set; }
    }
}