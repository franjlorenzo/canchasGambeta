using CanchasGambeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanchasGambeta.ViewModels
{
    public class ReservaVM
    {
        private int idReserva;
        private List<Cancha> listaCanchas;
        private List<Horario> listaHorarios;
        private DateTime? fecha;
        private bool servicioAsador;
        private bool servicioInstrumento;
        private List<Insumo> listaInsumos;

        public int IdReserva { get => idReserva; set => idReserva = value; }
        public List<Cancha> ListaCanchas { get => listaCanchas; set => listaCanchas = value; }
        public List<Horario> ListaHorarios { get => listaHorarios; set => listaHorarios = value; }
        public DateTime? Fecha { get => fecha; set => fecha = value; }
        public bool ServicioAsador { get => servicioAsador; set => servicioAsador = value; }
        public bool ServicioInstrumento { get => servicioInstrumento; set => servicioInstrumento = value; }
        public List<Insumo> ListaInsumos { get => listaInsumos; set => listaInsumos = value; }

        public ReservaVM()
        {
            listaCanchas = new List<Cancha>();
            listaHorarios = new List<Horario>();
            listaInsumos = new List<Insumo>();
            cargarListas();
        }

        private void cargarListas()
        {
            listaCanchas = AccesoBD.AD_Reserva.obtenerCanchas();
            listaHorarios = AccesoBD.AD_Reserva.obtenerHorarios();
            listaInsumos = AccesoBD.AD_Insumo.obtenerInsumos();
        }
    }
}