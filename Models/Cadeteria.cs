using System.Linq.Expressions;

namespace PracticaTP1;

public class Cadeteria
{
    public string nombreCadeteria { get; set; }
    public string telefonoCadeteria { get; set; }
    public List<Cadete> listaCadetes { get; set; }
    //public List<Pedidos> pedidosNoAsignados { get; set; } = new List<Pedidos>();
    public List<Pedidos> listadoPedidos { get; set; }

    public Cadeteria ()
    {
        
    }
    public Cadeteria(string nombre, string telefono, List<Cadete> listadoCadetes)
    {
        this.nombreCadeteria = nombre;
        this.telefonoCadeteria = telefono;
        this.listaCadetes = listadoCadetes;
    }
    public List<Pedidos> ObtenerPedidos()
    {
        return listadoPedidos;
    }
    public List<Cadete> ObtenerCadetes()
    {
        return listaCadetes;
    }
    public Cadete BuscarCadetePorID(int idCadete)
    {
        var cadeteBuscado = listaCadetes.SingleOrDefault(cadete => cadete.id == idCadete);
        if (cadeteBuscado == null)
        {
            throw new KeyNotFoundException($"No existe un cadete con el id: {idCadete}");
        }
        return cadeteBuscado;
    }
    public Pedidos BuscarUnPedidoPorId(int nroPedido)
    {
        /* var p = pedidosNoAsignados.FirstOrDefault(pedido => pedido.nroPedido == nroPedido);
        if (p != null) return p; */

        var pedidoBuscado = listadoPedidos.FirstOrDefault(pedido => pedido.nroPedido == nroPedido);
        if (pedidoBuscado != null) return pedidoBuscado;

        return null;
    }
    public bool CambiarEstadoPedido(int nroPedido, Estado nuevoEstado)
    {
        var pedido = BuscarUnPedidoPorId(nroPedido);
        if (pedido != null)
        {
            
            if (pedido.estadoPedido == Estado.Pendiente && (nuevoEstado == Estado.Asignado || nuevoEstado == Estado.Cancelado))
            {
                pedido.estadoPedido = nuevoEstado;
                return true;
            }
            if (pedido.estadoPedido == Estado.Asignado && nuevoEstado == Estado.EnCamino) return true;
            if (pedido.estadoPedido == Estado.Asignado && nuevoEstado == Estado.Entregado)
            {
                pedido.estadoPedido = nuevoEstado;
                return true;
            }
        }
        return false;
    }
    public bool AgregarCadete(Cadete cadeteAgregar)
    {
        if (listaCadetes.Any(cadete => cadete.id == cadeteAgregar.id)) return false;
        listaCadetes.Add(cadeteAgregar);
        return true;
    }
    public Pedidos DarAltaPedido(int nroPedido, string observacion, Cliente cliente)
    {
        Pedidos nuevoPedido = new Pedidos(nroPedido, observacion, cliente);
        listadoPedidos.Add(nuevoPedido);
        return nuevoPedido;
    }
    public bool AsignarPedido(int idCadete, Pedidos nuevoPedido)
    {
        var cadete = BuscarCadetePorID(idCadete);
        if (cadete != null)
        {
            //listadoPedidos.Add(nuevoPedido); Comento, porque sino se volvia a agregar al JSON y yo solo necesitaba que se actualice
            CambiarEstadoPedido(nuevoPedido.nroPedido, Estado.Asignado);
            nuevoPedido.cadeteAsignado = cadete;
            return true;
            /* pedidosNoAsignados.Remove(nuevoPedido); */
        }
        return false;
    }
    public bool ReasignarPedido(int nroPedidoReasignar, int idCadeteNuevo)
    {
        Pedidos pedido = BuscarUnPedidoPorId(nroPedidoReasignar);
        if (pedido != null)
        {
            Cadete nuevoCadete = BuscarCadetePorID(idCadeteNuevo);
            if (nuevoCadete != null)
            {
                pedido.cadeteAsignado = nuevoCadete;
                return true;
            }
        }
        return false;
    }
    public double JornalACobrar(int idCadete)
    {
        var cadete = BuscarCadetePorID(idCadete);
        if(cadete == null) return 0;

        int cantidad = listadoPedidos.Count(p => p.cadeteAsignado != null && p.cadeteAsignado.id == idCadete);

        return cantidad * 500; 
    }
}