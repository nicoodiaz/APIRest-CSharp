namespace PracticaTP1;

public class Cadete
{
    public int id { get; set;}
    public string nombreCadete { get; set; }
    public string direccionCadete { get; set; }
    public string telefonoCadete { get; set; }
    public Cadete()
    {
        
    }
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombreCadete = nombre;
        this.direccionCadete = direccion;
        this.telefonoCadete = telefono;
    }

/*     public float JornalACobrar()
    {
        return pedidosACadete.Count() * 500;
    }

    public void AgregarPedido(Pedidos nuevoPedido)
    {
        pedidosACadete.Add(nuevoPedido);   
    }
    public void RemoverPedido(Pedidos pedidoARemover)
    {
        pedidosACadete.Remove(pedidoARemover);
    } */
}