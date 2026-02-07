using System.Net.WebSockets;

namespace PracticaTP1;

public class Pedidos
{
    public int nroPedido { get; set; }
    public string observacion { get; set; }
    public Cliente cliente { get; set; }
    public Estado estadoPedido { get; set; }
    public Cadete cadeteAsignado { get; set; }

    public Pedidos()
    {
        this.cliente = new Cliente();
    }
    public Pedidos(int nroPedido, string observacion, Cliente cliente)
    {
        this.nroPedido = nroPedido;
        this.observacion = observacion;
        this.cliente = new Cliente(cliente.nombreCliente, cliente.direccionCliente, cliente.telefonoCliente, cliente.datosReferencia);
        this.estadoPedido = Estado.Pendiente;
    }

    public string VerDireccionCliente()
    {   
        return "{cliente.direccionCliente}";
    }
    public string VerDatosCliente()
    {
        string datos = $"Nombre: {cliente.nombreCliente} | Telefono: {cliente.telefonoCliente} | Direccion: {cliente.direccionCliente} | Referencia sobre donde vive: {cliente.datosReferencia}";
        return datos;
    }
}