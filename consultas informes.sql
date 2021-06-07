select * from Reserva
select nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
from Usuario u join Reserva r on u.idUsuario = r.cliente
     join Cancha c on c.idCancha = r.cancha
	 join Horario h on h.idHorario = r.horario

--Informe de las reservas activas del dia de la fecha en adelante
select nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
from Usuario u join Reserva r on u.idUsuario = r.cliente
     join Cancha c on c.idCancha = r.cancha
	 join Horario h on h.idHorario = r.horario
where fecha >= GETDATE()-1 and estado = 1
order by fecha

--Informe de las reservas canceladas del dia de la fecha en adelante
select nombreCompleto, tipoCancha, h.horario, fecha, servicioAsador, servicioInstrumentos, estado
from Usuario u join Reserva r on u.idUsuario = r.cliente
     join Cancha c on c.idCancha = r.cancha
	 join Horario h on h.idHorario = r.horario
where fecha >= GETDATE()-1 and estado = 0
order by fecha

--Informe de las canchas más utilizadas
select c.idCancha, tipoCancha, count(*) 'Veces utilizada'
from Cancha c join Reserva r on r.cancha = c.idCancha
where estado = 1
group by c.idCancha, tipoCancha
order by 3 desc

--Informe de los clientes con más reservas
select nombreCompleto, email, telefono, count(*) 'Cant reservas'
from Usuario u join Reserva r on u.idUsuario = r.cliente
where estado = 1
group by nombreCompleto, email, telefono
order by 4 desc

--Informe de los horarios más reservados
select h.horario, count(*) 'Cant reservas'
from Horario h join HorarioReservas hr on h.idHorario = hr.horario
group by h.horario
order by 2 desc

select idPedido, proveedor, nombreCompleto, fecha
from Pedido pe join Proveedor pr on pr.idProveedor = pe.proveedor
where idPedido = 3

select dp.insumo 'idInsumo', i.insumo, cantidadPedida
from DetallePedido dp join Insumo i on i.idInsumo = dp.insumo
where pedido = 3

select dp.insumo 'idInsumo', i.insumo
from DetallePedido dp join Insumo i on i.idInsumo = dp.insumo
where pedido = 3

select distinct nombreCompleto, fecha, fechaRecibido
from Proveedor pr join Pedido pe on pr.idProveedor = pe.proveedor
     join DetallePedido dp on pe.idPedido = dp.pedido
where idPedido = 3

select i.insumo, cantidadPedida, cantidadRecibida
from DetallePedido dp join Insumo i on dp.insumo = i.idInsumo
where pedido = 3